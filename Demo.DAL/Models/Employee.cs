﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
      

        public string Name { get; set; }
        public int? age { get; set; }
        
        public string Address { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        
        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }

        public DateTime HireDate {  get; set; }

        public string ImageName { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;
        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }//fk
       //fk optional=>onDelete :restrict
       //fk required=>onDelete:cascade
       [InverseProperty("employees")]
        public Department Department { get; set; }  
         



    }
}
