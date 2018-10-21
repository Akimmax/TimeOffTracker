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
    class TimeOffRequestService : BaseService
    {
        public TimeOffRequestService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }

        public Task CreateAsync(TimeOffRequestDTO requestDTO)
        {
            if (requestDTO == null)
            {
                throw new ArgumentNullException(nameof(requestDTO));
            }

            var request = mapper.Map<TimeOffRequestDTO, TimeOffRequest>(requestDTO);

            unitOfWork.TimeOffRequests.Create(request);

            return unitOfWork.SaveAsync();
        }

        public Task DeleteAsync(int requestId)
        {
            unitOfWork.TimeOffRequests.Delete(requestId);

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

            foreach (var requestDTO in requestsDTO)
            {
                if (requestDTO.Checks.All(c => c.Status.Title == "Accepted"))
                {
                    var date = requestDTO.Checks.Max(c => c.SolvedDate);
                    requestDTO.Approved = date.ToString("dd/MM/yyyy");
                }
                else
                {
                    requestDTO.Approved = "Not Approved";
                }

                requestDTO.AmountDaysOff = (requestDTO.StartTimeOffDate - requestDTO.StartTimeOffDate).TotalDays + 1;
            }

            return requestsDTO;
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

    }
}
