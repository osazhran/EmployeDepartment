using BLL.Interfaces;
using DAL.Data;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : ModuleBase
    {

        private protected readonly AppDbContext _dbContext;

        public GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int Add(T Entity)
        {
            _dbContext.Add(Entity);
            return _dbContext.SaveChanges();
        }

        public int Delete(T Entitiy)
        {
            _dbContext.Remove(Entitiy);
            return _dbContext.SaveChanges();
        }
        public int Update(T Entity)
        {
            _dbContext.Update(Entity);
            return (_dbContext.SaveChanges());
        }

        public IEnumerable<T> GetAll()
        {
            if (typeof(T) == typeof(Employee))
            {
               return (IEnumerable<T>) _dbContext.Employees.Include(E=>E.Department).AsNoTracking().ToList();
            }
            else
            {
                return _dbContext.Set<T>().AsNoTracking().ToList();
            }
        }

        public T GetById(int id)
        {
            return _dbContext.Find<T>(id);
        }

    }
}
