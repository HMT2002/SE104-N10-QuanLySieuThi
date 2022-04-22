using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using SE104_N10_QuanLySieuThi.classes;
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
using System.Windows.Input;


namespace SE104_N10_QuanLySieuThi.ViewModel
{
    class ProducViewModel : BaseViewModel
    {
        public SanPham sp=new SanPham();
        public ObservableCollection<SanPham> SP { get; set; }
        ItemsControl itemsControl = new ItemsControl();

        public ICommand BillCommand { get; set; }

        public ICommand LoadedPageCmd { get; set; }
        public ICommand LoadedItemCtrlCmd { get; set; }

        public ICommand LoadedGridCmd { get; set; }

        public int countclick = 0;
        public TextBlock OrClickMeCount=new TextBlock();

        public ICommand IncrementOrClickMeCountCommand { get; set; }
        public ICommand DecreasementOrClickMeCountCommand { get; set; }

        public bool isMainLoaded=false;


        public SqlConnection ketnoi = new SqlConnection(@"Data Source=LAPTOP-H3DR409O\MSSQLSERVER01;Initial Catalog=QuanLySieuThi;Integrated Security=True");

        public ProducViewModel()
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            sp = new SanPham();
            BillCommand = new RelayCommand<object>((p) => { return true; }, (p) => { Bill(p); });
            LoadedPageCmd = new RelayCommand<Page>((p) => { return true; }, (p) => { LoadPage(p); });
            LoadedGridCmd = new RelayCommand<Grid>((p) => { return true; }, (p) => { LoadGrid(p); });
            IncrementOrClickMeCountCommand = new RelayCommand<Button>((p) => { return true; }, (p) => { Increase(p); });
            DecreasementOrClickMeCountCommand = new RelayCommand<Button>((p) => { return true; }, (p) => { Decrease(p); });
            LoadedItemCtrlCmd= new RelayCommand<ItemsControl>((p) => { itemsControl = p; return true; }, (p) => { if (!isMainLoaded) { AddItemIntoItemCtrol(p);isMainLoaded = true; }; });

            sp.getAllProductFromDatabase();
        }


        private void AddItemIntoItemCtrol(ItemsControl p)
        {
            p.ItemsSource = sp.ListAll;
            //for (int i = 0; i < sp.ListAll.Count; i++)
            //{
                
            //    Button btn = new Button();
            //    btn.Width = 100;
            //    btn.Height = 100;
            //    btn.Content = new Image(){Source = sp.ListAll[i].Bitimg };
            //    p.Items.Add(btn);
            //}
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

        void LoadPage(Page p)
        {

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
