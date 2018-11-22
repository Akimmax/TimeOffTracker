using System.Collections.Generic;
using TOT.Entities.TimeOffRequests;
using TOT.Interfaces;
using TOT.Dto.TimeOffRequests;

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
            return mapper.Map<IEnumerable<TimeOffType>,
                IEnumerable<TimeOffTypeDTO>>(
                unitOfWork.TimeOffTypes.GetAll()); 
        }

    }
}
