using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TNTExpress.Classes.SnackBarMessage;

namespace TNTExpress.Classes
{
    public class DG
    {
        readonly SqlConnection connection =
               new SqlConnection(@"Data Source=DENIS-PC;
                                Initial Catalog=TNTExpress;
                                Integrated Security=True");
        readonly SB sB;
        SqlDataAdapter adapter;
        DataTable dataTable;
        readonly DataGrid dataGrid;

        private string firstColumn;
        public string FirstColumn
        {
            get
            {
                firstColumn = SelectColumn(0);
                return firstColumn;
            }
        }

        public DG(DataGrid dataGrid, 
            Snackbar snackbar, SnackbarMessage snackbarMessage)
        {
            this.dataGrid = dataGrid;
            sB = new SB(snackbar, snackbarMessage);
        }

        public void Loader(string sqlCommand)
        {
            try
            {
                adapter = new SqlDataAdapter(sqlCommand, connection);
                dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGrid.ItemsSource = dataTable.DefaultView;
            }
            catch (SqlException sqlEx)
            {
                sB.Info(sqlEx.Message);
            }
        }

        public string SelectColumn(int numColumn)
        {
            string id = null;
            if (dataGrid != null)
            {
                DataRowView dataRowView = dataGrid.SelectedItem as DataRowView;
                if (dataRowView != null)
                {
                    DataRow dataRow = (DataRow)dataRowView.Row;
                    foreach (var item in dataRow.ItemArray)
                    {
                        Debug.WriteLine(item);
                    }
                    id = dataRow.ItemArray[numColumn].ToString();
                }
            }
            return id;
        }

        public void CloseSnackbar()
        {
            sB.CloseSnackbar();
        }
    }
}
