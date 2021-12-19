using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
            int number = (cb.SelectedItem as ListObject).number;
            using (AutoCaffeeBDContext bd = new AutoCaffeeBDContext(options))
            {
                if (number == 1)
                {
                    dg.ItemsSource = bd.Personals.Include(item => item.Dolg).Include(item => item.Rol).ToList();
                }
                else if (number == 2)
                {
                    dg.ItemsSource = bd.Dolgs.ToList();
                }
                else if(number == 3)
                {
                    dg.ItemsSource = bd.Rols.ToList();
                }
                else if(number == 4)
                {
                    dg.ItemsSource = bd.Dishes.ToList();
                }
                else if(number == 5)
                {
                    dg.ItemsSource = bd.Orderstrings.Include(item => item.Dish).Include(item2 => item2.Order).ToList();
                }
                else if(number == 6)
                {
                    dg.ItemsSource = bd.Orders.Include(item=>item.Orderstatus).Include(item=>item.Personal).ToList();

                    bd.Orderstrings.Include(item => item.Dish).Include(item2 => item2.Order).ToList();

                    foreach(var a in bd.Orders.Where(it => it.Id == 1).ToList().FirstOrDefault().Orderstrings)
                    {
                        Debug.WriteLine(a.Dish);
                    }
                    // сделать норм вывод коллекции или кнопку подробнее потом запилить

                }
                else if(number == 7)
                {
                    
                }
                else if(number == 8)
                {

                }
                else if(number == 9)
                {

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
