using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
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
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace SE104_N10_QuanLySieuThi.ViewModel
{
    class AddSupplierViewModel:BaseViewModel
    {
        private string _SuppliertId;
        public string SuppliertId { get => _SuppliertId; set { _SuppliertId = value; OnPropertyChanged(); } }
        private string _SuppliertName;
        public string SuppliertName { get => _SuppliertName; set { _SuppliertName = value; OnPropertyChanged(); } }
        public ICommand AddSupplierCmd { get; set; }
        private string _Note;
        public string Note { get => _Note; set { _Note = value; OnPropertyChanged(); } }
        private string _Phone;
        public string Phone { get => _Phone; set { _Phone = value; OnPropertyChanged(); } }
        private string _Origin;
        public string Origin { get => _Origin; set { _Origin = value; OnPropertyChanged(); } }

        public AddSupplierViewModel()
        {
            AddSupplierCmd = new RelayCommand<object>((p) => { return true; }, (p) => { AddSupplier(p); });
            NewSupplier();
        }
        private void AddSupplier(object p)
        {
            try
            {
                if (DataProvider.Ins.DB.NHACUNGCAP.Where(x => x.TEN == SuppliertName).Count() > 0)
                {
                    MessageBox.Show("Supplier name existed.");
                    return;
                }
                if (SuppliertName.CompareTo(string.Empty) == 0 || Phone.CompareTo(string.Empty) == 0 || Origin.CompareTo(string.Empty) == 0)
                {
                    MessageBox.Show("Please fill information.");
                    return;
                }
                var nv = new NHACUNGCAP() { GHICHU = Note, MACC = SuppliertId, TEN = SuppliertName, SODT = Phone, XUATXU = Origin };
                DataProvider.Ins.DB.NHACUNGCAP.Add(nv);
                DataProvider.Ins.DB.SaveChanges();
                NewSupplier();
                Complete(p);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void NewSupplier()
        {
            do
            {
                SuppliertId = Converter.Instance.RandomString(5);
            }
            while (DataProvider.Ins.DB.NHACUNGCAP.Where(x => x.MACC == SuppliertId).Count() > 0);
            Phone = "";
            Origin = "";
            SuppliertName = "";
            Note = "";
        }
        private void Complete(object p)
        {
            Window win = p as Window;
            win.Close();
        }
    }
}
