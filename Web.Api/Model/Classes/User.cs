using Web.Api.Model.Classes.BaseEntities;
using System;
using System.Collections.Generic;

namespace Web.Api.Model.Classes
{
    public partial class User : BaseIdentificableEntity
    {
        public User()
        {
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Enabled { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<PermissionUser> PermissionUsers { get; set; }
        public virtual ICollection<ProfileUser> ProfileUsers { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}
