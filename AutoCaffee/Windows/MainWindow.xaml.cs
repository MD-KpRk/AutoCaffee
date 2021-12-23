using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

namespace AutoCaffee
{
    public partial class MainWindow : Window
    {
        Personal currentUser;
        private bool hamPanelActive;
        public bool HamPanelActive
        {
            get => hamPanelActive;
            set
            {
                if (value == true)
                {
                    MenuColumn.Width = new GridLength(190);
                    blackout.Visibility = Visibility.Visible;
                }
                else
                {
                    MenuColumn.Width = new GridLength(50);
                    blackout.Visibility = Visibility.Hidden;
                }
                hamPanelActive = value;
            }
        }

        public MainWindow(WindowState state,  Personal user)
        {
            WindowState = state;
            InitializeComponent();
            currentUser = user;
            MainFrame.Navigate(new Pages.HamHome(currentUser));
        }

        private void DoubleAnimation_Completed(object sender, EventArgs e)
        {
            (sender as Button).FontSize = 20;
        }

        #region Hamburger Panel

        private void HamButton_Click(object sender, RoutedEventArgs e) => HamPanelActive = !HamPanelActive;

        private void blackout_MouseDown(object sender, MouseButtonEventArgs e) => HamPanelActive = false;

        private void HamHome_Click(object sender, RoutedEventArgs e) // Главная
        {
            HamPanelActive = false;
            MainFrame.Navigate(new Pages.HamHome(currentUser));
        }

        private void HamBD_Click(object sender, RoutedEventArgs e) // База данных
        {
            HamPanelActive = false;
            MainFrame.Navigate(new Pages.HamBD());

        }



        #endregion

        private void HamRole_Click(object sender, RoutedEventArgs e) // Роли
        {
            HamPanelActive = false;
            MainFrame.Navigate(new Pages.HamRole());

        }

        private void HamPersonal_Click(object sender, RoutedEventArgs e) // Персонал
        {
            HamPanelActive = false;

        }

        private void HamOrder_Click(object sender, RoutedEventArgs e) // Заказы
        {
            HamPanelActive = false;

        }

        private void HamDish_Click(object sender, RoutedEventArgs e) // Блюда
        {
            HamPanelActive = false;

        }

        private void HamLogOut_Click(object sender, RoutedEventArgs e) // Выход
        {
            HamPanelActive = false;

            if (MessageBox.Show("Вы желаете вернуться к окну авторизации ?", "Выход из системы", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                new AuthWindow().Show();
                Close();
            }
        }
    }
}
