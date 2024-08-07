using System.ComponentModel.DataAnnotations;

namespace BakeryOnline_MVC.Areas.Admin.Models
{
    public class LoginVM
    {
        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(100, ErrorMessage = "Length from {2} to {1}"), MinLength(2)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
