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
    class SettingViewModel : BaseViewModel
    {
        public NhanVien nv = new NhanVien();

        public ICommand CheckedGenderCmd { get; set; }
        public ICommand LoadAvaterCmd { get; set; }
        public ICommand LoadedPageCmd { get; set; }
        public ICommand PickImage { get; set; }

        public Button btnAvatar = new Button();
        public Image imgAvatar = new Image();

        private BitmapImage bitimg = new BitmapImage();
        public BitmapImage Bitimg { get => bitimg; set => bitimg = value; }

        private bool _IsMale;
        public bool IsMale { get => _IsMale; set { _IsMale = value; OnPropertyChanged(); } }
        private bool _IsFeMale;
        public bool IsFeMale { get => _IsFeMale; set { _IsFeMale = value; OnPropertyChanged(); } }

        private string _Gender;
        public string Gender { get => _Gender; set { _Gender = value; OnPropertyChanged(); } }
        public SettingViewModel()
        {
            nv.ID = "NV003";
            loadEmployee();



            LoadedPageCmd = new RelayCommand<Page>((p) => { return true; }, (p) => { LoadPage(p); });
            LoadAvaterCmd = new RelayCommand<Button>((p) => { btnAvatar = p; return true; }, (p) => { CreateAvatar(p); });

            CheckedGenderCmd = new RelayCommand<object>((p) => { return true; }, (p) => { CheckGender(); });


            PickImage = new RelayCommand<Button>((p) => { btnAvatar = p; return true; }, (p) => { Imagepick(p); });

        }
        private void loadEmployee()
        {
            var objectList = DataProvider.Ins.DB.NHANVIEN.Where(x => x.MANV == nv.ID);
            foreach (var item in objectList)
            {
                nv = new NhanVien();
                nv.nhanvien = item;
                nv.Name = item.HOTEN;
                nv.ID = item.MANV;
                nv.Mail = item.MAIL;
                nv.Phone = item.SODT;
                nv.Position = item.POSITION;
                nv.Birthday = (DateTime)item.NGSINH;
                nv.Startdate = (DateTime)item.NGVL;
                nv.Salary = (decimal)item.LUONG;
                nv.Cmnd = item.CMND;
                nv.Bitimg = Converter.Instance.ConvertByteToBitmapImage(item.PICBI);
                nv.Img.Source = nv.Bitimg;
                nv.Gender = item.GENDER;
            }
            Gender = nv.Gender;

            if (nv.Gender.CompareTo("Male")==0)
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

        void LoadPage(Page p)
        {

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
            nv.chooseImg();
            Bitimg = nv.Bitimg;
            imgAvatar.Source = nv.Bitimg;
            p.Content = imgAvatar;
        }
    }
}
