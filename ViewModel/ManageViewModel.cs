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
    class ManageViewModel:BaseViewModel
    {
        NhanVien nv = new NhanVien();

        public Button btnAvatar = new Button();

        public ICommand LoadedPageCmd { get; set; }

        public ICommand PasswordChangedCommand { get; set; }

        public ICommand CreateEmployeeCmd { get; set; }
        public ICommand DeleteEmployeeCmd { get; set; }
        public ICommand ModifyEmployeeCmd { get; set; }
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
            CreateEmployeeCmd = new RelayCommand<UniformGrid>((p) => { return true; }, (p) => { CreateEmployee(p); });
            DeleteEmployeeCmd = new RelayCommand<UniformGrid>((p) => { return true; }, (p) => { DeleteEmployee(p); });
            ModifyEmployeeCmd = new RelayCommand<UniformGrid>((p) => { return true; }, (p) => { ModifyEmployee(p); });

            PickImage = new RelayCommand<Button>((p) => {btnAvatar=p; return true; }, (p) => { Imagepick(p);});
            LoadedGridItemCmd = new RelayCommand<UniformGrid>((p) => { return true; }, (p) => { LoadedGridItem(p); });
            nv.getAllEmployeeFromDatabase();

        }

        private void ModifyEmployee(UniformGrid p)
        {
            try
            {
                if (nv.DeleteEmployee())
                {
                    p.Children.Clear();
                    CreateEmployee(p);
                    LoadedGridItem(p);
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

                LoadedGridItem(p);
            }
            else
            {
                MessageBox.Show("fail xoá");
                return;
            }
        }

        private void LoadedGridItem(UniformGrid p)
        {
            try {
            NhanVien supplier = new NhanVien();
            supplier.getAllEmployeeFromDatabase();
            for (int i = 0; i < supplier.ListAll.Count; i++)
            {
                Button btn = new Button();
                btn.Height = 50;
                btn.Width = 120;
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
            nv.getSpecificEmployeeFromDatabase(btn.Tag.ToString());
            Id = nv.Id;
            Name = nv.Name;
            Mail = nv.Mail;
            Salary = nv.Salary;
            Phone = nv.Phone;
            CMND = nv.Cmnd;
            Position = nv.Position;
            Joineddate = DateTime.Parse(nv.Startdate);
            Birthday = DateTime.Parse(nv.Birthday);
            btnAvatar.Content = nv.Img;
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
                LoadedGridItem(p);
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
