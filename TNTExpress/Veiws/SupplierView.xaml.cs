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
using TNTExpress.Classes.DataBaseWork;
using TNTExpress.Classes.Extra;
using TNTExpress.Classes.SnackBarMessage;

namespace TNTExpress.Veiws
{
    /// <summary>
    /// Interaction logic for SupplierView.xaml
    /// </summary>
    public partial class SupplierView : UserControl
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

        public SupplierView()
        {
            InitializeComponent();

            dG = new DG(dgSupplier, snack, snackMessage);

            sB = new SB(snack, snackMessage);

            dataBaseQuery = new DataBaseQuery(snack, snackMessage);

            this.Loaded += delegate 
            {
                dG.Loader("SELECT * FROM dbo.[Supplier]");
            };

            snackMessage.ActionClick += delegate { dG.CloseSnackbar(); };
        }

        private void dgSupplier_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgSupplier.SelectedItem != null)
                id = dG.FirstColumn;
            try
            {
                connection.Open();
                cmd = new SqlCommand($"SELECT * FROM dbo.[Supplier]" +
                    $"WHERE[Id] = {id}", connection);
                reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    tbEditName.Text = reader[1].ToString();
                    tbEditAddress.Text = reader[2].ToString();
                    tbEditPhoneNumber.Text = reader[3].ToString();
                    tbEditEmail.Text = reader[4].ToString();
                    tbEditFieldOfActivity.Text = reader[5].ToString();
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
            dG.Loader("SELECT * FROM dbo.[Supplier] " +
                $"WHERE [Name] LIKE '%{tbSearch.Text}%'");
        }

        private void btnAddSupplier_Click(object sender, RoutedEventArgs e)
        {
            bool resultEmail = ExtraClass.IsValidString(tbEmail.Text, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            bool resultPhoneNumber = ExtraClass.IsValidString(tbPhoneNumber.Text, @"^\+\d{1}\(\d{3}\)\d{3}-\d{2}-\d{2}$");

            if (string.IsNullOrEmpty(tbName.Text))
            {
                sB.Info("Введите наименование");
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
                sB.Info("Введите почту");
            }
            else if (string.IsNullOrEmpty(tbFieldOfActivity.Text))
            {
                sB.Info("Введите область деятельности");
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
                dataBaseQuery.SqlQuery("INSERT INTO dbo.[Supplier] " +
                    $"VALUES ('{tbName.Text}', '{tbAddress.Text}', " +
                    $"'{tbPhoneNumber.Text}', '{tbEmail.Text}', " +
                    $"'{tbFieldOfActivity.Text}')",
                "Данные успешно добавлены", "Ошибка");
                dG.Loader("SELECT * FROM dbo.[Supplier]");
            }
        }

        private void btnEditSupplier_Click(object sender, RoutedEventArgs e)
        {
            bool resultEmail = ExtraClass.IsValidString(tbEditEmail.Text, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            bool resultPhoneNumber = ExtraClass.IsValidString(tbEditPhoneNumber.Text, @"^\+\d{1}\(\d{3}\)\d{3}-\d{2}-\d{2}$");

            if (dgSupplier.SelectedItem is null)
            {
                sB.Info("Выберете строку для редактирования");
            }
            else if (string.IsNullOrEmpty(tbEditName.Text))
            {
                sB.Info("Введите наименование");
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
                sB.Info("Введите почту");
            }
            else if (string.IsNullOrEmpty(tbEditFieldOfActivity.Text))
            {
                sB.Info("Введите область деятельности");
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
                dataBaseQuery.SqlQuery("UPDATE dbo.[Supplier] " +
                    $"SET [Name] = '{tbEditName.Text}', " +
                    $"[Address] = '{tbEditAddress.Text}'," +
                    $"[PhoneNumber] = '{tbEditPhoneNumber.Text}'," +
                    $"[Email] = '{tbEditEmail.Text}'," +
                    $"[FieldOfActivity] = '{tbEditFieldOfActivity.Text}'" +
                    $"WHERE [Id] = '{id}'",
                "Данные успешно изменены", "Ошибка");
                dG.Loader("SELECT * FROM dbo.[Supplier]");
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            dataBaseQuery.SqlQuery("DELETE FROM dbo.[Supplier] " +
                $"WHERE [Id] = {id}", "Данные успешно удалены", "Ошибка");
            dG.Loader("SELECT * FROM dbo.Supplier");
        }
    }
}