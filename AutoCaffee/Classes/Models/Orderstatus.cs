using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoCaffee
{
    public partial class Orderstatus : IComparable
    {
        [Key]
        [Visible(false)]
        public int Id { get; set; }
        [ColumnName("Наименование")]
        public string Title { get; set; }

        public List<Order> Orders { get; set; }

        public int CompareTo(object obj) =>Title.CompareTo(obj as string);
        public override string ToString() => Title;

    }
}
