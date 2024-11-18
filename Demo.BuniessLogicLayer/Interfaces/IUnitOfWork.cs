using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BuniessLogicLayer.Interfaces
{
    public interface IUnitOfWork
    {
        // Signature For Property For Each And Every Repos Interface
        public IEmployeeRepos EmployeeRepos { get; set; }
        public IDepartmentRepos DepartmentRepos { get; set; }

        int Compelete();

    }
}
