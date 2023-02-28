using Web.Api.Model.Classes.BaseEntities;
using System.Collections.Generic;

namespace Web.Api.Model.Classes
{
    public class Function : BaseDescriptibleEntity
    {
        public ICollection<Permission> Permission { get; set; }
    }
}
