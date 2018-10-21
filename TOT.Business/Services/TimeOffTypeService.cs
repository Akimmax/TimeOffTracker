using System.Collections.Generic;
using TOT.Dto.Request_EntitiesDTO;
using TOT.Entities.Request_Entities;
using TOT.Interfaces;

namespace TOT.Business.Services
{
    public class TimeOffTypeService : BaseService
    {
        public TimeOffTypeService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }

        public IEnumerable<TimeOffTypeDTO> GetAll()
        {
            return mapper.Map<IEnumerable<TimeOffType>, IEnumerable<TimeOffTypeDTO>>(
                unitOfWork.TimeOffTypes.GetAll());
        }

    }
}
