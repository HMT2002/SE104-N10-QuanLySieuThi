using MaterialDesignThemes.Wpf;
using SE104_N10_QuanLySieuThi.classes;
using SE104_N10_QuanLySieuThi.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SE104_N10_QuanLySieuThi.ViewModel
{
    public class ManageViewModel : BaseViewModel
    {
        public static string _currentUser;

        public NhanVien nv = new NhanVien();

        public Button btnAvatar = new Button();

        NHANVIEN curUser;

        ACCOUNT curACC;

        public ICommand LoadedPageCmd { get; set; }
        public ICommand PasswordChangedCommand { get; set; }

        public ICommand CreateEmployeeCmd { get; set; }
        public ICommand NewEmployeeCmd { get; set; }
        public ICommand DeleteEmployeeCmd { get; set; }
        public ICommand ModifyEmployeeCmd { get; set; }
        public ICommand PickImage { get; set; }
        public ICommand LoadedItemCtrlCmd { get; set; }

        public ICommand LoadedStateCmd { get; set; }

        public ICommand ClickedItemCtrlCmd { get; set; }
        public ICommand LoadAvaterCmd { get; set; }

        public ICommand CheckedGenderCmd { get; set; }

        public bool isMainLoaded = false;

        public List<NhanVien> lstEmployye;

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
        private string _Id;
        public string Id { get => _Id; set { _Id = value; OnPropertyChanged(); } }
        private string _Position;
        public string Position { get => _Position; set { _Position = value; OnPropertyChanged(); } }
        private string _CMND;
        public string CMND { get => _CMND; set { _CMND = value; OnPropertyChanged(); } }
        private string _Note;
        public string Note { get => _Note; set { _Note = value; OnPropertyChanged(); } }
        private string _Gender;
        public string Gender { get => _Gender; set { _Gender = value; OnPropertyChanged(); } }

        private string _Acc;
        public string Acc { get => _Acc; set { _Acc = value; OnPropertyChanged(); } }
        private static readonly System.Text.RegularExpressions.Regex _regex = new System.Text.RegularExpressions.Regex("[^0-9.-]+");

        private string _Salary;
        public string Salary { get => _Salary; set { 
                _Salary = value;
                OnPropertyChanged();
            }
        }
        private DateTime _Joineddate;
        public DateTime Joineddate { get => _Joineddate; set { _Joineddate = value; OnPropertyChanged(); } }

        private DateTime _Birthday;
        public DateTime Birthday { get => _Birthday; set { _Birthday = value; OnPropertyChanged(); } }

        public BitmapImage Bitimg { get => bitimg; set => bitimg = value; }
        private BitmapImage bitimg = new BitmapImage();

        private ObservableCollection<NhanVien> _NhanVienList;

        public ObservableCollection<NhanVien> NhanVienList { get => _NhanVienList; set { _NhanVienList = value; OnPropertyChanged(); } }

        private bool _IsMale;
        public bool IsMale { get => _IsMale; set { _IsMale = value; OnPropertyChanged(); } }
        private bool _IsFeMale;
        public bool IsFeMale { get => _IsFeMale; set { _IsFeMale = value; OnPropertyChanged(); } }

        private string _Search;
        public string Search { get => _Search; set 
            {
                _Search = value;
                NewEmployee();
                view.Filter = UserFilter;
                FilterItem(); 
                OnPropertyChanged(); 
            } 
        }

        private List<string> _SearchType=new List<string>() { "ID","Name"};

        public List<string> SearchType { get => _SearchType; set { _SearchType = value; OnPropertyChanged(); } }

        private List<string> _PositionType = new List<string>() { "Manager", "Employee" };

        public List<string> PositionType { get => _PositionType; set { _PositionType = value; OnPropertyChanged(); } }


        public CollectionView view;


        public ManageViewModel()
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            Password = "";
            UserName = "";
            Mail = "";
            Position = "";
            Phone = "";
            Name = "";
            Note = "";
            Acc = "";
            Id = Converter.Instance.RandomString(5);
            while (DataProvider.Ins.DB.NHANVIEN.Where(x => x.MANV == Id).Count() > 0)
            {
                Id = Converter.Instance.RandomString(5);
            }
            Salary = 0.ToString();
            Joineddate = DateTime.Now;
            Birthday = DateTime.Now;
            IsMale = true;
            IsFeMale = false;
            Gender = "Male";

            curUser = DataProvider.Ins.DB.NHANVIEN.Where(x => x.MANV == MainViewModel._currentUser).SingleOrDefault();
            curACC = DataProvider.Ins.DB.ACCOUNT.Where(x => x.ACC == curUser.ACC).SingleOrDefault();



            CheckedGenderCmd = new RelayCommand<object>((p) => { return true; }, (p) => { CheckGender(); });


            LoadAvaterCmd = new RelayCommand<Button>((p) => { btnAvatar = p; return true; }, (p) => { CreateAvatar(p); });
            NewEmployeeCmd = new RelayCommand<object>((p) => {

                return true;
            }, (p) => { NewEmployee(); });

            CreateEmployeeCmd = new RelayCommand<object>((p) => {
                if (string.IsNullOrEmpty(Id))
                {
                    return false;
                }
                var display = DataProvider.Ins.DB.NHANVIEN.Where(x => x.MANV == Id);

                if (display == null || display.Count() != 0)
                {
                    return false;
                }

                return true;
            }, (p) => { CreateEmployee(); });

            ModifyEmployeeCmd = new RelayCommand<object>((p) => {

                return true;
            }, (p) => { ModifyEmployee(); });

            DeleteEmployeeCmd = new RelayCommand<object>((p) => { return true; }, (p) => { DeleteEmployee(); });
            LoadedItemCtrlCmd = new RelayCommand<ListBox>((p) => { return true; }, (p) => { LoadNhanVienData(); });
            LoadedStateCmd = new RelayCommand<object>((p) => { return true; }, (p) => { LoadPageState(p); });

            PickImage = new RelayCommand<Button>((p) => { btnAvatar = p; return true; }, (p) => { Imagepick(p); });

        }

        private void LoadPageState(object p)
        {

            if (curACC.PRI == 1)
            {
            TextBox txtbx = p as TextBox;
            txtbx.IsReadOnly = true;
            }

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
        public bool UserFilter(object obj)
        {
            if (string.IsNullOrEmpty(Search))
            {
                return true;
            }
            switch (SearchTypeItem)
            {
                case "Name":
                    return (obj as NhanVien).nhanvien.HOTEN.IndexOf(Search, StringComparison.OrdinalIgnoreCase) >= 0;
                case "ID":
                    return (obj as NhanVien).nhanvien.MANV.IndexOf(Search, StringComparison.OrdinalIgnoreCase) >= 0;
                default:
                    break;
            }

            return true; ;
        }

        private void FilterItem()
        {
            CollectionViewSource.GetDefaultView(NhanVienList).Refresh();
        }
        private string _SearchTypeItem;
        public string SearchTypeItem { get => _SearchTypeItem; set
            {
                _SearchTypeItem = value;
                view.Filter = UserFilter;
                OnPropertyChanged();
            }
        }

        private string _PositionTypeItem;
        public string PositionTypeItem { get => _PositionTypeItem; set { _PositionTypeItem = value; OnPropertyChanged(); } }
        private void NewEmployee()
        {
            Password = "";
            UserName = "";
            Password = "";
            Mail = "";
            Id = Converter.Instance.RandomString(5);
            while (DataProvider.Ins.DB.NHANVIEN.Where(x => x.MANV == Id).Count() > 0)
            {
                Id = Converter.Instance.RandomString(5);
            }
            Position = "";
            Phone = "";
            imgAvatar = new Image();
            Salary = 0.ToString();
            Name = "";
            Joineddate = DateTime.Now;
            Birthday = DateTime.Now;
            Bitimg = null;
            btnAvatar.Content = null;
            IsMale = true;
            IsFeMale = false;
            Gender = "Male";
            CMND = "";
            Acc = "";
            SelectedPositon = null;
            btnAvatar.Background = new SolidColorBrush(Colors.AliceBlue);

        }

        private void CreateAvatar(Button p)
        {

            p.Content = imgAvatar;
        }


        string tempacc = "";
        private NhanVien _SelectedItem;
        public NhanVien SelectedItem
        {
            get => _SelectedItem; set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    btnAvatar.Background = new SolidColorBrush(Colors.AliceBlue);

                    Name = SelectedItem.nhanvien.HOTEN;
                    Id = SelectedItem.nhanvien.MANV;
                    Phone = SelectedItem.nhanvien.SODT;
                    CMND = SelectedItem.nhanvien.CMND;
                    Salary = ((decimal)SelectedItem.nhanvien.LUONG).ToString();

                    Birthday =(DateTime) SelectedItem.nhanvien.NGSINH;
                    Joineddate =(DateTime) SelectedItem.nhanvien.NGVL;
                    Position = SelectedItem.nhanvien.POSITION;
                    SelectedPositon= SelectedItem.nhanvien.POSITION;
                    Mail = SelectedItem.nhanvien.MAIL;
                    Note = SelectedItem.nhanvien.GHICHU;
                    Acc = SelectedItem.nhanvien.ACC;
                    Bitimg = Converter.Instance.ConvertByteToBitmapImage(SelectedItem.nhanvien.PICBI);
                    imgAvatar.Source = Bitimg;
                    btnAvatar.Content = imgAvatar;

                    Gender = SelectedItem.nhanvien.GENDER;
                    if (SelectedItem.nhanvien.GENDER == null)
                    {
                        Gender = "unknow";
                    }
                    if (Gender.CompareTo("Male")==0)
                    {
                        IsMale = true;
                        IsFeMale = false;

                    }
                    else
                    {
                        IsMale = false;
                        IsFeMale = true;
                    }

                    Acc = SelectedItem.nhanvien.ACC;
                    tempacc = Acc;
                    var nv = DataProvider.Ins.DB.ACCOUNT.Where(x => x.ACC == SelectedItem.nhanvien.ACC).SingleOrDefault();
                    if (nv != null)
                    {
                        Password =Converter.Instance.Base64Decode(Converter.Instance.MD5Decrypt( nv.PASS)); 
                    }
                    else
                    {
                        Password = "";
                    }
                }
            }
        }


        private string _SelectedPositon;
        public string SelectedPositon { get => _SelectedPositon; set { _SelectedPositon = value; OnPropertyChanged(); } }

        private void LoadNhanVienData()
        {
            NhanVienList = new ObservableCollection<NhanVien>();
            
            var objectList = DataProvider.Ins.DB.NHANVIEN;
            foreach (var item in objectList)
            {
                NhanVien nhanvien = new NhanVien();
                nhanvien.nhanvien = item;
                nhanvien.Bitimg= Converter.Instance.ConvertByteToBitmapImage(item.PICBI);
                nhanvien.Img.Source = nhanvien.Bitimg;          
                NhanVienList.Add(nhanvien);
            }
            view = (CollectionView)CollectionViewSource.GetDefaultView(NhanVienList);
            view.Filter = UserFilter;
        }

        private void ModifyEmployee()
        {
            decimal n1;

            if (!decimal.TryParse(Salary, out n1))
            {
                MessageBox.Show("Enter salary as number only");
                return;
            }

            if (Acc.Length < 6 || Password.Length < 6||!Acc.Any(char.IsDigit)||!Password.Any(char.IsDigit))
            {
                MessageBox.Show("Username or password must be over 6 letters, contains number");
                return;
            }

            if (SelectedPositon == null)
            {
                MessageBox.Show("Please select position");
                return;
            }
            if (SelectedPositon.CompareTo(string.Empty) == 0)
            {
                MessageBox.Show("Please select position");
                return;
            }
            var nv = DataProvider.Ins.DB.NHANVIEN.Where(x => x.MANV == SelectedItem.nhanvien.MANV).SingleOrDefault();
            nv.HOTEN = Name;
            nv.LUONG = Decimal.Parse(Salary);
            nv.POSITION = SelectedPositon;
            nv.CMND = CMND;
            nv.MAIL = Mail;
            nv.MANV = Id;
            nv.SODT = Phone;
            nv.NGSINH = Birthday;
            nv.NGVL = Joineddate;
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

            int pri = 1;
            if (SelectedPositon.CompareTo("Manager") == 0)
            {
                pri = 2;
            }

            var account = DataProvider.Ins.DB.ACCOUNT.Where(x => x.ACC == tempacc).FirstOrDefault();
            var newaccount = new ACCOUNT() { ACC = Acc, PASS = Converter.Instance.MD5Encrypt(Converter.Instance.Base64Encode(Password)), PRI = pri };

            DataProvider.Ins.DB.ACCOUNT.Remove(account);
            DataProvider.Ins.DB.ACCOUNT.Add(newaccount);
            tempacc = "";

            DataProvider.Ins.DB.SaveChanges();
            LoadNhanVienData();
            NewEmployee();
        }

        private void DeleteEmployee()
        {
            NHANVIEN nv = DataProvider.Ins.DB.NHANVIEN.Where(x => x.MANV == SelectedItem.nhanvien.MANV).FirstOrDefault();
            DataProvider.Ins.DB.NHANVIEN.Remove(nv);
            DataProvider.Ins.DB.SaveChanges();
            SelectedItem = null;

            LoadNhanVienData();
            NewEmployee();
        }

        private void CreateEmployee()
        {


            decimal n1;

            if (!decimal.TryParse(Salary, out n1))
            {
                MessageBox.Show("Enter salary as number only");
                return;
            }

            if (Acc.Length < 6 || Password.Length < 6 || !Acc.Any(char.IsDigit) || !Password.Any(char.IsDigit))
            {
                MessageBox.Show("Username or password must be over 6 letters, contains number");
                return;
            }


            if (SelectedPositon == null)
            {
                MessageBox.Show("Please select position");
                return;
            }
            if (SelectedPositon.CompareTo(string.Empty) == 0||Name.CompareTo(string.Empty) == 0||Phone.CompareTo(string.Empty) == 0||CMND.CompareTo(string.Empty) == 0||Mail.CompareTo(string.Empty) == 0||Gender.CompareTo(string.Empty) == 0||Acc.CompareTo(string.Empty) == 0||Password.CompareTo(string.Empty) == 0)
            {
                MessageBox.Show("Please fill information");
                return;
            }
            int pri = 1;
            if (SelectedPositon.CompareTo("Manager") == 0)
            {
                pri = 2;
            }

            var nv1 = new NHANVIEN() { HOTEN = Name, MANV = Id, SODT = Phone, POSITION = SelectedPositon, LUONG= Decimal.Parse(Salary), CMND=CMND,MAIL=Mail,PICBI= Converter.Instance.ConvertBitmapImageToBytes(Bitimg),GENDER=Gender,NGSINH=Birthday,NGVL=Joineddate ,GHICHU=Note,ACC=Acc};
            var account = new ACCOUNT() { ACC = Acc, PASS = Converter.Instance.MD5Encrypt(Converter.Instance.Base64Encode(Password)), PRI = pri };

            var nv2 = new NhanVien() { Name = Name, ID = Id, Phone = Phone, Position = SelectedPositon, Salary = Decimal.Parse(Salary), Cmnd = CMND, Mail = Mail,Bitimg=Bitimg,Gender=Gender,Birthday=Birthday,Startdate=Joineddate,nhanvien=nv1 };
            NhanVienList.Add(nv2);

            DataProvider.Ins.DB.NHANVIEN.Add(nv1);
            DataProvider.Ins.DB.ACCOUNT.Add(account);

            DataProvider.Ins.DB.SaveChanges();
            LoadNhanVienData();
            NewEmployee();
        }

        private void Imagepick(Button p)
        {
            btnAvatar.Background = null;
            nv.chooseImg();
            Bitimg = nv.Bitimg;
            imgAvatar.Source = nv.Bitimg;
            p.Content = imgAvatar;
        }

        public byte[] convertImgToByte(BitmapImage bitimg)
        {
            MemoryStream memStream = new MemoryStream();
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitimg));
            encoder.Save(memStream);
            return memStream.ToArray();
        }
    }
}
