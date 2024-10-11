using System.ComponentModel.DataAnnotations;

namespace DemoPL.ViewModels
{
	public class ForgetPasswordViewModel
	{
		[EmailAddress(ErrorMessage = "Invalid Email")]
		[Required(ErrorMessage = "Email is required")]

		public string Email { get; set; }
	}
}
