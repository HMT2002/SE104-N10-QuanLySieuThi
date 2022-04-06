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
using System.Windows.Media.Imaging;

namespace SE104_N10_QuanLySieuThi.classes
{
    class HinhAnh
    {
        private int id;
        private string name;
        private byte[] data;
        BitmapImage bitimg;
        Image img;

        public HinhAnh(int id, string name, byte[] data=null, BitmapImage bitimg=null,Image img=null)
        {
            this.id = id;
            this.name = name;
            this.data = data;
            this.bitimg = bitimg;

            if (img == null)
            {
                this.img = new Image();
                this.img.Source = this.bitimg;
            }
            else
                this.img = img;

        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public byte[] Data { get => data; set => data = value; }
        public BitmapImage Bitimg { get => bitimg; set => bitimg = value; }
        public Image Img { get => img; set => img = value; }

        public SqlConnection ketnoi = new SqlConnection(@"Data Source=LAPTOP-H3DR409O\MSSQLSERVER01;Initial Catalog=QuanLySieuThi;Integrated Security=True");

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
            using (var ms = new System.IO.MemoryStream(this.data))
            {
                this.Bitimg = new BitmapImage();
                this.Bitimg.BeginInit();
                this.Bitimg.CacheOption = BitmapCacheOption.OnLoad; // here
                this.Bitimg.StreamSource = ms;
                this.Bitimg.EndInit();
                return this.Bitimg;
            }
        }

        public void chooseImgAndAddToDatabase()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Chon anh(*.jpg; *.png; *.gif) | *.jpg; *.png; *.gif";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //HowKTeam
                this.bitimg = new BitmapImage(new Uri(dialog.FileName));
                /*this. name được phép trùng*/;
                /*this.id Không được phép trùng do là khóa chính, phải là int */
                this.data = convertImgToByte();
                this.img.Source = this.bitimg;
                addBinaryArrIntoSQL();

            }
        }

        public void openImgFromDatabase()
        {
            this.ketnoi.Open();
            this.bitimg = new BitmapImage();
            SqlCommand cmd = new SqlCommand(@"select PICBI from HINHANH where PICID = "+this.Id.ToString(), this.ketnoi);
            using (SqlDataReader d = cmd.ExecuteReader())
            {
                if (d.Read())
                {
                    this.data = (byte[])d["PICBI"];
                    this.bitimg = convertImgFromByte();
                    //Do stuff with salt and pass
                }
                else
                {
                    // NO User with email exists
                }
            }
            this.ketnoi.Close();
            this.img.Source = this.bitimg;
        }

        public void addBinaryArrIntoSQL()
        {
            this.ketnoi.Open();
            using (SqlCommand cmd = new SqlCommand(@"INSERT INTO HINHANH (PICID, PICNAME, PICBI) VALUES (" + this.id + ", '" + this.name + "',@binaryValue)", this.ketnoi))
            {
                // Replace 8000, below, with the correct size of the field
                cmd.Parameters.Add("@binaryValue", SqlDbType.VarBinary, 999999).Value = this.data;
                cmd.ExecuteNonQuery();
            }
            this.ketnoi.Close();
        }
    }
}
