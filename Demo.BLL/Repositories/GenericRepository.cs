using Demo.BLL.Interface;
using Demo.DAL.Contexts;
using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly MvcAppDbcontext _dbcontext;

        public GenericRepository(MvcAppDbcontext dbcontext)
        {
            this._dbcontext = dbcontext;
        }
        public void Add(T item)
        {
            _dbcontext.Add(item);
        }

        public void Delete(T item)
        {
            _dbcontext.Remove(item);
        }

        public IEnumerable<T> GetAll()
        {
            if(typeof(T)==typeof(Employee))
            {
                return (IEnumerable<T>)_dbcontext.Empolyee.Include(E=>E.Department).ToList();


            }
            return _dbcontext.Set<T>().ToList();

        }

        public T GetById(int id)
          =>  _dbcontext.Set<T>().Find(id);
        

        public void Update(T item)
        {
            _dbcontext.Update(item);
        }
    }
}
