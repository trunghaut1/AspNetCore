using System;
using System.Collections.Generic;

namespace AspNetCore.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderDetail = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public int CatId { get; set; }
        public string Name { get; set; }
        public int? Price { get; set; }
        public int? Quantity { get; set; }
        public string Image { get; set; }
        public string Describe { get; set; }

        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
        public virtual Cat Cat { get; set; }
    }
}
