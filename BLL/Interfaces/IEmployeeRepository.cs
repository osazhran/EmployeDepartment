using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IEmployeeRepository:IGenericRepository<Employee>
    {
        // specifc method for employee
        IQueryable<Employee> GetEmployeeByAddres(string address);
        IQueryable<Employee> SearchByName(string name);
    }
}
