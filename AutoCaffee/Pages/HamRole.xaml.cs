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
    /// Логика взаимодействия для HamRole.xaml
    /// </summary>
    public partial class HamRole : Page
    {
        private int tbc;
        public int TBCount
        {
            get => tbc;
            set
            {
                tbcount.Text = "Сотрудников с этой ролью: " + value;
                tbc = value;
                if (value == 0) dg2.Visibility = Visibility.Collapsed;
                else dg2.Visibility = Visibility.Visible;
            }
        }

        void ContextMenuShow(Rol role)
        {
            RoleContextMenu.Width = new GridLength(200);
            //Не забыть добавить проверку на существование выбранного предмета в бд

            TBCount = role.Personals.Count;
            dg2.ItemsSource = role.Personals;

        }

        void ContextMenuHide()
        {
            RoleContextMenu.Width = new GridLength(0);
        }

        public HamRole()
        {
            InitializeComponent();

            using(AutoCaffeeBDContext bd = new AutoCaffeeBDContext(ConfigurationHelper.dbContextOptions))
            {
                bd.Personals.Include(item => item.Dolg).Include(item => item.Rol).ToList();
                List<Rol> rols = bd.Rols.ToList();

                //foreach (var item in rols)
                //    dg.Items.Add(item);

                dg.ItemsSource = rols;
            }
        }


        private void dg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ContextMenuShow(dg.SelectedItem as Rol);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ContextMenuHide();
        }
    }
}
