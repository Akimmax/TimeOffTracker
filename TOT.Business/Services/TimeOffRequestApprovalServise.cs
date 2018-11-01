using System;
using TOT.Interfaces;
using TOT.Dto.TimeOffRequests;
using System.Threading.Tasks;
using TOT.Entities.TimeOffRequests;

namespace TOT.Business.Services
{
    public class TimeOffRequestApprovalServise : BaseService
    {

        public TimeOffRequestApprovalServise(IUnitOfWork unitOfWork, IMapper mapper)
           : base(unitOfWork, mapper)
        {
        }

        public Task CreateAsync(TimeOffRequestApprovalDTO approvalDTO)
        {
            if (approvalDTO == null)
            {
                throw new ArgumentNullException(nameof(approvalDTO));
            }

            var entry = mapper.Map<TimeOffRequestApprovalDTO, TimeOffRequestApproval>(approvalDTO);

            unitOfWork.RequestApprovals.Create(entry);

            return unitOfWork.SaveAsync();

        }

    }
}

