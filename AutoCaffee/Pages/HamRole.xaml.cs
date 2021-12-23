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

        Rol SelectedItem { get; set; }

        void ContextMenuShow()
        {
            RoleContextMenu.Width = 200;
        }

        void ContextMenuHide()
        {

        }

        public HamRole()
        {
            InitializeComponent();

            using(AutoCaffeeBDContext bd = new AutoCaffeeBDContext(ConfigurationHelper.dbContextOptions))
            {
                List<Rol> rols = bd.Rols.ToList();

                //foreach (var item in rols)
                //    dg.Items.Add(item);

                dg.ItemsSource = rols;
            }
        }


        private void dg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Rol rol = dg.SelectedItem as Rol;


            //Не забыть добавить проверку на существование выбранного предмета в бд

        }
    }
}
