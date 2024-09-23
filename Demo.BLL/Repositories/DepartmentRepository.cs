using Demo.BLL.Interface;
using Demo.DAL.Contexts;
using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly  MvcAppDbcontext _dbcontext;
        public DepartmentRepository(MvcAppDbcontext dbcontext)//ask clr for object from dbcontext
        {
            _dbcontext= dbcontext;
        }
        public int Add(Department department)
        {
           _dbcontext.Add(department);
         return _dbcontext.SaveChanges();      
        }

        public int Delete(Department department)
        {
            _dbcontext.Remove(department);
            return _dbcontext.SaveChanges();
        }

        public IEnumerable<Department> GetAll()
          =>  _dbcontext.Department.ToList();

        

        public Department GetById(int id)
        {
          //var department=  _dbcontext.Department.Local.Where(d => d.Id == id).FirstOrDefault();
          //  if(department == null)
          //      department=_dbcontext.Department.Where(d=>d.Id==id).FirstOrDefault();
          //      return department;
            
            return _dbcontext.Department.Find(id);
        }

        public int Update(Department department)
        {
               _dbcontext.Update(department);
            return _dbcontext.SaveChanges();    
                
                
        }
    }
}
