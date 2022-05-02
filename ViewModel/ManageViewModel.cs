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
using System.Windows.Media.Imaging;

namespace SE104_N10_QuanLySieuThi.ViewModel
{
    public class ManageViewModel : BaseViewModel
    {
        public NhanVien nv = new NhanVien();

        public Button btnAvatar = new Button();

        public ICommand LoadedPageCmd { get; set; }


        public ICommand PasswordChangedCommand { get; set; }

        public ICommand CreateEmployeeCmd { get; set; }

        public ICommand NewEmployeeCmd { get; set; }

        public ICommand DeleteEmployeeCmd { get; set; }
        public ICommand ModifyEmployeeCmd { get; set; }
        public ICommand PickImage { get; set; }

        public ICommand LoadedItemCtrlCmd { get; set; }
        public ICommand ClickedItemCtrlCmd { get; set; }
        public ICommand LoadAvaterCmd { get; set; }

        public ICommand txtSearch_TextChanged { get; set; }

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

        private string _Gender;
        public string Gender { get => _Gender; set { _Gender = value; OnPropertyChanged(); } }
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
                view.Filter = UserFilter;
                FilterItem(); 
                OnPropertyChanged(); 
            } 
        }

        private List<string> _SearchType=new List<string>() { "ID","Name"};

        public List<string> SearchType { get => _SearchType; set { _SearchType = value; OnPropertyChanged(); } }


        public CollectionView view;


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
            Name = "";
            imgAvatar = new Image();
            Salary = 0;
            Joineddate = DateTime.Now;
            Birthday = DateTime.Now;
            IsMale = true;
            IsFeMale = false;
            Gender = "Male";

            LoadedPageCmd = new RelayCommand<Page>((p) => { return true; }, (p) => { LoadPage(p); });

            CheckedGenderCmd = new RelayCommand<object>((p) => { return true; }, (p) => { CheckGender(p); });


            LoadAvaterCmd = new RelayCommand<Button>((p) => { btnAvatar = p; return true; }, (p) => { CreateAvatar(p); });
            NewEmployeeCmd = new RelayCommand<object>((p) => {

                return true;
            }, (p) => { NewEmployee(p); });

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
            }, (p) => { CreateEmployee(p); });

            ModifyEmployeeCmd = new RelayCommand<object>((p) => {
                if (SelectedItem==null)
                {
                    return false;
                }
                var display = DataProvider.Ins.DB.NHANVIEN.Where(x => x.MANV == Id);

                if (display == null || display.Count() == 0)
                {
                    return false;
                }

                return true;
            }, (p) => { ModifyEmployee(p); });

            DeleteEmployeeCmd = new RelayCommand<object>((p) => { return true; }, (p) => { DeleteEmployee(p); });
            LoadedItemCtrlCmd = new RelayCommand<ListBox>((p) => { itemsControl = p; return true; }, (p) => { if (!isMainLoaded) {LoadNhanVienData(); isMainLoaded = true; }; });
            PickImage = new RelayCommand<Button>((p) => { btnAvatar = p; return true; }, (p) => { Imagepick(p); });

        }
        private void CheckGender(object p)
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
                case "1":
                    return (obj as NhanVien).Name.IndexOf(Search, StringComparison.OrdinalIgnoreCase) >= 0;
                case "0":
                    return (obj as NhanVien).ID.IndexOf(Search, StringComparison.OrdinalIgnoreCase) >= 0;
                default:
                    break;
            }

            return true; ;
        }

        private void FilterItem()
        {
            CollectionViewSource.GetDefaultView(NhanVienList).Refresh();
        }

        private void NewEmployee(object p)
        {
            Password = "";
            UserName = "";
            Mail = "";
            Id = "";
            Position = "";
            Phone = "";
            imgAvatar = new Image();
            Salary = 0;
            Name = "";
            Joineddate = DateTime.Now;
            Birthday = DateTime.Now;
            Bitimg = null;
            btnAvatar.Content = null;
            IsMale = true;
            IsFeMale = false;
            Gender = "Male";
        }

        private void CreateAvatar(Button p)
        {
            p.Content = imgAvatar;
        }

        private NhanVien _SelectedItem;
        public NhanVien SelectedItem
        {
            get => _SelectedItem; set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    Name = SelectedItem.Name;
                    Id = SelectedItem.ID;
                    Phone = SelectedItem.Phone;
                    CMND = SelectedItem.Cmnd;
                    Salary = SelectedItem.Salary;

                    Birthday = SelectedItem.Birthday;
                    Joineddate = SelectedItem.Startdate;

                    Position = SelectedItem.Position;
                    Mail = SelectedItem.Mail;
                    Bitimg = SelectedItem.Bitimg;
                    imgAvatar.Source = Bitimg;
                    btnAvatar.Content = imgAvatar;

                    Gender = SelectedItem.Gender;
                    if (SelectedItem.Gender == null)
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
                }
            }

        }

        public enum SelectSearchType { ID,Name}

        private string _SearchTypeItem;
        public string SearchTypeItem { get => _SearchTypeItem; set
            {
                _SearchTypeItem = value;
                view.Filter = UserFilter;
                OnPropertyChanged();
            } 
        }

        private void LoadNhanVienData()
        {
            NhanVienList = new ObservableCollection<NhanVien>();
            
            var objectList = DataProvider.Ins.DB.NHANVIEN;
            int i = 1;
            foreach (var item in objectList)
            {
                NhanVien nhanvien = new NhanVien();

                nhanvien.nhanvien = item;
                nhanvien.Name = item.HOTEN;

                nhanvien.ID = item.MANV;
                nhanvien.Mail = item.MAIL;
                nhanvien.Phone = item.SODT;
                nhanvien.Position = item.POSITION;
                nhanvien.Birthday = (DateTime)item.NGSINH;
                nhanvien.Startdate = (DateTime)item.NGVL;
                nhanvien.Salary = (decimal)item.LUONG;
                nhanvien.Cmnd = item.CMND;
                nhanvien.Bitimg= Converter.Instance.ConvertByteToBitmapImage(item.PICBI);
                nhanvien.Img.Source = nhanvien.Bitimg;
                nhanvien.Gender = item.GENDER;
                
                NhanVienList.Add(nhanvien);
                i++;
            }

            view = (CollectionView)CollectionViewSource.GetDefaultView(NhanVienList);
            view.Filter = UserFilter;
        }

        private void ModifyEmployee(object p)
        {
            var nv = DataProvider.Ins.DB.NHANVIEN.Where(x => x.MANV == SelectedItem.ID).SingleOrDefault();
            nv.HOTEN = Name;
            nv.LUONG = Salary;
            nv.POSITION = Position;
            nv.CMND = CMND;
            nv.MAIL = Mail;
            nv.MANV = Id;
            nv.SODT = Phone;
            nv.NGSINH = Birthday;
            nv.NGVL = Joineddate;
            nv.PICBI = Converter.Instance.ConvertBitmapImageToBytes(Bitimg);
            if (IsMale)
            {
                nv.GENDER = "Male";

            }
            else
            {
                nv.GENDER = "Female";
            }
            DataProvider.Ins.DB.SaveChanges();
            LoadNhanVienData();
        }

        private void DeleteEmployee(object p)
        {

        }

        private void CreateEmployee(object p)
        {
            var nv = new NHANVIEN() { HOTEN = Name, MANV = Id, SODT = Phone, POSITION = Position,LUONG=Salary,CMND=CMND,MAIL=Mail,PICBI= convertImgToByte(Bitimg),GENDER=Gender,NGSINH=Birthday,NGVL=Joineddate }; 
            var nv2 = new NhanVien() { Name = Name, ID = Id, Phone = Phone, Position = Position, Salary = Salary, Cmnd = CMND, Mail = Mail,Bitimg=Bitimg,Gender=Gender,Birthday=Birthday,Startdate=Joineddate };
            NhanVienList.Add(nv2);
            DataProvider.Ins.DB.NHANVIEN.Add(nv);
            DataProvider.Ins.DB.SaveChanges();
            LoadNhanVienData();
        }

        private void Imagepick(Button p)
        {
            nv.chooseImg();
            Bitimg = nv.Bitimg;
            imgAvatar.Source = nv.Bitimg;
            p.Content = imgAvatar;
        }

        void LoadPage(Page p)
        {

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
