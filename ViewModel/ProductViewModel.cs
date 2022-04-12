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
    class ProducViewModel : BaseViewModel
    {
        public ICommand BillCommand { get; set; }

        public SqlConnection ketnoi = new SqlConnection(@"Data Source=LAPTOP-H3DR409O\MSSQLSERVER01;Initial Catalog=QuanLySieuThi;Integrated Security=True");

        public ProducViewModel()
        {
            BillCommand = new RelayCommand<object>((p) => { return true; }, (p) => { Bill(p); });
        }
        void Bill(object p)
        {
            winBill win = new winBill();
            win.Show();
        }
    }
}
