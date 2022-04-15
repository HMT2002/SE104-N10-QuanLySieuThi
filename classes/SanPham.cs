using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE104_N10_QuanLySieuThi.classes
{
    class SanPham
    {
        private string id;

        private HinhAnh anh;

        private string name;

        private string dvt;

        private int amount;

        private decimal price;

        NhaCungCap supplier;

        public SqlConnection ketnoi = new SqlConnection(@"Data Source=LAPTOP-H3DR409O\MSSQLSERVER01;Initial Catalog=QuanLySieuThi;Integrated Security=True");
        private DataTable datatable=new DataTable();
        private SqlDataAdapter adapter=new SqlDataAdapter();

        private List<SanPham> listAll;

        public SanPham(string id, HinhAnh anh, string name, string dvt, int amount, decimal price, NhaCungCap supplier,List<SanPham> listAll)
        {
            this.id = id;
            this.anh = anh;
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
            this.anh = null;
            this.name = null;
            this.dvt = null;
            this.amount = 0;
            this.price = 0;
            this.supplier = null;
            this.listAll = null;
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

        public HinhAnh Anh { get => anh; set => anh = value; }
        public string Name { get => name; set => name = value; }
        public int Amount { get => amount; set => amount = value; }
        public decimal Price { get => price; set => price = value; }
        public string Id { get => id; set => id = value; }
        public string Dvt { get => dvt; set => dvt = value; }
        public NhaCungCap Supplier { get => supplier; set => supplier = value; }
        public DataTable Datatable { get => datatable; set => datatable = value; }
        public SqlDataAdapter Adapter { get => adapter; set => adapter = value; }
        internal List<SanPham> ListAll { get => listAll; set => listAll = value; }
    }
}
