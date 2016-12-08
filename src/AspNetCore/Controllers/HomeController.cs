using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCore.Models;

namespace AspNetCore.Controllers
{
    public class HomeController : Controller
    {
        private IProductRepository productRepo;
        public int pageSize = 8;

        public HomeController(IProductRepository productRepo)
        {
            this.productRepo = productRepo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
