using AutoMapper;
using TOT.Dto.TimeOffRequests;
using TOT.Entities.TimeOffRequests;

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
        }
    }
}
