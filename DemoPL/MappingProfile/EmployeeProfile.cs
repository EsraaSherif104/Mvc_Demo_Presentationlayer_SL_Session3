using AutoMapper;
using Demo.DAL.Models;
using DemoPL.ViewModels;

namespace DemoPL.MappingProfile
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap().ForMember(d=>d.Name,o=>o.MapFrom(s=>s.Name));
        }
    }
}
