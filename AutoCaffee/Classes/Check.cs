using System;
using System.Collections.Generic;

namespace AutoCaffee
{
    public partial class Check
    {
        public int Id { get; set; }
        public int Totalsum { get; set; }
        public int? Idorder { get; set; }
        public int? Idclient { get; set; }

        public virtual Client IdclientNavigation { get; set; }
        public virtual Order IdorderNavigation { get; set; }
    }
}
