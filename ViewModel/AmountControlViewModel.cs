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
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;


namespace SE104_N10_QuanLySieuThi.ViewModel
{
    class AmountControlViewModel:BaseViewModel
    {
        public ICommand DecreaseSelectAmmount { get; set; }


        public ICommand IncreaseSelectAmmount { get; set; }
        public AmountControlViewModel()
        {
            IncreaseSelectAmmount = new RelayCommand<object>((p) => { return true; }, (p) => { Increase(p); });
            DecreaseSelectAmmount = new RelayCommand<object>((p) => { return true; }, (p) => { Decrease(p); });
        }

        private void Increase(object p)
        {
        }

        private void Decrease(object p)
        {


        }
    }
}
