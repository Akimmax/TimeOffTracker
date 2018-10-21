using System;
using System.Threading.Tasks;
using TOT.Entities.Request_Entities;

namespace TOT.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Check> Checks { get; }
        IRepository<RequestStatus> RequestStatuses { get; }
        IRepository<TimeOffRequest> TimeOffRequests { get; }
        IRepository<TimeOffType> TimeOffTypes { get; }

        Task SaveAsync();
    }
}
