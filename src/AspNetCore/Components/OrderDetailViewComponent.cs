using AspNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Components
{
    public class OrderDetailViewComponent : ViewComponent
    {
        private IOrderRepository repository;
        public OrderDetailViewComponent(IOrderRepository repository)
        {
            this.repository = repository;
        }
        public IViewComponentResult Invoke(int id)
        {
            return View(repository.GetOrderDetailById(id));
        }
    }
}
