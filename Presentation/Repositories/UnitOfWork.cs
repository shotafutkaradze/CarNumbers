using Doamin.Entities;
using Doamin.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doamin.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private CarBDContext _context;
        private IGenericRepository<CarNumbers> _carNumbers;
        private IGenericRepository<OrderCase> _orders;
        public UnitOfWork(CarBDContext context)
        {
            _context = context;
        }


        public IGenericRepository<CarNumbers> CarNumber => _carNumbers ??= new GenericRepository<CarNumbers>(_context);

        public IGenericRepository<OrderCase> OrderCases => _orders ??= new GenericRepository<OrderCase>(_context);

        public void Dispose()
        {
            _context.Dispose();
        }
        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
