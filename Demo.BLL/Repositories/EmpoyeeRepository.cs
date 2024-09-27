using Demo.BLL.Interface;
using Demo.DAL.Contexts;
using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class EmpoyeeRepository : GenericRepository<Employee>, IEmpolyeeRepository
    {
        private readonly MvcAppDbcontext _dbcontext;

        public EmpoyeeRepository(MvcAppDbcontext dbcontext):base(dbcontext) 
        {
            this._dbcontext = dbcontext;
        }
        public IQueryable<Employee> GetEmployeesByAdress(string adress)
        
           => _dbcontext.Empolyee.Where(e => e.Address == adress);
                
    }
}

