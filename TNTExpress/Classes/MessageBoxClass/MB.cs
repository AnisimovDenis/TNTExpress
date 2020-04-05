using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TNTExpress.Classes.MessageBoxClass
{
    class MB
    {
        public static void MessageBoxError(string info)
        {
            MessageBox.Show(info, "Ошибка",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void MessageBoxInfo(string info)
        {
            MessageBox.Show(info, "Уведмоление",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
