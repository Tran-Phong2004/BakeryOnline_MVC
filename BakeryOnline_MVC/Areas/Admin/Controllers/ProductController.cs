using BakeryOnline_MVC.Areas.Admin.Models;
using BakeryOnline_MVC.Models;
using BakeryOnline_MVC.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;

namespace BakeryOnline_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        UnitOfWork _uow;
        ILogger<ProductController> _logger;
        public ProductController(UnitOfWork uow,ILogger<ProductController> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        [Route("Admin/[controller]/Management/{page:int?}")]
        public async Task<IActionResult> Index(int page = 1)
        {
            var pageSize = 10;
            var paging = await _uow.ProductRepository.Paging(_uow.ProductRepository.GetAll(), 
                                                             page, 
                                                             pageSize, 
                                                             filter: null, 
                                                             orderBy: qr => qr.OrderBy(p => p.Category.Name).ThenBy(p => p.Price),
                                                             include: qr => qr.Include(p => p.Category));
            var model = new ProductVM()
            {
                Paging = paging
            };

            ViewBag.CategoryList = _uow.CategoryRepository.GetAll().ToList();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductVM model) 
        {
            //product sẽ add vào database
            if(model != null)
            {
                var product = new Product()
                {
                    Name = model.Input.Name,
                    Price = model.Input.Price,
                    Description = model.Input.Description,
                    Stock_Quantity = model.Input.Stock
                };

                var inputCategory = await _uow.CategoryRepository.FindByIdAsyn<int>(model.Input.Category_Id);
                if (inputCategory != null)
                {
                    product.Category = inputCategory;
                }

                try
                {
                    var result = await HandleImageUpload(model, product);
                    if (!result)
                    {
                        TempData["ErrorUploadProductImg"] = "Upload product image error";
                    }
                }
                catch (Exception ex) // ghi log ra console khi có exeption lúc upload file ảnh
                {
                    _logger.LogError("Error when upload product image: " + ex.Message);
                }
                _uow.ProductRepository.AddEntity(product);
                _uow.SaveChange();
                TempData["AddSuccees"] = $"Thêm thành công sản phẩm {product.Name}";
            }
            return RedirectToAction("Index","Product");
        }

        //Xử lý upload hình ảnh product 
        private async Task<bool> HandleImageUpload(ProductVM model,Product product)
        {
            try
            {
                var category = await _uow.CategoryRepository.FindByIdAsyn<int>(model.Input.Category_Id);
                if (model.Input.ImgFile != null && category != null)
                {
                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/ImgProduct/{category.Name}");

                    // Tạo thư mục nếu nó không tồn tại
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    var filePath = Path.Combine(directoryPath, model.Input.ImgFile.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.Input.ImgFile.CopyToAsync(stream);
                    }
                    SetImgPath(product, $"ImgProduct/{category.Name}/{model.Input.ImgFile.FileName}");

                    return true;
                }
            }
            catch (Exception ex) 
            {
                throw;
            }
            return false;
        }

        private void SetImgPath(Product product,string Path)
        {
            product.Img_Path = Path;
        }

        public async Task<IActionResult> DeleteProduct(int Id)
        {
            var deleteProduct = await _uow.ProductRepository.FindByIdAsyn(Id);
            if (deleteProduct != null)
            {
                //Xóa file hình ảnh
                if(!string.IsNullOrEmpty(deleteProduct.Img_Path)) {
                    //đường dẫn hình ảnh của sản phẩm
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/{deleteProduct.Img_Path}");
                    // Kiểm tra nếu tập tin tồn tại trước khi xóa
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }
                //Xóa dữ liệu trong database
                _uow.ProductRepository.DeleteEntity(deleteProduct);
                _uow.SaveChange();
                TempData["DeleteSuccess"] = $"Đã xóa sản phẩm {deleteProduct.Name}";
            }
            return RedirectToAction("Index", "Product");
        }
    }
}
