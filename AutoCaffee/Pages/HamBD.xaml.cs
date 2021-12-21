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
            DisableAllSearchElements();
            switch(tableForSearch)
            {
                case Tables.Personal: ShowSearchPanel(SearchPersonal); break;
                case Tables.Rols: ShowSearchPanel(SearchRole); break;
                case Tables.Dishes: ShowSearchPanel(SearchDish); break;
                case Tables.OrderStrings: ShowSearchPanel(SearchStrings); break;
            }


            void DisableAllSearchElements()
            {
                Separat.Visibility = Visibility.Collapsed;
                SearchTextBlock.Visibility = Visibility.Collapsed;
                SearchPersonal.Visibility = Visibility.Collapsed;
                SearchRole.Visibility = Visibility.Collapsed;
                SearchDish.Visibility = Visibility.Collapsed;
                SearchStrings.Visibility = Visibility.Collapsed;
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

        #region Поиск персонала
        private void PersonalSearchButton_Click(object sender, RoutedEventArgs e)
        {
            using (AutoCaffeeBDContext bd = new AutoCaffeeBDContext(ConfigurationHelper.dbContextOptions))
            {
                List<Personal> personals = bd.Personals.Include(item => item.Dolg).Include(item => item.Rol).ToList();
                dg.ItemsSource = personals.Where(item =>  item.Id.ToString().Contains(ptb1.Text) && 
                item.Firstname.ToLower().Contains(ptb2.Text.ToLower()) && item.Secondname.ToLower().Contains(ptb3.Text.ToLower()) && 
                item.Patronymic.ToLower().Contains(ptb4.Text.ToLower()) && item.Phonenumber.ToLower().Contains(ptb5.Text.ToLower()) &&  
                item.Dolg.ToString().ToLower().Contains(ptb6.Text.ToLower()) && item.Rol.ToString().ToLower().Contains(ptb7.Text.ToLower()) 
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
        #endregion

        #region Поиск ролей
        private void RoleSearchButton_Click(object sender, RoutedEventArgs e)
        {
            using (AutoCaffeeBDContext bd = new AutoCaffeeBDContext(ConfigurationHelper.dbContextOptions))
            {
                List<Rol> rols = bd.Rols.ToList();
                if (CBEnable.IsChecked == false) dg.ItemsSource = rols.Where(item=>item.Title.ToLower().Contains(rtb1.Text.ToLower()));
                else
                {
                    dg.ItemsSource = rols.Where(item=>item.Title.ToString().Contains(rtb1.Text.ToLower()) && rcb1.IsChecked == item.CanBD &&
                    rcb2.IsChecked == item.CanRole && rcb3.IsChecked == item.CanPersonal && rcb4.IsChecked == item.CanOrder && rcb5.IsChecked == item.CanFood);
                }
            }
        }
        private void RoleResetButton_Click(object sender, RoutedEventArgs e)
        {
            rcb1.IsChecked = rcb2.IsChecked = rcb3.IsChecked = rcb4.IsChecked = rcb5.IsChecked = false;
            rtb1.Text = "";
            CurrentTable = currentTable;
        }
        private void CBEnable_Checked(object sender, RoutedEventArgs e) => FlagSearch.IsEnabled = true;

        private void CBEnable_Unchecked(object sender, RoutedEventArgs e)
        {
            FlagSearch.IsEnabled = false;
            rcb1.IsChecked = rcb2.IsChecked = rcb3.IsChecked = rcb4.IsChecked = rcb5.IsChecked = false;
        }
        #endregion

        #region Поиск блюд
        private void DishResetButton_Click(object sender, RoutedEventArgs e)
        {
            DCBEnable.IsChecked = DCBEnable1.IsChecked = false;
            dtb1.Text = dtb2.Text = "";
            CurrentTable = currentTable;
        }
        private void DishSearchButton_Click(object sender, RoutedEventArgs e)
        {
            using (AutoCaffeeBDContext bd = new AutoCaffeeBDContext(ConfigurationHelper.dbContextOptions))
            {
                List<Dish> dishes = bd.Dishes.ToList();
                if (DCBEnable.IsChecked == false){
                    dg.ItemsSource = dishes.Where(item => item.Title.ToLower().Contains(dtb1.Text.ToLower()) &&
                    item.Price.ToString().ToLower().Contains(dtb2.Text.ToLower().Replace(',','.')));
                }
                else{
                dg.ItemsSource = dishes.Where(item => item.Title.ToLower().Contains(dtb1.Text.ToLower()) &&
                item.Price.ToString().ToLower().Contains(dtb2.Text.ToLower()) && item.Available == DCBEnable1.IsChecked);
                }
            }
        }
        private void DCBEnable_Checked(object sender, RoutedEventArgs e) => DishFlags.IsEnabled = true;
        private void DCBEnable_Unchecked(object sender, RoutedEventArgs e) { DCBEnable1.IsChecked = false; DishFlags.IsEnabled = false; }
        #endregion

        #region Поиск Строк заказов
        private void StringsResetButton_Click(object sender, RoutedEventArgs e)
        {
            stb1.Text = stb2.Text = stb3.Text = "";
            CurrentTable = currentTable;
        }
        private void StringsSearchButton_Click(object sender, RoutedEventArgs e)
        {
            using (AutoCaffeeBDContext bd = new AutoCaffeeBDContext(ConfigurationHelper.dbContextOptions))
            {
                List<Orderstring> orderstrings = bd.Orderstrings.Include(item => item.Dish).Include(item2 => item2.Order).ToList();

                try
                {
                    dg.ItemsSource = orderstrings.Where(item=>
                    (string.IsNullOrEmpty(stb1.Text) ? true : item.Count == Convert.ToInt32(stb1.Text)) &&
                    item.Dish.ToString().ToLower().Contains(stb2.Text.ToLower()) &&
                    (string.IsNullOrEmpty(stb3.Text) ? true : Convert.ToInt32(item.Order.ToString()) == Convert.ToInt32(stb3.Text))
                    );
                }
                catch(Exception)
                {
                    ShowErrorBox("В поля поиска введены некорректные данные!");
                }
            }
        }
        private void stb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(stb1.Text) && string.IsNullOrEmpty(stb2.Text) && string.IsNullOrEmpty(stb3.Text)) CurrentTable = currentTable;
        }




        #endregion

        private void OrdersResetButton_Click(object sender, RoutedEventArgs e)
        {
            otb1.Text = otb2.Text = otb3.Text = "";
            CurrentTable = currentTable;
        }

        private void OrdersSearchButton_Click(object sender, RoutedEventArgs e)
        {
            using (AutoCaffeeBDContext bd = new AutoCaffeeBDContext(ConfigurationHelper.dbContextOptions))
            {
                bd.Orderstrings.Include(item => item.Dish).Include(item2 => item2.Order).ToList();
                List<Order> orders = bd.Orders.Include(item => item.Orderstatus).Include(item => item.Personal).ToList();

                try
                {

                    dg.ItemsSource = orders.Where(item =>
                    (string.IsNullOrEmpty(otb1.Text) ? true : item.Id == Convert.ToInt32(otb1.Text)) &&
                    item.Personal.ToString().ToLower().Contains(otb2.Text.ToLower()) &&
                    item.Orderstatus.ToString().ToLower().Contains(otb3.Text.ToLower()) 
 
                    );
                }
                catch (Exception)
                {
                    ShowErrorBox("В поля поиска введены некорректные данные!");
                }
            }
        }

        private void otb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(otb1.Text) && string.IsNullOrEmpty(otb2.Text) && string.IsNullOrEmpty(otb3.Text))
                CurrentTable = currentTable;
        }
    }
}
