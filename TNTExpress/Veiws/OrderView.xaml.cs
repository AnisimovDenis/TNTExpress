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
using TNTExpress.Classes.ListWork;
using TNTExpress.Classes.SnackBarMessage;

namespace TNTExpress.Veiws
{
    /// <summary>
    /// Interaction logic for OrderView.xaml
    /// </summary>
    public partial class OrderView : UserControl
    {
        SqlConnection connection =
               new SqlConnection(@"Data Source=DENIS-PC;
                                Initial Catalog=TNTExpress;
                                Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader reader;
        SB sB;
        DG dG;
        DataBaseQuery dataBaseQuery;
        MyListBox lb;
        CB comboBoxEmployee;
        CB comboBoxClient;
        CB comboBoxRecipient;
        CB comboBoxSupplier;
        CB comboBoxArticle;
        CB comboBoxSortingCenter;
        CB comboBoxNameOrderTiming;
        CB comboBoxEditEmployee;
        CB comboBoxEditClient;
        CB comboBoxEditRecipient;
        CB comboBoxEditSupplier;
        CB comboBoxEditArticle;
        CB comboBoxEditSortingCenter;
        CB comboBoxEditNameOrderTiming;

        string id;

        public OrderView()
        {
            InitializeComponent();

            dG = new DG(dgOrder, snack, snackMessage);

            sB = new SB(snack, snackMessage);

            lb = new MyListBox(lbSortingCenter, snack, snackMessage);

            comboBoxEmployee = new CB(cbEmployee, snack, snackMessage);

            comboBoxClient = new CB(cbClient, snack, snackMessage);

            comboBoxRecipient = new CB(cbRecipient, snack, snackMessage);

            comboBoxSupplier = new CB(cbSupplier, snack, snackMessage);

            comboBoxArticle = new CB(cbArticle, snack, snackMessage);

            comboBoxSortingCenter = new CB(cbSortingCenter, snack, snackMessage);

            comboBoxNameOrderTiming = new CB(cbNameOrderTiming, snack, snackMessage);

            comboBoxEditEmployee = new CB(cbEditEmployee, snack, snackMessage);
            
            comboBoxEditClient = new CB(cbEditClient, snack, snackMessage);

            comboBoxEditRecipient = new CB(cbEditRecipient, snack, snackMessage);

            comboBoxEditSupplier = new CB(cbEditSupplier, snack, snackMessage);

            comboBoxEditArticle = new CB(cbEditArticle, snack, snackMessage);

            comboBoxEditSortingCenter = new CB(cbEditSortingCenter, snack, snackMessage);

            comboBoxEditNameOrderTiming = new CB(cbEditNameOrderTiming, snack, snackMessage);

            dataBaseQuery = new DataBaseQuery(snack, snackMessage);

            snackMessage.ActionClick += delegate { dG.CloseSnackbar(); };
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            dG.Loader("SELECT * FROM dbo.[OrderView] " +
                $"WHERE [Article] LIKE '%{tbSearch.Text}%'");
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            dataBaseQuery.SqlQuery("DELETE FROM dbo.[Order] " +
                $"WHERE [Id] = {id}", "Данные успешно удалены", "Ошибка");
            dG.Loader("SELECT * FROM dbo.[OrderView]");

            cbEditEmployee.Text = null;
            cbEditClient.Text = null;
            cbEditRecipient.Text = null;
            cbEditSupplier.Text = null;
            cbEditArticle.Text = null;
            tbEditShippingAddress.Text = null;
            cbEditSortingCenter.Text = null;
            tbEditRecipientAddress.Clear();
            cbEditNameOrderTiming.Text = null;
            tbEditPrice.Clear();
        }

        private void btnGridEditOrder_Click(object sender, RoutedEventArgs e)
        {
            btnGridEditOrder.IsEnabled = false;
            btnGridAddOrder.IsEnabled = true;
            gEditOrder.Visibility = Visibility.Visible;
            gAddOrder.Visibility = Visibility.Hidden;
        }

        private void btnGridAddOrder_Click(object sender, RoutedEventArgs e)
        {
            btnGridEditOrder.IsEnabled = true;
            btnGridAddOrder.IsEnabled = false;
            gEditOrder.Visibility = Visibility.Hidden;
            gAddOrder.Visibility = Visibility.Visible;
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            dG.Loader("SELECT * FROM dbo.[OrderView]");
            lb.Loader("SortingCenter", "NameSortingCenter");

            comboBoxEmployee.Loader("Employee", "FirstName + ' ' + LastName");

            comboBoxClient.Loader("Client", "FirstName + ' ' + LastName");
            comboBoxRecipient.Loader("Recipient", "FirstName + ' ' + LastName");
            comboBoxSupplier.Loader("Supplier", "Name");
            comboBoxArticle.Loader("Product", "Article");
            comboBoxSortingCenter.Loader("SortingCenter", "NameSortingCenter");
            comboBoxNameOrderTiming.Loader("OrderTiming", "NameOrderTiming");

            comboBoxEditEmployee.Loader("Employee", "FirstName + ' ' + LastName");
            comboBoxEditClient.Loader("Client", "FirstName + ' ' + LastName");
            comboBoxEditRecipient.Loader("Recipient", "FirstName + ' ' + LastName");
            comboBoxEditSupplier.Loader("Supplier", "Name");
            comboBoxEditArticle.Loader("Product", "Article");
            comboBoxEditSortingCenter.Loader("SortingCenter", "NameSortingCenter");
            comboBoxEditNameOrderTiming.Loader("OrderTiming", "NameOrderTiming");
        }

        private void btnAddSortingCenter_Click(object sender, RoutedEventArgs e)
        {
            dataBaseQuery.SqlQuery("INSERT INTO dbo.[SortingCenter] " +
                $"VALUES('{tbSortingCenter.Text}')",
                "Данные успешно добавлены", "Такой сортировочный центр уже есть");
            lb.Loader("SortingCenter", "NameSortingCenter");
            comboBoxEditSortingCenter.Loader("SortingCenter", "NameSortingCenter");
            comboBoxSortingCenter.Loader("SortingCenter", "NameSortingCenter");
        }

        private void dgOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgOrder.SelectedItem != null)
                id = dG.FirstColumn;
            try
            {
                connection.Open();
                cmd = new SqlCommand($"SELECT * FROM dbo.[OrderView]" +
                    $"WHERE[Id] = {id}", connection);
                reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    cbEditEmployee.Text = reader[1].ToString();
                    cbEditClient.Text = reader[2].ToString();
                    cbEditRecipient.Text = reader[3].ToString();
                    cbEditSupplier.Text = reader[4].ToString();
                    cbEditArticle.Text = reader[5].ToString();
                    tbEditShippingAddress.Text = reader[6].ToString();
                    cbEditSortingCenter.Text = reader[7].ToString();
                    tbEditRecipientAddress.Text = reader[8].ToString();
                    cbEditNameOrderTiming.Text = reader[9].ToString();
                    tbEditPrice.Text = reader[10].ToString();
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

        private void btnAddOrder_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(cbEmployee.Text))
            {
                sB.Info("Введите сотрудника");
            }
            else if (string.IsNullOrEmpty(cbClient.Text))
            {
                sB.Info("Введите клиента");
            }
            else if (string.IsNullOrEmpty(cbRecipient.Text))
            {
                sB.Info("Введите адресата");
            }
            else if (string.IsNullOrEmpty(cbArticle.Text))
            {
                sB.Info("Введите артикул товара");
            }
            else if (string.IsNullOrEmpty(tbShippingAddress.Text))
            {
                sB.Info("Введите адрес отправки");
            }
            else if (string.IsNullOrEmpty(cbSortingCenter.Text))
            {
                sB.Info("Введите сортировочный центр");
            }
            else if (string.IsNullOrEmpty(tbRecipientAddress.Text))
            {
                sB.Info("Введите адрес пребытия");
            }
            else if (string.IsNullOrEmpty(cbNameOrderTiming.Text))
            {
                sB.Info("Введите срочность");
            }
            else if (string.IsNullOrEmpty(tbPrice.Text))
            {
                sB.Info("Введите цену");
            }
            else
            {
                string firstNameEmployee = "";
                string lastNameEmployee = "";
                string[] employee = cbEmployee.Text.Split(new char[] { ' ' });
                firstNameEmployee = employee[0];
                lastNameEmployee = employee[1];

                string firstNameClient = "";
                string lastNameClient = "";
                string[] client = cbClient.Text.Split(new char[] { ' ' });
                firstNameClient = client[0];
                lastNameClient = client[1];

                string firstNameRecipient = "";
                string lastNameRecipient = "";
                string[] recipient = cbRecipient.Text.Split(new char[] { ' ' });
                firstNameRecipient = recipient[0];
                lastNameRecipient = recipient[1];

                try
                {
                    connection.Open();
                    cmd = new SqlCommand("INSERT INTO dbo.[Order] " +
                        $"VALUES((SELECT Id FROM dbo.[Employee] " +
                        $"WHERE [FirstName] = '{firstNameEmployee}' and [LastName] = '{lastNameEmployee}')," +
                        $"(SELECT Id FROM dbo.[Client] " +
                        $"WHERE [FirstName] = '{firstNameClient}' and [LastName] = '{lastNameClient}')," +
                        $"(SELECT Id FROM dbo.[Recipient] " +
                        $"WHERE [FirstName] = '{firstNameRecipient}' and [LastName] = '{lastNameRecipient}')," +
                        $"(SELECT Id FROM dbo.[Supplier] " +
                        $"WHERE [Name] = '{cbSupplier.Text}')," +
                        $"(SELECT Id FROM dbo.[Product] " +
                        $"WHERE [Article] = '{cbArticle.Text}'), " +
                        $"'{tbShippingAddress.Text}'," +
                        $"(SELECT Id FROM dbo.[SortingCenter] " +
                        $"WHERE NameSortingCenter = '{cbSortingCenter.Text}')," +
                        $"'{tbRecipientAddress.Text}'," +
                        $"(SELECT Id FROM dbo.[OrderTiming] " +
                        $"WHERE [NameOrderTiming] = '{cbNameOrderTiming.Text}')," +
                        $"@Price, " +
                        $"NULL)", connection);
                    cmd.Parameters.AddWithValue("Price", double.Parse(tbPrice.Text));
                    cmd.ExecuteNonQuery();
                    sB.Info("Данные успешно добавлены");

                    
                }
                catch (Exception ex)
                {
                    sB.Info(ex.Message);
                }
                finally
                {
                    connection.Close();
                    dG.Loader("SELECT * FROM dbo.[OrderView]");
                    cbEmployee.Text = null;
                    cbClient.Text = null;
                    cbRecipient.Text = null;
                    cbSupplier.Text = null;
                    cbArticle.Text = null;
                    tbShippingAddress.Text = null;
                    cbSortingCenter.Text = null;
                    tbRecipientAddress.Clear();
                    cbNameOrderTiming.Text = null;
                    tbPrice.Clear();
                }
            }
        }

