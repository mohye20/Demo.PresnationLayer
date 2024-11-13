using Demo.DataAcessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BuniessLogicLayer.Interfaces
{
    public interface IDepartmentRepos
    {
        IEnumerable<Department> GetAll();

        Department GetById(int id);

        int Add(Department departmnet);

        int Update(Department departmnet);

        int Delete(Department departmnet);
    }
}