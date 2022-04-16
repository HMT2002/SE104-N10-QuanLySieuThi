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
        private decimal _Salary;
        public decimal Salary { get => _Salary; set { _Salary = value; OnPropertyChanged(); } }
        private string _Joineddate;
        public string Joineddate { get => _Joineddate; set { _Joineddate = value; OnPropertyChanged(); } }
        public SqlConnection ketnoi = new SqlConnection(@"Data Source=LAPTOP-H3DR409O\MSSQLSERVER01;Initial Catalog=QuanLySieuThi;Integrated Security=True");

        public ManageViewModel()
        {
            Password = "";
            UserName = "";
            Mail = "";
            Id = "";
            Phone = "";
            Salary = 0;
            Joineddate = "";
            LoadedPageCmd = new RelayCommand<Page>((p) => { return true; }, (p) => { LoadPage(p); });
            CreateEmployeeCmd = new RelayCommand<Page>((p) => { return true; }, (p) => { CreateEmployee(p); });
            PickImage = new RelayCommand<Page>((p) => { return true; }, (p) => { Imagepick(p); });


        }

        private void CreateEmployee(Page p)
        {
            nv.Id = Id;
            nv.Name = Name;
            nv.Acc.Acc = UserName;
            nv.Acc.Password = Password;
            nv.Mail = Mail;
            nv.Phone = Phone;
            nv.Salary = Salary;
            nv.Startdate = Joineddate;
            if (nv.RegistEmployee())
            {
                MessageBox.Show("Tạo thành công");
            }
            else
            {
                MessageBox.Show("fail");
                MessageBox.Show(Joineddate.ToString());
            }

        }

        private void Imagepick(Page p)
        {
            nv.chooseImg();

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
