using SE104_N10_QuanLySieuThi.classes;
using SE104_N10_QuanLySieuThi.Model;
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
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace SE104_N10_QuanLySieuThi.ViewModel
{
    class CustomerViewModel : BaseViewModel
    {
        public bool isMainLoaded = false;

        public KhachHang kh = new KhachHang();

        public ICommand LoadedItemCtrlCmd { get; set; }

        public ICommand LoadAvaterCmd { get; set; }
        public Button btnAvatar = new Button();
        public ICommand PickImage { get; set; }
        public Image imgAvatar = new Image();
        private BitmapImage bitimg = new BitmapImage();
        public BitmapImage Bitimg { get => bitimg; set => bitimg = value; }



        private string _Id;
        public string Id { get => _Id; set { _Id = value; OnPropertyChanged(); } }

        private string _Name;
        public string Name { get => _Name; set { _Name = value; OnPropertyChanged(); } }
        private string _Note;
        public string Note { get => _Note; set { _Note = value; OnPropertyChanged(); } }

        private string _Phone;
        public string Phone { get => _Phone; set { _Phone = value; OnPropertyChanged(); } }

        private DateTime _Joineddate;
        public DateTime Joineddate { get => _Joineddate; set { _Joineddate = value; OnPropertyChanged(); } }

        private DateTime _Birthday;
        public DateTime Birthday { get => _Birthday; set { _Birthday = value; OnPropertyChanged(); } }

        private decimal _Gross;
        public decimal Gross { get => _Gross; set { _Gross = value; OnPropertyChanged(); } }

        private string _Gender;
        public string Gender { get => _Gender; set { _Gender = value; OnPropertyChanged(); } }

        private string _Mail;
        public string Mail { get => _Mail; set { _Mail = value; OnPropertyChanged(); } }

        private string _Acc;
        public string Acc { get => _Acc; set { _Acc = value; OnPropertyChanged(); } }

        private bool _IsMale;
        public bool IsMale { get => _IsMale; set { _IsMale = value; OnPropertyChanged(); } }
        private bool _IsFeMale;
        public bool IsFeMale { get => _IsFeMale; set { _IsFeMale = value; OnPropertyChanged(); } }

        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }

        public ICommand ModifyCmd { get; set; }

        public ICommand DeleteCmd { get; set; }

        public ICommand CheckedGenderCmd { get; set; }


        private ObservableCollection<KhachHang> _KhachHangList;

        public ObservableCollection<KhachHang> KhachHangList { get => _KhachHangList; set { _KhachHangList = value; OnPropertyChanged(); } }

        public CollectionView view;

        private List<string> _SearchType = new List<string>() { "ID", "Name" };

        public List<string> SearchType { get => _SearchType; set { _SearchType = value; OnPropertyChanged(); } }

        public CustomerViewModel(){
            LoadedItemCtrlCmd = new RelayCommand<ListBox>((p) => { return true; }, (p) => { if (!isMainLoaded) { LoadKhachHangData(); isMainLoaded = true; }; });
            LoadAvaterCmd = new RelayCommand<Button>((p) => { btnAvatar = p; return true; }, (p) => { CreateAvatar(p); });
            PickImage = new RelayCommand<Button>((p) => { btnAvatar = p; return true; }, (p) => { Imagepick(p); });
            CheckedGenderCmd = new RelayCommand<object>((p) => { return true; }, (p) => { CheckGender(); });

            DeleteCmd = new RelayCommand<object>((p) => { return true; }, (p) => { Delete(p); });
            ModifyCmd = new RelayCommand<object>((p) => { return true; }, (p) => { Modify(p); });
            LoadKhachHangData();
        }

        private void Delete(object p)
        {
            KHACHHANG nv = DataProvider.Ins.DB.KHACHHANG.Where(x => x.MAKH == SelectedItem.khachhang.MAKH).SingleOrDefault();
            DataProvider.Ins.DB.KHACHHANG.Remove(nv);
            ACCOUNT account = DataProvider.Ins.DB.ACCOUNT.Where(x => x.ACC == SelectedItem.khachhang.ACC).SingleOrDefault();
            DataProvider.Ins.DB.ACCOUNT.Remove(account);
            DataProvider.Ins.DB.SaveChanges();
            SelectedItem = null;

            NewCustomer();
            LoadKhachHangData();
            winCustomerDetail win = p as winCustomerDetail;
            win.Close();
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
        private void Modify(object p)
        {
            var kh = DataProvider.Ins.DB.KHACHHANG.Where(x => x.MAKH == SelectedItem.khachhang.MAKH).SingleOrDefault();
            kh.HOTEN = Name;
            kh.DOANHSO = Gross;
            kh.GENDER = Gender;
            kh.SODT = Phone;
            kh.MAIL = Mail;
            kh.MAKH = Id;
            kh.NGSINH = Birthday;
            kh.NGDK = Joineddate;
            kh.GHICHU = Note;
            kh.ACC = Acc;
            kh.PICBI = Converter.Instance.ConvertBitmapImageToBytes(Bitimg);
            if (IsMale)
            {
                kh.GENDER = "Male";

            }
            else
            {
                kh.GENDER = "Female";
            }

            int pri = 1;
            var account = DataProvider.Ins.DB.ACCOUNT.Where(x => x.ACC == SelectedItem.khachhang.ACC).SingleOrDefault();
            if (account == null)
            {
                var newacc = new ACCOUNT() { PASS = Converter.Instance.MD5Encrypt(Converter.Instance.Base64Encode(Password)), ACC = Acc, PRI = 0 };
                DataProvider.Ins.DB.ACCOUNT.Add(newacc);
            }
            else
            {
                account.ACC = Acc;
                account.PASS = Converter.Instance.MD5Encrypt(Converter.Instance.Base64Encode(Password));
                account.PRI = pri;
            }



            DataProvider.Ins.DB.SaveChanges();

            LoadKhachHangData();

            winCustomerDetail win = p as winCustomerDetail;
            win.Close();
        }

        private void Imagepick(Button p)
        {
            btnAvatar.Background = new SolidColorBrush(Colors.AliceBlue);
            kh.chooseImg();
            Bitimg = kh.Bitimg;
            imgAvatar.Source = kh.Bitimg;
            p.Content = imgAvatar;
        }
        private void CreateAvatar(Button p)
        {
            p.Content = imgAvatar;
        }

        private void LoadKhachHangData()
        {
            KhachHangList = new ObservableCollection<KhachHang>();

            var objectList = DataProvider.Ins.DB.KHACHHANG;
            foreach (var item in objectList)
            {
                KhachHang khachhang = new KhachHang();
                khachhang.khachhang = item;
                khachhang.Bitimg = Converter.Instance.ConvertByteToBitmapImage(item.PICBI);
                khachhang.Img.Source = khachhang.Bitimg;
                KhachHangList.Add(khachhang);
            }
            view = (CollectionView)CollectionViewSource.GetDefaultView(KhachHangList);
            view.Filter = UserFilter;
        }
        private KhachHang _SelectedItem;
        public KhachHang SelectedItem
        {
            get => _SelectedItem; set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    NewCustomer();

                    btnAvatar.Background = new SolidColorBrush(Colors.AliceBlue);

                    Name = SelectedItem.khachhang.HOTEN;
                    Id = SelectedItem.khachhang.MAKH;
                    Phone = SelectedItem.khachhang.SODT;
                    Gross = (decimal)SelectedItem.khachhang.DOANHSO;

                    Birthday = (DateTime)SelectedItem.khachhang.NGSINH;
                    Joineddate = (DateTime)SelectedItem.khachhang.NGDK;
                    Mail = SelectedItem.khachhang.MAIL;
                    Note = SelectedItem.khachhang.GHICHU;
                    Acc = SelectedItem.khachhang.ACC;
                    Bitimg = Converter.Instance.ConvertByteToBitmapImage(SelectedItem.khachhang.PICBI);
                    imgAvatar.Source = Bitimg;
                    btnAvatar.Content = imgAvatar;

                    Gender = SelectedItem.khachhang.GENDER;
                    if (SelectedItem.khachhang.GENDER == null)
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

                    Acc = SelectedItem.khachhang.ACC;
                    var nv = DataProvider.Ins.DB.ACCOUNT.Where(x => x.ACC == SelectedItem.khachhang.ACC).SingleOrDefault();
                    if (nv != null)
                    {
                        Password = Converter.Instance.Base64Decode(Converter.Instance.MD5Decrypt(nv.PASS));
                    }
                    else
                    {
                        Password = "";
                    }

                    openCustomerDetail();
                }
            }
        }

        private void openCustomerDetail()
        {
            winCustomerDetail win = new winCustomerDetail();
            win.ShowDialog();
        }

        private void NewCustomer()
        {
            Id = "";
            Phone = "";
            Birthday = DateTime.Now;
            Joineddate = DateTime.Now;
            Gross = 0;
            Note = "";
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
                    return (obj as KhachHang).khachhang.HOTEN.IndexOf(Search, StringComparison.OrdinalIgnoreCase) >= 0;
                case "ID":
                    return (obj as KhachHang).khachhang.MAKH.IndexOf(Search, StringComparison.OrdinalIgnoreCase) >= 0;
                default:
                    break;
            }

            return true; ;
        }

        private void FilterItem()
        {
            CollectionViewSource.GetDefaultView(KhachHangList).Refresh();
        }
        private string _SearchTypeItem;
        public string SearchTypeItem
        {
            get => _SearchTypeItem; set
            {
                _SearchTypeItem = value;
                view.Filter = UserFilter;
                OnPropertyChanged();
            }
        }
        private string _Search;
        public string Search
        {
            get => _Search; set
            {
                _Search = value;
                NewCustomer();
                view.Filter = UserFilter;
                FilterItem();
                OnPropertyChanged();
            }
        }
    }
}
