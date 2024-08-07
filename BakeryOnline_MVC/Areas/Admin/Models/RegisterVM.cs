using System.ComponentModel.DataAnnotations;

namespace BakeryOnline_MVC.Areas.Admin.Models
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(100,ErrorMessage = "Length from {2} to {1}"), MinLength(2)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Compare("Password",ErrorMessage = "Password and Cofirm Password do not match")]

        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
