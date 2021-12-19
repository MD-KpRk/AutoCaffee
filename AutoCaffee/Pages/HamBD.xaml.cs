using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutoCaffee.Pages
{
    public class ListObject
    {
        public int number { get; set; }
        public string title { get; set; }
        public ListObject(int number, string title)
        {
            this.number = number;
            this.title = title;
        }
        public override string ToString()
        {
            return title;
        }
    }

    public partial class HamBD : Page
    {
        public HamBD()
        {
            InitializeComponent();

            cb1.ItemsSource = new ListObject[] {
                new ListObject(1,"Сотрудники"),
                new ListObject(2,"Должности"),
                new ListObject(3,"Роли"),
                new ListObject(4,"Блюда"),
                new ListObject(5,"Строки заказов"),
                new ListObject(6,"Заказы"),
                new ListObject(7,"Клиент"),
                new ListObject(8,"Счёт"),
                new ListObject(9,"Состояния заказов")
            };

        }

        private void cb1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            var optionsBuilder = new DbContextOptionsBuilder<AutoCaffeeBDContext>();
            var options = optionsBuilder.UseSqlServer(ConfigurationHelper.getInstance().conString).Options;
            using (AutoCaffeeBDContext bd = new AutoCaffeeBDContext(options))
            {
                if ((cb.SelectedItem as ListObject).number == 1)
                {

                    dg.ItemsSource = bd.Clients.ToList();


                }
            }
        }

        private void dg_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            PropertyDescriptor desc = e.PropertyDescriptor as PropertyDescriptor;
            ColumnNameAttribute att = desc.Attributes[typeof(ColumnNameAttribute)] as ColumnNameAttribute;
            VisibleAttribute att2 = desc.Attributes[typeof(VisibleAttribute)] as VisibleAttribute;
            if (att != null) e.Column.Header = att.Name;
            if(att2 != null) if (att2.visible == false) e.Column.Visibility = Visibility.Collapsed;
        }
    }
}
