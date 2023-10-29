using AutoMapper;
using EmployeeERP.Models.DbEntities;
using EmployeeERP.Models.DTO;

namespace EmployeeERP.Models.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<TimeOffRequest, TimeOffRequestDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.TimeOffStart, opt => opt.MapFrom(src => ToDateOnly(src.TimeOffStart)))
            .ForMember(dest => dest.TimeOffEnd, opt => opt.MapFrom(src => ToDateOnly(src.TimeOffEnd)))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.DateOfRequest, opt => opt.MapFrom(src => ToDateOnly(src.DateOfRequest)))
            .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.UserName));

            CreateMap<Employee, EmployeeDto>();

            CreateMap<ICollection<Employee>, ICollection<EmployeeDto>>()
            .ConvertUsing(src => src.Select(employee => new EmployeeDto 
            { 
                Id = employee.Id, 
                Name = employee.UserName, 
                DateOfBirth = ToDateOnly(employee.DateOfBirth) 
            }).ToList());

            CreateMap<ICollection<TimeOffRequest>, ICollection<TimeOffRequestDto>>()
            .ConvertUsing(src => src.Select(tor => new TimeOffRequestDto
            { 
                Id = tor.Id,
                TimeOffStart = ToDateOnly(tor.TimeOffStart), 
                TimeOffEnd = ToDateOnly(tor.TimeOffEnd), 
                Description = tor.Description, 
                Status = tor.Status,
                DateOfRequest = ToDateOnly(tor.DateOfRequest),
                EmployeeName = tor.Employee.UserName
            }).ToList());
        }

        private DateOnly ToDateOnly(DateTime dateTime) => new(dateTime.Year, dateTime.Month, dateTime.Day);
    }
}
