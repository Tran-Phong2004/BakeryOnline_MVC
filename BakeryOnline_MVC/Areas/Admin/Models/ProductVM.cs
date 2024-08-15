using BakeryOnline_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BakeryOnline_MVC.Areas.Admin.Models
{
    public class ProductVM
    {
        public Paging<Product> Paging { get; set; }

        [BindProperty]
        public Input Input { get; set; }
    }

    public class Input 
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int Stock {  get; set; }

        [Required(ErrorMessage = "Category is required")]
        public int Category_Id { get; set; }
        public IFormFile ImgFile { get; set; }
    }
}
