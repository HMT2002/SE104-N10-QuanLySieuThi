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
    public class ControlBarViewModel:BaseViewModel
    {
        #region commands
        public ICommand CloseWindowCmd { get; set; }
        public ICommand MinWindowCmd { get; set; }
        public ICommand MaxWindowCmd { get; set; }
        public ICommand MouseMoveWindowCmd { get; set; }


        #endregion

        #region vary

        #endregion

        public ControlBarViewModel()
        {
            CloseWindowCmd = new RelayCommand<UserControl>((p) => { return p==null?false:true; }, (p) => {
                FrameworkElement window= GetWindowParent(p);
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
