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
    /// Interaction logic for DirectorProductView.xaml
    /// </summary>
    public partial class DirectorProductView : UserControl
    {
        DG dG;
        public DirectorProductView()
        {
            InitializeComponent();

            dG = new DG(dgProduct, snack, snackMessage);

            this.Loaded += delegate
            {
                dG.Loader("SELECT * FROM dbo.[ProductStrength]");
            };

            snackMessage.ActionClick += delegate { dG.CloseSnackbar(); };
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            dG.Loader("SELECT * FROM dbo.[ProductStrength] " +
                $"WHERE [Name] LIKE '%{tbSearch.Text}%'");
        }
    }
}
