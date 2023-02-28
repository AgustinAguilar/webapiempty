using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Model.Classes.BaseEntities
{
    public class BaseDescriptibleEntity : BaseNameableEntity
    {
        public string Description { get; set; }
    }
}
