using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Api.Exceptions;
using Web.Api.Model.Enums;
using Web.Api.Repositories.IRepositories;
using Web.Api.Resources;
using Web.Api.Services.IServices;
using Web.Api.ViewModels;

namespace Web.Api.Services
{
    public class AuthService : IAuthService
    {
        readonly IPasswordHasherService _passwordHasherService;
        readonly IAccessesRepository _accessesRepository;
        readonly IConfiguration _configuration;
        readonly IUsersRepository _usersRepository;

        public AuthService(IAccessesRepository accessesRepository,
            IPasswordHasherService passwordHasherService,
            IUsersRepository usersRepository,
            IConfiguration configuration)
        {
            _passwordHasherService = passwordHasherService;
            _accessesRepository = accessesRepository;
            _usersRepository = usersRepository;
            _configuration = configuration;
        }

        public async Task<UserViewModel> Login(UserLoginViewModel userToSingIn, string ipAddress, CancellationToken cancellationToken)
        {

            var user = await _usersRepository.GetForLoginExist(userToSingIn.Email, cancellationToken);

            if (user == null) throw new AuthorizationException(Messages.UnauthorizedUserOrPasswordIncorrect);

            if (!user.Enabled) throw new AuthorizationException(Messages.UnauthorizedUserNotEnabled);

            if (user.Profiles == null || user.Profiles.Count == 0) throw new AuthorizationException(Messages.UnauthorizedUserWithoutContentProfile);

            if (!_passwordHasherService.Check(user.Password, userToSingIn.Password).Verified) throw new AuthorizationException(Messages.UnauthorizedUserOrPasswordIncorrect);

            var jwtToken = generateJwtToken(user);
            var refreshToken = generateRefreshToken(ipAddress);

            await _accessesRepository.Save(new AccessViewModel
            {
                AccessTypeId = (int)AccessType.Login,
                Date = DateTime.Now,
                UserId = user.Id
            }, cancellationToken);

            return new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                JwtToken = jwtToken,
                RefreshToken = refreshToken.Token,
                Profiles = user.Profiles
            };
        }

        public async Task<bool> Logout(UserLoginViewModel userToSingOut, CancellationToken cancellationToken)
        {
            var res = await _accessesRepository.Save(new AccessViewModel
            {
                AccessTypeId = (int)AccessType.Logout,
                Date = DateTime.Now,
                UserId = userToSingOut.Id
            }, cancellationToken);

            return res != null;
        }

        public async Task<UserViewModel> RefreshToken(string token, string ipAddress, CancellationToken cancellationToken)
        {
            var user = await _usersRepository.GetByRefreshToken(token, cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null) return null;

            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            if (!refreshToken.IsActive) return null;

            var newRefreshToken = generateRefreshToken(ipAddress);
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            refreshToken.ReplacedByToken = newRefreshToken.Token;


            _usersRepository.UpdateRefreshToken(user.Id, newRefreshToken, refreshToken, cancellationToken);

            var jwtToken = generateJwtToken(user);

            return new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                JwtToken = jwtToken,
                RefreshToken = newRefreshToken.Token
            };
        }

        private string generateJwtToken(UserLoginViewModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection(CodesStrings.AppSettingsAudienceSectionName).GetValue<string>(CodesStrings.AppSettingsAudienceSectionSecretName));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private RefreshTokenViewModel generateRefreshToken(string ipAddress)
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                return new RefreshTokenViewModel
                {
                    Token = Convert.ToBase64String(randomBytes),
                    Expires = DateTime.UtcNow.AddDays(7),
                    Created = DateTime.UtcNow,
                    CreatedByIp = ipAddress
                };
            }
        }
    }
}
