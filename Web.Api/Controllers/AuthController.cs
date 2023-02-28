using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Web.Api.Services.IServices;
using Web.Api.ViewModels;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ClaimsController
    {
        private readonly IAuthService _service;
        public AuthController(IAuthService service)
        {
            _service = service;
        }

        // POST: api/Auth/Login
        /// <summary>
        /// Sign in and return user object with authorization and refresh tokens
        /// </summary>
        /// <param name="userToSingIn"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login([FromBody] UserLoginViewModel userToSingIn, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Accepted(await _service.Login(userToSingIn, ipAddress(), cancellationToken));
        }

        // POST: api/Auth/Login
        /// <summary>
        /// Sign out user
        /// </summary>
        /// <param name="userToSingOut"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Logout([FromBody] UserLoginViewModel userToSingOut, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Accepted(await _service.Logout(userToSingOut, cancellationToken));
        }

        [HttpPost("refresh-token")]
        public IActionResult RefreshToken(string refreshToken, CancellationToken cancellationToken)
        {
            var response = _service.RefreshToken(refreshToken, ipAddress(), cancellationToken);

            if (response == null)
                return Unauthorized(new { message = "Invalid token" });


            return Ok(response);
        }


        private string ipAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}
