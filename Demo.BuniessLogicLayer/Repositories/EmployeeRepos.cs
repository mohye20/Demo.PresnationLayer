using Demo.BuniessLogicLayer.Interfaces;
using Demo.DataAcessLayer.Context;
using Demo.DataAcessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BuniessLogicLayer.Repositories
{
    public class EmployeeRepos : GenericRepo<Employee>, IEmployeeRepos
    {
        private readonly MVCAppDbContext _dbContext;

        public EmployeeRepos(MVCAppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Employee> GetEmployeesByAddress(string address)

        => _dbContext.Employees.Where(E => E.Address == address);
    }
}