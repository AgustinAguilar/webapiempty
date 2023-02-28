using Web.Api.ViewModels.ClassesBase;
using System;

namespace Web.Api.ViewModels
{
    public class UserPutViewModel : IdentificableViewModel
    {
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Enabled { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? ProfileId { get; set; }
    }
}
