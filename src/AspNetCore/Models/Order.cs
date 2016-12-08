using System;
using System.Collections.Generic;

namespace AspNetCore.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetail = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime? Date { get; set; }
        public string Address { get; set; }

        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
        public virtual User User { get; set; }
    }
}
