using System.ComponentModel.DataAnnotations;

namespace CookieMVC.Web.Models
{
    public class LoginFormModel
    {
        [Required(ErrorMessage="* required"), EmailAddress, Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "* required"), DataType(DataType.Password), Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Keep me logged-in after I close my browser")]
        public bool CreatePersistentSession { get; set; }
    }
}
