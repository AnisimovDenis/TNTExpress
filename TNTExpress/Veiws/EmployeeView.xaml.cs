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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TNTExpress.Classes;
using TNTExpress.Classes.ComboBoxWork;
using TNTExpress.Classes.DataBaseWork;
using TNTExpress.Classes.Extra;
using TNTExpress.Classes.SnackBarMessage;

namespace TNTExpress.Veiws
{
    /// <summary>
    /// Interaction logic for EmployeeView.xaml
    /// </summary>
    public partial class EmployeeView : UserControl
    {
        readonly SqlConnection connection =
            new SqlConnection(@"Data Source=DENIS-PC;
                                Initial Catalog=TNTExpress;
                                Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader reader;
        readonly DG dG;
        readonly SB sB;
        readonly CB comboBoxAddEmployee;
        readonly CB comboBoxEditEmployee;
        readonly DataBaseQuery dataBaseQuery;
        string id;

        public EmployeeView()
        {
            InitializeComponent();

            dG = new DG(dgEmployee, snack, snackMessage);

            sB = new SB(snack, snackMessage);

            dataBaseQuery = new DataBaseQuery(snack, snackMessage);

            comboBoxAddEmployee = new CB(cbLogin, snack, snackMessage);

            comboBoxEditEmployee = new CB(cbEditLogin, snack, snackMessage);
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
                dG.Loader("SELECT * FROM dbo.[EmployeeUser] " +
                    $"WHERE [Employee] LIKE '%{tbSearch.Text}%'");
        }

        private void btnAddEmployee_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(cbLogin.Text))
            {
                sB.Info("Введите логин");
            }
            else if (string.IsNullOrEmpty(tbFirstName.Text))
            {
                sB.Info("Введите имя");
            }
            else if (string.IsNullOrEmpty(tbLastName.Text))
            {
                sB.Info("Введите фамилию");
            }
            else if (string.IsNullOrEmpty(tbPhoneNumber.Text))
            {
                sB.Info("Введите номер телефона");
            }
            else if (string.IsNullOrEmpty(tbEmail.Text))
            {
                sB.Info("Введите email");
            }
            else
            {
                dataBaseQuery.SqlQuery("INSERT INTO dbo.[Employee] " +
                    $"VALUES ((SELECT Id FROM dbo.[User] WHERE [Login] = '{cbLogin.Text}'), '{tbFirstName.Text}', '{tbLastName.Text}'," +
                    $"'{tbPhoneNumber.Text}', '{tbEmail.Text}')",
                    "Данные успешно добавлены", "Пользователь с данным логином уже есть");
                cbLogin.Text = null;
                tbFirstName.Clear();
                tbLastName.Clear();
                tbEmail.Clear();
                tbPhoneNumber.Clear();
                dG.Loader("SELECT * FROM dbo.[EmployeeUser]");
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            dataBaseQuery.SqlQuery("DELETE FROM dbo.[Employee] " +
                $"WHERE [Id] = {id}", "Данные успешно удалены", "Ошибка");
            dG.Loader("SELECT * FROM dbo.[EmployeeUser]");
        }

        private void dgEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgEmployee.SelectedItem != null)
                id = dG.FirstColumn;
            try
            {
                connection.Open();
                cmd = new SqlCommand($"SELECT (SELECT [Login] FROM dbo.[User] " +
                    $"WHERE IdUser = Id), " +
                    $"FirstName, LastName, PhoneNumber, Email FROM dbo.[Employee]" +
                    $"WHERE [Id] = {id}", connection);
                reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    cbEditLogin.Text = reader[0].ToString();
                    tbEditFirstName.Text = reader[1].ToString();
                    tbEditLastName.Text = reader[2].ToString();
                    tbEditPhoneNumber.Text = reader[3].ToString();
                    tbEditEmail.Text = reader[4].ToString();
                }
            }
            catch (SqlException sqlExc)
            {
                sB.Info(sqlExc.Message);
            }
            finally
            {
                connection.Close();
            }        
        }

        private void btnEditEmployee_Click(object sender, RoutedEventArgs e)
        {
            bool resultEmail = ExtraClass.IsValidString(tbEditEmail.Text, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            bool resultPhoneNumber = ExtraClass.IsValidString(tbEditPhoneNumber.Text, @"^\+\d{1}\(\d{3}\)\d{3}-\d{2}-\d{2}$");

            if (dgEmployee.SelectedItem is null)
            {
                sB.Info("Выберете строку для редактирования");
            }
            else if (string.IsNullOrEmpty(cbEditLogin.Text))
            {
                sB.Info("Введите логин");
            }
            else if (string.IsNullOrEmpty(tbEditFirstName.Text))
            {
                sB.Info("Введите имя");
            }
            else if (string.IsNullOrEmpty(tbEditLastName.Text))
            {
                sB.Info("Введите фамилию");
            }
            else if (string.IsNullOrEmpty(tbEditPhoneNumber.Text))
            {
                sB.Info("Введите номер телефона");
            }
            else if (string.IsNullOrEmpty(tbEditEmail.Text))
            {
                sB.Info("Введите email");
            }
            else if (resultEmail == false)
            {
                sB.Info("Email введен не корректно");
            }
            else if (resultPhoneNumber == false)
            {
                sB.Info("Телефон введен не корректно");
            }
            else
            {
                dataBaseQuery.SqlQuery("UPDATE dbo.[Employee] " +
                "SET [IdUser] = (SELECT Id FROM dbo.[User] " +
                $"WHERE [Login] = '{cbEditLogin.Text}')," +
                $"[FirstName] = '{tbEditFirstName.Text}'," +
                $"[LastName] = '{tbEditLastName.Text}'," +
                $"[PhoneNumber] = '{tbEditPhoneNumber.Text}'," +
                $"Email = '{tbEditEmail.Text}' " +
                $"WHERE Id = {id}", "Данные успешно обновлены", "Ошибка");
                dG.Loader("SELECT * FROM dbo.[EmployeeUser]");

                tbEditEmail.Clear();
                tbEditPhoneNumber.Clear();
                tbEditFirstName.Clear();
                tbEditLastName.Clear();
                cbEditLogin.Text = null;
            }
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            dG.Loader("SELECT * FROM dbo.[EmployeeUser]");

            snackMessage.ActionClick += delegate { dG.CloseSnackbar(); };

            comboBoxAddEmployee.Loader("User", "Login");

            comboBoxEditEmployee.Loader("User", "Login");
        }

        private void btnExcel_Click(object sender, RoutedEventArgs e)
        {
            ExcelClass.ConvertToExcel(dgEmployee);
        }
    }
}
