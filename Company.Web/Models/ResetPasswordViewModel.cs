using System.ComponentModel.DataAnnotations;

namespace Company.Web.Models
{
	public class ResetPasswordViewModel
	{

		[Required(ErrorMessage = "Password is Required")]
		[MinLength(8, ErrorMessage = "Minimum Password Length is 8")]
		[DataType(DataType.Password)]
		public string NewPassword { get; set; } // 


		[Required(ErrorMessage = "Confirm Password is Required")]
		[Compare(nameof(NewPassword), ErrorMessage = "Confirm Password does not match Password")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }
	}
}
