using SE104_N10_QuanLySieuThi.classes;
using SE104_N10_QuanLySieuThi.windows;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace SE104_N10_QuanLySieuThi.ViewModel
{
    public class BuyViewModel:BaseViewModel
    {

        SanPham prod = new SanPham();

        public ICommand LoadedPageCmd { get; set; }

        public ICommand PasswordChangedCommand { get; set; }

        public ICommand AddProductCmd { get; set; }

        public ICommand DeleteProductCmd { get; set; }

        public ICommand FixProductCmd { get; set; }

        public ICommand PickImage { get; set; }

        public ICommand LoadedGridItemCmd { get; set; }

        private string _UserName;
        public string UserName { get => _UserName; set { _UserName = value; OnPropertyChanged(); } }
        private string _Name;
        public string Name { get => _Name; set { _Name = value; OnPropertyChanged(); } }
        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }

        private string _Id;
        public string Id { get => _Id; set { _Id = value; OnPropertyChanged(); } }
        private string _Mensurement;
        public string Mensurement { get => _Mensurement; set { _Mensurement = value; OnPropertyChanged(); } }
        private decimal _Price;
        public decimal Price { get => _Price; set { _Price = value; OnPropertyChanged(); } }
        private string _Quantiy;
        public string Quantiy { get => _Quantiy; set { _Quantiy = value; OnPropertyChanged(); } }
        private string _ProductName;
        public string ProductName { get => _ProductName; set { _ProductName = value; OnPropertyChanged(); } }
        private string _SupplierId;
        public string SupplierId { get => _SupplierId; set { _SupplierId = value; OnPropertyChanged(); } }
        private string _SupplierName;
        public string SupplierName { get => _SupplierName; set { _SupplierName = value; OnPropertyChanged(); } }
        private string _SupplierCountry;
        public string SupplierCountry { get => _SupplierCountry; set { _SupplierCountry = value; OnPropertyChanged(); } }

        public BuyViewModel()
        {
            Id = "";
            Mensurement = "";
            Price = 0;
            Quantiy = "";
            ProductName = "";
            SupplierId = "";
            SupplierName = "";
            SupplierCountry = "";

            PickImage = new RelayCommand<Button>((p) => { return true; }, (p) => { Imagepick(p); });
            //AddProductCmd = new RelayCommand<Grid>((p) => { return true; }, (p) => { Imagepick(p); });
            //DeleteProductCmd = new RelayCommand<Grid>((p) => { return true; }, (p) => { Imagepick(p); });
            //FixProductCmd = new RelayCommand<Grid>((p) => { return true; }, (p) => { Imagepick(p); });
            LoadedGridItemCmd = new RelayCommand<Button>((p) => { return true; }, (p) => { Imagepick(p); });

        }

        private void ClickId(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            NhanVien kh = new NhanVien();
            kh.getSpecificEmployeeFromDatabase(btn.Tag.ToString());
            Id = kh.Id;
        }

        private void Imagepick(Button p)
        {
            prod.chooseImg();
            p.Content = new Image() { Source = prod.Bitimg };
        }


    }
}

