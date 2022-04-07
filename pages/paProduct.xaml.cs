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
            createButtons();
        }
        private void openImgFromDatabaseWithId(int id)
        {
            HinhAnh anh = new HinhAnh(id, "test"+id.ToString());
            try
            {
                anh.openImgFromDatabase();
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

        private void createButtons()
        {
            for (int i = 0; i < 25; i++)
            {
                HinhAnh anh = new HinhAnh(i, "test");
                Button btn = new Button();
                btn.Height = 50;
                btn.Width = 50;
                anh.Img.Height = 20;
                anh.Img.Width = 20;
                Thickness myThickness = new Thickness();
                myThickness.Bottom = 20;
                myThickness.Left = 0;
                myThickness.Right = 0;
                myThickness.Top = 0;
                anh.Img.Margin = myThickness;

                anh.Img.Stretch=Stretch.Fill;
                btn.Content = anh.Img;
                myThickness = new Thickness();
                myThickness.Bottom = 0;
                myThickness.Left = 120 * (i % 5);
                myThickness.Right = 0;
                myThickness.Top = -400 + 120 * (i / 5);
                btn.Margin = myThickness;

                grdMain.Children.Add(btn);
            }
        }
    }
}
