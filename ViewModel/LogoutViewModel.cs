using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SE104_N10_QuanLySieuThi.ViewModel
{
    class LogoutViewModel:BaseViewModel
    {
        public ICommand QuitCmd { get; set; }

        public LogoutViewModel()
        {
            QuitCmd = new RelayCommand<object>((p) => { return true; }, (p) => { Quit(); });

        }

        private void Quit()
        {
            Application.Current.Shutdown();
        }
    }
}
