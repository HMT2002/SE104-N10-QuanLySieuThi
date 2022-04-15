using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE104_N10_QuanLySieuThi.classes
{
    public class NhaCungCap
    {
        private string id;

        private string name;

        private string country;

        private string number;

        
        public SqlConnection ketnoi = new SqlConnection(@"Data Source=LAPTOP-H3DR409O\MSSQLSERVER01;Initial Catalog=QuanLySieuThi;Integrated Security=True");
        private DataTable datatable = new DataTable();
        private SqlDataAdapter adapter = new SqlDataAdapter();

        private List<NhaCungCap> listAll;
        public NhaCungCap(string id, string name, string country, string number,List<NhaCungCap> listAll)
        {
            this.id = id;
            this.name = name;
            this.country = country;
            this.number = number;
            this.listAll = listAll;
        }
        public NhaCungCap()
        {
            this.id = null;
            this.name = null;
            this.country = null;
            this.number = null;
            this.listAll = null;
        }
        public void getSpecificSupplierFromDatabase()
        {
            this.ketnoi.Open();
            SqlCommand cmd = new SqlCommand(@"select MACC,TEN,SODT,XUATXU from NHACUNGCAP where MACC='" + this.id + "'", this.ketnoi);
            using (SqlDataReader d = cmd.ExecuteReader())
            {
                if (d.Read())
                {
                    this.id = (string)d["MACC"];
                    this.name = (string)d["TEN"];
                    this.country = (string)d["XUATXU"];
                    this.number = (string)d["SODT"];

                    //Do stuff with salt and pass
                }
                else
                {
                    // NO User with email exists
                }

            }
            this.ketnoi.Close();
        }

        public void getAllSupplierFromDatabase()
        {
            listAll = new List<NhaCungCap>();
            ketnoi.Open();
            SqlCommand cmd = new SqlCommand(@"select MACC,TEN,SODT,XUATXU from NHACUNGCAP", this.ketnoi);

            Adapter = new SqlDataAdapter();
            Adapter.SelectCommand = cmd;

            datatable = new DataTable();
            Adapter.Fill(datatable);
            ketnoi.Close();

            for (int i = 0; i < datatable.Rows.Count; i++)
            {
                NhaCungCap item = new NhaCungCap();
                item.id = datatable.Rows[i]["MACC"].ToString();
                item.name = datatable.Rows[i]["TEN"].ToString();
                item.number = datatable.Rows[i]["SODT"].ToString();
                item.country = datatable.Rows[i]["XUATXU"].ToString();
                listAll.Add(item);

                //all of suppliers
            }
        }


        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Country { get => country; set => country = value; }
        public string Number { get => number; set => number = value; }
        public DataTable Datatable { get => datatable; set => datatable = value; }
        public SqlDataAdapter Adapter { get => adapter; set => adapter = value; }
        public List<NhaCungCap> ListAll { get => listAll; set => listAll = value; }
    }
}
