using System.ComponentModel.DataAnnotations;

namespace DemoPL.ViewModels
{
	public class LoginViewModel
	{
		[EmailAddress(ErrorMessage = "Invalid Email")]
		[Required(ErrorMessage = "Email is required")]

		public string Email { get; set; }

		[Required(ErrorMessage = "password  is required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
