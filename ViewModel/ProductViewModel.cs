using Caliburn.Micro;
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
using System.Windows.Input;


namespace SE104_N10_QuanLySieuThi.ViewModel
{
    class ProducViewModel : BaseViewModel
    {

        private string _EmployeeId;
        public string EmployeeId { get => _EmployeeId; set { _EmployeeId = value; OnPropertyChanged(); } }

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

        public ICommand LoadedPageCmd { get; set; }
        public ICommand LoadedItemCtrlCmd { get; set; }

        public ICommand LoadedGridCmd { get; set; }

        public int countclick = 0;
        public TextBlock OrClickMeCount=new TextBlock();

        public ICommand IncrementOrClickMeCountCommand { get; set; }
        public ICommand DecreasementOrClickMeCountCommand { get; set; }

        public bool isMainLoaded=false;


        public SqlConnection ketnoi = new SqlConnection(@"Data Source=LAPTOP-H3DR409O\MSSQLSERVER01;Initial Catalog=QuanLySieuThi;Integrated Security=True");

        private ObservableCollection<SanPham> _TonKhoList;

        public ObservableCollection<SanPham> TonKhoList { get => _TonKhoList; set { _TonKhoList = value; OnPropertyChanged(); } }

        public ProducViewModel()
        {
            EmployeeId = "NV005";
            var display = DataProvider.Ins.DB.NHANVIEN.Where(x => x.MANV == EmployeeId);
            if (display != null && display.Count() == 1)
            {
                foreach (var item in display)
                {
                    NhanVien nhanvien = new NhanVien();
                    nhanvien.nhanvien = item;
                    EmployeeId = nhanvien.nhanvien.HOTEN;
                }
            }
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            sp = new SanPham();
            BillCommand = new RelayCommand<object>((p) => { return true; }, (p) => { openWinAddNewProduct(p); });
            LoadedPageCmd = new RelayCommand<Page>((p) => { return true; }, (p) => { LoadPage(p); });
            LoadedGridCmd = new RelayCommand<Grid>((p) => { return true; }, (p) => { LoadGrid(p); });
            IncrementOrClickMeCountCommand = new RelayCommand<Button>((p) => { return true; }, (p) => { Increase(p); });
            DecreasementOrClickMeCountCommand = new RelayCommand<Button>((p) => { return true; }, (p) => { Decrease(p); });
            LoadedItemCtrlCmd= new RelayCommand<ItemsControl>((p) => { return true; }, (p) => { if (!isMainLoaded) {LoadTonKhoData(); AddItemIntoItemCtrol(p);isMainLoaded = true; }; });

            sp.getAllProductFromDatabase();
        }

        private void LoadTonKhoData()
        {
            TonKhoList = new ObservableCollection<SanPham>();

            var objectList = DataProvider.Ins.DB.SANPHAM;
            int i = 1;
            foreach(var item in objectList)
            {
                SanPham sanpham = new SanPham();
                sanpham.sanpham = item;
                TonKhoList.Add(sanpham);
                i++;
            }
        }

        private void AddItemIntoItemCtrol(ItemsControl p)
        {

        }

        private void Decrease(Button p)
        {
            countclick--;
            OrClickMeCount = new TextBlock();
            if (OrClickMeCount != null)
                OrClickMeCount.Text = countclick.ToString();
            p.Content = OrClickMeCount;
        }

        private void Increase(Button p)
        {
            countclick++;
            OrClickMeCount = new TextBlock();
            if (OrClickMeCount != null)
                OrClickMeCount.Text = countclick.ToString();
            p.Content = OrClickMeCount;
        }

        public void LoadGrid(Grid p)
        {
            //LoadProductFromDatabase(p);
            //LoadEmployeeFromdatabase(p);
        }

        void LoadPage(Page p)
        {

        }

        private void ClickId(object sender, EventArgs e)
        {

        }

        void openWinAddNewProduct(object p)
        {
            winImportProduct win = new winImportProduct();
            win.ShowDialog();
        }


    }
}
