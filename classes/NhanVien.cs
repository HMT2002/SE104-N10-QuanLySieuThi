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
        private string id;

        private string name;

        private string mail;

        Account acc=new Account();

        private string phone;

        private string startdate;

        private decimal salary;

        private string position;

        private string cmnd;

        private string birthday;

        public SqlConnection ketnoi = new SqlConnection(@"Data Source=LAPTOP-H3DR409O\MSSQLSERVER01;Initial Catalog=QuanLySieuThi;Integrated Security=True");
        private DataTable datatable = new DataTable();
        private SqlDataAdapter adapter = new SqlDataAdapter();

        private List<NhanVien> listAll;

        public ICommand ClickedItemCtrlCmd { get; set; }


        public NhanVien(string id, string name, string phone, string startdate, decimal salary,string position,string cmnd,List<NhanVien> listAll,string mail,Account acc,string birthday)
        {
            this.id = id;
            this.name = name;
            this.phone = phone;
            this.startdate = startdate;
            this.salary = salary;
            this.listAll = listAll;
            this.mail = mail;
            this.acc = acc;
            this.position = position;
            this.cmnd = cmnd;
            this.birthday = birthday;
        }
        public NhanVien()
        {
            this.id = null;
            this.name = null;
            this.phone = null;
            this.startdate = null;
            this.salary = 0;
            this.listAll = null;
            this.mail = null;
            this.acc = null;
            this.position = null;
            this.cmnd = null;
            this.birthday = null;
            ClickedItemCtrlCmd = new RelayCommand<System.Windows.Controls.Button>((p) => { return true; }, (p) => { SelectedEmployee(); });

        }
        public NhanVien SelectedEmployee()
        {
            NhanVien nv = new NhanVien();
            nv.id = this.id;
            nv.name = this.name;
            nv.phone = this.phone;
            nv.startdate = this.startdate;
            nv.salary = this.salary;
            nv.listAll = this.listAll;
            nv.mail = this.mail;
            nv.acc = this.acc;
            nv.position = this.position;
            nv.cmnd = this.cmnd;
            nv.birthday = this.birthday;
            MessageBox.Show("1234567980"+nv.id);
                return nv;
        }
        public void getSpecificEmployeeFromDatabase(string id)
        {
            try
            {
                this.ketnoi.Open();
                SqlCommand cmd = new SqlCommand(@"select * from NHANVIEN where MANV='" + id + "'", this.ketnoi);
                using (SqlDataReader d = cmd.ExecuteReader())
                {
                    if (d.Read())
                    {
                        this.id = (string)d["MANV"];
                        this.name = (string)d["HOTEN"];
                        this.phone = (string)d["SODT"];
                        this.startdate = (string)d["NGVL"];
                        this.salary = (decimal)d["LUONG"];
                        this.data = (byte[])d["PICBI"];
                        this.bitimg = this.convertImgFromByte();
                        this.img = new Image();
                        this.img.Source = this.bitimg;
                        this.mail = (string)d["MAIL"];
                        this.position = (string)d["POSITION"];
                        this.cmnd = (string)d["CMND"];
                        this.birthday = (string)d["NGSINH"];

                        //Do stuff with salt and pass
                    }
                    else
                    {
                        // NO User with email exists
                    }
                }
                this.ketnoi.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public void getAllEmployeeFromDatabase()
        {
            try
            {
                listAll = new List<NhanVien>();
                ketnoi.Open();
                SqlCommand cmd = new SqlCommand(@"select * from NHANVIEN", this.ketnoi);

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
                    item.startdate = (string)Datatable.Rows[i]["NGVL"];
                    item.salary = (decimal)Datatable.Rows[i]["LUONG"];
                    item.data = (byte[])Datatable.Rows[i]["PICBI"];
                    item.bitimg = item.convertImgFromByte();
                    item.img = new Image();
                    item.img.Source = item.bitimg;
                    item.mail = (string)Datatable.Rows[i]["MAIL"];
                    item.position = (string)Datatable.Rows[i]["POSITION"];
                    item.cmnd = (string)Datatable.Rows[i]["CMND"];
                    item.birthday = (string)Datatable.Rows[i]["NGSINH"];
                    listAll.Add(item);

                    //all of suppliers
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("getAllEmployeeFromDatabase: " + ex.Message);
            }

        }

        private byte[] data=new byte[99];
        BitmapImage bitimg=new BitmapImage();
        Image img=new Image();

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
                this.img = new Image();
                /*this.id Không được phép trùng do là khóa chính, phải là int */
                this.data = this.convertImgToByte();
                this.img.Source = this.bitimg;
                //this.addBinaryArrIntoSQL();


            }
        }

        public void openImgFromDatabase()
        {

            try
            {
                this.ketnoi.Open();

                this.bitimg = new BitmapImage();
                this.img = new Image();
                SqlCommand cmd = new SqlCommand(@"select PICBI from NHANVIEN where MANV = '" + this.Id + "'", this.ketnoi);
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
            catch (Exception ex)
            {
                MessageBox.Show("openImgFromDatabase: "+ex.Message);
            }
        }

        public void addBinaryArrIntoSQL()
        {
            if (this.bitimg == null)
            {
                return;
            }
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
        public string Startdate { get => startdate; set => startdate = value; }
        public decimal Salary { get => salary; set => salary = value; }
        public List<NhanVien> ListAll { get => listAll; set => listAll = value; }
        public SqlDataAdapter Adapter { get => adapter; set => adapter = value; }
        public DataTable Datatable { get => datatable; set => datatable = value; }
        public string Mail { get => mail; set => mail = value; }
        public Account Acc { get => acc; set => acc = value; }
        public string Position { get => position; set => position = value; }
        public string Cmnd { get => cmnd; set => cmnd = value; }
        public string Birthday { get => birthday; set => birthday = value; }

        public bool RegistEmployee()
        {

            try
            {
                this.ketnoi.Open();
                using (SqlCommand cmd = new SqlCommand(@"insert into NHANVIEN(MANV,HOTEN,SODT,NGVL,LUONG,MAIL,POSITION,CMND,NGSINH)values('"+this.id+
                    "','"+this.name+"','"+this.phone+"','"+this.startdate+"','"+this.salary+"','"+this.mail+"','" + this.position + "','" + this.cmnd + "','" + this.birthday + "')", this.ketnoi))
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

        public bool DeleteEmployee()
        {

            try
            {
                this.ketnoi.Open();
                using (SqlCommand cmd = new SqlCommand(@"delete from NHANVIEN where MANV = '"+this.id+"'", this.ketnoi))
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

        public bool ModifyEmployee()
        {

            try
            {
                this.ketnoi.Open();
                using (SqlCommand cmd = new SqlCommand(@"update into NHANVIEN(MANV,HOTEN,SODT,NGVL,LUONG,MAIL,POSITION,CMND,NGSINH)values('" + this.id +
                    "','" + this.name + "','" + this.phone + "','" + this.startdate + "','" + this.salary + "','" + this.mail + "','" + this.position + "','" + this.cmnd + "','" + this.birthday + "')", this.ketnoi))
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
