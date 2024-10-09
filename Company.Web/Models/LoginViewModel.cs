using System.ComponentModel.DataAnnotations;

namespace Company.Web.Models
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "Email is Required")]
		[EmailAddress(ErrorMessage = "Invalid Email")]
		public string Email { get; set; }


		[Required(ErrorMessage = "Password is Required")]
		[MinLength(8, ErrorMessage = "Minimum Password Length is 8")]
		[DataType(DataType.Password)]
		public string Password { get; set; } // 

        public bool RememberMe { get; set; }

    }
}
