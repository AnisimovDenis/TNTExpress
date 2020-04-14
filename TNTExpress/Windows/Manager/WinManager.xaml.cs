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
        readonly SqlConnection connection =
            new SqlConnection(@"Data Source=DENIS-PC;
                                Initial Catalog=TNTExpress;
                                Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader reader;
        SB sB;
        DG dG;
        DataBaseQuery dataBaseQuery;
        string id;

        public WinManager()
        {
            InitializeComponent();

            tbDragger.MouseDown += delegate { this.DragMove(); };

            btnBack.Click += delegate { ExtraClass.OpenWinAutorization(this); };

            btnHidde.Click += delegate { ExtraClass.MinimizedWindow(this); };

            btnClose.Click += delegate { ExtraClass.Shutdown(); };


            dG = new DG(dgClient, snack, snackMessage);

            sB = new SB(snack, snackMessage);

            dataBaseQuery = new DataBaseQuery(snack, snackMessage);

            this.Loaded += delegate { dG.Loader("SELECT Id, FirstName + N' ' + LastName as Client," +
                " Address, PhoneNumber, Email FROM dbo.Client"); };

            snackMessage.ActionClick += delegate { dG.CloseSnackbar(); };
        }

        private void btnUser_Click(object sender, RoutedEventArgs e)
        {
            userView.Visibility = Visibility.Visible;
            employeeView.Visibility = Visibility.Hidden;
        }

        private void btnEmployee_Click(object sender, RoutedEventArgs e)
        {
            employeeView.Visibility = Visibility.Visible;
            userView.Visibility = Visibility.Hidden;
        }

        private void dgClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            id = dG.FirstColumn;
            try
            {
                connection.Open();
                cmd = new SqlCommand($"SELECT FirstName, LastName, PhoneNumber, Email, Address FROM dbo.[Client]" +
                    $"WHERE[Id] = {id}", connection);
                reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    tbEditFirstName.Text = reader[0].ToString();
                    tbEditLastName.Text = reader[1].ToString();
                    tbEditPhoneNumber.Text = reader[2].ToString();
                    tbEditEmail.Text = reader[3].ToString();
                    tbEditAddress.Text = reader[4].ToString();
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

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            dG.Loader("SELECT FirstName + N' ' + LastName as Client, " +
                "Address, PhoneNumber, Email FROM dbo.Client " +
                $"WHERE[FirstName] LIKE '%{tbSearch.Text}%' or" +
                $"[LastName] LIKE '%{tbSearch.Text}%'");
        }

        private void btnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            bool resultEmail = ExtraClass.IsValidString(tbEmail.Text, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            bool resultPhoneNumber = ExtraClass.IsValidString(tbPhoneNumber.Text, @"^\+\d{1}\(\d{3}\)\d{3}-\d{2}-\d{2}$");


            if (string.IsNullOrEmpty(tbFirstName.Text))
            {
                sB.Info("Введите имя");
            }
            else if (string.IsNullOrEmpty(tbLastName.Text))
            {
                sB.Info("Введите фамилию");
            }
            else if (string.IsNullOrEmpty(tbAddress.Text))
            {
                sB.Info("Введите адрес");
            }
            else if (string.IsNullOrEmpty(tbPhoneNumber.Text))
            {
                sB.Info("Введите номер телефона");
            }
            else if (string.IsNullOrEmpty(tbEmail.Text))
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
                dataBaseQuery.SqlQuery("INSERT INTO dbo.[Client] " +
                $"VALUES ('{tbFirstName.Text}', '{tbLastName.Text}', " +
                $"'{tbAddress.Text}', '{tbPhoneNumber.Text}', '{tbEmail.Text}')",
                "Данные успешно добавлены", "Ошибка");
                tbAddress.Clear();
                tbFirstName.Clear();
                tbLastName.Clear();
                tbEmail.Clear();
                tbPhoneNumber.Clear();
                dG.Loader("SELECT * FROM dbo.[EmployeeUser]");
            }

        }

        private void btnEditEmployee_Click(object sender, RoutedEventArgs e)
        {
            bool resultEmail = ExtraClass.IsValidString(tbEmail.Text, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            bool resultPhoneNumber = ExtraClass.IsValidString(tbPhoneNumber.Text, @"^\+\d{1}\(\d{3}\)\d{3}-\d{2}-\d{2}$");


            if (string.IsNullOrEmpty(tbEditFirstName.Text))
            {
                sB.Info("Введите имя");
            }
            else if (string.IsNullOrEmpty(tbEditLastName.Text))
            {
                sB.Info("Введите фамилию");
            }
            else if (string.IsNullOrEmpty(tbEditAddress.Text))
            {
                sB.Info("Введите адрес");
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
                dataBaseQuery.SqlQuery("UPDATE dbo.[Client] " +
                    $"SET [FirstName] = {tbEditFirstName.Text}, " +
                    $"[LastName] = '{tbEditLastName.Text}'," +
                    $"[Address] = '{tbEditAddress.Text}'," +
                    $"[Email] = '{tbEditEmail.Text}'," +
                    $"[PhoneNumber] = '{tbEditPhoneNumber.Text}', " +
                    $"WHERE [Email] = '{tbEditEmail.Text}'",
                "Данные успешно добавлены", "Ошибка");
                tbAddress.Clear();
                tbFirstName.Clear();
                tbLastName.Clear();
                tbEmail.Clear();
                tbPhoneNumber.Clear();
                dG.Loader("SELECT * FROM dbo.[Client]");
            }
        }
    }
}