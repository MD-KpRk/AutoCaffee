using System;
using System.Collections.Generic;

namespace AutoCaffee
{
    public class Client
    {
        [ColumnName("Номер")]
        public int Id { get; set; }
        [ColumnName("Имя клиента")]
        public string Name { get; set; }

        [Visible(false)]
        public List<Check> Checks { get; set; }
    }
}
