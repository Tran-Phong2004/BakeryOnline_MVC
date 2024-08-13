using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
namespace BakeryOnline_MVC.Models
{
    public class AppUser : IdentityUser
    {
        [Column("DayOfBirth")]
        public DateTime? DayOfBirth { get; set; }

        [Column("CreationTime")]
        public DateTime CreationTime { get; set; }
    }
}
