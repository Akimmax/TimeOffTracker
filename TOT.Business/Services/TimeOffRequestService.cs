using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TOT.Business.Exceptions;
using TOT.Entities.TimeOffRequests;
using TOT.Interfaces;
using TOT.Dto.TimeOffRequests;
using Microsoft.AspNetCore.Identity;
using TOT.Entities.IdentityEntities;
using System.Linq;
using TOT.Entities.TimeOffPolicies;

namespace TOT.Business.Services
{
    public class TimeOffRequestService : BaseService
    {
        public TimeOffRequestService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }

        public Task CreateAsync(TimeOffRequestDTO request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var entry = mapper.Map<TimeOffRequestDTO, TimeOffRequest>(request);

            unitOfWork.TimeOffRequests.Create(entry);
           
            return unitOfWork.SaveAsync();
        }

        public Task DeleteAsync(int id)
        {
            unitOfWork.TimeOffRequests.Delete(id);

            return unitOfWork.SaveAsync();
        }

        public Task UpdateAsync(TimeOffRequestDTO requestDTO)
        {
            if (requestDTO == null)
            {
                throw new ArgumentNullException(nameof(requestDTO));
            }

            var request = unitOfWork.TimeOffRequests.Get(requestDTO.Id);

            if (request != null)
            {
                request.Note = requestDTO.Note;
            }
            else
            {
                throw new EntityNotFoundException<TimeOffRequest>(requestDTO.Id);
            }

            return unitOfWork.SaveAsync();

        }

        public TimeOffRequestDTO GetById(int requestId)
        {
            var request = unitOfWork.TimeOffRequests.Get(requestId);

            if (request == null)
            {
                throw new EntityNotFoundException<TimeOffRequest>(requestId);
            }

            return mapper.Map<TimeOffRequest, TimeOffRequestDTO>(request);
        }

        public IEnumerable<TimeOffRequestDTO> GetAll()
        {
            var requests = unitOfWork.TimeOffRequests.GetAll();
            var requestsDTO = mapper.Map<IEnumerable<TimeOffRequest>, IEnumerable<TimeOffRequestDTO>>(requests);

            return requestsDTO;
        }

        public IEnumerable<User> GetUsers(int typeId, int positionId, UserManager<User> userManager)
        {

            var appropriateEmployeePositionTimeOffPolicy = GetEmployeePositionTimeOffPolicyByTypeAndPosition(typeId, positionId);

            if (appropriateEmployeePositionTimeOffPolicy == null)
            {
                throw new EntityNotFoundException("Appropriate policy");
            }

            var approvals = appropriateEmployeePositionTimeOffPolicy.Approvals;

            var aviableUserAsApprovals = userManager.Users.Where(u =>
            approvals.Any(a => a.UserId == u.Id));//select Users available to approve requests this policy

            if (aviableUserAsApprovals == null || !aviableUserAsApprovals.Any())
            {
                throw new ApprovalsNotFoundException("Approvals for appropriate policy");
            }

            return aviableUserAsApprovals;
        }

        public EmployeePositionTimeOffPolicy GetEmployeePositionTimeOffPolicyByTypeAndPosition
            (int typeId, int positionId)
        {
            return unitOfWork.EmployeePositionTimeOffPolicy.Find(
              emtp => emtp.TypeId == typeId && emtp.PositionId == positionId);
        }

    }
}
