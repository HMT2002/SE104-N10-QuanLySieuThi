using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE104_N10_QuanLySieuThi.classes
{
    class SanPham
    {
        private int id;

        private HinhAnh anh;

        private string name;

        private string dvt;

        private int amount;

        private float price;

        private string xuatxu;



        public HinhAnh Anh { get => anh; set => anh = value; }
        public string Name { get => name; set => name = value; }
        public int Amount { get => amount; set => amount = value; }
        public float Price { get => price; set => price = value; }
        public int Id { get => id; set => id = value; }
        public string Dvt { get => dvt; set => dvt = value; }
        public string Xuatxu { get => xuatxu; set => xuatxu = value; }
    }
}
