using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE104_N10_QuanLySieuThi.classes
{
    class HoaDon
    {
        private int id;

        private SanPham[] products;

        private NhanVien employe;

        private KhachHang customer;

        public SanPham[] Products { get => products; set => products = value; }
        public NhanVien Employe { get => employe; set => employe = value; }
        public KhachHang Customer { get => customer; set => customer = value; }
        public int Id { get => id; set => id = value; }
    }
}
