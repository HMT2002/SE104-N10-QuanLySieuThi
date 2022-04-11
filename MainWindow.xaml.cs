using Microsoft.Win32;
using SE104_N10_QuanLySieuThi.pages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
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
        public SqlConnection ketnoi = new SqlConnection(@"Data Source=.;Initial Catalog=QuanLySieuThi;Integrated Security=True");
        public SqlCommand command;

        public MainWindow()
        {
            InitializeComponent();
        }
        private void btnBuy_Click(object sender, RoutedEventArgs e)
        {
            paBuy page = new paBuy();
            page.Height = fraMain.MinHeight;
            page.Width = fraMain.MinWidth;
            fraMain.Content = page;
        }

        private void btnSell_Click(object sender, RoutedEventArgs e)
        {
            paSell page = new paSell();
            page.Height = fraMain.MinHeight;
            page.Width = fraMain.MinWidth;
            fraMain.Content = page;

        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            paHome page = new paHome();
            page.Height = fraMain.MinHeight;
            page.Width = fraMain.MinWidth;
            fraMain.Content = page;
        }

        private void btnProduct_Click(object sender, RoutedEventArgs e)
        {
            paProduct page = new paProduct();
            page.Height = fraMain.MinHeight;
            page.Width = fraMain.MinWidth;
            fraMain.Content = page;
        }

        private void btnCustomer_Click(object sender, RoutedEventArgs e)
        {
            paCustomer page = new paCustomer();
            page.Height = fraMain.MinHeight;
            page.Width = fraMain.MinWidth;
            fraMain.Content = page;

        }

        private void btnSetting_Click(object sender, RoutedEventArgs e)
        {
            paSetting page = new paSetting();
            page.Height = fraMain.MinHeight;
            page.Width = fraMain.MinWidth;
            fraMain.Content = page;

        }

        private void btnManage_Click(object sender, RoutedEventArgs e)
        {
            paManage page = new paManage();
            page.Height = fraMain.MinHeight;
            page.Width = fraMain.MinWidth;
            fraMain.Content = page;
        }

        private void btnStatic_Click(object sender, RoutedEventArgs e)
        {
            paStatic page = new paStatic();
            page.Height = fraMain.MinHeight;
            page.Width = fraMain.MinWidth;
            fraMain.Content = page;

        }
    }
}
