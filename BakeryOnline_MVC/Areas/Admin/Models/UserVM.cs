using BakeryOnline_MVC.Models;
using Microsoft.AspNetCore.Identity;

namespace BakeryOnline_MVC.Areas.Admin.Models
{
    public class UserVM
    {
        public Paging<AppUser> Paging { get; set; }
        public Dictionary<string, List<string>> UserRoles { get; set; } = new Dictionary<string, List<string>>();
    }
}
