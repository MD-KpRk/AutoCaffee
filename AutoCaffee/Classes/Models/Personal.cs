using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoCaffee
{
    public partial class Personal
    {
        public Personal()
        {
            //Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Secondname { get; set; }
        public string Patronymic { get; set; }
        public string Hashpass { get; set; }
        public string Phonenumber { get; set; }

        public int DolgId { get; set; }
        public Dolg Dolg { get; set; }

    }
}
