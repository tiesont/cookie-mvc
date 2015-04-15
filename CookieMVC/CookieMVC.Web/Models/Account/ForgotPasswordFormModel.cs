using System.ComponentModel.DataAnnotations;

namespace CookieMVC.Web.Models
{
    public class ForgotPasswordFormModel
    {
        [Required(ErrorMessage="* required"), EmailAddress, Display(Name = "Email")]
        public string Email { get; set; }
    }
}
