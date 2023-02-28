using AutoMapper;
using Web.Api.Model.Classes;
using Web.Api.ViewModels;
using System.Linq;

namespace Web.Api.MappingServices
{
    public class UserViewModelMappingService : AutoMapper.Profile
    {
        public UserViewModelMappingService()
        {
            CreateMap<User, UserViewModel>()
                .ForMember(dest => dest.Profiles, orig =>  orig.MapFrom(ent=> ent.ProfileUsers.Select(p=> p.Profile)))  
                .ReverseMap();
        }
    }


}
