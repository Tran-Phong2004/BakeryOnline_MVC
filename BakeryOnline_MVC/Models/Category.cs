using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BakeryOnline_MVC.Models
{
    [Table("Category")]
    public class Category
    {
        [Key]
        [Column("Category_ID")]
        public int ID { get; set; }

        [Required]
        [Column("Category_Name")]
        public string Name { get; set; }

        public List<Product> Products { get; set; }
    }
}
