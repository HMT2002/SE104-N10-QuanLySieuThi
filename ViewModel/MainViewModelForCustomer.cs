using SE104_N10_QuanLySieuThi.classes;
using SE104_N10_QuanLySieuThi.Model;
using SE104_N10_QuanLySieuThi.pages;
using SE104_N10_QuanLySieuThi.windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SE104_N10_QuanLySieuThi.ViewModel
{
    class MainViewModelForCustomer:BaseViewModel
    {

        public bool IsLoad = false;
        public ICommand LoadedWindowCmd { get; set; }
        public ICommand LogoutWindowCmd { get; set; }

        public Account LoginAcc { get => loginAcc; set => loginAcc = value; }

        public Account loginAcc;

        public Page pageCustomer { get; set; }

        public Page pageLogout { get; set; }

        public ICommand ChangeTab { get; set; }


        public static string _currentUser;
        public string currentUser { get => _currentUser; set { _currentUser = value; OnPropertyChanged(); } }

        public MainViewModelForCustomer()
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            loginAcc = new Account();

            pageCustomer = new paCustomerForCustomer();

            pageLogout = new paLogout();


            LoadedWindowCmd = new RelayCommand<Window>((p) => { return true; }, (p) => { Start(p); });

            LogoutWindowCmd = new RelayCommand<Window>((p) => { return true; }, (p) => { Logout(p); });

        }

        private void Logout(Window p)
        {
            winLogin win = new winLogin();
            win.ShowDialog();
            p.Close();
        }

        void Start(Window p)
        {

        }
    }
}
