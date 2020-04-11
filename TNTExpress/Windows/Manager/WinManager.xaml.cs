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
using TNTExpress.Classes;
using TNTExpress.Classes.ComboBoxWork;
using TNTExpress.Classes.DataBaseWork;
using TNTExpress.Classes.Extra;
using TNTExpress.Classes.ListWork;
using TNTExpress.Classes.SnackBarMessage;
using TNTExpress.Windows.Autorization;

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

            tbDragger.MouseDown += delegate { this.DragMove(); };

            btnBack.Click += delegate { ExtraClass.OpenWinAutorization(this); };
        }
    }
}