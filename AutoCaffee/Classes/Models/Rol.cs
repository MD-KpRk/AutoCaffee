using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCaffee
{
    public partial class Rol : IComparable
    {
        [Key]
        [Visible(false)]
        public int Id { get; set; }
        [ColumnName("Наименование")]
        public string Title { get; set; }

        [ColumnName("Управление БД")]
        public bool CanBD { get; set; }
        [ColumnName("Ролями")]
        public bool CanRole { get; set; }
        [ColumnName("Персоналом")]
        public bool CanPersonal { get; set; }
        [ColumnName("Заказами")]
        public bool CanOrder { get; set; }
        [ColumnName("Блюдами")]
        public bool CanFood { get; set; }

        [Visible(false)]
        public List<Personal> Personals { get; set; } = new List<Personal>();

        public int CompareTo(object obj) => Title.CompareTo(obj);
        public override string ToString() => Title;
    }
}
