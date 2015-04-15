using System.ComponentModel.DataAnnotations;

namespace CookieMVC.Web.Models
{
    public class ResetPasswordFormModel
    {
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Required, DataType(DataType.Password), Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password), Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Token { get; set; }
    }
}
