using System;
using System.Collections.Generic;

namespace AspNetCore.Models
{
    public partial class User
    {
        public User()
        {
            Order = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool? IsAdmin { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool? Gender { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }
}
