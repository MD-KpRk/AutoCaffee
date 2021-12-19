using System;
using System.Collections.Generic;

namespace AutoCaffee
{
    public partial class Orderstatus : IComparable
    {

        public int Id { get; set; }
        public string Title { get; set; }

        public List<Order> Orders { get; set; }

        public int CompareTo(object obj) =>Title.CompareTo(obj as string);
        public override string ToString() => Title;

    }
}
