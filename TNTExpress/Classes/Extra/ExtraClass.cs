using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TNTExpress.Windows.Autorization;

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
    }
}
