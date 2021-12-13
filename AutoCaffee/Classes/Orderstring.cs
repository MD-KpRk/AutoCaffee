﻿using System;
using System.Collections.Generic;

namespace AutoCaffee
{
    public partial class Orderstring
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int DishId { get; set; }

        public virtual Dish IddishNavigation { get; set; }
        public virtual Order IdorderNavigation { get; set; }
    }
}
