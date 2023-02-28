using System.Collections.Generic;
using System.Linq;

namespace Web.Api.ViewModels
{
    public class UserLoginViewModel
    {
        public UserLoginViewModel()
        {
            Profiles = new HashSet<ProfilesBaseViewModel>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Enabled { get; set; }
        internal ICollection<ProfilesBaseViewModel> Profiles { get; set; }
        public string Profile => Profiles.FirstOrDefault()?.Name;

    }
}
