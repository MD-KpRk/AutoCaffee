using System;
using System.Collections.Generic;

#nullable disable

namespace AutoCaffee
{
    public partial class Dolg
    {
        public Dolg()
        {
            staff = new HashSet<staff>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Price { get; set; }
        public bool Available { get; set; }

        public virtual ICollection<staff> staff { get; set; }
    }
}
