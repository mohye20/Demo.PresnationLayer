using Demo.DataAcessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BuniessLogicLayer.Interfaces
{
    internal interface IEmployeeRepos
    {
        IEnumerable<Employee> GetAll();

        Employee GetById(int id);

        int Add(Employee employee);

        int Update(Employee employee);

        int Delete(Employee employee);
    }
}