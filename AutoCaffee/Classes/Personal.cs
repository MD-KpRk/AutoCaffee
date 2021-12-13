using System;
using System.Collections.Generic;


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
        public Dolg Dolg;

        //public virtual Dolg IddolgNavigation { get; set; }
        //public virtual ICollection<Order> Orders { get; set; }
    }
}
