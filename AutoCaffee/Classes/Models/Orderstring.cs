using System;
using System.Collections.Generic;

namespace AutoCaffee
{
    public partial class Orderstring
    {
        [Visible(false)]
        public int Id { get; set; }
        [ColumnName("Кол-во")]
        public int Count { get; set; }

        [ColumnName("Блюдо")]
        public Dish Dish { get; set; }
        [Visible(false)]
        public int DishId { get; set; }

        [Visible(false)]
        public Order Order { get; set; }
        [ColumnName("Номер заказа")]
        public int OrderId { get; set; }
    }
}
