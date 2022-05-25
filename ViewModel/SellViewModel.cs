using SE104_N10_QuanLySieuThi.classes;
using SE104_N10_QuanLySieuThi.Model;
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
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;


namespace SE104_N10_QuanLySieuThi.ViewModel
{
    public class SellViewModel : BaseViewModel
    {
        ListBox lstbxSelected = new ListBox();
        public ICommand LoadedItemCtrlCmd { get; set; }

        public ICommand BuyProductCmd { get; set; }
        public ICommand ConfirmPaymentCmd { get; set; }


        public ICommand listboxSelectedItem_SelectionChangedCmd { get; set; }

        public ICommand IncreaseSelectAmmountCmd { get; set; }
        public ICommand DecreaseSelectAmmountCmd { get; set; }


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
        private decimal _ThanhTien;
        public decimal ThanhTien { get => _ThanhTien; set { _ThanhTien = value; OnPropertyChanged(); } }

        private decimal _ThanhTienCoGiamGia;
        public decimal ThanhTienCoGiamGia { get => _ThanhTienCoGiamGia; set { _ThanhTienCoGiamGia = value; OnPropertyChanged(); } }

        private float _GiamGia;
        public float GiamGia { get => _GiamGia; set { _GiamGia = value; OnPropertyChanged(); } }

        private List<string> _CategoryList = new List<string>() { "Cái", "Kg", "Trái", "Bao", "Lít", "Chai" };

        public List<string> CategoryList { get => _CategoryList; set { _CategoryList = value; OnPropertyChanged(); } }

        private ObservableCollection<SanPham> _ListSelecteditems;
        public ObservableCollection<SanPham> ListSelecteditems { get => _ListSelecteditems; set { _ListSelecteditems = value; OnPropertyChanged(); } }

        public static ObservableCollection<SanPham> SelectedBillList { get; set; }


        public bool isMainLoaded = false;

        SanPham sp = new SanPham();
        private ObservableCollection<SanPham> _SanPhamList;

        public ObservableCollection<SanPham> SanPhamList { get => _SanPhamList; set { _SanPhamList = value; OnPropertyChanged(); } }

        public CollectionView view;

        private int _SelectAmmount = 1;
        public int SelectAmmount { get => _SelectAmmount; set { _SelectAmmount = value; OnPropertyChanged(); } }

        public KhachHang _Khachhang = new KhachHang();
        public KhachHang Khachhang { get => _Khachhang; set { _Khachhang = value; OnPropertyChanged(); } }

        public NhanVien _Nhanvien = new NhanVien();
        public NhanVien Nhanvien { get => _Nhanvien; set { _Nhanvien = value; OnPropertyChanged(); } }

        private string _EmployeeId;
        public string EmployeeId { get => _EmployeeId; set { _EmployeeId = value; OnPropertyChanged(); } }

        private string _CustomerId;
        public string CustomerId { get => _CustomerId; set { _CustomerId = value; OnPropertyChanged(); } }


        private List<KhachHang> _ListCustomer = new List<KhachHang>();
        public List<KhachHang> ListCustomer { get => _ListCustomer; set { _ListCustomer = value; OnPropertyChanged(); } }

        private List<NhanVien> _ListEmployee = new List<NhanVien>();
        public List<NhanVien> ListEmployee { get => _ListEmployee; set { _ListEmployee = value; OnPropertyChanged(); } }
        private KhachHang _SeletedCustomer;
        public KhachHang SeletedCustomer { get => _SeletedCustomer; set { 
                _SeletedCustomer = value;
                if (SeletedCustomer != null)
                {
                    CustomerId = SeletedCustomer.khachhang.MAKH;

                }
                OnPropertyChanged();
            } 
        }
        private NhanVien _SeletedEmployee;
        public NhanVien SeletedEmployee { get => _SeletedEmployee; set { 
                _SeletedEmployee = value;
                if (SeletedEmployee != null)
                {
                    EmployeeId = SeletedEmployee.nhanvien.MANV;

                }
                OnPropertyChanged(); 
            } 
        }

