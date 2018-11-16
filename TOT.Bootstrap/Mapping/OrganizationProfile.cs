using AutoMapper;
using TOT.Dto;
using TOT.Entities;
using TOT.Entities.TimeOffPolicies;
using TOT.Entities.TimeOffRequests;

namespace TOT.Bootstrap.Mapping
{
    public class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            CreateMap<TimeOffType, TimeOffTypeDTO>();
            CreateMap<TimeOffTypeDTO, TimeOffType>();

            CreateMap<EmployeePosition, EmployeePositionDTO>();
            CreateMap<EmployeePositionDTO, EmployeePosition>();

            CreateMap<TimeOffPolicy, TimeOffPolicyDTO>();
            CreateMap<TimeOffPolicyDTO, TimeOffPolicy>();

            CreateMap<TimeOffPolicyApprover, TimeOffPolicyApproverDTO>();
            CreateMap<TimeOffPolicyApproverDTO, TimeOffPolicyApprover>()
                .ForMember(m => m.EmployeePositionId,
                m => m.ResolveUsing(src => src.EmployeePosition.Id));

            CreateMap<EmployeePositionTimeOffPolicy, EmployeePositionTimeOffPolicyDTO>();
            CreateMap<EmployeePositionTimeOffPolicyDTO, EmployeePositionTimeOffPolicy>()
                .ForMember(m => m.PolicyId,
                m => m.ResolveUsing(src => src.Policy.Id))
                .ForMember(m => m.TypeId,
                m => m.ResolveUsing(src => src.Type.Id));
        }
    }
}
