using Web.Api.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Web.Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        private IConfiguration _configuration { get; }
        private IWebHostEnvironment _webHostingEnvironment { get; }

        public InfoController(IConfiguration configuration, IWebHostEnvironment webHostingEnvironment)
        {
            _configuration = configuration;
            _webHostingEnvironment = webHostingEnvironment;
        }

        // GET: api/GetVersion
        [HttpGet]
        [Route("GetVersion")]
        [ProducesResponseType(typeof(InfoViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetVersion()
        {
            var assemblyName = Assembly.GetExecutingAssembly().GetName();
            return await Task.FromResult(Ok(new InfoViewModel
            {
                Name = assemblyName.Name,
                Version = new InfoVersionViewModel
                {
                    Major = assemblyName.Version.Major,
                    Minor = assemblyName.Version.Minor,
                    Patches = assemblyName.Version.Build,
                    Build = assemblyName.Version.Revision,
                },
                Enviroment = _webHostingEnvironment.EnvironmentName,
                PublishDate = System.IO.File.GetLastWriteTime(Assembly.GetExecutingAssembly().Location).ToString("dd/MM/yyyy HH:mm:ss")
            }));
        }
    }
}
