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
        public OrderListViewModel GetOrderDetail(string userId, int pageSize, int page)
        {
            IEnumerable<OrderDetail> orderDetail = db.OrderDetail.Include(o => o.Product).Include(o => o.Order)
                .Where(o => o.Order.UserId == userId)
                .OrderByDescending(o => o.OrderId)
                .Skip((page - 1) * pageSize).Take(pageSize);
            return new OrderListViewModel
            {
                OrderDetails = orderDetail,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = db.OrderDetail.Where(o => o.Order.UserId == userId).Count()
                }
            };
        }
        public OrderViewModel GetOrder(int pageSize, int page)
        {
            IEnumerable<Order> order = db.Order.Include(o => o.OrderDetail)
                .OrderByDescending(o => o.Id)
                .Skip((page - 1) * pageSize).Take(pageSize);
            return new OrderViewModel
            {
                Orders = order,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = db.Order.Count()
                }
            };
        }
        public bool SaveOrder(int id, bool shipped)
        {
            try
            {
                Order order = db.Order.Find(id);
                order.Shipped = shipped;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool DeleteOrder(int id)
        {
            try
            {
                Order order = db.Order.Find(id);
                db.Order.Remove(order);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public IEnumerable<OrderDetail> GetOrderDetailById(int id)
        {
            return db.OrderDetail.Include(o => o.Product).Where(o => o.OrderId == id).AsEnumerable();
        }
    }
}
