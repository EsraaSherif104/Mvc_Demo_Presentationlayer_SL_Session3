using System.ComponentModel.DataAnnotations;

namespace DemoPL.ViewModels
{
	public class ResetPasswordViewModel
	{
		[Required(ErrorMessage ="New Password Is Required")]
		[DataType(DataType.Password)]
		public string NewPassword { get; set; }


		[Required(ErrorMessage = "Confirm New Password Is Required")]
		[DataType(DataType.Password)]
		[Compare("NewPassword",ErrorMessage ="Password does't match")]
		public string ConfirmNewPassword { get; set; }
	}
}
