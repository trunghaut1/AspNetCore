using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCore.Models;
using AspNetCore.Infrastructure;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AspNetCore.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;
        public CartController(IProductRepository repo)
        {
            repository = repo;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(GetCart());
        }
        public IActionResult RefreshCart()
        {
            return ViewComponent("CartSummary");
        }
        public bool AddToCart(int id, int quantity)
        {
            Product product = repository.GetById(id);
            if (product != null)
            {
                Cart cart = GetCart();
                cart.AddItem(product, quantity);
                SaveCart(cart);
                return true;
            }
            return false;
        }
        public bool DownCart(int id)
        {
            Product product = repository.GetById(id);
            if (product != null)
            {
                Cart cart = GetCart();
                var line = cart.Lines.Where(o => o.Product.Id == id).SingleOrDefault();
                if (line.Quantity > 1)
                    line.Quantity -= 1;
                else return false;
                SaveCart(cart);
                return true;
            }
            return false;
        }
        public bool RemoveFromCart(int id)
        {
            Product product = repository.GetById(id);
            if (product != null)
            {
                Cart cart = GetCart();
                cart.RemoveLine(product);
                SaveCart(cart);
                return true;
            }
            return false;
        }
        private Cart GetCart()
        {
            Cart cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
            return cart;
        }

        private void SaveCart(Cart cart)
        {
            HttpContext.Session.SetJson("Cart", cart);
        }
    }
}
