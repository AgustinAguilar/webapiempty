using Web.Api.ViewModels.ClassesBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.ViewModels
{
    public class InfoViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Enviroment { get; set; }
        public InfoVersionViewModel Version { get; set; }
        public string PublishDate { get; set; }
    }
}
