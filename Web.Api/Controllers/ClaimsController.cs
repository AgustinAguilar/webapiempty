using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClaimsController : ControllerBase
    {
        protected int UserId => int.Parse(User.Claims.First(i => i.Type == ClaimTypes.Name).Value);
        protected string Email => User.Claims.First(i => i.Type == ClaimTypes.Email).Value;
    }
}
