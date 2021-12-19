using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoCaffee
{
    public class Client : IComparable
    {
        [ColumnName("Номер")]
        [Key]
        public int Id { get; set; }
        [ColumnName("Имя клиента")]
        public string Name { get; set; }

        [Visible(false)]
        public List<Check> Checks { get; set; }

        public int CompareTo(object obj) => Name.CompareTo(obj as string);

        public override string ToString() => Name;

    }
}
