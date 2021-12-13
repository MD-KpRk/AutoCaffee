using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoCaffee
{
    public partial class Dolg
    {
        public Dolg()
        {
            //Personals = new HashSet<Personal>();
        }
        
        public int Id { get; set; }
        public string Title { get; set; }

        public List<Personal> Personals { get; set; }

    }
}
