using Demo.DAL.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace DemoPL.ViewModels
{
	public class RegisterViewModel
	{
		
	   [Required(ErrorMessage = "First Name is required")]

		public string FName { get; set; }

		[Required(ErrorMessage = "Last Name is required")]

		public string LName { get; set; }
		

		
		[EmailAddress(ErrorMessage ="Invalid Email")]
		[Required(ErrorMessage = "Email is required")]

		public string Email { get; set; }

		[Required(ErrorMessage = "password  is required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required(ErrorMessage ="confirm password is Required")]
		[DataType(DataType.Password)]
		[Compare("Password",ErrorMessage ="password Dosen't match")]
		public string ConfirmPassword { get; set;}
		public bool IsAgree { get; set; }


	}
}
