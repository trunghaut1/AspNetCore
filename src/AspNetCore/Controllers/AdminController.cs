using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCore.Models;
using Microsoft.AspNetCore.Http;
using AspNetCore.Models.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AspNetCore.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IProductRepository productRepo;
        private IOrderRepository orderRepo;
        private IHostingEnvironment _environment;
        public int pageSize = 5;

        public AdminController(IProductRepository productRepo, IHostingEnvironment environment, IOrderRepository orderRepo)
        {
            this.productRepo = productRepo;
            this.orderRepo = orderRepo;
            _environment = environment;
        }
        // GET: /<controller>/
        public IActionResult Index(int page = 1)
        {
            return View(productRepo.GetPage(pageSize, page));
        }
        public IActionResult Product(int id)
        {
            return View(new ProductFormViewModel
            {
                product = productRepo.GetById(id)
            });
        }
        [HttpPost]
        public async Task<IActionResult> Product(ProductFormViewModel model)
        {
            if(model.img != null)
            {
                var uploads = Path.Combine(_environment.WebRootPath, "images");
                using (var fileStream = new FileStream(Path.Combine(uploads, model.img.FileName), FileMode.Create))
                {
                    await model.img.CopyToAsync(fileStream);
                }
                model.product.Image = $"images/{model.img.FileName}";
            }
            if(productRepo.SaveProduct(model.product))
            {
                TempData["Success"] = "Đã lưu sản phẩm";
                return View("Index", productRepo.GetPage(pageSize, 1));
            }
            else
            {
                TempData["Error"] = "Lỗi lưu sản phẩm";
                return View(model);
            }
        }
        public ViewResult CreateProduct() => View("Product", new ProductFormViewModel());
        public IActionResult DeleteProduct(int id)
        {
            if(productRepo.Delete(id))
            {
                TempData["Success"] = "Đã xóa sản phẩm";
                return View("Index", productRepo.GetPage(pageSize, 1));
            }
            else
            {
                TempData["Error"] = "Lỗi xóa sản phẩm";
                return View("Index", productRepo.GetPage(pageSize, 1));
            }
        }
        public IActionResult Order(int page = 1)
        {
            return View(orderRepo.GetOrder(10, page));
        }
        public IActionResult ChangeOrder(int id, bool shipped)
        {
            if(orderRepo.SaveOrder(id,shipped))
            {
                TempData["Success"] = "Cập nhật đơn hàng thành công";
            }
            else
            {
                TempData["Error"] = "Cập nhật đơn hàng thất bại";
            }
            return View("Order", orderRepo.GetOrder(10, 1));
        }
        public IActionResult DeleteOrder(int id)
        {
            if (orderRepo.DeleteOrder(id))
            {
                TempData["Success"] = "Xóa đơn hàng thành công";
            }
            else
            {
                TempData["Error"] = "Xóa đơn hàng thất bại";
            }
            return View("Order", orderRepo.GetOrder(10, 1));
        }
    }
}
