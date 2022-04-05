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

namespace SE104_N10_QuanLySieuThi
{
    /// <summary>
    /// Interaction logic for paProduct.xaml
    /// </summary>
    public partial class paProduct : Page
    {
 
        SqlConnection ketnoi = new SqlConnection(@"Data Source=LAPTOP-H3DR409O\MSSQLSERVER01;Initial Catalog=QuanLySieuThi;Integrated Security=True");
        SqlCommand command;

        public paProduct()
        {
            InitializeComponent();
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

        private void bntTest_Click(object sender, RoutedEventArgs e)
        {
            //chonHinhAnh();
            //moHinhAnh();
        }
        private void executeSQLComm(string comm)
        {
        }

        private void chonHinhAnh()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Chon anh(*.jpg; *.png; *.gif) | *.jpg; *.png; *.gif";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //HowKTeam
                BitmapImage temp = new BitmapImage(new Uri(dialog.FileName));
                string name = "test"/*được phép trùng*/;
                int id = 0; /*Không được phép trùng do là khóa chính, phải là int */
                addBinaryArrIntoSQL(convertImgToByte(temp),name,id);
                BitmapImage res = convertImgFromByte(convertImgToByte(temp));
                imgTest.Source = res;
            }

        }

        private void moHinhAnh()
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
            imgTest.Source = res;
        }

        private void addBinaryArrIntoSQL(byte[] bytearr,string picname,int picid)
        {
            ketnoi.Open();
            using (SqlCommand cmd = new SqlCommand(@"INSERT INTO HINHANH (PICID, PICNAME, PICBI) VALUES ("+picid+", '"+picname+"',@binaryValue)", ketnoi))
            {
                // Replace 8000, below, with the correct size of the field
                cmd.Parameters.Add("@binaryValue", SqlDbType.VarBinary, 999999).Value = bytearr;
                cmd.ExecuteNonQuery();
            }
            ketnoi.Close();
        }
    }
}
