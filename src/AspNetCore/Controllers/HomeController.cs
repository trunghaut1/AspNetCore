﻿using System;
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
        public int pageSize = 5;

        public HomeController(IProductRepository productRepo, ICatRepository catRepo)
        {
            this.productRepo = productRepo;
            this.catRepo = catRepo;
        }
        public IActionResult Index()
        {
            return View(productRepo.GetByNumber(8));
        }
        public IActionResult Product(int id)
        {
            ViewBag.RelatedPro = productRepo.GetByNumber(4);
            Product value = productRepo.GetById(id);
            if(value != null)
            {
                return View(value);
            } 
            return View(new Product());
        }
        public IActionResult List(int id, int page = 1)
        {
            if(id > 0)
            {
                ViewBag.CatID = id;
                ViewBag.CatName = catRepo.GetById(id)?.Name;
                return View(productRepo.GetByCat(id, pageSize, page));
            }
            else
            {
                return View(productRepo.GetPage(pageSize, page));
            }
        }
        public IActionResult Search(string search, int page = 1)
        {
            ViewBag.Search = search;
            return View(productRepo.Search(search, pageSize, page));
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
