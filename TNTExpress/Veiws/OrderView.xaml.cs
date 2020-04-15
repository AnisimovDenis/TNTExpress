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
using TNTExpress.Classes.ListWork;
using TNTExpress.Classes.SnackBarMessage;

namespace TNTExpress.Veiws
{
    /// <summary>
    /// Interaction logic for OrderView.xaml
    /// </summary>
    public partial class OrderView : UserControl
    {
        readonly System.Data.SqlClient.SqlConnection connection =
               new SqlConnection(@"Data Source=DENIS-PC;
                                Initial Catalog=TNTExpress;
                                Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader reader;
        SB sB;
        DG dG;
        DataBaseQuery dataBaseQuery;
        MyListBox lb;

        string id;

        public OrderView()
        {
            InitializeComponent();
            dG = new DG(dgOrder, snack, snackMessage);

            sB = new SB(snack, snackMessage);

            lb = new MyListBox(lbSortingCenter, snack, snackMessage);

            dataBaseQuery = new DataBaseQuery(snack, snackMessage);

            snackMessage.ActionClick += delegate { dG.CloseSnackbar(); };
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            dG.Loader("SELECT * FROM dbo.[ProductStrength] " +
                $"WHERE [Name] LIKE '%{tbSearch.Text}%'");
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            dataBaseQuery.SqlQuery("DELETE FROM dbo.[Product] " +
                $"WHERE [Id] = {id}", "Данные успешно удалены", "Ошибка");
            dG.Loader("SELECT * FROM dbo.ProductStrength");
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
        }

        private void btnAddSortingCenter_Click(object sender, RoutedEventArgs e)
        {
            dataBaseQuery.SqlQuery("INSERT INTO dbo.[SortingCenter] " +
                $"VALUES('{tbSortingCenter.Text}')",
                "Данные успешно добавлены", "Такой сортировочный центр уже есть");
            lb.Loader("SortingCenter", "NameSortingCenter");
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
                    cbEditSortingCenter.Text = reader[9].ToString();
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
    }
}