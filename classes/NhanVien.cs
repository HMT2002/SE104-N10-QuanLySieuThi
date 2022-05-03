using SE104_N10_QuanLySieuThi.Model;
using SE104_N10_QuanLySieuThi.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;


namespace SE104_N10_QuanLySieuThi.classes
{
    public class NhanVien
    {
        public NHANVIEN nhanvien { get; set; }
        public int STT;
        public string ID;
        public string Name;

        private string mail;

        Account acc = new Account();

        private string phone;

        private DateTime startdate;

        private decimal salary;

        private string position;

        private string cmnd;

        private DateTime birthday;
        public string Phone { get => phone; set => phone = value; }
        public DateTime Startdate { get => startdate; set => startdate = value; }
        public decimal Salary { get => salary; set => salary = value; }

        public string Mail { get => mail; set => mail = value; }
        public Account Acc { get => acc; set => acc = value; }
        public string Position { get => position; set => position = value; }
        public string Cmnd { get => cmnd; set => cmnd = value; }
        public DateTime Birthday { get => birthday; set => birthday = value; }

        private string gender;
        public string Gender { get => gender; set => gender = value; }

        private byte[] data = new byte[99];
        BitmapImage bitimg = new BitmapImage();
        Image img = new Image();

        public byte[] Data { get => data; set => data = value; }
        public BitmapImage Bitimg { get => bitimg; set => bitimg = value; }
        public Image Img { get => img; set => img = value; }

        public byte[] convertImgToByte()
        {
            MemoryStream memStream = new MemoryStream();
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(this.bitimg));
            encoder.Save(memStream);
            return memStream.ToArray();
        }

        public BitmapImage convertImgFromByte()
        {
            using (var ms = new System.IO.MemoryStream(nhanvien.PICBI))
            {
                this.Bitimg = new BitmapImage();
                this.Bitimg.BeginInit();
                this.Bitimg.CacheOption = BitmapCacheOption.OnLoad; // here
                this.Bitimg.StreamSource = ms;
                this.Bitimg.EndInit();
                return this.Bitimg;
            }
        }

        public void chooseImg()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Chon anh(*.jpg; *.png; *.gif) | *.jpg; *.png; *.gif";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //HowKTeam
                this.bitimg = new BitmapImage(new Uri(dialog.FileName));
                /*this. name được phép trùng*/
                this.img = new Image();
                /*this.id Không được phép trùng do là khóa chính, phải là int */

                this.data = this.convertImgToByte();

                this.img.Source = this.bitimg;
                //this.addBinaryArrIntoSQL();
            }
        }


    }
}
