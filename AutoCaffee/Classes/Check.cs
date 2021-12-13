using System;
using System.Collections.Generic;

namespace AutoCaffee
{
    public partial class Check
    {
        public int Id { get; set; }
        public int Totalsum { get; set; }
        public int OrderId { get; set; }
        public Order Order;


        public int ClientId { get; set; }
        public Client Client;


        //public virtual Client IdclientNavigation { get; set; }
        //public virtual Order IdorderNavigation { get; set; }
    }
}
