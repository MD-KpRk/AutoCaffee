using System;
using System.Collections.Generic;

#nullable disable

namespace AutoCaffee
{
    public partial class Orderstatus
    {
        public Orderstatus()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
