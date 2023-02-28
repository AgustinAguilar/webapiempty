using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Model.Classes.BaseEntities
{
    public class BaseStates : BaseIdentificableEntity
    {
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public string Observations { get; set; }

    }
}
