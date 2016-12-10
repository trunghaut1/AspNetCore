using AspNetCore.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Models
{
    public class EFOrderRepository : IOrderRepository
    {
        private DatabaseContext db;
        public EFOrderRepository(DatabaseContext db)
        {
            this.db = db;
        }
        public bool Add(Order order)
        {
            try
            {
                db.Order.Add(order);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public OrderListViewModel GetOrderDetail(int pageSize, int page)
        {
            IEnumerable<OrderDetail> orderDetail = db.OrderDetail.Include(o => o.Product).Include(o => o.Order)
                .OrderByDescending(o => o.OrderId)
                .Skip((page - 1) * pageSize).Take(pageSize);
            return new OrderListViewModel
            {
                OrderDetails = orderDetail,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = db.OrderDetail.Count()
                }
            };
        }
    }
}
