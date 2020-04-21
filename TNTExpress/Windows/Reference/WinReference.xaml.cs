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
using TNTExpress.Classes.Extra;

namespace TNTExpress.Windows.Reference
{
    /// <summary>
    /// Interaction logic for WinReference.xaml
    /// </summary>
    public partial class WinReference : Window
    {
        public WinReference()
        {
            InitializeComponent();

            tbDragger.MouseDown += delegate { this.DragMove(); };

            btnClose.Click += delegate { this.Close(); };
        }
    }
}
