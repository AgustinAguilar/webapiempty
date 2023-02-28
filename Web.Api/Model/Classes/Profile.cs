using Web.Api.Model.Classes.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Model.Classes
{
    public class Profile : BaseDescriptibleEntity
    {
        public ICollection<ProfileUser> ProfileUsers { get; set; }
        public ICollection<ProfilePermission> ProfilePermissions { get; set; }
    }
}
