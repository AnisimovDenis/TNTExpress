using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TNTExpress.Classes.SnackBarMessage
{
    public class SB
    {
        Snackbar snackbar;
        SnackbarMessage message;
        public SB(Snackbar snackbar, SnackbarMessage message)
        {
            this.snackbar = snackbar;
            this.message = message;
        }

        public void Info(string info)
        {
            snackbar.IsActive = true;
            message.Content = info;
        }

        public void CloseSnackbar()
        {
            snackbar.IsActive = false;
        }
    }
}
