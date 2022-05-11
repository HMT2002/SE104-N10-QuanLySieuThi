﻿using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
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
using System.Windows.Media.Imaging;

namespace SE104_N10_QuanLySieuThi.ViewModel
{
    class ProducViewModel : BaseViewModel
    {
        public NhanVien nv = new NhanVien();

        public static string _currentUser;

        public ICommand PickImage { get; set; }
        public ICommand LoadAvaterCmd { get; set; }
        public Button btnAvatar = new Button();
        public Image imgAvatar = new Image();
        public BitmapImage Bitimg { get => bitimg; set => bitimg = value; }
        private BitmapImage bitimg = new BitmapImage();

        private string _ProductId;
        public string ProductId { get => _ProductId; set { _ProductId = value; OnPropertyChanged(); } }

        private string _ImportId;
        public string ImportId { get => _ImportId; set { _ImportId = value; OnPropertyChanged(); } }

        private string _SuppliertId;
        public string SuppliertId { get => _SuppliertId; set { _SuppliertId = value; OnPropertyChanged(); } }

        private string _ProductName;
        public string ProductName { get => _ProductName; set { _ProductName = value; OnPropertyChanged(); } }
        private string _Note;
        public string Note { get => _Note; set { _Note = value; OnPropertyChanged(); } }

        private string _Phone;
        public string Phone { get => _Phone; set { _Phone = value; OnPropertyChanged(); } }
        private string _Origin;
        public string Origin { get => _Origin; set { _Origin = value; OnPropertyChanged(); } }

        private string _SuppliertName;
        public string SuppliertName { get => _SuppliertName; set { _SuppliertName = value; OnPropertyChanged(); } }

        private int _Ammount;
        public int Ammount { get => _Ammount; set
            { 
                _Ammount = value;
                Summary = value * Price;
                OnPropertyChanged();
            }
        }

        private decimal _Price;
        public decimal Price { get => _Price; set 
            {
                _Price = value;
                Summary = Ammount * value;
                OnPropertyChanged();
            } 
        }

        private decimal _Summary;
        public decimal Summary { get => _Summary; set { _Summary = value; OnPropertyChanged(); } }

        private DateTime _ImportDate;
        public DateTime ImportDate { get => _ImportDate; set { _ImportDate = value; OnPropertyChanged(); } }

        public SanPham sp=new SanPham();
        public ObservableCollection<SanPham> SP { get; set; }

