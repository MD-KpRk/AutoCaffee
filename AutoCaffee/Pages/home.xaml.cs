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

namespace AutoCaffee.Pages
{
    /// <summary>
    /// Логика взаимодействия для home.xaml
    /// </summary>
    public partial class home : Page
    {
        public home(Personal User)
        {
            InitializeComponent();
            UserLabel.Text =  UserLabel.Text+" " + User.Firstname + " " + User.Secondname;

            var optionsBuilder = new DbContextOptionsBuilder<AutoCaffeeBDContext>();
            var options = optionsBuilder.UseSqlServer(ConfigurationHelper.getInstance().conString).Options;

            try
            {
                UserLabel.Text = UserLabel.Text + "\nВаша должность: " + User.Dolg?.Title;
            }
            catch (Exception)
            {
                UserLabel.Text = UserLabel.Text + "\nВаша должность: Ошибка загрузки";
            }
        }

        private void RichTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
        }
    }
}
