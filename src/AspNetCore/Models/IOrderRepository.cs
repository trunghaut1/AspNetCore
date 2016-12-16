using AspNetCore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Models
{
    public interface IOrderRepository
    {
        bool Add(Order order);
        OrderListViewModel GetOrderDetail(string userId, int pageSize, int page);
        OrderViewModel GetOrder(int pageSize, int page);
        bool SaveOrder(int id, bool shipped);
        bool DeleteOrder(int id);
        IEnumerable<OrderDetail> GetOrderDetailById(int id);
    }
}
