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

        public MainViewModel()
        {

            LoadedWindowCmd = new RelayCommand<object>((p) => { return true; }, (p) => {
                IsLoad = true;
                winLogin win = new winLogin();
                win.ShowDialog();
            });
        }
    }
}