        private void btnEditOrder_Click(object sender, RoutedEventArgs e)
        {
            if (dgOrder.SelectedItem is null)
            {
                sB.Info("Выберете строку для редактирования");
            }
            else if (string.IsNullOrEmpty(cbEditEmployee.Text))
            {
                sB.Info("Введите сотрудника");
            }
            else if (string.IsNullOrEmpty(cbEditClient.Text))
            {
                sB.Info("Введите клиента");
            }
            else if (string.IsNullOrEmpty(cbEditRecipient.Text))
            {
                sB.Info("Введите адресата");
            }
            else if (string.IsNullOrEmpty(cbEditArticle.Text))
            {
                sB.Info("Введите артикул товара");
            }
            else if (string.IsNullOrEmpty(tbEditShippingAddress.Text))
            {
                sB.Info("Введите адрес отправки");
            }
            else if (string.IsNullOrEmpty(cbEditSortingCenter.Text))
            {
                sB.Info("Введите сортировочный центр");
            }
            else if (string.IsNullOrEmpty(tbEditRecipientAddress.Text))
            {
                sB.Info("Введите адрес пребытия");
            }
            else if (string.IsNullOrEmpty(cbEditNameOrderTiming.Text))
            {
                sB.Info("Введите срочность");
            }
            else if (string.IsNullOrEmpty(tbEditPrice.Text))
            {
                sB.Info("Введите цену");
            }
            else
            {
                string firstNameEmployee = "";
                string lastNameEmployee = "";
                string[] employee = cbEditEmployee.Text.Split(new char[] { ' ' });
                firstNameEmployee = employee[0];
                lastNameEmployee = employee[1];

                string firstNameClient = "";
                string lastNameClient = "";
                string[] client = cbEditClient.Text.Split(new char[] { ' ' });
                firstNameClient = client[0];
                lastNameClient = client[1];

                string firstNameRecipient = "";
                string lastNameRecipient = "";
                string[] recipient = cbEditRecipient.Text.Split(new char[] { ' ' });
                firstNameRecipient = recipient[0];
                lastNameRecipient = recipient[1];

                try
                {
                    connection.Open();
                    cmd = new SqlCommand("UPDATE dbo.[Order] " +
                        "SET[IdEmployee] = (SELECT Id FROM dbo.[Employee] " +
                        $"WHERE[FirstName] = '{firstNameEmployee}' and [LastName] = '{lastNameEmployee}'), " +
                        $"[IdClient] = (SELECT Id FROM dbo.[Client] " +
                        $"WHERE [FirstName] = '{firstNameClient}' and [LastName] = '{lastNameClient}'), " +
                        $"[IdRecipient] = (SELECT Id FROM dbo.[Recipient] " +
                        $"WHERE [FirstName] = '{firstNameRecipient}' and [LastName] = '{lastNameRecipient}'), " +
                        $"[IdSupplier] = (SELECT Id FROM dbo.[Supplier]" +
                        $"WHERE [Name] = '{cbEditSupplier.Text}'), " +
                        $"[IdProduct] = (SELECT Id FROM dbo.[Product] " +
                        $"WHERE [Article] = '{cbEditArticle.Text}'), " +
                        $"[ShippingAddress] = '{tbEditShippingAddress.Text}', " +
                        $"[IdSortingСenter] = (SELECT Id FROM dbo.[SortingCenter] " +
                        $"WHERE NameSortingCenter = '{cbEditSortingCenter.Text}'), " +
                        $"[RecipientAddress] = '{tbEditRecipientAddress.Text}', " +
                        $"[IdOrderTiming] = (SELECT Id FROM dbo.[OrderTiming] " +
                        $"WHERE [NameOrderTiming] = '{cbEditNameOrderTiming.Text}'), " +
                        $"[Price] = @Price " +
                        $"WHERE[Id] = '{id}'", connection);
                    cmd.Parameters.AddWithValue("Price", double.Parse(tbEditPrice.Text));
                    cmd.ExecuteNonQuery();
                    sB.Info("Данные успешно изменены");
                }
                catch (Exception ex)
                {
                    sB.Info(ex.Message);
                }
                finally
                {
                    connection.Close();
                    dG.Loader("SELECT * FROM dbo.[OrderView]");
                    cbEditEmployee.Text = null;
                    cbEditClient.Text = null;
                    cbEditRecipient.Text = null;
                    cbEditSupplier.Text = null;
                    cbEditArticle.Text = null;
                    tbEditShippingAddress.Text = null;
                    cbEditSortingCenter.Text = null;
                    tbEditRecipientAddress.Clear();
                    cbEditNameOrderTiming.Text = null;
                    tbEditPrice.Clear();
                }
            }
        }

        private void MenuItem_Click_ListBox(object sender, RoutedEventArgs e)
        {
            dataBaseQuery.SqlQuery("DELETE FROM dbo.[SortingCenter] " +
                $"WHERE [NameSortingCenter] = '{lbSortingCenter.SelectedItem}'", "Данные успешно удалены", "Ошибка");
            lb.Loader("SortingCenter", "NameSortingCenter");
            comboBoxEditSortingCenter.Loader("SortingCenter", "NameSortingCenter");
            comboBoxSortingCenter.Loader("SortingCenter", "NameSortingCenter");

            
        }

        private void btnExcel_Click(object sender, RoutedEventArgs e)
        {
            ExcelClass.ConvertToExcel(dgOrder);
        }
    }
}