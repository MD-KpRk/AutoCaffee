using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoCaffee
{
    public partial class Dolg : IComparable
    {
        [Visible(false)]
        [Key]
        public int Id { get; set; }
        [ColumnName("Наименование")]
        public string Title { get; set; }

        [Visible(false)]
        public List<Personal> Personals { get; set; }

        public int CompareTo(object obj) => Title.CompareTo(obj);
        public override string ToString() => Title;
    }
}
