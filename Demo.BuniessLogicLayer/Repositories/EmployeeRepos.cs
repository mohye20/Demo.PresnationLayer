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
    internal class EmployeeRepos : IEmployeeRepos
    {
        private readonly MVCAppDbContext _dbContext;

        public EmployeeRepos(MVCAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Add(Employee employee)
        {
            _dbContext.Add(employee);
            return _dbContext.SaveChanges();
        }

        public int Delete(Employee employee)
        {
            _dbContext.Remove(employee);
               return _dbContext.SaveChanges();
        }

        public IEnumerable<Employee> GetAll()
        {
            return _dbContext.Employees.ToList();
        }

        public Employee GetById(int id)
        
        => _dbContext.Employees.Find(id);
        

        public int Update(Employee employee)
        {
            _dbContext.Employees.Update(employee);
            return _dbContext.SaveChanges();
        }
    }
}