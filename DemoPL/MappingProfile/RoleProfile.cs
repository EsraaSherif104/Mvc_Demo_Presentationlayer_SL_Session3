using AutoMapper;
using DemoPL.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace DemoPL.MappingProfile
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<IdentityRole, RoleViewModel>().
                ForMember(x => x.RoleName, o => o.MapFrom(s => s.Name))
                .ReverseMap();
        }
    }
}
