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
        public SqlConnection ketnoi = new SqlConnection(@"Data Source=LAPTOP-H3DR409O\MSSQLSERVER01;Initial Catalog=QuanLySieuThi;Integrated Security=True");
        public SqlCommand command;

        public MainWindow()
        {
            InitializeComponent();
            fraMain.Content = new paHome();

        }

        public byte[] convertImgToByte(BitmapImage bitimg)
        {
            MemoryStream memStream = new MemoryStream();
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitimg));
            encoder.Save(memStream);
            return memStream.ToArray();
        }

        public BitmapImage convertImgFromByte(byte[] array)
        {
            using (var ms = new System.IO.MemoryStream(array))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad; // here
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }

        public void chonHinhAnh(Image img)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Chon anh(*.jpg; *.png; *.gif) | *.jpg; *.png; *.gif";
            if (dialog.ShowDialog() == !DialogResult)
            {
                //HowKTeam
                BitmapImage temp = new BitmapImage(new Uri(dialog.FileName));
                string name = "test"/*được phép trùng*/;
                int id = 0; /*Không được phép trùng do là khóa chính, phải là int */
                addBinaryArrIntoSQL(convertImgToByte(temp), name, id);
                BitmapImage res = convertImgFromByte(convertImgToByte(temp));
                img.Source = res;
            }

        }

        public void moHinhAnh(Image img)
        {
            ketnoi.Open();
            BitmapImage res = new BitmapImage();
            SqlCommand cmd = new SqlCommand(@"select PICBI from HINHANH where PICID = 4", ketnoi);
            using (SqlDataReader d = cmd.ExecuteReader())
            {
                if (d.Read())
                {
                    byte[] bytearr = (byte[])d["PICBI"];
                    res = convertImgFromByte(bytearr);
                    //Do stuff with salt and pass
                }
                else
                {
                    // NO User with email exists
                }
            }
            ketnoi.Close();
            img.Source = res;
        }

        public void addBinaryArrIntoSQL(byte[] bytearr, string picname, int picid)
        {
            ketnoi.Open();
            using (SqlCommand cmd = new SqlCommand(@"INSERT INTO HINHANH (PICID, PICNAME, PICBI) VALUES (" + picid + ", '" + picname + "',@binaryValue)", ketnoi))
            {
                // Replace 8000, below, with the correct size of the field
                cmd.Parameters.Add("@binaryValue", SqlDbType.VarBinary, 999999).Value = bytearr;
                cmd.ExecuteNonQuery();
            }
            ketnoi.Close();
        }
    
        private void btnBuy_Click(object sender, RoutedEventArgs e)
        {
            fraMain.Content = new paBuy();
        }

        private void btnSell_Click(object sender, RoutedEventArgs e)
        {
            fraMain.Content = new paSell();

        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            fraMain.Content = new paHome();
        }

        private void btnProduct_Click(object sender, RoutedEventArgs e)
        {
            fraMain.NavigationService.Navigate(new paProduct());
        }

        private void btnCustomer_Click(object sender, RoutedEventArgs e)
        {
            fraMain.Content = new paCustomer();

        }

        private void btnSetting_Click(object sender, RoutedEventArgs e)
        {
            fraMain.Content = new paSetting();

        }

        private void btnManage_Click(object sender, RoutedEventArgs e)
        {
           fraMain.Content = new paManage();
        }

        private void btnStatic_Click(object sender, RoutedEventArgs e)
        {
            fraMain.Content = new paStatic();

        }
    }
}
