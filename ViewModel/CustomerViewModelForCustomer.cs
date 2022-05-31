using SE104_N10_QuanLySieuThi.classes;
using SE104_N10_QuanLySieuThi.Model;
using SE104_N10_QuanLySieuThi.classes;
using SE104_N10_QuanLySieuThi.Model;
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
    class CustomerViewModelForCustomer:BaseViewModel
    {
        public KhachHang kh = new KhachHang();
        private string _Id;
        public string Id { get => _Id; set { _Id = value; OnPropertyChanged(); } }


        public Image imgAvatar = new Image();

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

        private string _Position;
        public string Position { get => _Position; set { _Position = value; OnPropertyChanged(); } }
        private string _CMND;
        public string CMND { get => _CMND; set { _CMND = value; OnPropertyChanged(); } }
        private string _Note;
        public string Note { get => _Note; set { _Note = value; OnPropertyChanged(); } }

        private string _Acc;
        public string Acc { get => _Acc; set { _Acc = value; OnPropertyChanged(); } }
        private decimal _Salary;
        public decimal Salary { get => _Salary; set { _Salary = value; OnPropertyChanged(); } }
        private DateTime _Joineddate;
        public DateTime Joineddate { get => _Joineddate; set { _Joineddate = value; OnPropertyChanged(); } }

        private DateTime _Birthday;
        public DateTime Birthday { get => _Birthday; set { _Birthday = value; OnPropertyChanged(); } }



        public ICommand CheckedGenderCmd { get; set; }
        public ICommand LoadAvaterCmd { get; set; }
        public ICommand LoadedPageCmd { get; set; }
        public ICommand PickImage { get; set; }

        public ICommand ModifyEmployeeCmd { get; set; }


        public Button btnAvatar = new Button();

        private BitmapImage bitimg = new BitmapImage();
        public BitmapImage Bitimg { get => bitimg; set => bitimg = value; }

        private bool _IsMale;
        public bool IsMale { get => _IsMale; set { _IsMale = value; OnPropertyChanged(); } }
        private bool _IsFeMale;
        public bool IsFeMale { get => _IsFeMale; set { _IsFeMale = value; OnPropertyChanged(); } }

        private string _Gender;
        public string Gender { get => _Gender; set { _Gender = value; OnPropertyChanged(); } }
        public CustomerViewModelForCustomer()
        {
            LoadedPageCmd = new RelayCommand<Page>((p) => { return true; }, (p) => { LoadPage(p); });
            LoadAvaterCmd = new RelayCommand<Button>((p) => { btnAvatar = p; return true; }, (p) => { CreateAvatar(p); });
            CheckedGenderCmd = new RelayCommand<object>((p) => { return true; }, (p) => { CheckGender(); });
            PickImage = new RelayCommand<Button>((p) => { btnAvatar = p; return true; }, (p) => { Imagepick(p); });

            ModifyEmployeeCmd = new RelayCommand<object>((p) => {
                var display = DataProvider.Ins.DB.KHACHHANG.Where(x => x.MAKH == Id);

                if (display == null || display.Count() == 0)
                {
                    return false;
                }

                return true;
            }, (p) => { ModifyEmployee(); });

            loadEmployee();
        }
        public void loadEmployee()
        {
            var obj = DataProvider.Ins.DB.KHACHHANG.Where(x => x.MAKH == MainViewModelForCustomer._currentUser).SingleOrDefault();
            if (obj != null)
            {
                kh.khachhang = obj;
                Name = kh.khachhang.HOTEN;
                Id = kh.khachhang.MAKH;
                Phone = kh.khachhang.SODT;
                Salary = (decimal)kh.khachhang.DOANHSO;

                Birthday = (DateTime)kh.khachhang.NGSINH;
                Joineddate = (DateTime)kh.khachhang.NGDK;

                Mail = kh.khachhang.MAIL;
                Note = kh.khachhang.GHICHU;
                Acc = kh.khachhang.ACC;
                Bitimg = Converter.Instance.ConvertByteToBitmapImage(kh.khachhang.PICBI);
                imgAvatar.Source = Bitimg;
                btnAvatar.Content = imgAvatar;

                Gender = kh.khachhang.GENDER;
                if (kh.khachhang.GENDER == null)
                {
                    Gender = "unknow";
                }
                if (Gender.CompareTo("Male") == 0)
                {
                    IsMale = true;
                    IsFeMale = false;

                }
                else
                {
                    IsMale = false;
                    IsFeMale = true;
                }
                var pass = DataProvider.Ins.DB.ACCOUNT.Where(x => x.ACC == kh.khachhang.ACC).SingleOrDefault();
                Password = Converter.Instance.Base64Decode(Converter.Instance.MD5Decrypt(pass.PASS));
            }

        }

        private void ModifyEmployee()
        {
            var nv = DataProvider.Ins.DB.KHACHHANG.Where(x => x.MAKH == Id).SingleOrDefault();
            nv.HOTEN = Name;
            nv.DOANHSO = Salary;
            nv.MAIL = Mail;
            nv.MAKH = Id;
            nv.SODT = Phone;
            nv.NGSINH = Birthday;
            nv.NGDK = Joineddate;
            nv.GHICHU = Note;
            nv.ACC = Acc;
            nv.PICBI = Converter.Instance.ConvertBitmapImageToBytes(Bitimg);
            if (IsMale)
            {
                nv.GENDER = "Male";

            }
            else
            {
                nv.GENDER = "Female";
            }

            var account = DataProvider.Ins.DB.ACCOUNT.Where(x => x.ACC == Acc).SingleOrDefault();
            if (account == null)
            {
                account = new ACCOUNT() { ACC = Acc, PASS = Converter.Instance.MD5Encrypt(Converter.Instance.Base64Encode(Password)), PRI = 0 };
                DataProvider.Ins.DB.ACCOUNT.Add(account);
            }
            else
            {
                account.ACC = Acc;
                account.PASS = Converter.Instance.MD5Encrypt(Converter.Instance.Base64Encode(Password));
                account.PRI = 0;
            }
            DataProvider.Ins.DB.SaveChanges();
            loadEmployee();
        }


        void LoadPage(Page p)
        {
            //loadEmployee();

        }

        private void CreateAvatar(Button p)
        {
            p.Content = imgAvatar;
        }
        private void CheckGender()
        {
            if (IsMale == false)
            {
                IsMale = true;
                IsFeMale = false;
            }
            else if (IsMale == true)
            {
                IsMale = false;
                IsFeMale = true;
            }
        }
        private void Imagepick(Button p)
        {
            kh.chooseImg();
            Bitimg = kh.Bitimg;
            imgAvatar.Source = kh.Bitimg;
            p.Content = imgAvatar;
        }
    }
}
