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
        OrderListViewModel GetOrderDetail(int pageSize, int page);
    }
}
