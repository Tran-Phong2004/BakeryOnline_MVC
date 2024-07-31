using NuGet.Protocol.Plugins;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BakeryOnline_MVC.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        [Column("Product_ID")]

        public int ID { get; set; }

        [Required]
        [Column("Product_Name")]
        public string Name { get; set; }

        [Column("Product_Description")]
        public string? Description { get; set; }

        [Column("Price")]
        public decimal? Price { get; set; }

        [Column("Stock_Quantity")]

        public int? Stock_Quantity { get; set; }

        [Column("Image_Path")]

        public string? Img_Path { get; set; }

        public int Category_ID { get; set; }

        [ForeignKey(nameof(Category_ID))]
        public Category Category { get; set; }
    }
}