        public SellViewModel()
        {
            ListSelecteditems = new ObservableCollection<SanPham>();
            LoadedItemCtrlCmd = new RelayCommand<ListBox>((p) => { lstbxSelected = p; return true; }, (p) => { if (!isMainLoaded) { LoadSanPhamData(); isMainLoaded = true; }; });
            BuyProductCmd = new RelayCommand<object>((p) => { return true; }, (p) => { openBill(); });
            ConfirmPaymentCmd = new RelayCommand<Window>((p) => { return true; }, (p) => { confirmPayment(p); });

            IncreaseSelectAmmountCmd = new RelayCommand<object>((p) => { return true; }, (p) => { Increase(p); });
            DecreaseSelectAmmountCmd = new RelayCommand<object>((p) => { return true; }, (p) => { Decrease(p); });

            SelectAmmount = 1;
            GiamGia = 0;
            loadListCustomer();
            loadListEmployee();
        }

        private void Decrease(object p)
        {
            if (p is ListViewItem)
            {
                ListViewItem item = (ListViewItem)p;
                SelectedSelectItem = item.Content as SanPham;
            }
            SelectedSelectItem.Amount--;

            if (SelectedSelectItem.Amount <= 0)
            {
                
                ListSelecteditems.Remove(SelectedSelectItem);
                SelectedItem = null;
            }
        }

        public void Increase(object p)
        {

            if (p is ListViewItem)
            {
                ListViewItem item = (ListViewItem)p;
                SelectedSelectItem = item.Content as SanPham;
            }
            var sanpham = DataProvider.Ins.DB.SANPHAM.Where(x => x.MASP == SelectedSelectItem.sanpham.MASP).SingleOrDefault();
            if (sanpham.SL < SelectedSelectItem.Amount)
            {
                MessageBox.Show("đã hết hàng");
            }
            SelectedSelectItem.Amount++;

        }


        private void loadListCustomer()
        {
            var list = DataProvider.Ins.DB.KHACHHANG;
            foreach(var item in list)
            {
                KhachHang kh = new KhachHang();
                kh.khachhang = item;
                ListCustomer.Add(kh);
            }
        }

        private void loadListEmployee()
        {
            var list = DataProvider.Ins.DB.NHANVIEN;
            foreach (var item in list)
            {
                NhanVien nv = new NhanVien();
                nv.nhanvien = item;
                ListEmployee.Add(nv);
            }
        }

        private void confirmPayment(Window p)
        {
            string sohd = Converter.Instance.RandomString(5);
            while (DataProvider.Ins.DB.HOADON.Where(x => x.SOHD == sohd).Count() > 0)
            {
                sohd = Converter.Instance.RandomString(5);
            }
            var hd = new HOADON() { SOHD = sohd,NGHD=DateTime.Now,MANV=MainViewModel._currentUser,MAKH=Khachhang.khachhang.MAKH,TRIGIA=ThanhTienCoGiamGia ,GIAMGIA=GiamGia};
            foreach (SanPham sp in ListSelecteditems)
            {
                var cthd = new CTHD() { SOHD = sohd, MASP = sp.sanpham.MASP, SL = sp.Amount,};
                DataProvider.Ins.DB.CTHD.Add(cthd);

                var sanpham = DataProvider.Ins.DB.SANPHAM.Where(x => x.MASP == sp.sanpham.MASP).SingleOrDefault();
                sanpham.SL -= sp.Amount;
            }
            DataProvider.Ins.DB.HOADON.Add(hd);
            DataProvider.Ins.DB.SaveChanges();
            winBill win = p as winBill;
            win.Close();
        }

