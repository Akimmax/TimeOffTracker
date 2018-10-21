using System.Collections.Generic;
using TOT.Dto.Request_EntitiesDTO;
using TOT.Entities.Request_Entities;
using TOT.Interfaces;

namespace TOT.Business.Services
{
    class RequestStatusService : BaseService
    {
        public RequestStatusService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }

        public IEnumerable<RequestStatusDTO> GetAll()
        {
            return mapper.Map<IEnumerable<RequestStatus>, IEnumerable<RequestStatusDTO>>(
                unitOfWork.RequestStatuses.GetAll());
        }

    }
}
