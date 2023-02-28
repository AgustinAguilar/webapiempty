using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Model.Classes.BaseEntities
{
    public class BaseNameableEntity : BaseIdentificableEntity
    {
        public string Name { get; set; }
    }
}
