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
                if (hamPanelActive == false)
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

            var options = new DbContextOptionsBuilder<AutoCaffeeBDContext>().UseSqlServer(ConfigurationHelper.getInstance().conString).Options;

            using (AutoCaffeeBDContext db = new AutoCaffeeBDContext(options))
            {
                currentUser = db.Personals.Include(u => u.Dolg).Where(item => item.Id == user.Id).First();
            }


            MainFrame.Navigate(new Pages.home(currentUser));
        }

        private void DoubleAnimation_Completed(object sender, EventArgs e)
        {
            (sender as Button).FontSize = 20;
        }

        //////////////////////////////// Гамбургер панель
        private void HamButton_Click(object sender, RoutedEventArgs e)
        {
            HamPanelActive = !HamPanelActive;
        }

        private void blackout_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HamPanelActive = false;
        }

        private void HamHome_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Pages.home(currentUser));
        }

        ////////////////////////////////
    }
}
