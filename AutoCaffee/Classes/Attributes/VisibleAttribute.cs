using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCaffee
{
    public class VisibleAttribute : Attribute
    {
        public bool visible { get; set; }
        public VisibleAttribute(bool visible) { this.visible = visible; }
    }
}
