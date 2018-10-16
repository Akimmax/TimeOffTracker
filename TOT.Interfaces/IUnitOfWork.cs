using System;
using System.Threading.Tasks;
using TOT.Entities.Request_Entities;

namespace TOT.Interfaces
{
    public interface IUnitOfWork
    {
        Task SaveAsync();
    }
}
