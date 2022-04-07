using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE104_N10_QuanLySieuThi.classes
{
    class KhachHang
    {
        private int id;

        private string name;

        private string phone;

        private DateTime startdate;

        private DateTime birth;

        private float revenue;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Phone { get => phone; set => phone = value; }
        public DateTime Startdate { get => startdate; set => startdate = value; }
        public DateTime Birth { get => birth; set => birth = value; }
        public float Revenue { get => revenue; set => revenue = value; }
    }
}
