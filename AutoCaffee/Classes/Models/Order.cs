using System;
using System.Collections.Generic;

namespace AutoCaffee
{
    public class Order : IComparable
    {
        [ColumnName("Номер")]
        public int Id { get; set; }
        public int PersonalId { get; set; }
        public Personal Personal;

        public int OrderstatusId { get; set; }
        public Orderstatus Orderstatus;

        public override string ToString() => Id.ToString();
        public int CompareTo(object obj) => Id.CompareTo(Convert.ToInt32(obj as string));

        //public virtual Personal IdstaffNavigation { get; set; }
        //public virtual Orderstatus IdstatusNavigation { get; set; }
        //public virtual ICollection<Check> Checks { get; set; }
    }
}
