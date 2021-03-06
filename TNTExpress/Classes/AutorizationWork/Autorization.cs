﻿using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TNTExpress.Classes.SnackBarMessage;
using TNTExpress.Windows.Admin;
using TNTExpress.Windows.Director;
using TNTExpress.Windows.Employee;
using TNTExpress.Windows.Manager;

namespace TNTExpress.Classes.AutorizationWork
{
    public class Autorization
    {
        readonly SB sB;
        readonly SqlConnection connection =
            new SqlConnection(@"Data Source=DENIS-PC;
                                Initial Catalog=TNTExpress;
                                Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader reader;

        TextBox textBox;
        PasswordBox passwordBox;
        Window window;
        public Autorization(Window window, TextBox textBox, PasswordBox passwordBox,
            Snackbar snackbar, SnackbarMessage snackbarMessage)
        {
            this.passwordBox = passwordBox;
            this.textBox = textBox;
            this.window = window;
            sB = new SB(snackbar, snackbarMessage);
        }

        public void Enter(string login)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox.Text))
                {
                    sB.Info("Введите логин");
                }
                else if (string.IsNullOrEmpty(passwordBox.Password))
                {
                    sB.Info("Введите пароль");
                }
                else
                {
                    connection.Open();
                    cmd = new SqlCommand("SELECT [Id], [Password], [IdRole] FROM dbo.[User] " +
                        $"WHERE [Login] = '{login}'", connection);
                    reader = cmd.ExecuteReader();
                    reader.Read();

                    App.IdUser = reader[0].ToString();
                    string password = reader[1].ToString();
                    string role = reader[2].ToString();

                    if (passwordBox.Password != password)
                    {
                        sB.Info("Неверный логин или пароль");
                    }
                    else
                    {
                        switch (role)
                        {
                            case "4":
                                WinDirector winDirector = new WinDirector();
                                winDirector.Show();
                                window.Close();
                                break;
                            case "3":
                                WinManager winManager = new WinManager();
                                winManager.Show();
                                window.Close();
                                break;
                            case "2":
                                WinEmployee winEmployee = new WinEmployee();
                                winEmployee.Show();
                                window.Close();
                                break;
                            case "1":
                                WinAdmin winAdmin = new WinAdmin();
                                winAdmin.Show();
                                window.Close();
                                break;
                        }
                    }
                }

            }
            catch
            {
                sB.Info("Неверный логин или пароль");
            }
            finally
            {
                connection.Close();
            }
        }

        public void CloseSnack()
        {
            sB.CloseSnackbar();
        }
    }
}
