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
    /// Interaction logic for DirectorClientView.xaml
    /// </summary>
    public partial class DirectorClientView : UserControl
    {
        DG dG;
        public DirectorClientView()
        {
            InitializeComponent();

            dG = new DG(dgClient, snack, snackMessage);

            this.Loaded += delegate
            {
                dG.Loader("SELECT Id, FirstName + N' ' + LastName as Client," +
                    " Address, PhoneNumber, Email FROM dbo.Client");
            };

            snackMessage.ActionClick += delegate { dG.CloseSnackbar(); };
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            dG.Loader("SELECT FirstName + N' ' + LastName as Client, " +
                "Address, PhoneNumber, Email FROM dbo.Client " +
                $"WHERE[FirstName] LIKE '%{tbSearch.Text}%' or" +
                $"[LastName] LIKE '%{tbSearch.Text}%'");
        }
    }
}
