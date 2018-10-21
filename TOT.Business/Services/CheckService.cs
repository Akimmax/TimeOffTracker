using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using TOT.Business.Exceptions;
using TOT.Dto.Request_EntitiesDTO;
using TOT.Entities.Request_Entities;
using TOT.Interfaces;

namespace TOT.Business.Services
{
    class CheckService : BaseService
    {
        public CheckService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }

        public Task CreateAsync(CheckDTO checkDTO)
        {
            if (checkDTO == null)
            {
                throw new ArgumentNullException(nameof(checkDTO));
            }

            var check = mapper.Map<CheckDTO, Check>(checkDTO);

            unitOfWork.Checks.Create(check);

            return unitOfWork.SaveAsync();
        }

        public Task DeleteAsync(int checkId)
        {
            unitOfWork.Checks.Delete(checkId);

            return unitOfWork.SaveAsync();
        }

        public CheckDTO GetById(int checkId)
        {
            var check = unitOfWork.Checks.Get(checkId);

            if (check == null)
            {
                throw new EntityNotFoundException<Check>(checkId);
            }

            return mapper.Map<Check, CheckDTO>(check);
        }

        public IEnumerable<CheckDTO> GetAll()
        {
            return mapper.Map<IEnumerable<Check>, IEnumerable<CheckDTO>>(
                unitOfWork.Checks.GetAll());
        }

        public Task UpdateAsync(CheckDTO checkDTO)
        {
            if (checkDTO == null)
            {
                throw new ArgumentNullException(nameof(checkDTO));
            }

            if (mapper.Map<CheckDTO, Check>(checkDTO) is Check check)
            {
                unitOfWork.Checks.Update(check);
            }
            else
            {
                return CreateAsync(checkDTO);
            }

            return unitOfWork.SaveAsync();
        }

        public IEnumerable<CheckDTO> GetAll(string userId)
        {
            return mapper.Map<IEnumerable<Check>, IEnumerable<CheckDTO>>(
                unitOfWork.Checks.Filter(c => c.UserId == userId));
        }
    }
}
