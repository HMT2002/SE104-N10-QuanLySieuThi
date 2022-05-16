using SE104_N10_QuanLySieuThi.classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Imaging;
namespace SE104_N10_QuanLySieuThi.ViewModel
{


    public class BillViewModel:BaseViewModel
    {
        public SanPham sp = new SanPham();

        public ObservableCollection<SanPham> list;

        private decimal _ThanhTien;

        public decimal ThanhTien { get => _ThanhTien; set { _ThanhTien = value; OnPropertyChanged(); } }

        public BillViewModel()
        {

            list = SellViewModel.SelectedBillList;
            foreach(SanPham sp in SellViewModel.SelectedBillList)
            {
                ThanhTien += (decimal)sp.sanpham.GIA;
            }

        }

    }
}