        private void LoadSanPhamData()
        {
            SanPhamList = new ObservableCollection<SanPham>();

            var objectList = DataProvider.Ins.DB.SANPHAM;
            foreach (var item in objectList)
            {
                SanPham sanpham = new SanPham();
                sanpham.sanpham = item;
                sanpham.Amount = 0;
                sanpham.Bitimg = Converter.Instance.ConvertByteToBitmapImage(item.PICBI);
                sanpham.Img.Source = sanpham.Bitimg;
                SanPhamList.Add(sanpham);
            }
            view = (CollectionView)CollectionViewSource.GetDefaultView(SanPhamList);
            view.Filter = UserFilter;

        }
        public bool UserFilter(object obj)
        {
            if (string.IsNullOrEmpty(Search))
            {
                return true;
            }
            switch (SearchTypeItem)
            {
                case "Name":
                    return (obj as SanPham).sanpham.TENSP.IndexOf(Search, StringComparison.OrdinalIgnoreCase) >= 0;
                case "ID":
                    return (obj as SanPham).sanpham.MASP.IndexOf(Search, StringComparison.OrdinalIgnoreCase) >= 0;
                default:
                    break;
            }

            return true; ;
        }
        private string _Search;

        public string Search
        {
            get => _Search; set
            {
                _Search = value;
                view.Filter = UserFilter;
                FilterItem();
                OnPropertyChanged();
            }
        }
        private void FilterItem()
        {
            CollectionViewSource.GetDefaultView(SanPhamList).Refresh();
        }
        private string _SearchTypeItem;
        public string SearchTypeItem
        {
            get => _SearchTypeItem; set
            {
                _SearchTypeItem = value;
                view.Filter = UserFilter;
                OnPropertyChanged();
            }
        }
        private void openBill()
        {
            Khachhang.khachhang = DataProvider.Ins.DB.KHACHHANG.Where(x => x.MAKH == CustomerId).SingleOrDefault();
            Nhanvien.nhanvien = DataProvider.Ins.DB.NHANVIEN.Where(x => x.MANV == EmployeeId).SingleOrDefault();
            if (Khachhang.khachhang == null || Nhanvien.nhanvien == null)
            {
                return;
            }

            SelectedBillList = ListSelecteditems;
            foreach (SanPham sp in ListSelecteditems)
            {
                ThanhTien += (decimal)sp.sanpham.GIA * sp.Amount;
            }

            applyDisccount();
            ThanhTienCoGiamGia = ThanhTien - (ThanhTien / 100 * (decimal)GiamGia);
            winBill win = new winBill();
            win.ShowDialog();
        }

        public void applyDisccount()
        {
            if (Khachhang.khachhang.DOANHSO >= 200000)
            {
                GiamGia = 10;
            }
            if(Khachhang.khachhang.DOANHSO >= 300000)
            {
                GiamGia = 20;

            }
            if(Khachhang.khachhang.DOANHSO>= 500000)
            {
                GiamGia = 30;

            }
        }

        private void AddItemIntoListBox(ListBox p)
        {

        }

        private SanPham _SelectedItem;
        public SanPham SelectedItem
        {
            get => _SelectedItem; set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    if (SelectedItem.sanpham.SL == null)
                    {
                        return;
                    }
                    if (SelectedItem.sanpham.SL <= 0)
                    {
                        return;
                    }
                    if (SelectedItem.Amount>0)
                    {
                        return;
                    }
                    
                    SanPham sp = new SanPham();
                    sp = SelectedItem;
                    SelectedItem.Amount = 1;
                    ListSelecteditems.Add(SelectedItem);
                }
            }
        }
        private SanPham _SelectedSelectItem;
        public SanPham SelectedSelectItem
        {
            get => _SelectedSelectItem; set
            {
                _SelectedSelectItem = value;
                if (SelectedSelectItem != null)
                {

                }

                OnPropertyChanged();
            }
        }
        private int _SelectedSelectItemIndex;
        public int SelectedSelectItemIndex
        {
            get => _SelectedSelectItemIndex; set
            {
                _SelectedSelectItemIndex = value;
                OnPropertyChanged();
            }
        }

    }
}
