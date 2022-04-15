using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            SqlCommand cmd = new SqlCommand(@"select MANV,HOTEN,SODT,NGVL,LUONG from NHANVIEN where MANV='" + id + "'", this.ketnoi);
            using (SqlDataReader d = cmd.ExecuteReader())
            {
                if (d.Read())
                {
                    this.id = (string)d["MANV"];
                    this.name = (string)d["HOTEN"];
                    this.phone = (string)d["SODT"];
                    this.startdate = (DateTime)d["NGVL"];
                    this.salary = (int)d["LUONG"];

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
            SqlCommand cmd = new SqlCommand(@"select MANV,HOTEN,SODT,NGVL,LUONG from NHANVIEN", this.ketnoi);

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
                listAll.Add(item);

                //all of suppliers
            }

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
