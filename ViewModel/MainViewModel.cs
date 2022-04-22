using SE104_N10_QuanLySieuThi.classes;
using SE104_N10_QuanLySieuThi.pages;
using SE104_N10_QuanLySieuThi.windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SE104_N10_QuanLySieuThi.ViewModel
{
    public class MainViewModel:BaseViewModel
    {
        public bool IsLoad = false;
        public ICommand LoadedWindowCmd { get; set; }
        public Account LoginAcc { get => loginAcc; set => loginAcc = value; }

        public Account loginAcc;

        public Page pageProduct { get; set; }

        public Page pageManage { get; set; }


        public Page pageSell { get; set; }


        public Page pageBuy { get; set; }

        public Page pageStatic { get; set; }

        public Page pageSetting { get; set; }


        public Page pageCustomer { get; set; }


        public Page pageHome { get; set; }

        public ICommand ChangeTab { get; set; }

        public MainViewModel()
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            loginAcc = new Account();

            pageProduct = new paProduct();


            pageBuy = new paBuy();

            pageSell = new paSell();

            pageManage = new paManage();

            pageSetting = new paSetting();

            pageCustomer = new paCustomer();

            pageHome = new paHome();

            pageStatic = new paStatic();


            LoadedWindowCmd = new RelayCommand<Window>((p) => { return true; }, (p) => { Start(p); });
        }

        void Start(Window p)
        {
            IsLoad = true;
            p.Hide();
            winLogin loginWindow = new winLogin();
            loginWindow.ShowDialog();
            if (loginWindow.DataContext == null)
                return;
            var loginVM = loginWindow.DataContext as LoginViewModel;

            if (loginVM.IsLogin)
            {
                loginAcc = loginVM.Acc;
                p.Show();
                //MessageBox.Show("welcome " + loginAcc.Acc + " " + loginAcc.Password+" "+loginAcc.Pri.ToString());
            }
            else
            {
                p.Close();
            }
        }
    }
}
