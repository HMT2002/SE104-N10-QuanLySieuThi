using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace SE104_N10_QuanLySieuThi.ViewModel
{
    class ManageViewModel:BaseViewModel
    {
        public ICommand LoadedPageCmd { get; set; }

        public SqlConnection ketnoi = new SqlConnection(@"Data Source=LAPTOP-H3DR409O\MSSQLSERVER01;Initial Catalog=QuanLySieuThi;Integrated Security=True");

        public ManageViewModel()
        {
            LoadedPageCmd = new RelayCommand<Page>((p) => { return true; }, (p) => { LoadPage(p); });

        }

        void LoadPage(Page p)
        {
            LoadManageFromDatabase(p);
        }

        void LoadManageFromDatabase(Page p)
        {

        }
    }
}
