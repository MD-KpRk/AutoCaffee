using System;
using System.Collections.Generic;

#nullable disable

namespace AutoCaffee
{
    public partial class Orderstring
    {
        public int? Idorder { get; set; }
        public int? Iddish { get; set; }

        public virtual Dish IddishNavigation { get; set; }
        public virtual Order IdorderNavigation { get; set; }
    }
}
