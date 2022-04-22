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
    public class SanPham
    {
        private string id;

        private string name;

        private string dvt;

        private int amount;

        private decimal price;

        NhaCungCap supplier;

        public SqlConnection ketnoi = new SqlConnection(@"Data Source=LAPTOP-H3DR409O\MSSQLSERVER01;Initial Catalog=QuanLySieuThi;Integrated Security=True");
        private DataTable datatable=new DataTable();
        private SqlDataAdapter adapter=new SqlDataAdapter();

        private List<SanPham> listAll;

        public SanPham(string id, string name, string dvt, int amount, decimal price, NhaCungCap supplier,List<SanPham> listAll)
        {
            this.id = id;
            this.name = name;
            this.dvt = dvt;
            this.amount = amount;
            this.price = price;
            this.supplier = supplier;
            this.listAll = listAll;
        }


        public SanPham()
        {
            this.id = null;
            this.name = null;
            this.dvt = null;
            this.amount = 0;
            this.price = 0;
            this.supplier = null;
            this.listAll = null;
            this.data = new byte[99];
            this.img = new Image();
            this.img.Source = bitimg;
        }
        void getSpecificProductFromDatabase(string id)
        {
            this.ketnoi.Open();
            SqlCommand cmd = new SqlCommand(@"select MASP,TENSP,SODT,DVT,MACC,GIA,SL from SANPHAM where MASP='" + id + "'", this.ketnoi);
            using (SqlDataReader d = cmd.ExecuteReader())
            {
                if (d.Read())
                {
                    this.id = (string)d["MASP"];
                    this.name = (string)d["TENSP"];
                    this.dvt = (string)d["DVT"];
                    this.supplier.Id = (string)d["MACC"];
                    this.supplier.getSpecificSupplierFromDatabase();
                    this.price = (decimal)d["GIA"];
                    this.amount = (int)d["SL"];

                    //Do stuff with salt and pass
                }
                else
                {
                    // NO User with email exists
                }

            }
            this.ketnoi.Close();
        }

        public void getAllProductFromDatabase()
        {
            listAll = new List<SanPham>();
            ketnoi.Open();
            SqlCommand cmd = new SqlCommand(@"select MASP,TENSP,DVT,MACC,GIA,SL from SANPHAM", this.ketnoi);

            Adapter = new SqlDataAdapter();
            Adapter.SelectCommand = cmd;

            datatable = new DataTable();
            Adapter.Fill(datatable);
            ketnoi.Close();

            for (int i = 0; i < datatable.Rows.Count; i++)
            {
                SanPham item = new SanPham();
                item.id = datatable.Rows[i]["MASP"].ToString();
                item.name = datatable.Rows[i]["TENSP"].ToString();
                item.dvt = datatable.Rows[i]["DVT"].ToString();
                item.supplier = new NhaCungCap();
                item.supplier.Id = datatable.Rows[i]["MACC"].ToString();
                item.supplier.getSpecificSupplierFromDatabase();
                item.price =(decimal)datatable.Rows[i]["GIA"];
                item.amount = (int)datatable.Rows[i]["SL"];
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

        public void chooseImg()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Chon anh(*.jpg; *.png; *.gif) | *.jpg; *.png; *.gif";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //HowKTeam
                this.bitimg = new BitmapImage(new Uri(dialog.FileName));
                /*this. name được phép trùng*/
                ;
                /*this.id Không được phép trùng do là khóa chính, phải là int */
                this.data = convertImgToByte();
                this.img.Source = this.bitimg;

            }
        }

        public void openImgFromDatabase()
        {
            this.ketnoi.Open();
            this.bitimg = new BitmapImage();
            this.img = new Image();
            SqlCommand cmd = new SqlCommand(@"select PICBI from SANPHAM where MASP = " + this.Id.ToString(), this.ketnoi);
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
                using (SqlCommand cmd = new SqlCommand(@"update SANPHAM set PICBI = @binaryValue where MASP = '"+this.id+"'", this.ketnoi))
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

        public string Name { get => name; set => name = value; }
        public int Amount { get => amount; set => amount = value; }
        public decimal Price { get => price; set => price = value; }
        public string Id { get => id; set => id = value; }
        public string Dvt { get => dvt; set => dvt = value; }
        public NhaCungCap Supplier { get => supplier; set => supplier = value; }
        public DataTable Datatable { get => datatable; set => datatable = value; }
        public SqlDataAdapter Adapter { get => adapter; set => adapter = value; }
        internal List<SanPham> ListAll { get => listAll; set => listAll = value; }

        public bool RegistProduct()
        {

            try
            {
                this.ketnoi.Open();
                using (SqlCommand cmd = new SqlCommand(@"insert into SANPHAM(MASP,TENSP,DVT,MACC,GIA,SL,PICBI)values('"+this.id+"','"+this.name+"','"+this.dvt+"','"+this.Supplier.Id+"',"+this.price+","+this.amount+")", this.ketnoi))
                {
                    cmd.ExecuteNonQuery();

                }
                this.ketnoi.Close();
                this.addBinaryArrIntoSQL();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.ketnoi.Close();
                return false;
            }
        }

        public bool DeleteProduct()
        {

            try
            {
                this.ketnoi.Open();
                using (SqlCommand cmd = new SqlCommand(@"delete from SANPHAM where MASP = '" + this.id + "'", this.ketnoi))
                {
                    cmd.ExecuteNonQuery();

                }
                this.ketnoi.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.ketnoi.Close();
                return false;
            }
        }

        public bool ModifyProduct()
        {

            try
            {
                this.ketnoi.Open();
                using (SqlCommand cmd = new SqlCommand(@"update into SANPHAM(MASP,TENSP,DVT,MACC,GIA,SL)values('" + this.id + "','" + this.name + "','" + this.dvt + "','" + this.Supplier.Id + "'," + this.price + "," + this.amount + ")", this.ketnoi))
                {
                    cmd.ExecuteNonQuery();
                }
                this.ketnoi.Close();
                this.addBinaryArrIntoSQL();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.ketnoi.Close();
                return false;
            }
        }
    }
}
