using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Model.Classes
{
    public class ProfileUser
    {
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

    }
}
