using AutoMapper;
using Demo.DAL.Models;
using DemoPL.ViewModels;

namespace DemoPL.MappingProfile
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser,UserViewModel>(); 
        }
    }
}
