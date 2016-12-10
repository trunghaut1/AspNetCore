using AspNetCore.Infrastructure;
using AspNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Components
{
    public class MenuViewComponent : ViewComponent
    {
        private ICatRepository repo;

        public MenuViewComponent(ICatRepository repo)
        {
            this.repo = repo;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.cartNumber = GetCart().Lines.Count();
            return View(repo.Cats);
        }
        private Cart GetCart()
        {
            Cart cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
            return cart;
        }
    }
}
