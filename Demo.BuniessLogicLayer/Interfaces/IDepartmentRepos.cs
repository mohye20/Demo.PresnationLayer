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
        IEnumerable<Departmnet> GetAll();

        Departmnet GetById(int id);

        int Add(Departmnet departmnet);

        int Update(Departmnet departmnet);

        int Delete(Departmnet departmnet);
    }
}