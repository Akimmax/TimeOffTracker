using System;
using System.Threading.Tasks;
using TOT.Entities.TimeOffRequests;

namespace TOT.Interfaces
{
    public interface IUnitOfWork
    {
        Task SaveAsync();
    }
}
