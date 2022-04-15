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

        public ICommand LoadedPageCmd { get; set; }

        public ICommand LoadedGridCmd { get; set; }



        public SqlConnection ketnoi = new SqlConnection(@"Data Source=LAPTOP-H3DR409O\MSSQLSERVER01;Initial Catalog=QuanLySieuThi;Integrated Security=True");

        public ProducViewModel()
        {
            BillCommand = new RelayCommand<object>((p) => { return true; }, (p) => { Bill(p); });
            LoadedPageCmd = new RelayCommand<Page>((p) => { return true; }, (p) => { LoadPage(p); });
            LoadedGridCmd = new RelayCommand<Grid>((p) => { return true; }, (p) => { LoadGrid(p); });

        }

        public void LoadGrid(Grid p)
        {
            LoadProductFromDatabase(p);
        }

        void LoadPage(Page p)
        {

        }

        void LoadProductFromDatabase(Grid g)
        {
            KhachHang supplier = new KhachHang();
            supplier.getAllCustomerFromDatabase();
            for (int i = 0; i < supplier.ListAll.Count; i++)
            {
                Button btn = new Button();
                btn.Content = supplier.ListAll[i].Anh.Img;
                btn.Height = 50;
                btn.Width = 120;
                Thickness myThickness = new Thickness();
                myThickness.Bottom = 0;
                myThickness.Left = 250 * (i % 5);
                myThickness.Right = 0;
                myThickness.Top = -400 + 250 * (i / 5);
                btn.Margin = myThickness;
                btn.Click += ClickId;
                btn.Tag = supplier.ListAll[i].Id;
                g.Children.Add(btn);
            }
        }

        private void ClickId(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            KhachHang kh = new KhachHang();
            kh.getSpecificCustomerFromDatabase(btn.Tag.ToString());

        }

        void Bill(object p)
        {
            winBill win = new winBill();
            win.Show();
        }
    }
}
