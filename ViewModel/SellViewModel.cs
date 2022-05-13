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
    class SellViewModel : BaseViewModel
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

        private List<string> _CategoryList = new List<string>() { "Cái", "Kg", "Trái", "Bao", "Lít", "Chai" };

        public List<string> CategoryList { get => _CategoryList; set { _CategoryList = value; OnPropertyChanged(); } }


        ObservableCollection<SanPham> _ListSelecteditems;
        public ObservableCollection<SanPham> ListSelecteditems { get => _ListSelecteditems; set { _ListSelecteditems = value; OnPropertyChanged(); } }



        public bool isMainLoaded = false;

        SanPham sp = new SanPham();
        private ObservableCollection<SanPham> _SanPhamList;

        public ObservableCollection<SanPham> SanPhamList { get => _SanPhamList; set { _SanPhamList = value; OnPropertyChanged(); } }

        public CollectionView view;

        private int _SelectAmmount = 1;
        public int SelectAmmount { get => _SelectAmmount; set { _SelectAmmount = value; OnPropertyChanged(); } }

        public SellViewModel()
        {
            ListSelecteditems = new ObservableCollection<SanPham>();
            LoadedItemCtrlCmd = new RelayCommand<ListBox>((p) => { lstbxSelected = p; return true; }, (p) => { if (!isMainLoaded) { LoadSanPhamData(); isMainLoaded = true; }; });
            ThanhTien = SoLuongSP * DonGia;
            BuyProductCmd = new RelayCommand<object>((p) => { return true; }, (p) => { openBill(); });
            SelectAmmount = 1;

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
        private SanPham _SelectedItem;
        public SanPham SelectedItem
        {
            get => _SelectedItem; set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    SanPham sp = new SanPham();
                    sp = SelectedItem;
                    ListSelecteditems.Add(sp);
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
                MessageBox.Show(ListSelecteditems[SelectedSelectItemIndex].Amount.ToString());
                OnPropertyChanged();
            }
        }

    }
}
