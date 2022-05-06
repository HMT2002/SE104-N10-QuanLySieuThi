using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SE104_N10_QuanLySieuThi.Model
{
    public class DataProvider
    {
        private static DataProvider _ins;

        public static DataProvider Ins { get { if (_ins == null) _ins = new DataProvider();return _ins; } set => _ins = value; }

        public QUANLYSIEUTHIEntities DB { get => db; set => db = value; }

        private QUANLYSIEUTHIEntities db;

        public DataProvider()
        {
            DB = new QUANLYSIEUTHIEntities();

        }
    }
}