        private SanPham _SelectedItem;
        public SanPham SelectedItem
        {
            get => _SelectedItem; set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    SelectedItem.sanpham.GIA = 0;
                }
            }
        }




        public ICommand BillCommand { get; set; }
        public ICommand openNewSupplierCommand { get; set; }


        public ICommand LoadedPageCmd { get; set; }
        public ICommand LoadedItemCtrlCmd { get; set; }

        public ICommand AddProductCmd { get; set; }


        public ICommand DeleteProductCmd { get; set; }
        public ICommand CompleteCmd { get; set; }


        public int countclick = 0;
        public TextBlock OrClickMeCount=new TextBlock();


        public bool isMainLoaded=false;


        public SqlConnection ketnoi = new SqlConnection(@"Data Source=LAPTOP-H3DR409O\MSSQLSERVER01;Initial Catalog=QuanLySieuThi;Integrated Security=True");

        private ObservableCollection<SanPham> _TonKhoList;

        public ObservableCollection<SanPham> TonKhoList { get => _TonKhoList; set { _TonKhoList = value; OnPropertyChanged(); } }

        private ObservableCollection<SanPham> _NhapHangList;

        public ObservableCollection<SanPham> NhapHangList { get => _NhapHangList; set { _NhapHangList = value; OnPropertyChanged(); } }

        private List<string> _ProductType = new List<string>() { "Cái", "Kg","Trái","Bao","Lít","Chai" ,"Gói"};

        public List<string> ProductType { get => _ProductType; set { _ProductType = value; OnPropertyChanged(); } }

        private List<string> _SupplierType = new List<string>();

        public List<string> SupplierType { get => _SupplierType; set { _SupplierType = value; OnPropertyChanged(); } }
        private string _SeletedProductType;
        public string SeletedProductType { get => _SeletedProductType; set { _SeletedProductType = value; OnPropertyChanged(); } }
        private string _SeletedSupplierType;
        public string SeletedSupplierType { get => _SeletedSupplierType; set { _SeletedSupplierType = value; OnPropertyChanged(); } }
        public ProducViewModel()
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            Ammount = 0;
            Price = 0;
            ImportDate = DateTime.Now;
            Phone = "";
            Origin = "";
            SuppliertName = "";
            
            Bitimg = null;
            sp = new SanPham();
            LoadAvaterCmd = new RelayCommand<Button>((p) => { btnAvatar = p; return true; }, (p) => { CreateAvatar(p); });
            PickImage = new RelayCommand<Button>((p) => { btnAvatar = p; return true; }, (p) => { Imagepick(p); });

            BillCommand = new RelayCommand<object>((p) => { return true; }, (p) => { openWinAddNewProduct(p); });
            openNewSupplierCommand = new RelayCommand<object>((p) => { return true; }, (p) => { openWinAddSupplier(); });

            LoadedPageCmd = new RelayCommand<Page>((p) => { return true; }, (p) => { LoadPage(p); });
            LoadedItemCtrlCmd= new RelayCommand<ItemsControl>((p) => { return true; }, (p) => { if (!isMainLoaded) {LoadTonKhoData(); AddItemIntoItemCtrol(p);isMainLoaded = true; }; });

            AddProductCmd = new RelayCommand<object>((p) => { return true; }, (p) => { AddProduct(); });
            DeleteProductCmd = new RelayCommand<object>((p) => { return true; }, (p) => { DeleteProduct(); });
            CompleteCmd = new RelayCommand<object>((p) => { return true; }, (p) => { CompleteAdding(); });

            NhapHangList = new ObservableCollection<SanPham>();


            NewProduct();
            NewSupplier();
        }

        private void NewSupplier()
        {
            do
            {
                SuppliertId = Converter.Instance.RandomString(5);
            }
            while (DataProvider.Ins.DB.NHACUNGCAP.Where(x => x.MACC == SuppliertId).Count() > 0);
            Phone = "";
            Origin = "";
            SuppliertName = "";
            Note = "";
        }

        private void openWinAddSupplier()
        {
            winAddSupplier win = new winAddSupplier();
            win.ShowDialog();


        }

        private void CompleteAdding()
        {
            NhapHangList.Clear();
        }

        private void DeleteProduct()
        {
            throw new NotImplementedException();
        }

        private void AddProduct()
        {
            if (DataProvider.Ins.DB.SANPHAM.Where(x => x.MASP == ProductId).Count() > 0)
            {
                MessageBox.Show("Product ID existed.");
                return;
            }
            var ncc = DataProvider.Ins.DB.NHACUNGCAP.Where(x => x.TEN == SeletedSupplierType).SingleOrDefault();
            var nv = new SANPHAM() { TENSP = ProductName, MASP = ProductId,  PICBI = Converter.Instance.ConvertBitmapImageToBytes(Bitimg),MACC=ncc.MACC,GHICHU=Note,DVT= SeletedProductType,GIA=Price };
            DataProvider.Ins.DB.SANPHAM.Add(nv);
            DataProvider.Ins.DB.SaveChanges();
            LoadTonKhoData();
            NewProduct();
        }

        private void Imagepick(Button p)
        {
            nv.chooseImg();
            Bitimg = nv.Bitimg;
            imgAvatar.Source = nv.Bitimg;
            p.Content = imgAvatar;
        }
        private void CreateAvatar(Button p)
        {
            p.Content = imgAvatar;
        }

        private void LoadTonKhoData()
        {
            TonKhoList = new ObservableCollection<SanPham>();

            var objectList = DataProvider.Ins.DB.SANPHAM;
            foreach (var item in objectList)
            {
                SanPham sanpham = new SanPham();
                sanpham.sanpham = item;
                sanpham.Bitimg = Converter.Instance.ConvertByteToBitmapImage(item.PICBI);
                sanpham.Img.Source = sanpham.Bitimg;
                TonKhoList.Add(sanpham);
            }
            view = (CollectionView)CollectionViewSource.GetDefaultView(TonKhoList);
            view.Filter = UserFilter;

        }

        private void NewProduct()
        {
            do
            {
                ProductId = Converter.Instance.RandomString(5);
            }
            while (DataProvider.Ins.DB.SANPHAM.Where(x => x.MASP == ProductId).Count() > 0);
            ProductName = "";
            Price = 0;
            Ammount = 0;
            Bitimg = null;
            btnAvatar.Content = null;
            Note = "";
        }

        private void AddItemIntoItemCtrol(ItemsControl p)
        {

        }

        public CollectionView view;
        private List<string> _SearchType = new List<string>() { "ID", "Name" };

        public List<string> SearchType { get => _SearchType; set { _SearchType = value; OnPropertyChanged(); } }
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
        private void FilterItem()
        {
            CollectionViewSource.GetDefaultView(NhapHangList).Refresh();
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

        void LoadPage(Page p)
        {

        }

        void openWinAddNewProduct(object p)
        {
            var supplierList = DataProvider.Ins.DB.NHACUNGCAP;
            int i = 1;
            foreach (var item in supplierList)
            {
                SupplierType.Add(item.TEN);
            }

            NhapHangList = new ObservableCollection<SanPham>();
            do
            {
                ProductId = Converter.Instance.RandomString(5);

            } while (DataProvider.Ins.DB.SANPHAM.Where(x => x.MASP == ProductId).Count() > 0);
            do
            {
                ImportId = Converter.Instance.RandomString(5);

            } while (DataProvider.Ins.DB.HOADON.Where(x => x.SOHD == ImportId).Count() > 0);

            winImportProduct win = new winImportProduct();
            win.ShowDialog();

        }


    }
}
