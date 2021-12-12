using System;
using System.Collections.Generic;

namespace AutoCaffee
{
    public partial class Order
    {
        public Order()
        {
            Checks = new HashSet<Check>();
        }

        public int Id { get; set; }
        public int? Idstaff { get; set; }
        public int? Idstatus { get; set; }

        public virtual Personal IdstaffNavigation { get; set; }
        public virtual Orderstatus IdstatusNavigation { get; set; }
        public virtual ICollection<Check> Checks { get; set; }
    }
}
