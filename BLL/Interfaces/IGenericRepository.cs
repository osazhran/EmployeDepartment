using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IGenericRepository<T> where T : ModuleBase
    {

        IEnumerable<T> GetAll();
        T GetById(int id);
        // here we make add, update and delete with int to return number of recored that has been affected
        int Add(T Entity);
        int Update(T Entity);
        int Delete(T Entity);
    }
}
