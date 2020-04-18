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
    /// Interaction logic for ProductView.xaml
    /// </summary>
    public partial class ProductView : UserControl
    {
        readonly SqlConnection connection =
               new SqlConnection(@"Data Source=DENIS-PC;
                                Initial Catalog=TNTExpress;
                                Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader reader;
        SB sB;
        DG dG;
        readonly CB comboBoxAddStrength;
        readonly CB comboBoxEditStrength;
        DataBaseQuery dataBaseQuery;
        string id;

        public ProductView()
        {
            InitializeComponent();

            dG = new DG(dgProduct, snack, snackMessage);

            sB = new SB(snack, snackMessage);

            dataBaseQuery = new DataBaseQuery(snack, snackMessage);

            comboBoxAddStrength = new CB(cbStrength, snack, snackMessage);

            comboBoxEditStrength = new CB(cbEditStrength, snack, snackMessage);

            comboBoxAddStrength.Loader("Strength", "NameStrength");

            comboBoxEditStrength.Loader("Strength", "NameStrength");

            this.Loaded += delegate
            {
                dG.Loader("SELECT * FROM dbo.[ProductStrength]");
            };

            snackMessage.ActionClick += delegate { dG.CloseSnackbar(); };
        }

        private void dgProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgProduct.SelectedItem != null)
                id = dG.FirstColumn;
            try
            {
                connection.Open();
                cmd = new SqlCommand($"SELECT * FROM dbo.[ProductStrength]" +
                    $"WHERE[Id] = {id}", connection);
                reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    tbEditArticle.Text = reader[1].ToString();
                    tbEditName.Text = reader[2].ToString();
                    tbEditDimensions.Text = reader[3].ToString();
                    tbEditWeight.Text = reader[4].ToString();
                    cbEditStrength.Text = reader[5].ToString();
                    tbEditFeatures.Text = reader[6].ToString();
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
            dG.Loader("SELECT * FROM dbo.[ProductStrength] " +
                $"WHERE [Name] LIKE '%{tbSearch.Text}%'");
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(tbArticle.Text))
            {
                sB.Info("Введите артикул");
            }
            else if (string.IsNullOrEmpty(tbName.Text))
            {
                sB.Info("Введите наименование");
            }
            else if (string.IsNullOrEmpty(tbDimensions.Text))
            {
                sB.Info("Введите габариты");
            }
            else if (string.IsNullOrEmpty(tbWeight.Text))
            {
                sB.Info("Введите размер");
            }
            else if (string.IsNullOrEmpty(cbStrength.Text))
            {
                sB.Info("Введите вес");
            }
            else if (string.IsNullOrEmpty(tbFeatures.Text))
            {
                sB.Info("Введите особенности");
            }
            else
            {
                dataBaseQuery.SqlQuery("INSERT INTO dbo.[Product]" +
                    $"VALUES ('{tbArticle.Text}', '{tbName.Text}', '{tbDimensions.Text}', '{tbWeight.Text}'," +
                    $"(SELECT Id FROM dbo.[Strength] WHERE [NameStrength] = '{cbStrength.Text}'), " +
                    $"'{tbFeatures.Text}')",
                "Данные успешно добавлены", "Ошибка");
                dG.Loader("SELECT * FROM dbo.[ProductStrength]");

            }
        }

        private void btnEditProduct_Click(object sender, RoutedEventArgs e)
        {

            if (dgProduct.SelectedItem is null)
            {
                sB.Info("Выберете строку для редактирования");
            }
            else if (string.IsNullOrEmpty(tbEditArticle.Text))
            {
                sB.Info("Введите артикул");
            }
            else if (string.IsNullOrEmpty(tbEditName.Text))
            {
                sB.Info("Введите наименование");
            }
            else if (string.IsNullOrEmpty(tbEditDimensions.Text))
            {
                sB.Info("Введите габариты");
            }
            else if (string.IsNullOrEmpty(tbEditWeight.Text))
            {
                sB.Info("Введите размер");
            }
            else if (string.IsNullOrEmpty(cbEditStrength.Text))
            {
                sB.Info("Введите вес");
            }
            else if (string.IsNullOrEmpty(tbEditFeatures.Text))
            {
                sB.Info("Введите особенности");
            }
            else
            {
                dataBaseQuery.SqlQuery("UPDATE dbo.[Product]",
                "Данные успешно изменены", "Ошибка");
                dG.Loader("SELECT * FROM dbo.[ProductStrength]");
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            dataBaseQuery.SqlQuery("DELETE FROM dbo.[Product] " +
                $"WHERE [Id] = {id}", "Данные успешно удалены", "Ошибка");
            dG.Loader("SELECT * FROM dbo.ProductStrength");
        }

        private void btnExcel_Click(object sender, RoutedEventArgs e)
        {
            ExcelClass.ConvertToExcel(dgProduct);
        }
    }
}