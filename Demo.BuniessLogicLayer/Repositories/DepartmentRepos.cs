using Demo.BuniessLogicLayer.Interfaces;
using Demo.DataAcessLayer.Context;
using Demo.DataAcessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo.BuniessLogicLayer.Repositories
{
    public class DepartmentRepos : IDepartmentRepos
    {
        private readonly MVCAppDbContext _dbContext;

        public DepartmentRepos(MVCAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Add(Department departmnet)
        {
            _dbContext.Add(departmnet);

            return _dbContext.SaveChanges();
        }

        public int Delete(Department departmnet)
        {
            _dbContext.Remove(departmnet);

            return _dbContext.SaveChanges();
        }

        public IEnumerable<Department> GetAll()
        {
            return _dbContext.Departmnets.ToList();
        }

        public Department GetById(int id)
        {
            //var Department = _dbContext.Departmnets.Local.Where(D => D.Id == id).FirstOrDefault();
            //if (Department is null) {
            //    Department =  _dbContext.Departmnets.Local.Where(D => D.Id == id).FirstOrDefault();
            //}

            //return Department; 

            return _dbContext.Departmnets.Find(id);
             
        }

        public int Update(Department departmnet)
        {
            _dbContext.Update(departmnet);
            return _dbContext.SaveChanges();
        }
    }
}