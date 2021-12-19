using System;
using System.Collections.Generic;

namespace AutoCaffee
{
    public partial class Orderstring
    {
        public int Id { get; set; }
        public int Count { get; set; }

#warning Поменять вот эту чушь на что-то приемлимое
        public virtual Dish IddishNavigation { get; set; }
        public int DishId { get; set; }
        public virtual Order IdorderNavigation { get; set; }
        public int OrderId { get; set; }
    }
}
