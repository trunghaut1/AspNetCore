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
        private ICatRepository catRepo;
        public int pageSize = 5;

        public AdminController(IProductRepository productRepo, IHostingEnvironment environment, IOrderRepository orderRepo,
            ICatRepository catRepo)
        {
            this.productRepo = productRepo;
            this.orderRepo = orderRepo;
            this.catRepo = catRepo;
            _environment = environment;
        }
        // GET: /<controller>/
        [AllowAnonymous]
        public IActionResult Index(int page = 1)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("admin"))
                {
                    return View(productRepo.GetPage(pageSize, page));
                }
                else
                {
                    TempData["Message"] = new[] { "danger", "Tài khoản không có quyền quản trị" };
                    return Redirect("/");
                }
            }
            else
            {
                return Redirect("/");
            }
        }
        public IActionResult Product(int id)
        {
            ViewBag.Cat = catRepo.Cats;
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
                TempData["Message"] = new[] { "success", "Đã lưu sản phẩm" };
                return View("Index", productRepo.GetPage(pageSize, 1));
            }
            else
            {
                TempData["Message"] = new[] { "danger", "Lỗi lưu sản phẩm" };
                return View(model);
            }
        }
        public ViewResult CreateProduct()
        {
            ViewBag.Cat = catRepo.Cats;
            return View("Product", new ProductFormViewModel());
        }
        public IActionResult DeleteProduct(int id)
        {
            if(productRepo.Delete(id))
            {
                TempData["Message"] = new[] {"success", "Đã xóa sản phẩm" };
                return View("Index", productRepo.GetPage(pageSize, 1));
            }
            else
            {
                TempData["Message"] = new[] {"danger", "Lỗi xóa sản phẩm" };
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
                TempData["Message"] = new[] { "success", "Cập nhật đơn hàng thành công" };
            }
            else
            {
                TempData["Message"] = new[] { "danger", "Cập nhật đơn hàng thất bại" };
            }
            return View("Order", orderRepo.GetOrder(10, 1));
        }
        public IActionResult DeleteOrder(int id)
        {
            if (orderRepo.DeleteOrder(id))
            {
                TempData["Message"] = new[] { "success", "Xóa đơn hàng thành công" };
            }
            else
            {
                TempData["Message"] = new[] { "danger", "Xóa đơn hàng thất bại" };
            }
            return View("Order", orderRepo.GetOrder(10, 1));
        }
    }
}
