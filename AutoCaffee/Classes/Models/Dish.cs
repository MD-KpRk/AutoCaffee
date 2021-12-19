using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoCaffee
{
    public partial class Dish : IComparable
    {
        [Visible(false)]
        [Key]
        public int Id { get; set; }
        [ColumnName("Наименование")]
        public string Title { get; set; }
        [ColumnName("Цена BYN")]
        public double Price { get; set; }
        [ColumnName("Наличие")]
        public bool Available { get; set; }

        public int CompareTo(object other)
        {
            return Title.CompareTo(other as string);
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
