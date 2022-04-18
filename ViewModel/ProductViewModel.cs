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

        public int countclick = 0;
        public TextBlock OrClickMeCount=new TextBlock();

        public ICommand IncrementOrClickMeCountCommand { get; set; }
        public ICommand DecreasementOrClickMeCountCommand { get; set; }


        public SqlConnection ketnoi = new SqlConnection(@"Data Source=LAPTOP-H3DR409O\MSSQLSERVER01;Initial Catalog=QuanLySieuThi;Integrated Security=True");

        public ProducViewModel()
        {
            BillCommand = new RelayCommand<object>((p) => { return true; }, (p) => { Bill(p); });
            LoadedPageCmd = new RelayCommand<Page>((p) => { return true; }, (p) => { LoadPage(p); });
            LoadedGridCmd = new RelayCommand<Grid>((p) => { return true; }, (p) => { LoadGrid(p); });
            IncrementOrClickMeCountCommand = new RelayCommand<Button>((p) => { return true; }, (p) => { Increase(p); });
            DecreasementOrClickMeCountCommand = new RelayCommand<Button>((p) => { return true; }, (p) => { Decrease(p); });

        }

        private void Decrease(Button p)
        {
            countclick--;
            OrClickMeCount = new TextBlock();
            if (OrClickMeCount != null)
                OrClickMeCount.Text = countclick.ToString();
            p.Content = OrClickMeCount;
        }

        private void Increase(Button p)
        {
            countclick++;
            OrClickMeCount = new TextBlock();
            if (OrClickMeCount != null)
                OrClickMeCount.Text = countclick.ToString();
            p.Content = OrClickMeCount;
        }

        public void LoadGrid(Grid p)
        {
            //LoadProductFromDatabase(p);
            //LoadEmployeeFromdatabase(p);
        }

        private void LoadEmployeeFromdatabase(Grid g)
        {
            NhanVien supplier = new NhanVien();
            supplier.getAllEmployeeFromDatabase();
            for (int i = 0; i < supplier.ListAll.Count; i++)
            {
                Button btn = new Button();
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
                btn.Content = supplier.ListAll[i].Img;
                g.Children.Add(btn);
            }
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
                btn.Content = supplier.ListAll[i].Img;
                g.Children.Add(btn);
            }
        }

        private void ClickId(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            NhanVien kh = new NhanVien();
            kh.getSpecificEmployeeFromDatabase(btn.Tag.ToString());
            btn.Content = kh.Img;
        }

        void Bill(object p)
        {
            winBill win = new winBill(countclick);
            
            win.Show();
        }
    }
}
