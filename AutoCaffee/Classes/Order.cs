using System;
using System.Collections.Generic;

namespace AutoCaffee
{
    public partial class Order
    {
        public Order()
        {
            //Checks = new HashSet<Check>();
        }

        public int Id { get; set; }
        public int PersonalId { get; set; }
        public Personal Personal;
        public int OrderstatusId { get; set; }
        public Orderstatus Orderstatus;

        //public virtual Personal IdstaffNavigation { get; set; }
        //public virtual Orderstatus IdstatusNavigation { get; set; }
        //public virtual ICollection<Check> Checks { get; set; }
    }
}
