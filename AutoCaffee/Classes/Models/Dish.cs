using System;
using System.Collections.Generic;

namespace AutoCaffee
{
    public partial class Dish
    {
        [Visible(false)]
        public int Id { get; set; }
        [ColumnName("Наименование")]
        public string Title { get; set; }
        [ColumnName("Цена BYN")]
        public double Price { get; set; }
        [ColumnName("Наличие")]
        public bool Available { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}
