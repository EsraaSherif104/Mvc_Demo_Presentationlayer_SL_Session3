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
    public class DepartmentRepository :GenericRepository<Department> ,IDepartmentRepository
    {
        public DepartmentRepository(MvcAppDbcontext dbcontext) :base(dbcontext)
        {
            
        }

    }
}
