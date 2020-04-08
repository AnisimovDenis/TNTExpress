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
        readonly CB comboBoxAddRole;
        readonly CB comboBoxEditRole;
        readonly DG dG;
        readonly DataBaseQuery dataBaseQuery;
        readonly MyListBox myListBox;

        public WinManager()
        {
            InitializeComponent();

            dG = new DG(dgUser, snack, snackMessage);

            dataBaseQuery = new DataBaseQuery(snack, snackMessage);

            myListBox = new MyListBox(lbRole, snack, snackMessage);

            comboBoxAddRole = new CB(cbRole, snack, snackMessage);

            comboBoxEditRole = new CB(cbEditRole, snack, snackMessage);

            sB = new SB(snack, snackMessage);

            dG.Loader("SELECT * FROM dbo.[UserRole]");

            snackMessage.ActionClick += delegate { dG.CloseSnackbar(); };

            myListBox.Loader("Role", "NameRole");

            comboBoxAddRole.Loader("Role", "NameRole");

            comboBoxEditRole.Loader("Role", "NameRole");
        }

        private void btnAddRole_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbAddRole.Text))
            {
                sB.Info("Введите роль");
            }
            else
            {
                dataBaseQuery.InsertData("INSERT INTO dbo.[Role] (NameRole) " +
                    $"VALUES ('{tbAddRole.Text}')", "Данная роль уже существует");
                tbAddRole.Clear();
                myListBox.Loader("Role", "NameRole");
                comboBoxAddRole.Loader("Role", "NameRole");
                comboBoxEditRole.Loader("Role", "NameRole");
            }
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbLogin.Text))
            {
                sB.Info("Введите логин");
            }
            else if (string.IsNullOrEmpty(tbPassword.Text))
            {
                sB.Info("Введите пароль");
            }
            else
            {
                dataBaseQuery.InsertData("INSERT INTO dbo.[User] (Login, Password, IdRole) " +
                $"VALUES ('{tbLogin.Text}', '{tbPassword.Text}', (SELECT Id FROM dbo.[Role] " +
                $"WHERE NameRole = '{cbRole.Text}'))", "Данный пользователь уже есть");
                tbLogin.Clear();
                tbPassword.Clear();
                cbRole.SelectedItem = null;
                dG.Loader("SELECT * FROM dbo.[UserRole]");
            }
        }

        private void btnEditUser_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(cbEditRole.Text);
        }

        private void dgUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnEditUser.IsEnabled = true;
        }
    }
}