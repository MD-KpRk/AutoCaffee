using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoCaffee
{
    public class Personal : IComparable
    {
        [Visible(false)]
        [Key]
        public int Id { get; set; }
        [ColumnName("Имя")]
        public string Firstname { get; set; }
        [ColumnName("Фамилия")]
        public string Secondname { get; set; }
        [ColumnName("Отчество")]
        public string Patronymic { get; set; }
        [Visible(false)]
        public string Hashpass { get; set; }
        [ColumnName("Номер телефона")]
        public string Phonenumber { get; set; }

        [Visible(false)]
        public int DolgId { get; set; }
        [ColumnName("Должность")]
        public Dolg Dolg { get; set; }
        [Visible(false)]
        public int RolId { get; set; }
        [ColumnName("Роль")]
        public Rol Rol { get; set; }


        [Visible(false)]
        public List<Order> Orders { get; set; }

        public int CompareTo(object obj) => Firstname.CompareTo(obj.ToString());
        public override string ToString() => Firstname;

    }
}
