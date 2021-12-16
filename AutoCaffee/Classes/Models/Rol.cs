using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCaffee
{
    public partial class Rol
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public bool CanBD { get; set; }
        public bool CanRole { get; set; }
        public bool CanPersonal { get; set; }
        public bool CanOrder { get; set; }
        public bool CanFood { get; set; }
    }
}
