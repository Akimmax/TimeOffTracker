using System;
using System.Threading.Tasks;

namespace TOT.Interfaces
{
    public interface IUnitOfWork
    {
        Task SaveAsync();
    }
}
