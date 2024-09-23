using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Contexts
{
    public class MvcAppDbcontext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        
          =>  optionsBuilder.UseSqlServer("Server=.;Database=MvcAppS3;Trust_connection=true");

        
       
        public DbSet<Department> Department { get; set; }

    }
}
