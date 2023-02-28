using Web.Api.Model.Classes.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Model.Classes
{
    public class Access : BaseIdentificableEntity
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public int AccessTypeId { get; set; }

        public AccessType AccessType { get; set; }

        public DateTime Date { get; set; }

    }
}
