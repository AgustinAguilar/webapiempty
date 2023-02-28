using Web.Api.Model.Classes;
using Web.Api.ViewModels;
using System.Linq;

namespace CartWeb.Kiosco.Api.MappingServices
{
    public class UserLoginViewModelMappingService : AutoMapper.Profile
    {
        public UserLoginViewModelMappingService()
        {
            CreateMap<User, UserLoginViewModel>()
                .ForMember(dest => dest.Profiles, orig => orig.MapFrom(ent => ent.ProfileUsers.Select(p => p.Profile)));
            CreateMap<UserLoginViewModel, User>();

        }
    }
}
