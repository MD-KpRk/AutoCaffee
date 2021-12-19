using System;
using System.Collections.Generic;

namespace AutoCaffee
{
    public partial class Check
    {
        public int Id { get; set; }
        public float Totalsum { get; set; }

#warning Сюда вставить дату

        public int OrderId { get; set; }
        public Order Order;

        public int ClientId { get; set; }
        public Client Client;

    }
}
