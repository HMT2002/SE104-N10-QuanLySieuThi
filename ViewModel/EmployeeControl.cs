using SE104_N10_QuanLySieuThi.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SE104_N10_QuanLySieuThi.ViewModel
{
    public class EmployeeControl : BaseViewModel
    {
        public ICommand CloseWindowCmd { get; set; }
        public ICommand MinWindowCmd { get; set; }
        public ICommand MaxWindowCmd { get; set; }
        public ICommand MouseMoveWindowCmd { get; set; }

        public ICommand ClickedItemCtrlCmduc { get; set; }


        public string tag { get => _tag; set { _tag = value; OnPropertyChanged(); } }

        private string _tag;

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
        private decimal _Salary;
        public decimal Salary { get => _Salary; set { _Salary = value; OnPropertyChanged(); } }
        private DateTime _Joineddate;
        public DateTime Joineddate { get => _Joineddate; set { _Joineddate = value; OnPropertyChanged(); } }

        private DateTime _Birthday;
        public DateTime Birthday { get => _Birthday; set { _Birthday = value; OnPropertyChanged(); } }

        NhanVien nv = new NhanVien();


        public EmployeeControl()
        {
            CloseWindowCmd = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, (p) => {
                FrameworkElement window = GetWindowParent(p);
                var w = window as Window;
                if (w != null)
                {
                    w.Close();
                }
            });
            MaxWindowCmd = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, (p) => {
                FrameworkElement window = GetWindowParent(p);
                var w = window as Window;
                if (w != null)
                {
                    if (w.WindowState != WindowState.Maximized)
                        w.WindowState = WindowState.Maximized;
                    else
                    {
                        w.WindowState = WindowState.Normal;

                    }
                }
            });
            MinWindowCmd = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, (p) => {
                FrameworkElement window = GetWindowParent(p);
                var w = window as Window;
                if (w != null)
                {
                    if (w.WindowState != WindowState.Minimized)
                        w.WindowState = WindowState.Minimized;
                    else
                    {
                        w.WindowState = WindowState.Normal;

                    }
                }
            });
            MouseMoveWindowCmd = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, (p) => {
                FrameworkElement window = GetWindowParent(p);
                var w = window as Window;
                if (w != null)
                {
                    w.DragMove();
                }
            });
            ClickedItemCtrlCmduc = new RelayCommand<Button>((p) => { return true; }, (p) => { ClickItem(p); });

        }
        private void ClickItem(Button sender)
        {
            //NhanVien n = new NhanVien();
            //nv.getAllEmployeeFromDatabase();
            //nv.getSpecificEmployeeFromDatabase(sender.Tag.ToString());
            //Id = nv.Id;
            //Name = nv.Name;
            //Mail = nv.Mail;
            //Salary = nv.Salary;
            //Phone = nv.Phone;
            //CMND = nv.Cmnd;
            //Position = nv.Position;
            //Joineddate = DateTime.Parse(nv.Startdate);
            //Birthday = DateTime.Parse(nv.Birthday);
            //btnAvatar.Content = nv.Img;
            MessageBox.Show("Picked");
        }

        FrameworkElement GetWindowParent(UserControl p)
        {
            FrameworkElement parent = p;
            while (parent.Parent != null)
            {
                parent = parent.Parent as FrameworkElement;
            }
            return parent;
        }
    }
}
