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
    /// Interaction logic for DirectorSupplierView.xaml
    /// </summary>
    public partial class DirectorSupplierView : UserControl
    {

        DG dG;
        public DirectorSupplierView()
        {
            InitializeComponent();

            dG = new DG(dgSupplier, snack, snackMessage);

            this.Loaded += delegate
            {
                dG.Loader("SELECT * FROM dbo.[Supplier]");
            };

            snackMessage.ActionClick += delegate { dG.CloseSnackbar(); };
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            dG.Loader("SELECT * FROM dbo.[Supplier] " +
                $"WHERE [Name] LIKE '%{tbSearch.Text}%'");
        }
    }
}
