using AutoMapper;
using Newtonsoft.Json.Linq;
using TOT.Dto;
using TOT.Dto.SelectLists;
using TOT.Entities;
using TOT.Entities.TimeOffPolicies;
using TOT.Entities.TimeOffRequests;
using TOT.Dto.TimeOffRequests;
using TOT.Dto.TimeOffPolicies;

namespace TOT.Bootstrap.Mapping
{
    public class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            CreateMap<TimeOffType, TimeOffTypeDTO>();
            CreateMap<TimeOffTypeDTO, TimeOffType>();

            CreateMap<TimeOffRequestApprovalStatuses, TimeOffRequestApprovalStatusesDTO>();
            CreateMap<TimeOffRequestApprovalStatusesDTO, TimeOffRequestApprovalStatuses>();

            CreateMap<TimeOffRequestApproval, TimeOffRequestApprovalDTO>();
            CreateMap<TimeOffRequestApprovalDTO, TimeOffRequestApproval>();

            CreateMap<TimeOffRequest, TimeOffRequestDTO>();
            CreateMap<TimeOffRequestDTO, TimeOffRequest>();

            //-----------------Policy---------------

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

            //---------Special Create/Update models--------

            CreateMap<PolicyCreateModel, EmployeePositionTimeOffPolicyDTO>()
                .ForMember(m=>m.Approvers, m=>m.Ignore())
                .ForMember(m => m.Policy,
                m => m.ResolveUsing(src => new TimeOffPolicyDTO()
                {
                    Name = src.Name,
                    TimeOffDaysPerYear = src.TimeOffDaysPerYear,
                    DelayBeforeAvailable = src.DelayBeforeAvailable
                }))
                .AfterMap((src,dest)=>dest.Policy.Id = src.PolicyId);
            CreateMap<EmployeePositionTimeOffPolicyDTO, PolicyCreateModel>()
                .ForMember(m => m.Name, m => m.ResolveUsing(src => src.Policy.Name))
                .ForMember(m => m.TimeOffDaysPerYear, m => m.ResolveUsing(src => src.Policy.TimeOffDaysPerYear))
                .ForMember(m => m.DelayBeforeAvailable, m => m.ResolveUsing(src => src.Policy.DelayBeforeAvailable))
                .ForMember(m => m.Approvers, m => m.ResolveUsing(src =>
                {
                    var obj = new JObject();
                    foreach (var item in src.Approvers)
                    {
                        obj.Add(item.EmployeePosition.Id.ToString(), item.Amount);
                    }
                    return obj.ToString();
                }))
                .AfterMap((src, dest) => dest.PolicyId = src.Policy.Id);
            //--------SelectLists--------------------------

            CreateMap<TimeOffPolicyApprover, ApproversSelectList>()
                .ForMember(x => x.Title,
                m => m.ResolveUsing(src => $"{src.EmployeePosition.Title} : {src.Amount}"));
        }
    }
}
