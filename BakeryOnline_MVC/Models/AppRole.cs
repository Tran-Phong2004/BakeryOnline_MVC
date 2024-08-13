using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BakeryOnline_MVC.Models
{
    public class AppRole : IdentityRole
    {
        [Column("IsRoleDefault", TypeName = "bit")]
        public bool? IsDefaultRole { get; set; }
    }
}
