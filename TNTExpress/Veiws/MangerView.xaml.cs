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
using TNTExpress.Classes.ComboBoxWork;
using TNTExpress.Classes.DataBaseWork;
using TNTExpress.Classes.ListWork;
using TNTExpress.Classes.SnackBarMessage;

namespace TNTExpress.Veiws
{
    /// <summary>
    /// Interaction logic for MangerView.xaml
    /// </summary>
    public partial class MangerView : UserControl
    {
        readonly SB sB;
        readonly CB comboBoxAddRole;
        readonly CB comboBoxEditRole;
        readonly DG dG;
        readonly DataBaseQuery dataBaseQuery;
        readonly MyListBox myListBox;

        public MangerView()
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
            else if (string.IsNullOrEmpty(cbRole.Text))
            {
                sB.Info("Введите роль");
            }
            else
            {
                dataBaseQuery.SqlQuery("INSERT INTO dbo.[User] (Login, Password, IdRole) " +
                $"VALUES ('{tbLogin.Text}', '{tbPassword.Text}', (SELECT Id FROM dbo.[Role] " +
                $"WHERE NameRole = '{cbRole.Text}'))", "Данные успешно добавлены",
                "Пользователь с таким логином уже есть");
                tbLogin.Clear();
                tbPassword.Clear();
                cbRole.SelectedItem = null;
                dG.Loader("SELECT * FROM dbo.[UserRole]");
            }
        }

        private void btnEditUser_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbEditLogin.Text))
            {
                sB.Info("Введите логин");
            }
            else if (string.IsNullOrEmpty(tbEditPassword.Text))
            {
                sB.Info("Введите пароль");
            }
            else if (string.IsNullOrEmpty(cbEditRole.Text))
            {
                sB.Info("Введите роль");
            }
            else
            {
                dataBaseQuery.SqlQuery("UPDATE dbo.[User] " +
                $"SET [Login] = '{tbEditLogin.Text}'," +
                $"[Password] = '{tbEditPassword.Text}'," +
                $"[IdRole] = (SELECT Id FROM dbo.[Role] " +
                $"WHERE NameRole = '{cbEditRole.Text}')" +
                $"WHERE [Login] = '{dG.FirstColumn}'", "Данные успешно изменены",
                "Пользователь с таким логином уже есть");
                dG.Loader("SELECT * FROM dbo.[UserRole]");
            }
        }

        private void dgUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnEditUser.IsEnabled = true;
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            dG.Loader("SELECT * FROM dbo.[UserRole] " +
                $"WHERE [Login] LIKE '{tbSearch.Text}%'");
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            dataBaseQuery.SqlQuery("DELETE FROM dbo.[User] " +
                $"WHERE [Login] = '{dG.FirstColumn}'", "Данные успешно удалены", "Ошибка");
            dG.Loader("SELECT * FROM dbo.[UserRole]");
        }
    }
}