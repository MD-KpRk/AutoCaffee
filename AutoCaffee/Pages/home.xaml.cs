using System.Windows.Controls;
using System.Windows.Media;

namespace AutoCaffee.Pages
{
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
            RoleCheck(userRole.CanFood, lb5);
        }

        void RoleCheck(bool boolean, TextBlock tb)
        {
            if (boolean) Apply(tb); else Cancel(tb);
        }

        void Apply(TextBlock lb)
        {
            lb.Text = "\xE73E"; lb.Foreground = new SolidColorBrush(Colors.Green);
        }

        void Cancel(TextBlock lb)
        {
            lb.Text = "\xE711"; lb.Foreground = new SolidColorBrush(Colors.Red);
        }
    }
}
