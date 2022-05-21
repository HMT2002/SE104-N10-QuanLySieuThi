using SE104_N10_QuanLySieuThi.classes;
using SE104_N10_QuanLySieuThi.Model;
using SE104_N10_QuanLySieuThi.windows;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SE104_N10_QuanLySieuThi.ViewModel
{
    class ImportProductViewModel:BaseViewModel
    {
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
        public string SeletedProduct { get => _SeletedProduct; set { _SeletedProduct = value;
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

        public ICommand ImportProductCmd { get; set; }

        public ImportProductViewModel()
        {
            ImportProductCmd = new RelayCommand<object>((p) => { return true; }, (p) => { ImportProduct(); });
            ImportID = Converter.Instance.RandomString(5);
            ImportDate = DateTime.Now;
            Price = 0;
            AmmountImport = 0;
            LoadProductList();
        }

        private void ImportProduct()
        {
            while (DataProvider.Ins.DB.NHAPHANG.Where(x => x.MANH == ImportID).Count() > 0)
            {
                ImportID = Converter.Instance.RandomString(5);
            }
            var nv = new NHAPHANG() { MANH = ImportID, MASP = curProduct.MASP,MANV = MainViewModel._currentUser,NGNH=ImportDate,SLNHAPHANG=AmmountImport };
            DataProvider.Ins.DB.NHAPHANG.Add(nv);

            var sp = DataProvider.Ins.DB.SANPHAM.Where(x => x.MASP == ProductID).SingleOrDefault();
            if (sp.SL == null)
            {
                sp.SL = 0;
            }
            sp.SL +=AmmountImport;
            DataProvider.Ins.DB.SaveChanges();
            NewImport();
            LoadProductList();
        }

        private void NewImport()
        {
            SeletedProduct = null;
            AmmountImport = 0;
            SummaryImport = 0;

        }

        private void LoadProductList()
        {
            ListProduct = new List<string>();
            var supplierList = DataProvider.Ins.DB.SANPHAM;
            foreach (var item in supplierList)
            {
                ListProduct.Add(item.TENSP);
            }
        }
    }
}
