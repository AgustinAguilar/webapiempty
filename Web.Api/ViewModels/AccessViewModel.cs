using Web.Api.ViewModels.ClassesBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.ViewModels
{
    public class AccessViewModel : IdentificableViewModel
    {
        public int UserId { get; set; }

        public int AccessTypeId { get; set; }
        public string AccessTypeName { get; set; }

        public UserViewModel User { get; set; }

        public DateTime Date { get; set; }

        public string CsvRows => $"{Id};{Date};{User.Name};{AccessTypeName}" + Environment.NewLine;

    }
}
