using Demo.BuniessLogicLayer.Interfaces;
using Demo.DataAcessLayer.Context;
using Demo.DataAcessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo.BuniessLogicLayer.Repositories
{
    public class DepartmentRepos : GenericRepo<Department>, IDepartmentRepos
    {
        public DepartmentRepos(MVCAppDbContext dbContext):base(dbContext)
        {
            
        }
    }
}