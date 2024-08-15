using BakeryOnline_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Web.Helpers;
using BakeryOnline_MVC.Models;
using BakeryOnline_MVC.Repository;
using Microsoft.IdentityModel.Tokens;

namespace BakeryOnline_MVC.Controllers
{
    public class ProductController : Controller
    {
        UnitOfWork uow;
        public ProductController(UnitOfWork uow)
        {
          this.uow = uow;
        }

        [HttpPost]
        public  async Task<IActionResult> GetProductByCategory(int[] CategoryId, int Page = 1)
        {
            var pageSize = 8;
            var listProduct = uow.ProductRepository.GetAll();

            var pagingResult = await uow.ProductRepository.Paging(listProduct, Page, pageSize, 
                                                                  filter: CategoryId.Length > 0 ? (p => CategoryId.Contains(p.Category_ID)) : (p => true),
                                                                  orderBy: qr => qr.OrderBy(product => product.Category_ID).ThenBy(product => product.ID),
                                                                  include: null);
        
            return PartialView("RenderProductByCategoryPartial",pagingResult);
        }

    }
}
