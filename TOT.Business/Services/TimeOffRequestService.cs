using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TOT.Business.Exceptions;
using TOT.Entities.TimeOffRequests;
using TOT.Interfaces;
using TOT.Dto.TimeOffRequests;

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

            if (mapper.Map<TimeOffRequestDTO, TimeOffRequest>(requestDTO) is TimeOffRequest request)
            {
                unitOfWork.TimeOffRequests.Update(request);
            }
            else
            {
                return CreateAsync(requestDTO);
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

    }
}
