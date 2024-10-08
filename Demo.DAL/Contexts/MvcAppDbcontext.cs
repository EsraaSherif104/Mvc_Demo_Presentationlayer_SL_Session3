﻿using Demo.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Contexts
{
    public class MvcAppDbcontext: IdentityDbContext<ApplicationUser>
    {
        public MvcAppDbcontext(DbContextOptions<MvcAppDbcontext>options):base(options) 
        {
            
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        //  =>  optionsBuilder.UseSqlServer("Server=.;Database=MvcAppS3;Trust_connection=true");



        public DbSet<Department> Department { get; set; }
        public DbSet<Employee> Empolyee { get; set; }

        

    }
}
