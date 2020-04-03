﻿using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace TNTExpress.Windows.Manager
{
    /// <summary>
    /// Логика взаимодействия для WinManager.xaml
    /// </summary>
    public partial class WinManager : Window
    {
        public WinManager()
        {
            InitializeComponent();

            snack.IsActive = true;
            snackMessage.Content = "Проверка";
        }

        private void snackMessage_ActionClick(object sender, RoutedEventArgs e)
        {
            snack.IsActive = false;
        }
    }
}