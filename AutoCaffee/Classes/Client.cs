using System;
using System.Collections.Generic;

namespace AutoCaffee
{
    public partial class Client
    {
        public Client()
        {
            Checks = new HashSet<Check>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Check> Checks { get; set; }
    }
}
