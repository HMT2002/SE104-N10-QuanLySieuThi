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
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;
using SE104_N10_QuanLySieuThi.Model;

namespace SE104_N10_QuanLySieuThi.ViewModel
{
    public class StaticViewModel : BaseViewModel
    {
        public SeriesCollection SeriesMostProduct { get; set; }

        public StaticViewModel()
        {
            loadPieChartMostProduct();

        }

        private void loadPieChartMostProduct()
        {
            int countGiaDung = 0;
            int countThucPham = 0;
            int countHoaMyPham = 0;
            int countKhac = 0;

            var Selling = DataProvider.Ins.DB.HOADON;
            foreach(var his in Selling)
            {
                var historysell = DataProvider.Ins.DB.CTHD.Where(x => x.SOHD == his.SOHD);
                foreach(var item in historysell)
                {
                    var loaisell = DataProvider.Ins.DB.SANPHAM.Where(x => x.MASP == item.MASP);
                    foreach(var loai in loaisell)
                    {
                        if (loai.LOAI == "Gia dụng")
                        {
                            countGiaDung +=(int) item.SL;
                        }
                        if (loai.LOAI == "Thực phẩm")
                        {
                            countThucPham += (int)item.SL;
                        }
                        if (loai.LOAI == "Hoá mỹ phẩm")
                        {
                            countHoaMyPham += (int)item.SL;
                        }
                        if (loai.LOAI == "Khác")
                        {
                            countKhac += (int)item.SL;
                        }
                    }
                }
            }

            SeriesMostProduct = new SeriesCollection()
            {

                new PieSeries
                {
                    Title = "Gia dụng",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(countGiaDung) },
                    DataLabels = true
                },

                new PieSeries
                {
                    Title = "Thực phẩm",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(countThucPham) },
                    DataLabels = true
                },

                 new PieSeries
                 {
                 Title = "Hoá mỹ phẩm",
                 Values = new ChartValues<ObservableValue> { new ObservableValue(countHoaMyPham) },
                 DataLabels = true

                 },
                 new PieSeries
                 {
                 Title = "Khác",
                 Values = new ChartValues<ObservableValue> { new ObservableValue(countKhac) },
                 DataLabels = true
                 }

            };
        }
    }
}
