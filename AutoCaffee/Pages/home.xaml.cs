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

            Rol userRole = User.Rol;

            UserLabel.Text =  UserLabel.Text+" " + User.Firstname + " " + User.Secondname + "\nВаша должность: " + User.Dolg?.Title;
            UserRole.Text = UserRole.Text + " " + userRole.Title;


            RoleCheck(userRole.CanBD, lb1);
            RoleCheck(userRole.CanRole, lb2);
            RoleCheck(userRole.CanPersonal, lb3);
            RoleCheck(userRole.CanOrder, lb4);

        }

        void RoleCheck(bool boolean, TextBlock tb)
        {
            if (boolean) Apply(tb);
            else Cancel(tb);
        }

        void Apply(TextBlock lb)
        {
            lb.Text = "\xE73E";
            lb.Foreground = new SolidColorBrush(Colors.Green);
        }

        void Cancel(TextBlock lb)
        {
            lb.Text = "\xE711";
            lb.Foreground = new SolidColorBrush(Colors.Red);
        }


    }
}
