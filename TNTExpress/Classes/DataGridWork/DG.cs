using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TNTExpress.Classes.SnackBarMessage;

namespace TNTExpress.Classes
{
    public class DG
    {
        SB sB;
        readonly SqlConnection connection = 
            new SqlConnection(@"Data Source=DENIS-PC;
                                Initial Catalog=TNTExpress;
                                Integrated Security=True");
        SqlDataAdapter adapter;
        DataTable dataTable;
        DataGrid dataGrid;

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

        public void CloseSnackbar()
        {
            sB.CloseSnackbar();
        }
    }
}
