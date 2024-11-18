using AutoMapper;
using Demo.DataAcessLayer.Models;
using Demo.PresnationLayer.ViewModels;

namespace Demo.PresnationLayer.MappingProfiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            //CreateMap<EmployeeViewModel, Employee>().ForMember(D=>D.Name , O=>O.MapFrom(S=>S.EmpName));
            CreateMap<EmployeeViewModel , Employee>().ReverseMap();
        }
    }
}
