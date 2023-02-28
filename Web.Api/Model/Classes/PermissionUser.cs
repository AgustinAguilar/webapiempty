using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Model.Classes
{
    public class PermissionUser
    {
        public int PermissionId { get; set; }
        public Permission Permission { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

    }
}
