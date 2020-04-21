using System.Windows.Controls;
using TNTExpress.Classes;

namespace TNTExpress.Veiws.Director
{
    /// <summary>
    /// Interaction logic for DirectorRecipientView.xaml
    /// </summary>
    public partial class DirectorRecipientView : UserControl
    {
        DG dG;
        public DirectorRecipientView()
        {
            InitializeComponent();

            dG = new DG(dgRecipient, snack, snackMessage);

            this.Loaded += delegate {
                dG.Loader("SELECT Id, FirstName + N' ' + LastName as Recipient," +
                            " Address, PhoneNumber, Email FROM dbo.Recipient");
            };

            snackMessage.ActionClick += delegate { dG.CloseSnackbar(); };
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            dG.Loader("SELECT FirstName + N' ' + LastName as Recipient, " +
                "Address, PhoneNumber, Email FROM dbo.Recipient " +
                $"WHERE[FirstName] LIKE '%{tbSearch.Text}%' or" +
                $"[LastName] LIKE '%{tbSearch.Text}%'");
        }
    }
}
