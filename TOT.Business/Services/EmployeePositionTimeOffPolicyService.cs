using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TOT.Business.Exceptions;
using TOT.Dto;
using TOT.Dto.TimeOffPolicies;
using TOT.Dto.TimeOffPolicies.Models;
using TOT.Entities.TimeOffPolicies;
using TOT.Interfaces;

namespace TOT.Business.Services
{
    public class EmployeePositionTimeOffPolicyService : BaseService
    {
        public IUnitOfWork _unitOfWork;

        public EmployeePositionTimeOffPolicyService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<EmployeePositionTimeOffPolicyDTO> GetFilteredPolicies(PolicyFilterModel model)
        {
            if (model == null)
            {
                return GetAll();
            }
            var Items = _unitOfWork.EmployeePositionTimeOffPolicies.GetAll();
            if (model.Type != null && model.Type.Id!=0)
            {
                Items = Items.Where(x => x.Type.Id == model.Type.Id);
            }
            if (model.Position != null && model.Position.Id != 0)
            {
                Items = Items.Where(x => x.PositionId == model.Position.Id);
            }
            if (!String.IsNullOrEmpty(model.Name))
            {
                Items = Items.Where(x => x.Policy.Name.Contains(model.Name));
            }
            if (model.SearchByDelay)
            {
                Items = Items.Where(x => x.Policy.DelayBeforeAvailable == model.DelayBeforeAvailable);
            }
            if (model.TimeOffDaysPerYear != null && model.TimeOffDaysPerYear!=0)
            {
                Items = Items.Where(x => x.Policy.TimeOffDaysPerYear == model.TimeOffDaysPerYear);
            }
            if (model.IsActive != null)
            {
                Items = Items.Where(x => x.IsActive == model.IsActive);
            }
            if (model.ApproverPositions != null && model.ApproverPositions.Id !=0)
            {
                Items = Items.Where(x => x.Approvers.Any(y=>y.EmployeePositionId == model.ApproverPositions.Id));
            }
            if (!Items.Any())
            {
                return new List<EmployeePositionTimeOffPolicyDTO>();
            }
            return mapper.Map<IEnumerable<EmployeePositionTimeOffPolicy>, IEnumerable<EmployeePositionTimeOffPolicyDTO>>(Items);
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

        public EmployeePositionTimeOffPolicyDTO GetByTypeIdAndPositionId(int Typeid, int PositionId)
        {
                var item = unitOfWork.EmployeePositionTimeOffPolicies
                    .Find(x => x.TypeId == Typeid &&
                        x.PositionId == PositionId &&
                        x.IsActive == true);
                if (item == null)
                {
                    item = unitOfWork.EmployeePositionTimeOffPolicies
                    .Find(x => x.TypeId == Typeid &&
                        x.IsActive == true);
                }
                if (item == null)
                {
                    throw new EntityNotFoundException("Policy");
                }
                return mapper.Map<EmployeePositionTimeOffPolicy, EmployeePositionTimeOffPolicyDTO>(item);
        }

        public Task DeleteByIdAsync(int id)
        {
            var template = unitOfWork.EmployeePositionTimeOffPolicies.Get(id);
            if (template == null)
            {
                throw new EntityNotFoundException<EmployeePositionTimeOffPolicy>(id);
            }
            if (template.Position == null)
            {
                throw new Exception("It is not posible to delete default Policy.");
            }
            var requsts = unitOfWork.TimeOffRequests.Find(x=>x.Policy.Id == id);
            if (requsts == null)
            {
                var previous = unitOfWork.EmployeePositionTimeOffPolicies.Find(x => x.NextPolicy.Id == id);
                if (previous !=null)
                {
                    previous.NextPolicy = null;
                }
                unitOfWork.EmployeePositionTimeOffPolicies.Delete(id);
            }
            else
            {
                if (template.IsActive == false)
                {
                    return Task.CompletedTask;
                }
                template.IsActive = false;
                unitOfWork.EmployeePositionTimeOffPolicies.Update(template);
            }
            unitOfWork.SaveAsync();
            return Task.CompletedTask;
        }

        public Task CreateAsync(EmployeePositionTimeOffPolicyDTO ItemDTO)
        {
            EmployeePositionTimeOffPolicyDTOChecker(ItemDTO);

            var policy = unitOfWork.TimeOffPolicies
                .Find(x=>
                x.Name == ItemDTO.Policy.Name &&
                x.TimeOffDaysPerYear == ItemDTO.Policy.TimeOffDaysPerYear &&
                x.DelayBeforeAvailable == ItemDTO.Policy.DelayBeforeAvailable);
            if (policy != null)
            {
                ItemDTO.Policy.Id = policy.Id;
            }
            var Item = mapper.Map<EmployeePositionTimeOffPolicyDTO, EmployeePositionTimeOffPolicy>(ItemDTO);

            var template = unitOfWork.EmployeePositionTimeOffPolicies
                .Find(x =>
                    x.IsActive == true &&
                    x.Position.Id == Item.Position.Id &&
                    x.TypeId == Item.TypeId
                    );
            if (template == null)
            {
                var previous = unitOfWork.EmployeePositionTimeOffPolicies
                .Find(x =>
                    x.IsActive == false &&
                    x.Position.Id == Item.Position.Id &&
                    x.TypeId == Item.TypeId &&
                    x.NextPolicy == null
                    );

                Item.Id = 0;
                Item.IsActive = true;
                unitOfWork.EmployeePositionTimeOffPolicies.Create(Item);
                if (previous != null)
                {
                    previous.NextPolicy = Item;
                    unitOfWork.EmployeePositionTimeOffPolicies.Update(previous);
                }
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

            var policy = unitOfWork.TimeOffPolicies
                .Find(x =>
                x.Name == ItemDTO.Policy.Name &&
                x.TimeOffDaysPerYear == ItemDTO.Policy.TimeOffDaysPerYear &&
                x.DelayBeforeAvailable == ItemDTO.Policy.DelayBeforeAvailable);
            if (policy != null)
            {
                ItemDTO.Policy.Id = policy.Id;
            }

            var Item = mapper.Map<EmployeePositionTimeOffPolicyDTO, EmployeePositionTimeOffPolicy>(ItemDTO);
            foreach (var appr in Item.Approvers)
            {
                appr.EmployeePositionTimeOffPolicyId = Item.Id;
            }
            var template = unitOfWork.TimeOffRequests
                .Find(x => x.Policy.Id == Item.Id);

            if (unitOfWork.EmployeePositionTimeOffPolicies.Get(Item.Id) is EmployeePositionTimeOffPolicy oldItem)
            {
                if (oldItem.IsActive == false)
                {
                    throw new Exception("It is not posible to change Policy in Archive");
                }
                if (Item.TypeId == oldItem.TypeId &&
                    Item.PolicyId == oldItem.PolicyId &&
                    Item.Position.Id == oldItem.Position.Id &&
                    Item.Policy.Name == oldItem.Policy.Name &&
                    Item.Policy.TimeOffDaysPerYear == oldItem.Policy.TimeOffDaysPerYear &&
                    Item.Policy.DelayBeforeAvailable == oldItem.Policy.DelayBeforeAvailable &&
                    Item.Approvers.OrderBy(x => x.EmployeePositionId)
                    .SequenceEqual(oldItem.Approvers.OrderBy(x => x.EmployeePositionId), new TimeOffPolicyApproverComparer()))
                {
                    return Task.CompletedTask;
                } else
                if (template == null)
                {
                    Item.IsActive = true;
                    if (unitOfWork.EmployeePositionTimeOffPolicies.Filter(x => x.IsActive && x.PolicyId == Item.PolicyId).Count() > 1 &&
                        (oldItem.Policy.Name != Item.Policy.Name
                        || oldItem.Policy.TimeOffDaysPerYear != Item.Policy.TimeOffDaysPerYear
                        || oldItem.Policy.DelayBeforeAvailable != Item.Policy.DelayBeforeAvailable))
                    {
                        Item.Policy.Id = 0;
                    }
                    unitOfWork.EmployeePositionTimeOffPolicies.Update(Item);
                    return Task.CompletedTask;
                }
                else
                {
                    Item.Id = 0;
                    Item.IsActive = true;

                    DeleteByIdAsync(oldItem.Id);
                    unitOfWork.EmployeePositionTimeOffPolicies.Create(Item);
                    return Task.CompletedTask;
                }
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
