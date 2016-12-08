using System;
using System.Collections.Generic;

namespace AspNetCore.Models
{
    public partial class Cat
    {
        public Cat()
        {
            Product = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}
