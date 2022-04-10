using SE104_N10_QuanLySieuThi.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SE104_N10_QuanLySieuThi.usercontrols
{
    /// <summary>
    /// Interaction logic for ucControlBar.xaml
    /// </summary>
    public partial class ucControlBar : UserControl
    {
        public ControlBarViewModel ViewModel { get; set; }
        public ucControlBar()
        {
            InitializeComponent();
            this.DataContext = ViewModel = new ControlBarViewModel();
        }

    }
}
