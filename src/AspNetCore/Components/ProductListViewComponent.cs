using AspNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Components
{
    public class ProductListViewComponent : ViewComponent
    {
        private IProductRepository repo;
        
        public ProductListViewComponent(IProductRepository repo)
        {
            this.repo = repo;
        }
        public IViewComponentResult Invoke()
        {
            //ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(repo.GetByNumber(4));
        }
    }
}
