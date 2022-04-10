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
        //private void btnBuy_Click(object sender, RoutedEventArgs e)
        //{
        //    fraMain.Content = new paBuy();
        //}

        //private void btnSell_Click(object sender, RoutedEventArgs e)
        //{
        //    fraMain.Content = new paSell();

        //}

        //private void btnHome_Click(object sender, RoutedEventArgs e)
        //{
        //    fraMain.Content = new paHome();
        //}

        //private void btnProduct_Click(object sender, RoutedEventArgs e)
        //{
        //    fraMain.NavigationService.Navigate(new paProduct());
        //}

        //private void btnCustomer_Click(object sender, RoutedEventArgs e)
        //{
        //    fraMain.Content = new paCustomer();

        //}

        //private void btnSetting_Click(object sender, RoutedEventArgs e)
        //{
        //    fraMain.Content = new paSetting();

        //}

        //private void btnManage_Click(object sender, RoutedEventArgs e)
        //{
        //   fraMain.Content = new paManage();
        //}

        //private void btnStatic_Click(object sender, RoutedEventArgs e)
        //{
        //    fraMain.Content = new paStatic();

        //}
    }
}
