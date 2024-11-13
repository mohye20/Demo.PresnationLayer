using Demo.DataAcessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BuniessLogicLayer.Interfaces
{
    internal interface IEmployeeRepos:IGenericRepo<Employee>
    {
        IQueryable<Employee> GetEmployeesByAddress(string address);
       
    }
}