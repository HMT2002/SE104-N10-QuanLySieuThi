using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace SE104_N10_QuanLySieuThi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void sect3_MouseEnter(object sender, MouseEventArgs e)
        {
            sect3.Foreground = new SolidColorBrush(Colors.Yellow);

        }

        private void sect3_MouseLeave(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();
            sect3.Foreground = (Brush)bc.ConvertFrom("#D800FF00");

        }

        private void sect2_MouseEnter(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();
            sect2.Foreground = (Brush)bc.ConvertFrom("#F757F4");

        }

        private void sect2_MouseLeave(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();
            sect2.Foreground = (Brush)bc.ConvertFrom("#D800FF00");
        }

        private void sect1_MouseEnter(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();
            sect1.Foreground = (Brush)bc.ConvertFrom("#57E4F7");

        }

        private void sect1_MouseLeave(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();
            sect1.Foreground = (Brush)bc.ConvertFrom("#D800FF00");
        }

        private void sect1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(e.Uri.ToString());
        }
    }
}
