using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Models.ViewModels
{
    public class OrderListViewModel
    {
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
