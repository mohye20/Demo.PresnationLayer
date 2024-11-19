
using Demo.BuniessLogicLayer.Interfaces;
using Demo.DataAcessLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BuniessLogicLayer.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly MVCAppDbContext _dbContext;

        public IEmployeeRepos EmployeeRepos { get ; set ; }
        public IDepartmentRepos DepartmentRepos { get ; set ; }


        public UnitOfWork(MVCAppDbContext dbContext) // Ask CLR For Object From DbContext
        {
            EmployeeRepos = new EmployeeRepos(dbContext);
            DepartmentRepos = new DepartmentRepos(dbContext);
            _dbContext = dbContext;
        }

        public async Task<int> CompeleteAsync()
        {
           return await _dbContext.SaveChangesAsync();
        }

      

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
