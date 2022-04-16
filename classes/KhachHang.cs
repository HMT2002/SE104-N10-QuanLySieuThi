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
    public class KhachHang
    {
        private string id;

        private string name;

        private string phone;

        private DateTime startdate;

        private DateTime birth;

        private decimal revenue;

        public SqlConnection ketnoi = new SqlConnection(@"Data Source=LAPTOP-H3DR409O\MSSQLSERVER01;Initial Catalog=QuanLySieuThi;Integrated Security=True");
        private DataTable datatable = new DataTable();
        private SqlDataAdapter adapter = new SqlDataAdapter();

        private List<KhachHang> listAll;

        public KhachHang(string id, string name, string phone, DateTime startdate, DateTime birth, decimal revenue, List<KhachHang> listAll)
        {
            this.id = id;
            this.name = name;
            this.phone = phone;
            this.startdate = startdate;
            this.birth = birth;
            this.revenue = revenue;
            this.listAll = listAll;
        }

        public KhachHang()
        {
        }

        public void getSpecificCustomerFromDatabase(string id)
        {
            this.ketnoi.Open();
            SqlCommand cmd = new SqlCommand(@"select MAKH,HOTEN,SODT,NGSINH,NGDK,DOANHSO,PICBI from KHACHHANG where MAKH='" + id + "'", this.ketnoi);
            using (SqlDataReader d = cmd.ExecuteReader())
            {
                if (d.Read())
                {
                    this.id = (string)d["MAKH"];
                    this.name = (string)d["HOTEN"];
                    this.phone = (string)d["SODT"];
                    this.birth = (DateTime)d["NGSINH"];
                    this.startdate = (DateTime)d["NGDK"];
                    this.revenue = (decimal)d["DOANHSO"];
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
        public void getAllCustomerFromDatabase()
        {
            listAll = new List<KhachHang>();
            ketnoi.Open();
            SqlCommand cmd = new SqlCommand(@"select MAKH,HOTEN,SODT,NGDK,NGSINH,DOANHSO,PICBI from KHACHHANG", this.ketnoi);

            Adapter = new SqlDataAdapter();
            Adapter.SelectCommand = cmd;

            Datatable = new DataTable();
            Adapter.Fill(Datatable);
            ketnoi.Close();

            for (int i = 0; i < Datatable.Rows.Count; i++)
            {
                KhachHang item = new KhachHang();
                item.id = Datatable.Rows[i]["MAKH"].ToString();
                item.name = Datatable.Rows[i]["HOTEN"].ToString();
                item.phone = Datatable.Rows[i]["SODT"].ToString();
                item.startdate = (DateTime)Datatable.Rows[i]["NGDK"];
                item.birth = (DateTime)Datatable.Rows[i]["NGSINH"];
                item.revenue=(decimal)Datatable.Rows[i]["DOANHSO"];
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
                this.img=new Image();
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
            SqlCommand cmd = new SqlCommand(@"select PICBI from KHACHHANG where MAKH = " + this.Id.ToString(), this.ketnoi);
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
                using (SqlCommand cmd = new SqlCommand(@"update KHACHHANG set PICBI = @binaryValue where MAKH = '" + this.id + "'", this.ketnoi))
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

        public byte[] Data { get => data; set => data = value; }
        public BitmapImage Bitimg { get => bitimg; set => bitimg = value; }
        public Image Img { get => img; set => img = value; }
        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Phone { get => phone; set => phone = value; }
        public DateTime Startdate { get => startdate; set => startdate = value; }
        public DateTime Birth { get => birth; set => birth = value; }
        public decimal Revenue { get => revenue; set => revenue = value; }
        public List<KhachHang> ListAll { get => listAll; set => listAll = value; }
        public SqlDataAdapter Adapter { get => adapter; set => adapter = value; }
        public DataTable Datatable { get => datatable; set => datatable = value; }
    }
}
