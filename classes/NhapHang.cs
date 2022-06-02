using SE104_N10_QuanLySieuThi.Model;
using SE104_N10_QuanLySieuThi.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace SE104_N10_QuanLySieuThi.classes
{
    public class NhapHang
    {
        private NHAPHANG _nhaphang;

        public NHAPHANG nhaphang { get => _nhaphang; set => _nhaphang = value; }
    }
}
