using Doamin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doamin.Repositories.Interface
{

    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<CarNumbers> CarNumber { get; }
        IGenericRepository<OrderCase> OrderCases { get; }

        Task<int> Save();
    }
}
