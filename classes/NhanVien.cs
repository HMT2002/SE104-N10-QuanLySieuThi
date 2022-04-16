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
    public class NhanVien
    {
        private string id;

        private string name;

        private string phone;

        private DateTime startdate;

        private float salary;

        public SqlConnection ketnoi = new SqlConnection(@"Data Source=LAPTOP-H3DR409O\MSSQLSERVER01;Initial Catalog=QuanLySieuThi;Integrated Security=True");
        private DataTable datatable = new DataTable();
        private SqlDataAdapter adapter = new SqlDataAdapter();

        private List<NhanVien> listAll;

        public NhanVien(string id, string name, string phone, DateTime startdate, float salary,List<NhanVien> listAll)
        {
            this.id = id;
            this.name = name;
            this.phone = phone;
            this.startdate = startdate;
            this.salary = salary;
            this.listAll = listAll;
        }
        public NhanVien()
        {

        }
        void getSpecificEmployeeFromDatabase(string id)
        {
            this.ketnoi.Open();
            SqlCommand cmd = new SqlCommand(@"select MANV,HOTEN,SODT,NGVL,LUONG,PICBI from NHANVIEN where MANV='" + id + "'", this.ketnoi);
            using (SqlDataReader d = cmd.ExecuteReader())
            {
                if (d.Read())
                {
                    this.id = (string)d["MANV"];
                    this.name = (string)d["HOTEN"];
                    this.phone = (string)d["SODT"];
                    this.startdate = (DateTime)d["NGVL"];
                    this.salary = (int)d["LUONG"];
                    this.data = (byte[])d["PICBI"];
                    this.bitimg = this.convertImgFromByte();
                    this.img = new Image();
                    this.img.Source = this.bitimg;
                    //Do stuff with salt and pass
                }
                else
                {
                    // NO User with email exists
                }

            }
            this.ketnoi.Close();
        }
        public void getAllEmployeeFromDatabase()
        {
            listAll = new List<NhanVien>();
            ketnoi.Open();
            SqlCommand cmd = new SqlCommand(@"select MANV,HOTEN,SODT,NGVL,LUONG,PICBI from NHANVIEN", this.ketnoi);

            Adapter = new SqlDataAdapter();
            Adapter.SelectCommand = cmd;

            Datatable = new DataTable();
            Adapter.Fill(Datatable);
            ketnoi.Close();

            for (int i = 0; i < Datatable.Rows.Count; i++)
            {
                NhanVien item = new NhanVien();
                item.id = Datatable.Rows[i]["MANV"].ToString();
                item.name = Datatable.Rows[i]["HOTEN"].ToString();
                item.phone = Datatable.Rows[i]["SODT"].ToString();
                item.startdate = (DateTime)Datatable.Rows[i]["NGVL"];
                item.salary = (int)Datatable.Rows[i]["LUONG"];
                item.data = (byte[])Datatable.Rows[i]["PICBI"];
                item.bitimg = item.convertImgFromByte();
                item.img = new Image();
                item.img.Source = item.bitimg;
                listAll.Add(item);

                //all of suppliers
            }

        }

        private byte[] data;
        BitmapImage bitimg;
        Image img;

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
                /*this. name được phép trùng*/
                this.img = new Image();
                /*this.id Không được phép trùng do là khóa chính, phải là int */
                this.data = this.convertImgToByte();
                this.img.Source = this.bitimg;
                this.addBinaryArrIntoSQL();


            }
        }

        public void openImgFromDatabase()
        {
            this.ketnoi.Open();
            this.bitimg = new BitmapImage();
            this.img = new Image();
            SqlCommand cmd = new SqlCommand(@"select PICBI from NHANVIEN where MANV = " + this.Id.ToString(), this.ketnoi);
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
            this.img.Source = this.bitimg;
            this.ketnoi.Close();
        }

        public void addBinaryArrIntoSQL()
        {
            this.ketnoi.Open();
            try
            {
                using (SqlCommand cmd = new SqlCommand(@"update NHANVIEN set PICBI = @binaryValue where MANV = '" + this.id + "'", this.ketnoi))
                {
                    // Replace 8000, below, with the correct size of the field
                    cmd.Parameters.Add("@binaryValue", SqlDbType.VarBinary, 5999999).Value = this.data;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.ketnoi.Close();
        }

        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Phone { get => phone; set => phone = value; }
        public DateTime Startdate { get => startdate; set => startdate = value; }
        public float Salary { get => salary; set => salary = value; }
        public List<NhanVien> ListAll { get => listAll; set => listAll = value; }
        public SqlDataAdapter Adapter { get => adapter; set => adapter = value; }
        public DataTable Datatable { get => datatable; set => datatable = value; }
    }
}
