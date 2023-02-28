using Web.Api.Model.Classes.BaseEntities;
using System.Collections.Generic;

namespace Web.Api.Model.Classes
{
    public class Option : BaseDescriptibleEntity
    {
        public string Url { get; set; }

        public string Icon { get; set; }

        public ICollection<Permission> Permission { get; set; }
    }
}
