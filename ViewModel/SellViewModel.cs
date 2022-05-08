using SE104_N10_QuanLySieuThi.classes;
using SE104_N10_QuanLySieuThi.windows;
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
    class SellViewModel:BaseViewModel
    {
        ListBox lstbxSelected = new ListBox();
        public ICommand LoadedItemCtrlCmd { get; set; }

        public ICommand BuyProductCmd { get; set; }

        public ICommand listboxSelectedItem_SelectionChangedCmd { get; set; }
        private int _Stt;
        public int Stt { get => _Stt; set { _Stt = value; OnPropertyChanged(); } }
        private string _MaSP;
        public string MaSP { get => _MaSP; set { _MaSP = value; OnPropertyChanged(); } }
        private string _TenSP;
        public string TenSP { get => _TenSP; set { _TenSP = value; OnPropertyChanged(); } }
        private long _SoLuongSP;
        public long SoLuongSP { get => _SoLuongSP; set { _SoLuongSP = value; OnPropertyChanged(); } }
        private long _DonGia;
        public long DonGia { get => _DonGia; set { _DonGia = value; OnPropertyChanged(); } }
        private long _ThanhTien;
        public long ThanhTien { get => _ThanhTien; set { _ThanhTien = value; OnPropertyChanged(); } }
        

        ObservableCollection<SanPham> _listSelecteditems;
        public ObservableCollection<SanPham> listSelecteditems { get => _listSelecteditems; set { _listSelecteditems = value; OnPropertyChanged(); } }

        public bool isMainLoaded = false;

        SanPham sp = new SanPham();

        
        public SellViewModel()
        {
            //sp.getAllProductFromDatabase();
            //listSelecteditems = new ObservableCollection<SanPham>(sp.getAllProductFromDatabase());
            LoadedItemCtrlCmd = new RelayCommand<ListBox>((p) => { lstbxSelected = p; return true; }, (p) => { if (!isMainLoaded) { AddItemIntoListBox(p); isMainLoaded = true; }; });
            ThanhTien = SoLuongSP * DonGia;
            BuyProductCmd = new RelayCommand<object>((p) => { return true; }, (p) => { openBill(); });
        }

        private void openBill()
        {
            winBill win = new winBill();
            win.ShowDialog();
        }

        private void AddItemIntoListBox(ListBox p)
        {

        }

        public void listboxSelectedItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox = sender as ListBox;
            MessageBox.Show(listBox.SelectedItem.ToString());
        }
    }
}
