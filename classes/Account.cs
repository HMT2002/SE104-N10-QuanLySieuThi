﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SE104_N10_QuanLySieuThi.classes
{
    class Account
    {
        private string acc;
        private int pri;
        private string password;

        public Account(string acc, string password)
        {
            this.Acc = acc;
            this.Password = password;
        }

        public string Acc { get => acc; set => acc = value; }
        public int Pri { get => pri; set => pri = value; }
        public string Password { get => password; set => password = value; }

        public SqlConnection ketnoi = new SqlConnection(@"Data Source=LAPTOP-H3DR409O\MSSQLSERVER01;Initial Catalog=QuanLySieuThi;Integrated Security=True");

        public bool CheckUser()
        {
            this.ketnoi.Open();
            try
            {
                string testpassword = "";
                SqlCommand cmd = new SqlCommand(@"select ACC,PASS,PRI from ACCOUNT where ACC = '" + this.Acc.ToString()+"'", this.ketnoi);
                using (SqlDataReader d = cmd.ExecuteReader())
                {
                    if (d.Read())
                    {
                        if (this.acc == (string)d["ACC"])
                        {
                            testpassword = (string)d["PASS"];
                            if (testpassword == this.Password)
                            {
                                this.Pri = (int)d["PRI"];
                                this.ketnoi.Close();
                                return true;
                            }
                        }

                    }
                }
                this.ketnoi.Close();
                return false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.ketnoi.Close();
                return false;
            }
        }
    }
}
