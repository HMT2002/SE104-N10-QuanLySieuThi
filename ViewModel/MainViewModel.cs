using SE104_N10_QuanLySieuThi.windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SE104_N10_QuanLySieuThi.ViewModel
{
    public class MainViewModel:BaseViewModel
    {
        public bool IsLoad = false;
        public MainViewModel()
        {
            if (!IsLoad)
            {
                IsLoad = true;
                winLogin win = new winLogin();
                win.ShowDialog();
            }

        }
    }
}
