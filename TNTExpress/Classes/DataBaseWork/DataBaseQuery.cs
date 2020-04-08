﻿using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        public void InsertData(string sqlCommand, string exceptionMessage)
        {
            try
            {
                connection.Open();
                cmd = new SqlCommand(sqlCommand, connection);
                cmd.ExecuteNonQuery();
                sB.Info("Данные успешно добавлены");
            }
            catch (Exception ex)
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
