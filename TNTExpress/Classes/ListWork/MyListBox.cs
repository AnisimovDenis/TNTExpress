using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TNTExpress.Classes.SnackBarMessage;

namespace TNTExpress.Classes.ListWork
{
    public class MyListBox
    {
        readonly SB sB;
        readonly SqlConnection connection =
            new SqlConnection(@"Data Source=DENIS-PC;
                                Initial Catalog=TNTExpress;
                                Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader reader;
        ListBox listBox;

        public MyListBox(ListBox listBox,
            Snackbar snackbar, SnackbarMessage snackbarMessage)
        {
            this.listBox = listBox;
            sB = new SB(snackbar, snackbarMessage);
        }

        public void Loader(string table, string column)
        {
            try
            {
                connection.Open();
                string sqlCommand = $"SELECT [{column}] FROM dbo.[{table}]";
                cmd = new SqlCommand(sqlCommand, connection);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    listBox.Items.Add(reader[0].ToString());
                }
            }
            catch (SqlException sqlEx)
            {
                sB.Info(sqlEx.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
