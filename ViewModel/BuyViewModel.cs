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
using System.Windows.Data;
using System.Windows.Input;


namespace SE104_N10_QuanLySieuThi.ViewModel
{
    public class BuyViewModel:BaseViewModel
    {
        private static BuyViewModel ins = new BuyViewModel();
        public ICommand LoadedGridItemCmd { get; set; }
        public ICommand openImportproductCmd { get; set; }

        private ObservableCollection<NhapHang> _DealingHistoryList;

        public ObservableCollection<NhapHang> DealingHistoryList { get => _DealingHistoryList; set { _DealingHistoryList = value; OnPropertyChanged(); } }


        SANPHAM curProduct = new SANPHAM();

        private string _ProductName;
        public string ProductName { get => _ProductName; set { _ProductName = value; OnPropertyChanged(); } }
        private string _ProductID;
        public string ProductID { get => _ProductID; set { _ProductID = value; OnPropertyChanged(); } }
        private string _ImportID;
        public string ImportID { get => _ImportID; set { _ImportID = value; OnPropertyChanged(); } }
        private DateTime _ImportDate;
        public DateTime ImportDate { get => _ImportDate; set { _ImportDate = value; OnPropertyChanged(); } }
        private List<string> _ListProduct = new List<string>();
        public List<string> ListProduct { get => _ListProduct; set { _ListProduct = value; OnPropertyChanged(); } }
        private string _SeletedProduct;
        public string SeletedProduct
        {
            get => _SeletedProduct; set
            {
                _SeletedProduct = value;
                if (SeletedProduct != null)
                {
                    var ncc = DataProvider.Ins.DB.SANPHAM.Where(x => x.TENSP == SeletedProduct).SingleOrDefault();
                    if (ncc != null)
                    {
                        curProduct = ncc;
                        ProductID = curProduct.MASP;
                        ProductName = curProduct.TENSP;
                        Ammount = (int)curProduct.SL;
                        Price = (decimal)curProduct.GIA;

                    }
                }
                OnPropertyChanged();
            }
        }
        private int _Ammount;
        public int Ammount
        {
            get => _Ammount; set
            {
                _Ammount = value;
                OnPropertyChanged();
            }
        }
        private decimal _SummaryImport;
        public decimal SummaryImport { get => _SummaryImport; set { _SummaryImport = value; OnPropertyChanged(); } }
        private int _AmmountImport;
        public int AmmountImport
        {
            get => _AmmountImport; set
            {
                _AmmountImport = value;
                SummaryImport = value * Price;
                OnPropertyChanged();
            }
        }
        private decimal _Price;
        public decimal Price
        {
            get => _Price; set
            {
                _Price = value;
                OnPropertyChanged();
            }
        }

        private List<string> _SearchType = new List<string>() { "ID", "Name" };

        public List<string> SearchType { get => _SearchType; set { _SearchType = value; OnPropertyChanged(); } }

        public CollectionView view;


        public ICommand ImportProductCmd { get; set; }



        public BuyViewModel()
        {
            openImportproductCmd = new RelayCommand<object>((p) => { return true; }, (p) => { openWinAddSupplier(); });
            ImportProductCmd = new RelayCommand<object>((p) => { return true; }, (p) => { ImportProduct(p); });
            ImportID = Converter.Instance.RandomString(5);
            ImportDate = DateTime.Now;
            Price = 0;
            AmmountImport = 0;

            LoadProductList();
            loadHistory();
        }
        private void openWinAddSupplier()
        {
            winImportProduct win = new winImportProduct();
            win.ShowDialog();

        }

        private void LoadProductList()
        {
            DealingHistoryList = new ObservableCollection<NhapHang>();
            ListProduct = new List<string>();
            var supplierList = DataProvider.Ins.DB.SANPHAM;
            foreach (var item in supplierList)
            {
                ListProduct.Add(item.TENSP);
            }


        }
        private void ImportProduct(object p)
        {
            while (DataProvider.Ins.DB.NHAPHANG.Where(x => x.MANH == ImportID).Count() > 0)
            {
                ImportID = Converter.Instance.RandomString(5);
            }
            var nv = new NHAPHANG() { MANH = ImportID, MASP = curProduct.MASP, MANV = MainViewModel._currentUser, NGNH = ImportDate, SLNHAPHANG = AmmountImport ,TRIGIA=SummaryImport};
            DataProvider.Ins.DB.NHAPHANG.Add(nv);

            var sp = DataProvider.Ins.DB.SANPHAM.Where(x => x.MASP == ProductID).SingleOrDefault();
            if (sp.SL == null)
            {
                sp.SL = 0;
            }
            sp.SL += AmmountImport;
            DataProvider.Ins.DB.SaveChanges();
            NewImport();
            LoadProductList();
            loadHistory();
            winImportProduct win = p as winImportProduct;
            win.Close();
        }

        private void NewImport()
        {
            SeletedProduct = null;
            AmmountImport = 0;
            SummaryImport = 0;

        }

        public void loadHistory()
        {
            DealingHistoryList = new ObservableCollection<NhapHang>();
            var dealing = DataProvider.Ins.DB.NHAPHANG;
            foreach (var item in dealing)
            {
                NhapHang nhaphang = new NhapHang();
                nhaphang.nhaphang = item;
                DealingHistoryList.Add(nhaphang);
            }
            view = (CollectionView)CollectionViewSource.GetDefaultView(DealingHistoryList);
            view.Filter = UserFilter;
        }

        private void FilterItem()
        {
            CollectionViewSource.GetDefaultView(DealingHistoryList).Refresh();
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
        private string _Search;
        public string Search
        {
            get => _Search; set
            {
                _Search = value;
                NewImport();
                view.Filter = UserFilter;
                FilterItem();
                OnPropertyChanged();
            }
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
                    return (obj as NhapHang).nhaphang.NHANVIEN.HOTEN.IndexOf(Search, StringComparison.OrdinalIgnoreCase) >= 0;

                case "ID":
                    return (obj as NhapHang).nhaphang.MANH.IndexOf(Search, StringComparison.OrdinalIgnoreCase) >= 0;

                default:
                    break;
            }

            return true; ;
        }

    }
}

