using BLL.Interfaces;
using DAL.Data;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        //private readonly AppDbContext _dbContext;

        public EmployeeRepository(AppDbContext dbContext):base(dbContext)
        {
            
        }
        public IQueryable<Employee> GetEmployeeByAddres(string address)
        {
            return _dbContext.Employees.Include(E=>E.DepartmentId).Where(E => E.Address.ToLower().Contains(address.ToLower()));
        }

        public IQueryable<Employee> SearchByName(string name)
        {
            return _dbContext.Employees.Include(E => E.Department).Where(E => E.Name.ToLower().Contains(name.ToLower()));
        }



    }
}
