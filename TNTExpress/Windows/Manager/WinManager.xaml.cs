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
        DG dG;
        SB sB;
        readonly DataBaseQuery dataBaseQuery;

        public WinManager()
        {
            InitializeComponent();

            dG = new DG(dgEmployee, snack, snackMessage);

            sB = new SB(snack, snackMessage);

            dataBaseQuery = new DataBaseQuery(snack, snackMessage);

            dG.Loader("SELECT * FROM dbo.[EmployeeUser]");

            tbDragger.MouseDown += delegate { this.DragMove(); };

            btnBack.Click += delegate { ExtraClass.OpenWinAutorization(this); };

            btnHidde.Click += delegate { ExtraClass.MinimizedWindow(this); };

            btnClose.Click += delegate { ExtraClass.Shutdown(); };
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            //if (string.IsNullOrEmpty(tbLogin.Text))
            //{
            //    sB.Info("Введите логин");
            //}
            //else if (string.IsNullOrEmpty(tbPassword.Text))
            //{
            //    sB.Info("Введите пароль");
            //}
            //else if (string.IsNullOrEmpty(cbRole.Text))
            //{
            //    sB.Info("Введите роль");
            //}
            //else
            //{
                
            //}
        }

        private void btnEditUser_Click(object sender, RoutedEventArgs e)
        {
            //if (string.IsNullOrEmpty(tbEditLogin.Text))
            //{
            //    sB.Info("Введите логин");
            //}
            //else if (string.IsNullOrEmpty(tbEditPassword.Text))
            //{
            //    sB.Info("Введите пароль");
            //}
            //else if (string.IsNullOrEmpty(cbEditRole.Text))
            //{
            //    sB.Info("Введите роль");
            //}
            //else
            //{
                
            //}
        }

        private void btnUser_Click(object sender, RoutedEventArgs e)
        {
            gEmployee.Visibility = Visibility.Hidden;
            userView.Visibility = Visibility.Visible;
        }
    }
}