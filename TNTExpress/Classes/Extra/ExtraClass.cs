using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TNTExpress.Windows.Autotification;

namespace TNTExpress.Classes.Extra
{
    public static class ExtraClass
    {
        public static void OpenWinAutorization(Window window)
        {
            MessageBoxResult message = MessageBox.Show("Вы уверены, " +
                "что хотите перейти на окно авторизации?", "Уведмоление",
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            switch (message)
            {
                case MessageBoxResult.Yes:
                    WinAutorization winAutorization = new WinAutorization();
                    winAutorization.Show();
                    window.Close();
                    break;
            }

        }

        public static void MinimizedWindow(Window window)
        {
            window.WindowState = WindowState.Minimized;
        }

        public static void Shutdown()
        {
            Application.Current.Shutdown();
        }

        public static bool IsValidString(string str, string regexStr)
        {
            Regex regex = new Regex(regexStr);
            return regex.IsMatch(str);
        }
    }
}
