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
using TNTExpress.Classes.DataBaseWork;
using TNTExpress.Classes.ListWork;
using TNTExpress.Classes.SnackBarMessage;

namespace TNTExpress.Windows.Manager
{
    /// <summary>
    /// Логика взаимодействия для WinManager.xaml
    /// </summary>
    public partial class WinManager : Window
    {
        readonly SB sB;
        readonly DG dG;
        readonly DataBaseQuery dataBaseQuery;
        readonly MyListBox myListBox;

        public WinManager()
        {
            InitializeComponent();

            dG = new DG(dgUser, snack, snackMessage);

            dataBaseQuery = new DataBaseQuery(snack, snackMessage);

            myListBox = new MyListBox(lbRole, snack, snackMessage);

            sB = new SB(snack, snackMessage);

            dG.Loader("SELECT * FROM dbo.[UserRole]");

            snackMessage.ActionClick += delegate { dG.CloseSnackbar(); };

            myListBox.Loader("Role", "NameRole");
        }

        private void btnAddRole_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbAddRole.Text))
            {
                sB.Info("Введите роль");
            }
            else
            {
                dataBaseQuery.InsertData("Role", new string[] { "NameRole" }, 
                    new string[] { tbAddRole.Text }, "Данная роль уже есть");
                tbAddRole.Clear();
                myListBox.Loader("Role", "NameRole");
            }
        }
    }
}