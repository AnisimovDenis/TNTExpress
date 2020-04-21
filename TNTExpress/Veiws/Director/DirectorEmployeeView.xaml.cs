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
    /// Interaction logic for DirectorEmployeeView.xaml
    /// </summary>
    public partial class DirectorEmployeeView : UserControl
    {

        readonly DG dG;
        public DirectorEmployeeView()
        {
            InitializeComponent();

            dG = new DG(dgEmployee, snack, snackMessage);

            snackMessage.ActionClick += delegate { dG.CloseSnackbar(); };
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            dG.Loader("SELECT * FROM dbo.[EmployeeUser] " +
                $"WHERE [Employee] LIKE '%{tbSearch.Text}%'");
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            dG.Loader("SELECT * FROM dbo.[EmployeeUser]");
        }
    }
}
