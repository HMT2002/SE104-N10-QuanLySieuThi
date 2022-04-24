using MaterialDesignThemes.Wpf;
using SE104_N10_QuanLySieuThi.classes;
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
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace SE104_N10_QuanLySieuThi.ViewModel
{
    public class ManageViewModel:BaseViewModel
    {
        public NhanVien nv = new NhanVien();

        public Button btnAvatar = new Button();

        public ICommand LoadedPageCmd { get; set; }

        public ICommand PasswordChangedCommand { get; set; }

        public ICommand CreateEmployeeCmd { get; set; }
        public ICommand DeleteEmployeeCmd { get; set; }
        public ICommand ModifyEmployeeCmd { get; set; }
        public ICommand PickImage { get; set; }

        public ICommand LoadedItemCtrlCmd { get; set; }
        public ICommand ClickedItemCtrlCmd { get; set; }
        public ICommand ClickedItemCtrlCmd2 { get; set; }

        public bool isMainLoaded = false;

        public List<NhanVien> lstEmployye;


        private string _UserName;
        public string UserName { get => _UserName; set { _UserName = value; OnPropertyChanged(); } }
        private string _Name;
        public string Name { get => _Name; set { _Name = value; OnPropertyChanged(); } }
        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }
        private string _Phone;
        public string Phone { get => _Phone; set { _Phone = value; OnPropertyChanged(); } }
        private string _Mail;
        public string Mail { get => _Mail; set { _Mail = value; OnPropertyChanged(); } }
        private string _Id;
        public string Id { get => _Id; set { _Id = value; OnPropertyChanged(); } }
        private string _Position;
        public string Position { get => _Position; set { _Position = value; OnPropertyChanged(); } }
        private string _CMND;
        public string  CMND { get => _CMND; set { _CMND = value; OnPropertyChanged(); } }
        private decimal _Salary;
        public decimal Salary { get => _Salary; set { _Salary = value; OnPropertyChanged(); } }
        private DateTime _Joineddate;
        public DateTime Joineddate { get => _Joineddate; set { _Joineddate = value; OnPropertyChanged(); } }

        private DateTime _Birthday;
        public DateTime Birthday { get => _Birthday; set { _Birthday = value; OnPropertyChanged(); } }

        public BitmapImage Bitimg { get => bitimg; set => bitimg = value; }

        public SqlConnection ketnoi = new SqlConnection(@"Data Source=LAPTOP-H3DR409O\MSSQLSERVER01;Initial Catalog=QuanLySieuThi;Integrated Security=True");

        private BitmapImage bitimg = new BitmapImage();

        public ItemsControl itemsControl = new ItemsControl();

        public ManageViewModel()
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            Password = "";
            UserName = "";
            Mail = "";
            Id = "";
            Position = "";
            Phone = "";
            Salary = 0;
            Joineddate = DateTime.Now;
            Birthday = DateTime.Now;
            LoadedPageCmd = new RelayCommand<Page>((p) => { return true; }, (p) => { LoadPage(p); });
            CreateEmployeeCmd = new RelayCommand<UniformGrid>((p) => { return true; }, (p) => { CreateEmployee(p); });
            DeleteEmployeeCmd = new RelayCommand<UniformGrid>((p) => { return true; }, (p) => { DeleteEmployee(p); });
            ModifyEmployeeCmd = new RelayCommand<UniformGrid>((p) => { return true; }, (p) => { ModifyEmployee(p); });
            LoadedItemCtrlCmd = new RelayCommand<ItemsControl>((p) => { itemsControl = p; return true; }, (p) => { if (!isMainLoaded) { AddItemIntoItemCtrol(p); isMainLoaded = true; }; });

            PickImage = new RelayCommand<Button>((p) => {btnAvatar=p; return true; }, (p) => { Imagepick(p);});
            nv.getAllEmployeeFromDatabase();
            lstEmployye = nv.ListAll;
            ClickedItemCtrlCmd = new RelayCommand<Button>((p) => { return true; }, (p) => { ItemSelected(p); });

        }
        private void ItemSelected(Button btn)
        {
            nv = new NhanVien();

            nv.getAllEmployeeFromDatabase();
            //Id = nv.Id;
            //Name = nv.Name;
            //Mail = nv.Mail;
            //Salary = nv.Salary;
            //Phone = nv.Phone;
            //CMND = nv.Cmnd;
            //Position = nv.Position;
            //btnAvatar.Content = nv.Img;
            MessageBox.Show("12345"+nv.Name+btn.Tag);
        }

        private void AddItemIntoItemCtrol(ItemsControl p)
        {
            p.ItemsSource = lstEmployye;
        }

        private void ModifyEmployee(UniformGrid p)
        {
            try
            {
                if (nv.DeleteEmployee())
                {
                    p.Children.Clear();
                    CreateEmployee(p);
                    LoadedItem();
                }
                else
                {
                    MessageBox.Show("fail sửa");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void DeleteEmployee(UniformGrid p)
        {
            if (nv.DeleteEmployee())
            {
                p.Children.Clear();
                Password = "";
                UserName = "";
                Mail = "";
                Id = "";
                Position = "";
                Phone = "";
                Salary = 0;
                Joineddate = DateTime.Now;
                Birthday = DateTime.Now;
                btnAvatar.Content = null;

                LoadedItem();
            }
            else
            {
                MessageBox.Show("fail xoá");
                return;
            }
        }

        private void LoadedItem()
        {
            nv.getAllEmployeeFromDatabase();
            itemsControl.ItemsSource = lstEmployye;
            //try {
            //NhanVien supplier = new NhanVien();
            //supplier.getAllEmployeeFromDatabase();
            //for (int i = 0; i < supplier.ListAll.Count; i++)
            //{
            //    Button btn = new Button();
            //    btn.Height = 50;
            //    btn.Width = 120;
            //    btn.Click += ClickId;
            //    btn.Tag = supplier.ListAll[i].Id;
            //    btn.Content = supplier.ListAll[i].Img;
            //    p.Children.Add(btn);

            //}
            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

        }

        private void ClickItem(object sender, EventArgs e)
        {
            //NhanVien n = new NhanVien();
            //nv.getAllEmployeeFromDatabase();
            //nv.getSpecificEmployeeFromDatabase(sender.Tag.ToString());
            //Id = nv.Id;
            //Name = nv.Name;
            //Mail = nv.Mail;
            //Salary = nv.Salary;
            //Phone = nv.Phone;
            //CMND = nv.Cmnd;
            //Position = nv.Position;
            //Joineddate = DateTime.Parse(nv.Startdate);
            //Birthday = DateTime.Parse(nv.Birthday);
            //btnAvatar.Content = nv.Img;
            MessageBox.Show("Picked");
        }

        private void CreateEmployee(UniformGrid p)
        {
            try
            {
                nv.Id = Id;
                nv.Name = Name;
                nv.Acc = new Account();
                nv.Acc.Acc = UserName;
                nv.Acc.Password = Password;
                nv.Mail = Mail;
                nv.Phone = Phone;
                nv.Salary = Salary;
                nv.Startdate = Joineddate.ToString("dd/MM/yyyy");
                nv.Position = Position;
                nv.Cmnd = CMND;
                nv.Birthday = Birthday.ToString("dd/MM/yyyy");

                if (nv.RegistEmployee())
                {
                    nv.openImgFromDatabase();
                }
                else
                {
                    MessageBox.Show("fail tạo");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            LoadedItem();


        }

        private void Imagepick(Button p)
        {
            nv.chooseImg();
            btnAvatar.Content = nv.Img;
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
