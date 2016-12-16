using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCore.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AspNetCore.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository repository;
        private IProductRepository productRepo;
        private Cart cart;

        public OrderController(IOrderRepository repoService, Cart cartService, IProductRepository productRepo)
        {
            repository = repoService;
            cart = cartService;
            this.productRepo = productRepo;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Checkout()
        {
            if(User.Identity.IsAuthenticated)
            {
                if (cart.Lines.Count() == 0)
                {
                    TempData["Message"] = new[] { "danger", "Giỏ hàng trống" };
                    return Redirect("/Cart");
                }
                ViewBag.cart = cart;
                return View(new Order());
            }
            TempData["Message"] = new[] {"danger", "Vui lòng đăng nhập để thực hiện thanh toán" };
            return Redirect("/Cart");
        }
        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            order.UserId = User.Identity.Name;
            order.Date = DateTime.Now;
            foreach(var value in cart.Lines)
            {
                order.OrderDetail.Add(new OrderDetail()
                {
                    OrderId = 0,
                    ProductId = value.Product.Id,
                    Quantity = value.Quantity
                });
                productRepo.MinusQuantity(value.Product.Id, value.Quantity);
            }
            if(repository.Add(order))
            {
                cart.Clear();
                TempData["Message"] = new[] { "success", "Đặt hàng thành công" };
                return Redirect("/Account");
            }
            else
            {
                TempData["Message"] = new[] { "danger", "Lỗi đặt hàng" };
                return View(order);
            }
        }
        public IActionResult ShowOrderDetail(int id)
        {
            return ViewComponent("OrderDetail", new { id = id});
        }
    }
}
