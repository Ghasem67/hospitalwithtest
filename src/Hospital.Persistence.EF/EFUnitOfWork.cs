using Hospital.Infrastructure;
using Hospital.Infrastructure.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Persistence.EF
{
    public class EFUnitOfWork : UnitOfWork
    {
        private readonly EFDbContext _dbContext;
        public EFUnitOfWork(EFDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void commit()
        {
            _dbContext.SaveChanges();
        }
    }
}
