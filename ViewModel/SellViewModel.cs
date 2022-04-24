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
    class SellViewModel:BaseViewModel
    {
        ListBox lstbxSelected = new ListBox();
        public ICommand LoadedItemCtrlCmd { get; set; }

        public ICommand listboxSelectedItem_SelectionChangedCmd { get; set; }

        ObservableCollection<SanPham> _listSelecteditems;
        public ObservableCollection<SanPham> listSelecteditems { get => _listSelecteditems; set { _listSelecteditems = value; OnPropertyChanged(); } }

        public bool isMainLoaded = false;

        SanPham sp = new SanPham();

        
        public SellViewModel()
        {
            sp.getAllProductFromDatabase();
            listSelecteditems = new ObservableCollection<SanPham>(sp.getAllProductFromDatabase());
            LoadedItemCtrlCmd = new RelayCommand<ListBox>((p) => { lstbxSelected = p; return true; }, (p) => { if (!isMainLoaded) { AddItemIntoListBox(p); isMainLoaded = true; }; });

        }
        private void AddItemIntoListBox(ListBox p)
        {

        }

        public void listboxSelectedItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox = sender as ListBox;
            MessageBox.Show(listBox.SelectedItem.ToString());
        }
    }
}
