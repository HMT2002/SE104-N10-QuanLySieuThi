using SE104_N10_QuanLySieuThi.classes;
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
    class ManageViewModel:BaseViewModel
    {
        NhanVien nv = new NhanVien();

        public ICommand LoadedPageCmd { get; set; }

        public ICommand PasswordChangedCommand { get; set; }

        public ICommand CreateEmployeeCmd { get; set; }

        public ICommand PickImage { get; set; }

        public ICommand LoadedGridItemCmd { get; set; }


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
        public SqlConnection ketnoi = new SqlConnection(@"Data Source=LAPTOP-H3DR409O\MSSQLSERVER01;Initial Catalog=QuanLySieuThi;Integrated Security=True");

        public ManageViewModel()
        {
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
            CreateEmployeeCmd = new RelayCommand<Grid>((p) => { return true; }, (p) => { CreateEmployee(p); });
            PickImage = new RelayCommand<Button>((p) => { return true; }, (p) => { Imagepick(p); });
            LoadedGridItemCmd = new RelayCommand<Grid>((p) => { return true; }, (p) => { LoadedGridItem(p); });
            nv.getAllEmployeeFromDatabase();

        }

        private void LoadedGridItem(Grid p)
        {
            try {
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
                p.Children.Add(btn);

            }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void ClickId(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            NhanVien kh = new NhanVien();
            kh.getSpecificEmployeeFromDatabase(btn.Tag.ToString());
            Id = kh.Id;
        }

        private void CreateEmployee(Grid p)
        {
            try
            {

                Button btn = new Button();
                btn.Height = 50;
                btn.Width = 120;
                btn.Click += ClickId;
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
                btn.Tag = Id;
                if (nv.RegistEmployee())
                {
                    MessageBox.Show("Tạo thành công");
                }
                else
                {
                    MessageBox.Show("fail");
                    return;
                }
                nv.openImgFromDatabase();
                btn.Content = nv.Img;

                Thickness myThickness = new Thickness();
                for (int i = 0; i < nv.ListAll.Count; i++)
                {
                    myThickness.Bottom = 0;
                    myThickness.Left = 250 * (i % 5);
                    myThickness.Right = 0;
                    myThickness.Top = -400 + 250 * (i / 5);
                }

                btn.Margin = myThickness;

                p.Children.Add(btn);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }


        }

        private void Imagepick(Button p)
        {
            nv.chooseImg();
            p.Content = new Image() { Source = nv.Bitimg };
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
