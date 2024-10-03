using System.ComponentModel.DataAnnotations;

namespace Company.Web.Models
{
    public class SignupViewModel
    {
       [Required(ErrorMessage ="FirstName is required")]
        public string FristName { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage ="Invalid Format for Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=(.*[A-Z]){1})(?=(.*[a-z]){1})(?=(.*\d){1})(?=(.*\W){1})(?!.*(.)\1\1).{8,}$", ErrorMessage = "Password must meet the complexity requirements.")]

        public string Password { get; set; }
        [Required(ErrorMessage = "ConfirmPassword is required")]
        [Compare(nameof(Password),ErrorMessage = "ConfirmPassword is not match password")]
        public string ConfirmPassword { get; set; }
        public bool IsAgree { get; set; }

    }
}
