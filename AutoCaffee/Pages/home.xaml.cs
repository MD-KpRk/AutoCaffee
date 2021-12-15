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
            UserLabel.Text =  UserLabel.Text+" " + User.Firstname + " " + User.Secondname + "\nВаша должность: " + User.Dolg?.Title;

            UserRole.Text = UserRole.Text + " " + User.Rol?.Title;

        }

        private void RichTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
        }
    }
}
