﻿using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interface
{
    public interface IEmpolyeeRepository:IGenericRepository<Employee>
    {
       IQueryable<Employee > GetEmployeesByAdress(string adress);
        IQueryable<Employee> GetEmployeesByName(string nameSearch);
    }
}
