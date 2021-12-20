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



    public delegate bool Func1<T>(List<T> list);


    public partial class HamBD : Page
    {
        enum Tables
        {
            Personal = 1,
            Dolgs,
            Rols,
            Dishes,
            OrderStrings,
            Orders,
            Clients,
            Checks,
            OrderStatuses
        }

        private Tables currentTable;
        Tables CurrentTable 
        { 
            get => currentTable;  
            set
            {
                using (AutoCaffeeBDContext bd = new AutoCaffeeBDContext(ConfigurationHelper.dbContextOptions))
                {
                    switch (value)
                    {
                        case Tables.Personal: dg.ItemsSource = bd.Personals.Include(item => item.Dolg).Include(item => item.Rol).ToList(); break;
                        case Tables.Dolgs:
                            dg.ItemsSource = bd.Dolgs.ToList();
                            break;
                        case Tables.Rols:
                            dg.ItemsSource = bd.Rols.ToList();
                            break;
                        case Tables.Dishes:
                            dg.ItemsSource = bd.Dishes.ToList();
                            break;
                        case Tables.OrderStrings:
                            dg.ItemsSource = bd.Orderstrings.Include(item => item.Dish).Include(item2 => item2.Order).ToList();
                            break;
                        case Tables.Orders:
                            dg.ItemsSource = bd.Orders.Include(item => item.Orderstatus).Include(item => item.Personal).ToList();

                            //bd.Orderstrings.Include(item => item.Dish).Include(item2 => item2.Order).ToList();
                            //foreach (var a in bd.Orders.Where(it => it.Id == 1).ToList().FirstOrDefault().Orderstrings)
                            //{
                            //    Debug.WriteLine(a.Dish);
                            //}
                            break;
                        case Tables.Clients:
                            dg.ItemsSource = bd.Clients.ToList();
                            break;
                        case Tables.Checks:
                            dg.ItemsSource = bd.Checks.Include(item => item.Order).Include(item => item.Client).ToList();
                            break;
                        case Tables.OrderStatuses:
                            dg.ItemsSource = bd.Orderstatuses.ToList();
                            break;
                    }
                    currentTable = value;
                }
            }
        }

        public bool ToTable<T>(List<T> list)
        {
            dg.ItemsSource = list;
            return true;
        }

        public HamBD()
        {
            InitializeComponent();


            cb1.ItemsSource = new List<ListObject>(){
                new ListObject((int)Tables.Personal,"Сотрудники"),
                new ListObject((int)Tables.Dolgs,"Должности"),
                new ListObject((int)Tables.Rols,"Роли"),
                new ListObject((int)Tables.Dishes,"Блюда"),
                new ListObject((int)Tables.OrderStrings,"Строки заказов"),
                new ListObject((int)Tables.Orders,"Заказы"),
                new ListObject((int)Tables.Clients,"Клиенты"),
                new ListObject((int)Tables.Checks,"Счета"),
                new ListObject((int)Tables.OrderStatuses,"Состояния заказов")
            };
        }

        private void cb1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CurrentTable = (Tables)((sender as ComboBox).SelectedItem as ListObject).number;
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
