using System.Collections.Generic;

namespace Web.Api.ViewModels
{
    public class UserRefreshTokenViewModel : UserLoginViewModel
    {
        public UserRefreshTokenViewModel()
        {
            RefreshTokens = new HashSet<RefreshTokenViewModel>();
        }
        public ICollection<RefreshTokenViewModel> RefreshTokens { get; set; }
    }





}
