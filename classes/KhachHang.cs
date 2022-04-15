﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        void getSpecificProducrFromDatabase(string id)
        {
            this.ketnoi.Open();
            SqlCommand cmd = new SqlCommand(@"select MAKH,HOTEN,SODT,NGSINH,NGDK,DOANHSO from KHACHHANG where MAKH='" + id + "'", this.ketnoi);
            using (SqlDataReader d = cmd.ExecuteReader())
            {
                if (d.Read())
                {
                    this.id = (string)d["MANV"];
                    this.name = (string)d["HOTEN"];
                    this.phone = (string)d["SODY"];
                    this.birth = (DateTime)d["NGSINH"];
                    this.startdate = (DateTime)d["NGDK"];
                    this.revenue = (decimal)d["DOANHSO"];
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
            listAll = new List<KhachHang>();
            ketnoi.Open();
            SqlCommand cmd = new SqlCommand(@"select MAKH,HOTEN,SODT,NGSINH,DOANHSO from KHACHHANG", this.ketnoi);

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
                listAll.Add(item);

                //all of suppliers
            }

        }
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
