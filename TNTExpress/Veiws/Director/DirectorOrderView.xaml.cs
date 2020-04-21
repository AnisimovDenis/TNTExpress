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
using TNTExpress.Classes;

namespace TNTExpress.Veiws.Director
{
    /// <summary>
    /// Interaction logic for DirectorOrderView.xaml
    /// </summary>
    public partial class DirectorOrderView : UserControl
    {
        DG dG;
        public DirectorOrderView()
        {
            InitializeComponent();

            dG = new DG(dgOrder, snack, snackMessage);
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            dG.Loader("SELECT * FROM dbo.[OrderView] " +
                $"WHERE [Article] LIKE '%{tbSearch.Text}%'");
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            dG.Loader("SELECT * FROM dbo.[OrderView]");
        }
    }
}
