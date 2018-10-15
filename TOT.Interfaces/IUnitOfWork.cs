using System;
using TOT.Entities.Request_Entities;

namespace TOT.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<TimeOffRequest> TimeOfRequests { get; }
        IRepository<TimeOffType> TimeOfTypes { get; }
        IRepository<Check> Checks { get; }
    }
}
