using Web.Api.ViewModels.ClassesBase;

namespace Web.Api.ViewModels
{
    public class InfoVersionViewModel : BaseViewModel
    {
        public int Major { get; set; }
        public int Minor { get; set; }
        public int Patches { get; set; }
        public int Build { get; set; }
    }
}
