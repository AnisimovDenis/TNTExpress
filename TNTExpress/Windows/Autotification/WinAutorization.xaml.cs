using System;
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
using TNTExpress.Classes.AutorizationWork;
using TNTExpress.Classes.Extra;

namespace TNTExpress.Windows.Autotification
{
    /// <summary>
    /// Логика взаимодействия для WinAutorization.xaml
    /// </summary>
    public partial class WinAutorization : Window
    {
        Autorization autorization;
        public WinAutorization()
        {
            InitializeComponent();

            autorization = new Autorization(this, tbLogin, pbPassword, snack, snackMessage);

            tbDragger.MouseDown += delegate { this.DragMove(); };

            btnEnter.Click += delegate { autorization.Enter(tbLogin.Text); };

            snackMessage.ActionClick += delegate { autorization.CloseSnack(); };

            btnHidde.Click += delegate { ExtraClass.MinimizedWindow(this); };

            btnClose.Click += delegate { ExtraClass.Shutdown(); };
        }
    }
}
