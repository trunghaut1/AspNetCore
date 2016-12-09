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
            //ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(repo.Cats);
        }
    }
}
