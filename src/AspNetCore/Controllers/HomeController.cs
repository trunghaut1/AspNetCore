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
        private ICatRepository catRepo;
        public int pageSize = 8;

        public HomeController(IProductRepository productRepo, ICatRepository catRepo)
        {
            this.productRepo = productRepo;
            this.catRepo = catRepo;
        }
        public IActionResult Index()
        {
            ViewBag.headerFull = "c-layout-header-fullscreen";
            ViewBag.headerTrans = "c-header-transparent-dark";
            ViewBag.header = "2";
            return View(productRepo.GetByNumber(8));
        }
        public IActionResult Product(int id)
        {
            Product value = productRepo.GetById(id);
            if(value != null)
                return View(value);
            return View(new Product());
        }
        public IActionResult List(int id, int page = 1)
        {
            if(id > 0)
            {
                ViewBag.CatID = id;
                ViewBag.Cat = catRepo.GetById(id)?.Name;
                return View(productRepo.GetByCat(id, pageSize, page));
            }
            else
            {
                return View(productRepo.GetPage(pageSize, page));
            }
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
