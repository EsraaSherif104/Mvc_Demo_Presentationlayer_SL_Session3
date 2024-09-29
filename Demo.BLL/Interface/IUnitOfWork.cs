using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interface
{
    public interface IUnitOfWork
    {
        //signature for property for each and every repository interface
        public IEmpolyeeRepository EmployeeRepository { get; set; }
        public IDepartmentRepository DepartmentRepository { get; set; }

        int Complete();
    }
}
