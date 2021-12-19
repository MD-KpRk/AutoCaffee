using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoCaffee
{
    public partial class Dolg
    {
        [Visible(false)]
        public int Id { get; set; }
        [ColumnName("Наименование")]
        public string Title { get; set; }

        [Visible(false)]
        public List<Personal> Personals { get; set; }

    }
}
