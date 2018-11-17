using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TOT.Business.Exceptions;
using TOT.Dto;
using TOT.Entities.TimeOffPolicies;
using TOT.Interfaces;

namespace TOT.Business.Services
{
    public class EmployeePositionTimeOffPolicyService : BaseService
    {
        public EmployeePositionTimeOffPolicyService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }

        public EmployeePositionTimeOffPolicyDTO GetById(int id)
        {
            var template = unitOfWork.EmployeePositionTimeOffPolicies.Get(id);
            if (template == null)
            {
                throw new EntityNotFoundException<EmployeePositionTimeOffPolicy>(id);
            }
            return mapper.Map<EmployeePositionTimeOffPolicy, EmployeePositionTimeOffPolicyDTO>(template);
        }

        public Task DeleteByIdAsync(int id)
        {
            var template = unitOfWork.EmployeePositionTimeOffPolicies.Get(id);
            if (template == null)
            {
                throw new EntityNotFoundException<EmployeePositionTimeOffPolicy>(id);
            }
            var requsts = unitOfWork.TimeOffRequests.Find(x=>x.Policy.Id == id);
            if (requsts == null)
            {
                unitOfWork.EmployeePositionTimeOffPolicies.Delete(id);
            }
            else
            {
                template.IsActive = false;
                unitOfWork.EmployeePositionTimeOffPolicies.Update(template);
            }
            return unitOfWork.SaveAsync();
        }

        public Task CreateAsync(EmployeePositionTimeOffPolicyDTO ItemDTO)
        {
            EmployeePositionTimeOffPolicyDTOChecker(ItemDTO);

            var Item = mapper.Map<EmployeePositionTimeOffPolicyDTO, EmployeePositionTimeOffPolicy>(ItemDTO);

            var template = unitOfWork.EmployeePositionTimeOffPolicies
                .Find(x =>
                    x.IsActive == true &&
                    x.Position.Id == Item.Position.Id &&
                    x.TypeId == Item.TypeId
                    );
            if (template == null)
            {
                Item.Id = 0;
                Item.IsActive = true;
                unitOfWork.EmployeePositionTimeOffPolicies.Create(Item);
                return unitOfWork.SaveAsync();
            }
            else if (Item.PolicyId == template.PolicyId &&
                Item.Approvers.OrderBy(x=>x.EmployeePositionId)
                .SequenceEqual(template.Approvers.OrderBy(x=>x.EmployeePositionId), new TimeOffPolicyApproverComparer()))
            {
                return Task.CompletedTask;
            }
            else
            {
                throw new ArgumentException("Can not exist two Policies with same Type and Employee Position");
            }
        }

        public Task UpdateAsync(EmployeePositionTimeOffPolicyDTO ItemDTO)
        {
            EmployeePositionTimeOffPolicyDTOChecker(ItemDTO);

            var Item = mapper.Map<EmployeePositionTimeOffPolicyDTO, EmployeePositionTimeOffPolicy>(ItemDTO);

            var template = unitOfWork.TimeOffRequests
                .Find(x => x.Policy.Id == Item.Id);

            if (template == null)
            {
                unitOfWork.EmployeePositionTimeOffPolicies.Update(Item);
                return unitOfWork.SaveAsync();
            }
            else if (unitOfWork.EmployeePositionTimeOffPolicies.Get(Item.Id) is EmployeePositionTimeOffPolicy oldItem)
            {
                if (Item.TypeId == oldItem.TypeId &&
                    Item.PolicyId == oldItem.PolicyId &&
                    Item.Position.Id == oldItem.Position.Id &&
                    Item.Approvers.OrderBy(x => x.EmployeePositionId)
                    .SequenceEqual(oldItem.Approvers.OrderBy(x => x.EmployeePositionId), new TimeOffPolicyApproverComparer()))
                {
                    return Task.CompletedTask;
                }

                Item.Id = 0;
                Item.IsActive = true;

                oldItem.IsActive = false;
                oldItem.NextPolicy = Item;

                unitOfWork.EmployeePositionTimeOffPolicies.Update(oldItem);
                unitOfWork.EmployeePositionTimeOffPolicies.Create(Item);
                return unitOfWork.SaveAsync();
            }
            else
            {
                return CreateAsync(ItemDTO);
            }
        }

        public IEnumerable<EmployeePositionTimeOffPolicyDTO> GetAll()
        {
            return mapper
                .Map<IEnumerable<EmployeePositionTimeOffPolicy>, IEnumerable<EmployeePositionTimeOffPolicyDTO>>(
                    unitOfWork.EmployeePositionTimeOffPolicies.GetAll());
        }

        protected void EmployeePositionTimeOffPolicyDTOChecker(EmployeePositionTimeOffPolicyDTO ItemDTO)
        {
            if (ItemDTO == null)
            {
                throw new ArgumentNullException(nameof(ItemDTO));
            }
            if (unitOfWork.TimeOffPolicies.Get(ItemDTO.Policy.Id) == null)
            {
                throw new ArgumentException("Policy should be filled");
            }
            if (unitOfWork.EmployeePositions.Get(ItemDTO.Position.Id) == null)
            {
                throw new ArgumentException("Position should be filled");
            }
            if (unitOfWork.TimeOffTypes.Get(ItemDTO.Type.Id) == null)
            {
                throw new ArgumentException("Type should be filled");
            }
            if (!ItemDTO.Approvers.Any())
            {
                throw new ArgumentException("Policy should contain at least one Approver");
            }
        }
    }
}
