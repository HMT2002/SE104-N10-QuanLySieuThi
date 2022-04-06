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
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using SE104_N10_QuanLySieuThi.classes;

namespace SE104_N10_QuanLySieuThi
{
    /// <summary>
    /// Interaction logic for paProduct.xaml
    /// </summary>
    public partial class paProduct : Page
    {
        
        public SqlConnection ketnoi = new SqlConnection(@"Data Source=LAPTOP-H3DR409O\MSSQLSERVER01;Initial Catalog=QuanLySieuThi;Integrated Security=True");
        public SqlCommand command;

        public paProduct()
        {
            InitializeComponent();
        }
        private void bntTest_Click(object sender, RoutedEventArgs e)
        {
            if (txtId.Text == "")
            {
                return;
            }
            int id = Int32.Parse(txtId.Text);
            saveImgToDatabaseWithId(id);
        }
        private void btnTestChoose_Click(object sender, RoutedEventArgs e)
        {
            if (txtId.Text == "") {
                return;
            }
            openImgFromDatabaseWithId(Int32.Parse(txtId.Text));
        }
        private void openImgFromDatabaseWithId(int id)
        {
            HinhAnh anh = new HinhAnh(id, "test"+id.ToString());
            try
            {
                anh.openImgFromDatabase();
                imgTest.Source = anh.Bitimg;
            }
            catch(Exception ex) 
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        private void saveImgToDatabaseWithId(int id)
        {
            try
            {
                HinhAnh anh = new HinhAnh(id, "test" + id.ToString());
                anh.chooseImgAndAddToDatabase();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }
    }
}
