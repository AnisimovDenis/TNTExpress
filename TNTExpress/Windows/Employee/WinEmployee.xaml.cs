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
using TNTExpress.Classes.Extra;

namespace TNTExpress.Windows.Employee
{
    /// <summary>
    /// Interaction logic for WinEmployee.xaml
    /// </summary>
    public partial class WinEmployee : Window
    {
        public WinEmployee()
        {
            InitializeComponent();

            tbDragger.MouseDown += delegate { this.DragMove(); };

            btnReference.Click += delegate { ExtraClass.Reference(); };

            btnBack.Click += delegate { ExtraClass.OpenWinAutorization(this); };

            btnHidde.Click += delegate { ExtraClass.MinimizedWindow(this); };

            btnClose.Click += delegate { ExtraClass.Shutdown(); };
        }

        private void Client_Click(object sender, RoutedEventArgs e)
        {
            clientView.Visibility = Visibility.Visible;
            recipientView.Visibility = Visibility.Hidden;
            supplierView.Visibility = Visibility.Hidden;
            productView.Visibility = Visibility.Hidden;
            orderView.Visibility = Visibility.Hidden;
        }

        private void btnRecipient_Click(object sender, RoutedEventArgs e)
        {
            recipientView.Visibility = Visibility.Visible;
            clientView.Visibility = Visibility.Hidden;
            supplierView.Visibility = Visibility.Hidden;
            productView.Visibility = Visibility.Hidden;
            orderView.Visibility = Visibility.Hidden;
        }

        private void btnSupplier_Click(object sender, RoutedEventArgs e)
        {
            supplierView.Visibility = Visibility.Visible;
            recipientView.Visibility = Visibility.Hidden;
            clientView.Visibility = Visibility.Hidden;
            productView.Visibility = Visibility.Hidden;
            orderView.Visibility = Visibility.Hidden;
        }

        private void btnProduct_Click(object sender, RoutedEventArgs e)
        {
            productView.Visibility = Visibility.Visible;
            recipientView.Visibility = Visibility.Hidden;
            clientView.Visibility = Visibility.Hidden;
            supplierView.Visibility = Visibility.Hidden;
            orderView.Visibility = Visibility.Hidden;
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            orderView.Visibility = Visibility.Visible;
            productView.Visibility = Visibility.Hidden;
            recipientView.Visibility = Visibility.Hidden;
            clientView.Visibility = Visibility.Hidden;
            supplierView.Visibility = Visibility.Hidden;
        }
    }
}