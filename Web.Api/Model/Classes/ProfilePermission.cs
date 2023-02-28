using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Model.Classes
{
    public class ProfilePermission
    {
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
        public int PermissionId { get; set; }
        public Permission Permission { get; set; }

    }
}
