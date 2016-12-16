using Newtonsoft.Json;
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
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public string Address { get; set; }
        public bool Shipped { get; set; }

        [JsonIgnore]
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
