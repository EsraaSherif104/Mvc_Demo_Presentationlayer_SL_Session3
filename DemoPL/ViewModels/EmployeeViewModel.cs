using Demo.DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace DemoPL.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "name is required")]
        [MaxLength(50, ErrorMessage = "max length is 50 chars")]
        [MinLength(5, ErrorMessage = "min length is 5 chars")]

        public string Name { get; set; }
        [Range(22, 35, ErrorMessage = "age must be in range from 22 to 35")]
        public int? age { get; set; }
        [RegularExpression("^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",
            ErrorMessage = "Adress must be like 123-street-city-country")]

        public string Address { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }

        public DateTime HireDate { get; set; }

        public IFormFile Image {  get; set; }

        public string ImageName { get; set; }

        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }//fk
                                              //fk optional=>onDelete :restrict
                                              //fk required=>onDelete:cascade
        [InverseProperty("employees")]
        public Department Department { get; set; }




    }
}
