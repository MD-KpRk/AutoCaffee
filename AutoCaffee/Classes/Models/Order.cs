using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoCaffee
{
    public class Order : IComparable
    {

        [ColumnName("Номер")]
        [Key]
        public int Id { get; set; }

        [Visible(false)]
        public int? PersonalId { get; set; }

        [ColumnName("Сотрудник")]
        public Personal Personal { get; set; }

        [Visible(false)]
        public int? OrderstatusId { get; set; }

        [ColumnName("Состояние")]
        public Orderstatus Orderstatus { get; set; }

        public override string ToString() => Id.ToString();
        public int CompareTo(object obj) => Id.CompareTo(Convert.ToInt32(obj as string));

        //[Visible(false)]
        public List<Orderstring> Orderstrings { get; set; } = new List<Orderstring>();

        [Visible(false)]
        public List<Check> Checks { get; set; } = new List<Check>();
    }


    public class OrderStringCollection : List<Orderstring>
    {
        public override string ToString()
        {
            return string.Join(", ", this);
        }
        public string ToString(string Separator)
        {
            return string.Join(Separator, this);
        }
    }

}
