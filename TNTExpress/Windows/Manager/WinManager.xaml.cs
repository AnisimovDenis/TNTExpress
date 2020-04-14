using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using TNTExpress.Classes.ComboBoxWork;
using TNTExpress.Classes.DataBaseWork;
using TNTExpress.Classes.Extra;
using TNTExpress.Classes.ListWork;
using TNTExpress.Classes.SnackBarMessage;
using TNTExpress.Veiws;
using TNTExpress.Windows.Autotification;

namespace TNTExpress.Windows.Manager
{
    /// <summary>
    /// Логика взаимодействия для WinManager.xaml
    /// </summary>
    public partial class WinManager : Window
    {
        

        public WinManager()
        {
            InitializeComponent();

            tbDragger.MouseDown += delegate { this.DragMove(); };

            btnBack.Click += delegate { ExtraClass.OpenWinAutorization(this); };

            btnHidde.Click += delegate { ExtraClass.MinimizedWindow(this); };

            btnClose.Click += delegate { ExtraClass.Shutdown(); };


            
        }

        private void btnUser_Click(object sender, RoutedEventArgs e)
        {
            userView.Visibility = Visibility.Visible;
            employeeView.Visibility = Visibility.Hidden;
            clientView.Visibility = Visibility.Hidden;
        }

        private void btnEmployee_Click(object sender, RoutedEventArgs e)
        {
            employeeView.Visibility = Visibility.Visible;
            userView.Visibility = Visibility.Hidden;
            clientView.Visibility = Visibility.Hidden;
            recipientView.Visibility = Visibility.Hidden;
        }

        private void Client_Click(object sender, RoutedEventArgs e)
        {
            clientView.Visibility = Visibility.Visible;
            userView.Visibility = Visibility.Hidden;
            employeeView.Visibility = Visibility.Hidden;
            recipientView.Visibility = Visibility.Hidden;
        }

        private void btnRecipient_Click(object sender, RoutedEventArgs e)
        {
            recipientView.Visibility = Visibility.Visible;
            userView.Visibility = Visibility.Hidden;
            employeeView.Visibility = Visibility.Hidden;
            clientView.Visibility = Visibility.Hidden;
        }
    }
}