using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNTExpress.Classes.SnackBarMessage;

namespace TNTExpress.Classes.DataBaseWork
{
    public class DataBaseQuery
    {
        SB sB;
        readonly SqlConnection connection =
            new SqlConnection(@"Data Source=DENIS-PC;
                                Initial Catalog=TNTExpress;
                                Integrated Security=True");
        SqlCommand cmd;
        public DataBaseQuery(Snackbar snackbar, SnackbarMessage snackbarMessage)
        {
            sB = new SB(snackbar, snackbarMessage);
        }

        public void InsertData(string table, string[] columns, string[] data, string exceptionMessage)
        {
            if (columns.Length != data.Length)
                throw new ArgumentOutOfRangeException("Различное " +
                    "количетсво столбцов и данных");


            string sqlCommand = $"INSERT INTO dbo.[{table}] (";
            try
            {
                connection.Open();

                for (int i = 0; i < columns.Length; i++)
                {
                    if (i != columns.Length - 1)
                    {
                        sqlCommand += $"{columns[i]},";
                    }
                    else
                    {
                        sqlCommand += $"{columns[i]}) VALUES (";
                    }
                }


                for (int i = 0; i < columns.Length; i++)
                {
                    if (i != columns.Length - 1)
                    {
                        sqlCommand += $"'{data[i]}',";
                    }
                    else
                    {
                        sqlCommand += $"'{data[i]}')";
                    }
                }

                cmd = new SqlCommand(sqlCommand, connection);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                sB.Info(exceptionMessage);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
