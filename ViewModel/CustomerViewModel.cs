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
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;


namespace SE104_N10_QuanLySieuThi.ViewModel
{
    class CustomerViewModel : BaseViewModel
    {
        public bool isMainLoaded = false;

        public ICommand LoadedItemCtrlCmd { get; set; }

        private ObservableCollection<KhachHang> _KhachHangList;

        public ObservableCollection<KhachHang> KhachHangList { get => _KhachHangList; set { _KhachHangList = value; OnPropertyChanged(); } }

        public CollectionView view;

        public CustomerViewModel(){
            LoadedItemCtrlCmd = new RelayCommand<ListBox>((p) => { return true; }, (p) => { if (!isMainLoaded) { LoadKhachHangData(); isMainLoaded = true; }; });

        }

        private void LoadKhachHangData()
        {
            KhachHangList = new ObservableCollection<KhachHang>();

            var objectList = DataProvider.Ins.DB.KHACHHANG;
            foreach (var item in objectList)
            {
                KhachHang khachhang = new KhachHang();
                khachhang.khachhang = item;
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
                    //Name = SelectedItem.nhanvien.HOTEN;
                    //Id = SelectedItem.nhanvien.MANV;
                    //Phone = SelectedItem.nhanvien.SODT;
                    //CMND = SelectedItem.nhanvien.CMND;
                    //Salary = (decimal)SelectedItem.nhanvien.LUONG;

                    //Birthday = (DateTime)SelectedItem.nhanvien.NGSINH;
                    //Joineddate = (DateTime)SelectedItem.nhanvien.NGVL;
                    //Position = SelectedItem.nhanvien.POSITION;
                    //SelectedPositon = SelectedItem.nhanvien.POSITION;
                    //Mail = SelectedItem.nhanvien.MAIL;
                    //Note = SelectedItem.nhanvien.GHICHU;
                    //Acc = SelectedItem.nhanvien.ACC;
                    //Bitimg = Converter.Instance.ConvertByteToBitmapImage(SelectedItem.nhanvien.PICBI);
                    //imgAvatar.Source = Bitimg;
                    //btnAvatar.Content = imgAvatar;

                    //Gender = SelectedItem.nhanvien.GENDER;
                    //if (SelectedItem.nhanvien.GENDER == null)
                    //{
                    //    Gender = "unknow";
                    //}
                    //if (Gender.CompareTo("Male") == 0)
                    //{
                    //    IsMale = true;
                    //    IsFeMale = false;

                    //}
                    //else
                    //{
                    //    IsMale = false;
                    //    IsFeMale = true;
                    //}

                    //Acc = SelectedItem.nhanvien.ACC;
                    //var nv = DataProvider.Ins.DB.ACCOUNT.Where(x => x.ACC == SelectedItem.nhanvien.ACC).SingleOrDefault();
                    //if (nv != null)
                    //{
                    //    Password = nv.PASS;

                    //}
                    //else
                    //{
                    //    Password = "";
                    //}
                }
            }
        }

        private void NewCustomer()
        {

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
