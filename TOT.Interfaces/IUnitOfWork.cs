using System;
using TimeOffTracker.Entities.Request_Entities;

namespace TimeOffTracker.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<TimeOffRequest> TimeOfRequests { get; }
        IRepository<TimeOffType> TimeOfTypes { get; }
        IRepository<Check> Checks { get; }
    }
}
