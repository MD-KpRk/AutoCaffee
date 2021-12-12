using System;
using System.Collections.Generic;

namespace AutoCaffee
{
    public partial class Orderstring
    {
        public int Id { get; set; }
        public int? Idorder { get; set; }
        public int? Iddish { get; set; }

        public virtual Dish IddishNavigation { get; set; }
        public virtual Order IdorderNavigation { get; set; }
    }
}
