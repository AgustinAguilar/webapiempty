using Web.Api.ViewModels.ClassesBase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Web.Api.ViewModels
{
    public class UserViewModel : IdentificableViewModel
    {
        public UserViewModel()
        {
            Profiles = new HashSet<ProfilesBaseViewModel>();
        }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Enabled { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
        public string Name => FirstName != string.Empty ? LastName + ", " + FirstName : "[Pending Name]";
        internal ICollection<ProfilesBaseViewModel> Profiles { get; set; }
        public int? ProfileId => Profiles.FirstOrDefault()?.Id;
        public string Profile => Profiles.FirstOrDefault()?.Name;
        public string Status => Enabled ? "Actived" : "Disabled";
    }
}
