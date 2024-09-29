using Demo.BLL.Interface;
using Demo.DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork , IDisposable
    {
        private readonly MvcAppDbcontext _dbcontext;

        public IEmpolyeeRepository EmployeeRepository { get; set; }
        public IDepartmentRepository DepartmentRepository { get ; set; }

        public UnitOfWork(MvcAppDbcontext dbcontext)//ask clr for object from dbcontext
        {
            EmployeeRepository = new EmpoyeeRepository(dbcontext);
            DepartmentRepository=new DepartmentRepository(dbcontext);
            this._dbcontext = dbcontext;
        }

        public int Complete()
        {
            return _dbcontext.SaveChanges();

        }

        public void Dispose()
        {
            _dbcontext.Dispose();
        }
    }
}
