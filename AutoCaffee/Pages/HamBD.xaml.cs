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
        void SwitchSearchPanel(Tables tableForSearch)
        {
            DisableAllSearchPanels();
            switch(tableForSearch)
            {
                case Tables.Personal: ShowSearchPanel(SearchPersonal); break;
            }


            void DisableAllSearchPanels()
            {
                Separat.Visibility = Visibility.Collapsed;
                SearchTextBlock.Visibility = Visibility.Collapsed;
                SearchPersonal.Visibility = Visibility.Collapsed;
            }
            void ShowSearchPanel(StackPanel panel)
            {
                Separat.Visibility = Visibility.Visible;
                SearchTextBlock.Visibility = Visibility.Visible;
                panel.Visibility = Visibility.Visible;
            }
        }



        enum Tables{ None = 0,Personal, Dolgs, Rols, Dishes, OrderStrings, Orders, Clients, Checks, OrderStatuses }

        private Tables currentTable = Tables.None;
        Tables CurrentTable 
        { 
            get => currentTable;  
            set
            {
                using (AutoCaffeeBDContext bd = new AutoCaffeeBDContext(ConfigurationHelper.dbContextOptions))
                {
                    SwitchSearchPanel(value);
                    switch (value)
                    {
                        case Tables.Personal:dg.ItemsSource = bd.Personals.Include(item => item.Dolg).Include(item => item.Rol).ToList(); break;
                        case Tables.Dolgs: dg.ItemsSource = bd.Dolgs.ToList(); break;
                        case Tables.Rols: dg.ItemsSource = bd.Rols.ToList(); break;
                        case Tables.Dishes: dg.ItemsSource = bd.Dishes.ToList(); break;
                        case Tables.OrderStrings: dg.ItemsSource = bd.Orderstrings.Include(item => item.Dish).Include(item2 => item2.Order).ToList(); break;
                        case Tables.Orders:
                            dg.ItemsSource = bd.Orders.Include(item => item.Orderstatus).Include(item => item.Personal).ToList();
                            bd.Orderstrings.Include(item => item.Dish).Include(item2 => item2.Order).ToList();
                            //foreach (var a in bd.Orders.Where(it => it.Id == 1).ToList().FirstOrDefault().Orderstrings)
                            //{
                            //    Debug.WriteLine(a.Dish);
                            //}
                            break;
                        case Tables.Clients: dg.ItemsSource = bd.Clients.ToList(); break;
                        case Tables.Checks: dg.ItemsSource = bd.Checks.Include(item => item.Order).Include(item => item.Client).ToList(); break;
                        case Tables.OrderStatuses: dg.ItemsSource = bd.Orderstatuses.ToList(); break;
                    }
                    currentTable = value;
                }
            }
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
            cb1.SelectedIndex = 0;
        }

        private void cb1_SelectionChanged(object sender, SelectionChangedEventArgs e) => CurrentTable = (Tables)((sender as ComboBox).SelectedItem as ListObject).number;

        private void dg_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            PropertyDescriptor descriptor = e.PropertyDescriptor as PropertyDescriptor;
            ColumnNameAttribute nameAttribute = descriptor.Attributes[typeof(ColumnNameAttribute)] as ColumnNameAttribute;
            VisibleAttribute visibleAttribute = descriptor.Attributes[typeof(VisibleAttribute)] as VisibleAttribute;
            if (nameAttribute != null) e.Column.Header = nameAttribute.Name;
            if(visibleAttribute != null) if (visibleAttribute.visible == false) e.Column.Visibility = Visibility.Collapsed;
        }

        public void ShowErrorBox(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Description_Click(object sender, RoutedEventArgs e)
        {
            if (dg.SelectedItem == null)
            {
                ShowErrorBox("Не выбран элемент");
                return;
            }
            if (dg.SelectedItems.Count > 1)
            {
                ShowErrorBox("Выберите 1 элемент");
                return;
            }
        }

        private void PersonalSearchButton_Click(object sender, RoutedEventArgs e)
        {
            using (AutoCaffeeBDContext bd = new AutoCaffeeBDContext(ConfigurationHelper.dbContextOptions))
            {
                List<Personal> personals = bd.Personals.Include(item => item.Dolg).Include(item => item.Rol).ToList();

                dg.ItemsSource = personals.Where(item 
                    => 
                item.Id.ToString().Contains(ptb1.Text) &&
                item.Firstname.ToLower().Contains(ptb2.Text.ToLower()) &&
                item.Secondname.ToLower().Contains(ptb3.Text.ToLower()) &&
                item.Patronymic.ToLower().Contains(ptb4.Text.ToLower()) &&
                item.Phonenumber.ToLower().Contains(ptb5.Text.ToLower()) &&
                item.Dolg.ToString().ToLower().Contains(ptb6.Text.ToLower()) &&
                item.Rol.ToString().ToLower().Contains(ptb7.Text.ToLower()) 
                );
            }
        }

        private void PersonalResetButton_Click(object sender, RoutedEventArgs e)
        {
            ptb1.Text = ptb2.Text = ptb3.Text = ptb4.Text = ptb5.Text = ptb6.Text = ptb7.Text = "";
            CurrentTable = currentTable;
        }

        private void TextTargetUpdated(object sender, TextChangedEventArgs e)
        {
            if(IsEmpty(ptb1) && IsEmpty(ptb2) && IsEmpty(ptb3) && IsEmpty(ptb4) && IsEmpty(ptb5) && IsEmpty(ptb6) && IsEmpty(ptb7)) CurrentTable = currentTable;
            bool IsEmpty(TextBox tb) => string.IsNullOrEmpty(tb.Text);

        }

    }
}
