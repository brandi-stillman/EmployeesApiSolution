using AutoMapper;
using EmployeesApi.Domain;
using EmployeesApi.Models;

namespace EmployeesApi.MapperProfiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeListItem>();
            CreateMap<Employee, GetEmployeeDetailsResponse>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(x => $"{x.LastName},{x.FirstName}"));
            CreateMap<PostEmployeeRequest, Employee>()
                .ForMember(dest => dest.Active, opt => opt.MapFrom(x => true))
                .ForMember(dest => dest.Salary, opt => opt.MapFrom(x => 80000));
        }
    }
}
