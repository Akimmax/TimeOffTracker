using System;
using System.Threading.Tasks;
using TOT.Entities.Request_Entities;

namespace TOT.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<TimeOffRequest> TimeOffRequests { get; }
        IRepository<TimeOffType> TimeOffTypes { get; }
        IRepository<Check> Checks { get; }
        IRepository<RequestStatus> RequstStatuses { get; }

        Task SaveAsync();
    }
}
