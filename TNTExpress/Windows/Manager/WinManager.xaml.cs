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
using System.Windows.Shapes;
using TNTExpress.Classes;
using TNTExpress.Classes.SnackBarMessage;

namespace TNTExpress.Windows.Manager
{
    /// <summary>
    /// Логика взаимодействия для WinManager.xaml
    /// </summary>
    public partial class WinManager : Window
    {
        DG dG;
        public WinManager()
        {
            InitializeComponent();

            dG = new DG(dgUser, snack, snackMessage);

            dG.Loader("SELECT * FROM dbo.[UserRole]");

            snackMessage.ActionClick += delegate { dG.CloseSnackbar(); };
        }

    }
}
