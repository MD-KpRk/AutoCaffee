using System;
using System.Collections.Generic;

namespace AutoCaffee
{
    public class Client
    {
        [Visible(false)]
        public int Id { get; set; }
        [ColumnName("Имя")]
        public string Name { get; set; }

        [Visible(false)]
        public List<Check> Checks { get; set; }
    }
}
