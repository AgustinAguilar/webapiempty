using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Utils.Smtp
{
    public class Smtp
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public bool Security { get; set; }
        public string SenderEmail { get; set; }
        public string Password { get; set; }
    }
}
