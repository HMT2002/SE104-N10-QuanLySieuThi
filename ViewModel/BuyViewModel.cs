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

        SanPham nv = new SanPham();

        public ICommand LoadedPageCmd { get; set; }

        public ICommand PasswordChangedCommand { get; set; }

        public ICommand CreateProductCmd { get; set; }

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
        private string _Price;
        public string Price { get => _Price; set { _Price = value; OnPropertyChanged(); } }
        private string _Quantiy;
        public string Quantiy { get => _Quantiy; set { _Quantiy = value; OnPropertyChanged(); } }
        private string _ProductName;
        public string ProductName { get => _ProductName; set { _ProductName = value; OnPropertyChanged(); } }
        private string _Id;
        public string Id { get => _Id; set { _Id = value; OnPropertyChanged(); } }
        private string _Id;
        public string Id { get => _Id; set { _Id = value; OnPropertyChanged(); } }
        private string _Id;
        public string Id { get => _Id; set { _Id = value; OnPropertyChanged(); } }

        public BuyViewModel()
        {
            Id = "";

        }
    }
}
