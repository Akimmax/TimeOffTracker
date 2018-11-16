using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TOT.Business.Exceptions;
using TOT.Dto;
using TOT.Entities.TimeOffPolicies;
using TOT.Interfaces;

namespace TOT.Business.Services
{
    public class TimeOffPolicyApproversService :BaseService
    {
        public TimeOffPolicyApproversService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }

        public TimeOffPolicyApproverDTO GetById(int id)
        {
            var template = unitOfWork.TimeOffPolicyApprovers.Get(id);
            if (template == null)
            {
                throw new EntityNotFoundException<TimeOffPolicyApprover>(id);
            }
            return mapper.Map<TimeOffPolicyApprover, TimeOffPolicyApproverDTO>(template);
        }

        public Task DeleteByIdAsync(int id)
        {
            var template = unitOfWork.TimeOffPolicyApprovers.Get(id);
            if (template == null)
            {
                throw new EntityNotFoundException<TimeOffPolicyApprover>(id);
            }
            unitOfWork.TimeOffPolicyApprovers.Delete(id);
            return unitOfWork.SaveAsync();
        }

        public Task CreateAsync(TimeOffPolicyApproverDTO ItemDTO)
        {
            TimeOffPolicyApproversDTOChecker(ItemDTO);

            var Item = mapper.Map<TimeOffPolicyApproverDTO, TimeOffPolicyApprover>(ItemDTO);

            var Comparer = new TimeOffPolicyApproverComparer();

            var template = unitOfWork.TimeOffPolicyApprovers.Find(x =>
                x.EmployeePositionId == Item.EmployeePositionId &&
                x.EmployeePositionTimeOffPolicyId == Item.EmployeePositionTimeOffPolicyId);
            if (template == null)
            {
                unitOfWork.TimeOffPolicyApprovers.Create(Item);
                return unitOfWork.SaveAsync();
            }
            else if (Comparer.Equals(template, Item))
            {
                return Task.CompletedTask;
            }
            else
            {
                throw new ArgumentException("Can not exist two Approvers with same Employee Position");
            }
            
        }

        public Task UpdateAsync(TimeOffPolicyApproverDTO ItemDTO)
        {
            TimeOffPolicyApproversDTOChecker(ItemDTO);

            var Item = mapper.Map<TimeOffPolicyApproverDTO, TimeOffPolicyApprover>(ItemDTO);

            var Comparer = new TimeOffPolicyApproverComparer();

            if (unitOfWork.TimeOffPolicyApprovers.Get(Item.Id) is TimeOffPolicyApprover oldItem)
            {
                if (Comparer.Equals(oldItem, Item))
                {
                    return Task.CompletedTask;
                }
                if (oldItem.EmployeePositionId != Item.EmployeePositionId ||
                    oldItem.EmployeePositionTimeOffPolicyId != Item.EmployeePositionTimeOffPolicyId)
                {
                    return CreateAsync(ItemDTO);
                }
            }
            unitOfWork.TimeOffPolicyApprovers.Update(Item);
            return unitOfWork.SaveAsync();
        }

        public IEnumerable<TimeOffPolicyApproverDTO> GetAll()
        {
            return mapper
                .Map<IEnumerable<TimeOffPolicyApprover>, IEnumerable<TimeOffPolicyApproverDTO>>(
                    unitOfWork.TimeOffPolicyApprovers.GetAll());
        }

        protected void TimeOffPolicyApproversDTOChecker(TimeOffPolicyApproverDTO ItemDTO)
        {
            if (ItemDTO == null)
            {
                throw new ArgumentNullException(nameof(ItemDTO));
            }
            if (ItemDTO.Amount <= 0)
            {
                throw new ArgumentException("Amoun should be greater than 0");
            }
            if (unitOfWork.EmployeePositions.Get(ItemDTO.EmployeePosition.Id) == null)
            {
                throw new ArgumentException("Position should be filled");
            }
            var template = unitOfWork.EmployeePositionTimeOffPolicies.Get(ItemDTO.EmployeePositionTimeOffPolicyId);
            if (template == null || template.IsActive == false)
            {
                throw new ArgumentException("Should be filled whith available Policy");
            }
        }
    }
}
