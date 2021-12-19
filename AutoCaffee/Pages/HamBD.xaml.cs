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
                int number = (cb.SelectedItem as ListObject).number;
                if (number == 1)
                {
                    //dg.ItemsSource = bd.Clients.Include(c => c.Checks).ToList();
                    dg.ItemsSource = bd.Personals.ToList();
                }
                if (number == 2)
                {
                    dg.ItemsSource = bd.Dolgs.ToList();
                }
                if (number == 3)
                {
                    dg.ItemsSource = bd.Rols.ToList();
                }
                if (number == 4)
                {
                    dg.ItemsSource = bd.Dishes.ToList();
                }
                if (number == 5)
                {
                    dg.ItemsSource = bd.Orderstrings.Include(item => item.Dish).Include(item2 => item2.Order).ToList();
                }
            }
        }

        private void dg_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            PropertyDescriptor descriptor = e.PropertyDescriptor as PropertyDescriptor;
            ColumnNameAttribute nameAttribute = descriptor.Attributes[typeof(ColumnNameAttribute)] as ColumnNameAttribute;
            VisibleAttribute visibleAttribute = descriptor.Attributes[typeof(VisibleAttribute)] as VisibleAttribute;
            if (nameAttribute != null) e.Column.Header = nameAttribute.Name;
            if(visibleAttribute != null) if (visibleAttribute.visible == false) e.Column.Visibility = Visibility.Collapsed;
        }
    }
}
