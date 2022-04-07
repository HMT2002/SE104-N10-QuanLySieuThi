using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE104_N10_QuanLySieuThi.classes
{
    class NhanVien
    {
        private int id;

        private string name;

        private string phone;

        private DateTime startdate;

        private float salary;



        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Phone { get => phone; set => phone = value; }
        public DateTime Startdate { get => startdate; set => startdate = value; }
        public float Salary { get => salary; set => salary = value; }
    }
}
