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
using System.Windows.Media;

namespace SE104_N10_QuanLySieuThi.ViewModel
{
    public class StaticViewModel : BaseViewModel
    {

        private SeriesCollection _SeriesMostProduct;
        public SeriesCollection SeriesMostProduct { get => _SeriesMostProduct; set { _SeriesMostProduct = value; OnPropertyChanged(); } }

        private SeriesCollection _SeriesMoney;
        public SeriesCollection SeriesMoney { get => _SeriesMoney; set { _SeriesMoney = value; OnPropertyChanged(); } }

        public Func<ChartPoint, string> _PointLable { get; set; }
        public Func<double,string> yFormatter { get; set; }

        public Func<double, string> zFormatter { get; set; }


        public ICommand LoadedPieChartCmd { get; set; }
        public ICommand LoadedLineChartCmd { get; set; }

        public ICommand LoadedBarChartCmd { get; set; }


        public ICommand LoadedPageCmd { get; set; }

        PieChart pieChart = new PieChart();

        CartesianChart lineChart = new CartesianChart();

        CartesianChart barChart = new CartesianChart();

        private DateTime _SelectedDate;
        public DateTime SelectedDate { get => _SelectedDate; set {
                _SelectedDate = value;
                if (SelectedDate != null)
                {
                    SeriesMoney = new SeriesCollection();
                    selectedYear = SelectedDate.Year;
                    loadLineChartProfit();

                }
                OnPropertyChanged();
            } 
        }

        public int selectedYear = DateTime.Now.Year;

        private string[] _Lables;
        public string[] Lables { get => _Lables; set { _Lables = value; OnPropertyChanged(); } }


        public StaticViewModel()
        {
            LoadedPageCmd = new RelayCommand<object>((p) => { return true; }, (p) => { loadPage(); });
            LoadedLineChartCmd = new RelayCommand<CartesianChart>(( p) => { return true; }, (p) => { lineChart = p; });
            LoadedBarChartCmd = new RelayCommand<CartesianChart>((p) => { return true; }, (p) => { barChart = p; });
            LoadedPieChartCmd = new RelayCommand<PieChart>((p) => { return true; }, (p) => { pieChart = p; });

            SeriesMostProduct = new SeriesCollection();
            SeriesMoney = new SeriesCollection();
            SelectedDate = DateTime.Now;

            loadPieChartMostProduct();
            loadLineChartProfit();
            loadBarChart();
        }

        private void loadBarChart()
        {
            decimal jan = 0, feb = 0, mar = 0, apr = 0, may = 0, jun = 0, jul = 0, aug = 0, sep = 0, oct = 0, nov = 0, dec = 0;
            decimal jan1 = 0, feb1 = 0, mar1 = 0, apr1 = 0, may1 = 0, jun1 = 0, jul1 = 0, aug1 = 0, sep1 = 0, oct1 = 0, nov1 = 0, dec1 = 0;

            var Selling = DataProvider.Ins.DB.HOADON;
            foreach (var his in Selling)
            {
                var historysell = DataProvider.Ins.DB.CTHD.Where(x => x.SOHD == his.SOHD);
                foreach (var item in historysell)
                {
                    var loaisell = DataProvider.Ins.DB.SANPHAM.Where(x => x.MASP == item.MASP);
                    foreach (var loai in loaisell)
                    {
                        if (loai.LOAI == "Gia dụng")
                        {
                            //countGiaDung += (int)item.SL;
                        }
                        if (loai.LOAI == "Thực phẩm")
                        {
                            //countThucPham += (int)item.SL;
                        }
                        if (loai.LOAI == "Hoá mỹ phẩm")
                        {
                            //countHoaMyPham += (int)item.SL;
                        }
                        if (loai.LOAI == "Khác")
                        {
                            //countKhac += (int)item.SL;
                        }
                    }
                }
            }
            zFormatter = value => value.ToString("N");

            //SeriesMoney.Add(new LineSeries
            //{
            //    Title = "8",
            //    Values = new ChartValues<double> { 5, 3, 2 },
            //    LineSmoothness = 0,
            //    PointGeometry = Geometry.Parse("m 25 70.36218 20 -28 -20 22 -8 -6 z"),
            //    PointGeometrySize = 50,
            //    PointForeground=Brushes.Green
            //});
            //SeriesMoney[2].Values.Add(2D);

        }

        private void loadPage()
        {
        }

