using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Web.Api.ViewModels;

namespace Web.Api.Services.IServices
{
    public interface IAuthService
    {
        Task<UserViewModel> Login(UserLoginViewModel userToSingIn, string ipAddress, CancellationToken cancellationToken);
        Task<UserViewModel> RefreshToken(string token, string ipAddress, CancellationToken cancellationToken);
        Task<bool> Logout(UserLoginViewModel userToSingOut, CancellationToken cancellationToken);
    }
}
