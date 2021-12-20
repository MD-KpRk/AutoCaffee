using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoCaffee
{
    public partial class Check : IComparable
    {

        [Key]
        [ColumnName("Номер чека")]
        public int Id { get; set; }
        [ColumnName("Общая сумма")]
        public double Totalsum { get; set; }
        [ColumnName("Дата/Время")]
        public DateTime Date { get; set; }

        [Visible(false)]
        public int OrderId { get; set; }
        [ColumnName("Номер заказа")]
        public Order Order { get; set; }

        [Visible(false)]
        public int ClientId { get; set; }
        [ColumnName("Клиент")]
        public Client Client { get; set; }


        public int CompareTo(object obj) => Id.CompareTo(Convert.ToInt32(obj.ToString()));
        public override string ToString() => Id.ToString();

    }
}