        private void loadLineChartProfit()
        {
            decimal jan = 0, feb = 0, mar = 0, apr = 0, may = 0, jun = 0, jul = 0, aug = 0, sep = 0, oct = 0, nov = 0, dec = 0;
            decimal jan1 = 0, feb1 = 0, mar1 = 0, apr1 = 0, may1 = 0, jun1 = 0, jul1 = 0, aug1 = 0, sep1 = 0, oct1 = 0, nov1 = 0, dec1 = 0;

            var hd = DataProvider.Ins.DB.HOADON;
            foreach(var item in hd)
            {
                DateTime time =(DateTime) item.NGHD;

                int day = time.Day;
                int month = time.Month;
                int year = time.Year;
                if (year != SelectedDate.Year)
                {
                    break;
                }
                switch (month)
                {
                    case 1:
                        jan += (decimal)item.TRIGIA;
                        break;
                    case 2:
                        feb += (decimal)item.TRIGIA;
                        break;
                    case 3:
                        mar += (decimal)item.TRIGIA;
                        break;
                    case 4:
                        apr += (decimal)item.TRIGIA;
                        break;
                    case 5:
                        may += (decimal)item.TRIGIA;
                        break;
                    case 6:
                        jun += (decimal)item.TRIGIA;
                        break;
                    case 7:
                        jul += (decimal)item.TRIGIA;
                        break;
                    case 8:
                        aug += (decimal)item.TRIGIA;
                        break;
                    case 9:
                        sep += (decimal)item.TRIGIA;
                        break;
                    case 10:
                        oct += (decimal)item.TRIGIA;
                        break;
                    case 11:
                        nov += (decimal)item.TRIGIA;
                        break;
                    case 12:
                        dec += (decimal)item.TRIGIA;
                        break;

                    default:
                        break;
                }

            }

            var Selling = DataProvider.Ins.DB.NHAPHANG;
            foreach (var his in Selling)
            {
                DateTime time = (DateTime)his.NGNH;
                int day = time.Day;
                int month = time.Month;
                int year = time.Year;
                if (year != SelectedDate.Year)
                {
                    break;
                }
                switch (month)
                {
                    case 1:
                        jan1 += (decimal)his.TRIGIA;
                        break;
                    case 2:
                        feb1 += (decimal)his.TRIGIA;
                        break;
                    case 3:
                        mar1 += (decimal)his.TRIGIA;
                        break;
                    case 4:
                        apr1 += (decimal)his.TRIGIA;
                        break;
                    case 5:
                        may1 += (decimal)his.TRIGIA;
                        break;
                    case 6:
                        jun1 += (decimal)his.TRIGIA;
                        break;
                    case 7:
                        jul1 += (decimal)his.TRIGIA;
                        break;
                    case 8:
                        aug1 += (decimal)his.TRIGIA;
                        break;
                    case 9:
                        sep1 += (decimal)his.TRIGIA;
                        break;
                    case 10:
                        oct1 += (decimal)his.TRIGIA;
                        break;
                    case 11:
                        nov1 += (decimal)his.TRIGIA;
                        break;
                    case 12:
                        dec1 += (decimal)his.TRIGIA;
                        break;

                    default:
                        break;
                }
            }

            SeriesMoney = new SeriesCollection();
            LineSeries lns0 = new LineSeries
            {
                Title = "Nhập",
                Values = new ChartValues<double> { decimal.ToDouble(jan1), decimal.ToDouble(feb1), decimal.ToDouble(mar1), decimal.ToDouble(apr1), decimal.ToDouble(may1), decimal.ToDouble(jun1), decimal.ToDouble(jul1), decimal.ToDouble(aug1), decimal.ToDouble(sep1), decimal.ToDouble(oct1), decimal.ToDouble(nov1), decimal.ToDouble(dec1) },

            };
            SeriesMoney.Add(lns0);
            LineSeries lns1 = new LineSeries
            {
                Title = "Bán",
                Values = new ChartValues<double> { decimal.ToDouble(jan), decimal.ToDouble(feb), decimal.ToDouble(mar), decimal.ToDouble(apr), decimal.ToDouble(may), decimal.ToDouble(jun), decimal.ToDouble(jul), decimal.ToDouble(aug), decimal.ToDouble(sep), decimal.ToDouble(oct), decimal.ToDouble(nov), decimal.ToDouble(dec) },
                PointGeometry = DefaultGeometries.Square,

            };
            SeriesMoney.Add(lns1);


            Lables = new[] { "Gia dụng", "Thực phẩm", "Hoá mỹ phẩm", "Khác", };

            yFormatter = value => value.ToString("C");

            //SeriesMoney.Add(new LineSeries
            //{
            //    Title = "8",
            //    Values = new ChartValues<double> { 5, 3, 2 },
            //    LineSmoothness = 0,
            //    PointGeometry = Geometry.Parse("m 25 70.36218 20 -28 -20 22 -8 -6 z"),
            //    PointGeometrySize = 50,
            //    PointForeground=Brushes.Green
            //});
            //SeriesMoney[2].Values.Add(2D);


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
