using SE104_N10_QuanLySieuThi.classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
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
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Net.Mail;
using System.Net;
using System.Globalization;
using SE104_N10_QuanLySieuThi.windows;
using SE104_N10_QuanLySieuThi.crystalreport;
using SAPBusinessObjects.WPF.Viewer;

namespace SE104_N10_QuanLySieuThi.ViewModel
{
    public class StaticViewModel : BaseViewModel
    {

        private SeriesCollection _SeriesMostProduct;
        public SeriesCollection SeriesMostProduct { get => _SeriesMostProduct; set { _SeriesMostProduct = value; OnPropertyChanged(); } }

        private SeriesCollection _SeriesProduct;
        public SeriesCollection SeriesProduct { get => _SeriesProduct; set { _SeriesProduct = value; OnPropertyChanged(); } }

        private SeriesCollection _SeriesMoney;
        public SeriesCollection SeriesMoney { get => _SeriesMoney; set { _SeriesMoney = value; OnPropertyChanged(); } }

        public Func<ChartPoint, string> _PointLable { get; set; }
        public Func<double,string> yFormatter { get; set; }

        public Func<double, string> zFormatter { get; set; }


        public ICommand LoadedPieChartCmd { get; set; }
        public ICommand LoadedLineChartCmd { get; set; }

        public ICommand LoadedBarChartCmd { get; set; }

        public ICommand LoadedPieChartPnlCmd { get; set; }
        public ICommand LoadedLineChartPnlCmd { get; set; }

        public ICommand LoadedBarChartPnlCmd { get; set; }

        public ICommand LoadedPageCmd { get; set; }

        public ICommand PrintStatisticCmd { get; set; }


        public ICommand LoadRptCmd { get; set; }

        public CrystalReportsViewer viewer = new CrystalReportsViewer();


        PieChart pieChart = new PieChart();

        CartesianChart lineChart = new CartesianChart();

        CartesianChart barChart = new CartesianChart();

        private string _TextNamThongKe;
        public string TextNamThongKe { get => _TextNamThongKe; set { _TextNamThongKe = value; OnPropertyChanged(); } }

        private string _TextThangThongKe;
        public string TextThangThongKe { get => _TextThangThongKe; set { _TextThangThongKe = value; OnPropertyChanged(); } }

        private string _TextNgayThongKe;
        public string TextNgayThongKe { get => _TextNgayThongKe; set { _TextNgayThongKe = value; OnPropertyChanged(); } }

        private string _TextLineThongKe;
        public string TextLineThongKe { get => _TextLineThongKe; set { _TextLineThongKe = value; OnPropertyChanged(); } }


        private decimal _TongDoanhThuThang;
        public decimal TongDoanhThuThang { get => _TongDoanhThuThang; set { _TongDoanhThuThang = value; OnPropertyChanged(); } }


        private decimal _TongDoanhThuThangTruoc;
        public decimal TongDoanhThuThangTruoc { get => _TongDoanhThuThangTruoc; set { _TongDoanhThuThangTruoc = value; OnPropertyChanged(); } }

        private double _TiLeSoSanhThangTruoc;
        public double TiLeSoSanhThangTruoc { get => _TiLeSoSanhThangTruoc; set { _TiLeSoSanhThangTruoc = value; OnPropertyChanged(); } }

        private string _TextTiLeSoSanhThangTruoc;
        public string TextTiLeSoSanhThangTruoc { get => _TextTiLeSoSanhThangTruoc; set { _TextTiLeSoSanhThangTruoc = value; OnPropertyChanged(); } }

        private List<string> _SearchType = new List<string>() { "Year", "Month","Day" };

        public List<string> SearchType { get => _SearchType; set { _SearchType = value; OnPropertyChanged(); } }

        CultureInfo culture;
        string specifier;

        private string _SearchTypeItem;
        public string SearchTypeItem
        {
            get => _SearchTypeItem; set
            {
                _SearchTypeItem = value;
                switch (SearchTypeItem)
                {
                    case "Day":
                        TextLineThongKe = TextNgayThongKe;
                        loadLineChartProfitDay();
                        break;
                    case "Month":
                        TextLineThongKe = TextThangThongKe;
                        loadLineChartProfitMonth();
                        break;
                    case "Year":
                        TextLineThongKe = TextNamThongKe;
                        loadLineChartProfitYear();
                        break;
                    default:
                        break;
                }
                OnPropertyChanged();
            }
        }

        private void loadLineChartProfitDay()
        {
            yFormatter = value => value.ToString("F");

        }

        private void loadLineChartProfitYear()
        {
            decimal jan = 0, feb = 0, mar = 0, apr = 0, may = 0, jun = 0, jul = 0, aug = 0, sep = 0, oct = 0, nov = 0, dec = 0;
            decimal jan1 = 0, feb1 = 0, mar1 = 0, apr1 = 0, may1 = 0, jun1 = 0, jul1 = 0, aug1 = 0, sep1 = 0, oct1 = 0, nov1 = 0, dec1 = 0;

            var hd = DataProvider.Ins.DB.HOADON;
            foreach (var item in hd)
            {
                DateTime time = (DateTime)item.NGHD;

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
                        jan += (decimal)item.TRIGIA -( (decimal)item.TRIGIA*(decimal)(item.GIAMGIA/100));
                        break;
                    case 2:
                        feb += (decimal)item.TRIGIA - ((decimal)item.TRIGIA * (decimal)(item.GIAMGIA / 100));
                        break;
                    case 3:
                        mar += (decimal)item.TRIGIA - ((decimal)item.TRIGIA * (decimal)(item.GIAMGIA / 100));
                        break;
                    case 4:
                        apr += (decimal)item.TRIGIA - ((decimal)item.TRIGIA * (decimal)(item.GIAMGIA / 100));
                        break;
                    case 5:
                        may += (decimal)item.TRIGIA - ((decimal)item.TRIGIA * (decimal)(item.GIAMGIA / 100));
                        break;
                    case 6:
                        jun += (decimal)item.TRIGIA - ((decimal)item.TRIGIA * (decimal)(item.GIAMGIA / 100));
                        break;
                    case 7:
                        jul += (decimal)item.TRIGIA - ((decimal)item.TRIGIA * (decimal)(item.GIAMGIA / 100));
                        break;
                    case 8:
                        aug += (decimal)item.TRIGIA - ((decimal)item.TRIGIA * (decimal)(item.GIAMGIA / 100));
                        break;
                    case 9:
                        sep += (decimal)item.TRIGIA - ((decimal)item.TRIGIA * (decimal)(item.GIAMGIA / 100));
                        break;
                    case 10:
                        oct += (decimal)item.TRIGIA - ((decimal)item.TRIGIA * (decimal)(item.GIAMGIA / 100));
                        break;
                    case 11:
                        nov += (decimal)item.TRIGIA - ((decimal)item.TRIGIA * (decimal)(item.GIAMGIA / 100));
                        break;
                    case 12:
                        dec += (decimal)item.TRIGIA - ((decimal)item.TRIGIA * (decimal)(item.GIAMGIA / 100));
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
                Title = "Import",
                Values = new ChartValues<double> { decimal.ToDouble(jan1), decimal.ToDouble(feb1), decimal.ToDouble(mar1), decimal.ToDouble(apr1), decimal.ToDouble(may1), decimal.ToDouble(jun1), decimal.ToDouble(jul1), decimal.ToDouble(aug1), decimal.ToDouble(sep1), decimal.ToDouble(oct1), decimal.ToDouble(nov1), decimal.ToDouble(dec1) },
                DataLabels = true,

            };
            SeriesMoney.Add(lns0);
            LineSeries lns1 = new LineSeries
            {
                Title = "Sell",
                Values = new ChartValues<double> { decimal.ToDouble(jan), decimal.ToDouble(feb), decimal.ToDouble(mar), decimal.ToDouble(apr), decimal.ToDouble(may), decimal.ToDouble(jun), decimal.ToDouble(jul), decimal.ToDouble(aug), decimal.ToDouble(sep), decimal.ToDouble(oct), decimal.ToDouble(nov), decimal.ToDouble(dec) },
                PointGeometry = DefaultGeometries.Square,
                DataLabels = true,

            };
            SeriesMoney.Add(lns1);
            
            Lables = new[] { "January", "Ferbuary", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            yFormatter = value => value.ToString("N");

        }

        private DateTime _SelectedDate;
        public DateTime SelectedDate
        {
            get => _SelectedDate; set
            {
                _SelectedDate = value;
                if (SelectedDate != null)
                {
                    selectedYear = SelectedDate.Year;

                    TextNamThongKe = "Summary sold products: " + SelectedDate.Year.ToString();
                    TextThangThongKe = SelectedDate.Month + @"/" + SelectedDate.Year;
                    TextNgayThongKe = SelectedDate.Day + @"/" + SelectedDate.Month + @"/" + SelectedDate.Year;
                    loadLineChartProfitMonth();
                    loadBarChart();
                    loadPieChartMostProduct();
                }
                OnPropertyChanged();
            }
        }

        private WrapPanel _pnlPieChart = new WrapPanel();

        private WrapPanel _pnlLineChart = new WrapPanel();

        private WrapPanel _pnlColumnChart = new WrapPanel();

        public int selectedYear = DateTime.Now.Year;

        private string[] _Lables;
        public string[] Lables { get => _Lables; set { _Lables = value; OnPropertyChanged(); } }

        private string[] _Kinds;
        public string[] Kinds { get => _Kinds; set { _Kinds = value; OnPropertyChanged(); } }
        public StaticViewModel()
        {
            LoadedPageCmd = new RelayCommand<object>((p) => { return true; }, (p) => { loadPage(); });
            LoadedLineChartCmd = new RelayCommand<CartesianChart>(( p) => { return true; }, (p) => { lineChart = p; });
            LoadedBarChartCmd = new RelayCommand<CartesianChart>((p) => { return true; }, (p) => { barChart = p; });
            LoadedPieChartCmd = new RelayCommand<PieChart>((p) => { return true; }, (p) => { pieChart = p; });

            LoadedLineChartPnlCmd = new RelayCommand<WrapPanel>((p) => { return true; }, (p) => {_pnlLineChart= p; });
            LoadedBarChartPnlCmd = new RelayCommand<WrapPanel>((p) => { return true; }, (p) => { _pnlColumnChart=p; });
            LoadedPieChartPnlCmd = new RelayCommand<WrapPanel>((p) => { return true; }, (p) => { _pnlPieChart=p; });


            PrintStatisticCmd = new RelayCommand<object>((p) => { return true; }, (p) => { PrintStatistic(); });

            LoadRptCmd = new RelayCommand<CrystalReportsViewer>((p) => { viewer = p; return true; }, (p) => { return; });


            SeriesMostProduct = new SeriesCollection();
            SeriesMoney = new SeriesCollection();
            SeriesProduct = new SeriesCollection();
            SelectedDate = DateTime.Now;

            TongDoanhThuThang = 0;
            TongDoanhThuThangTruoc = 0;
            TiLeSoSanhThangTruoc = 1;

            TextNamThongKe ="Summary sold products: " +SelectedDate.Year.ToString();
            TextThangThongKe = SelectedDate.Month + @"/" + SelectedDate.Year;
            TextNgayThongKe = SelectedDate.Day + @"/" + SelectedDate.Month + @"/" + SelectedDate.Year;

            loadPieChartMostProduct();
            loadLineChartProfitMonth();
            loadBarChart();
            loadBoard();
            crys = new CrystalReport4();

        }

        private void loadBoard()
        {
            specifier = "F";
            culture = CultureInfo.CreateSpecificCulture("en-CA");
            loadTongDoanhThuThang();
            loadTiLeSoSanh();
        }

        private void loadTiLeSoSanh()
        {

            var hd = DataProvider.Ins.DB.HOADON;
            foreach (var item in hd)
            {
                DateTime time = (DateTime)item.NGHD;

                int day = time.Day;
                int month = time.Month;
                int year = time.Year;
                if (year != DateTime.Now.Year || month != DateTime.Now.Month-1)
                {
                    continue;
                }
                TongDoanhThuThangTruoc += (decimal)item.TRIGIA;
            }

            if (TongDoanhThuThangTruoc < TongDoanhThuThang)
            {
                TiLeSoSanhThangTruoc = (double)(TongDoanhThuThang / (TongDoanhThuThangTruoc + (decimal)0.1));
                TextTiLeSoSanhThangTruoc = "Increase " + TiLeSoSanhThangTruoc.ToString(specifier,culture);
            }
            else if (TongDoanhThuThangTruoc > TongDoanhThuThang)
            {
                TiLeSoSanhThangTruoc = (double)(TongDoanhThuThangTruoc / (TongDoanhThuThang + (decimal)0.1));
                TextTiLeSoSanhThangTruoc = "Decrease " + TiLeSoSanhThangTruoc.ToString(specifier, culture) + @"% so với tháng trước";
            }
            else
            {
                TextTiLeSoSanhThangTruoc = "Sellings don't change";
            }
        }

        private void loadTongDoanhThuThang()
        {

            var hd = DataProvider.Ins.DB.HOADON;
            foreach (var item in hd)
            {
                DateTime time = (DateTime)item.NGHD;

                int day = time.Day;
                int month = time.Month;
                int year = time.Year;
                if (year != DateTime.Now.Year || month != DateTime.Now.Month)
                {
                    continue;
                }
                TongDoanhThuThang += (decimal)item.TRIGIA;
            }

        }
            private void PrintStatistic()
        {
            if (_pnlPieChart == null || _pnlColumnChart == null || _pnlLineChart == null)
            {
                return;
            }
            List<string> finame = new List<string>();

            RenderTargetBitmap bmp0 = new RenderTargetBitmap((int)_pnlPieChart.ActualWidth, (int)_pnlPieChart.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            bmp0.Render(_pnlPieChart);
            MemoryStream stream = new MemoryStream();
            BitmapEncoder encoder = new BmpBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp0));
            encoder.Save(stream);
            Bitmap bmp = new Bitmap(stream);

            string dat = SelectedDate.ToString("d_M_yyyy");

            string folderName = @"reports/" + SelectedDate.ToString("yyyy");
            // If directory does not exist, create it
            if (!Directory.Exists(folderName))
            {
                Directory.CreateDirectory(folderName);
            }

            bmp.Save(folderName + @"/piechart_" + SelectedDate.ToString("yyyy") + ".png", ImageFormat.Png);
            finame.Add(folderName + @"/piechart_" + SelectedDate.ToString("yyyy") + ".png");

            //Thread.Sleep(500);

            bmp0 = new RenderTargetBitmap((int)_pnlLineChart.ActualWidth, (int)_pnlLineChart.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            bmp0.Render(_pnlLineChart);
            stream = new MemoryStream();
            encoder = new BmpBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp0));
            encoder.Save(stream);
            bmp = new Bitmap(stream);

            folderName = @"reports/" + SelectedDate.ToString("MM_yyyy");
            // If directory does not exist, create it
            if (!Directory.Exists(folderName))
            {
                Directory.CreateDirectory(folderName);
            }

            bmp.Save(folderName + @"/linechart_" + SelectedDate.ToString("yyyy") + ".png", ImageFormat.Png);
            finame.Add(folderName + @"/linechart_" + SelectedDate.ToString("yyyy") + ".png");

            //Thread.Sleep(500);

            bmp0 = new RenderTargetBitmap((int)_pnlColumnChart.ActualWidth, (int)_pnlColumnChart.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            bmp0.Render(_pnlColumnChart);
            stream = new MemoryStream();
            encoder = new BmpBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp0));
            encoder.Save(stream);
            bmp = new Bitmap(stream);


            folderName = @"reports/" + dat;
            // If directory does not exist, create it
            if (!Directory.Exists(folderName))
            {
                Directory.CreateDirectory(folderName);
            }

            bmp.Save(folderName + @"/columnchart_" + dat + ".png", ImageFormat.Png);
            finame.Add(folderName + @"/columnchart_" + dat + ".png");
            //SendReport(finame);

            openPrintBillWin();
        }
        CrystalReport4 crys = new CrystalReport4();

        private void openPrintBillWin()
        {
            winStaticReport win = new winStaticReport();
            win.Show();
            crys = new CrystalReport4();
            crys.Load(@"CrystalReport3.rep");
            viewer.ViewerCore.ReportSource = crys;
            viewer.ViewerCore.SelectionFormula = "{Command.THANG}=" + SelectedDate.Month + "and {Command.NAM}=" + SelectedDate.Year;
        }

        private void SendReport(List<string> repos=null)
        {
            if (repos == null)
            {
                return;
            }
            Thread thread = new Thread(() =>
            {
                List<Attachment> atts = new List<Attachment>();
                foreach(string item in repos)
                {
                    Attachment att = new Attachment(item);
                    atts.Add(att);
                }

                var user = DataProvider.Ins.DB.NHANVIEN.Where(x => x.MANV == MainViewModel._currentUser).SingleOrDefault();
                string email = user.MAIL;
                string message = "Report of "+SelectedDate.ToString("d-MM-yyyy")+" : ";
                //GuiMail("<mail>", email, "Report " + SelectedDate.ToString("d-MM-yyyy"), message, atts);

            });
            thread.Start();
            //System.Windows.MessageBox.Show("Sent reports!, Please check your email");

        }

        void GuiMail(string from, string to, string sub, string message,List<Attachment> atts=null)
        {
            MailMessage mailmess = new MailMessage(from, to, sub, message);
            int i = 0;
            foreach (Attachment att in atts)
            {
                if (att != null)
                {
                    mailmess.Attachments.Add(att);
                }
            }

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("<mail>", "<pass>");
            client.Send(mailmess);
        }


        private void loadPage()
        {
            loadPieChartMostProduct();
            loadLineChartProfitMonth();
            loadBarChart();
        }

        private void loadBarChart()
        {
            int countGiaDung = 0;
            int countThucPham = 0;
            int countHoaMyPham = 0;
            int countKhac = 0;


            int countGiaDung1 = 0;
            int countThucPham1 = 0;
            int countHoaMyPham1 = 0;
            int countKhac1 = 0;

            DateTime time = SelectedDate;

            int day = time.Day;
            int month = time.Month;
            int year = time.Year;

            var Selling = DataProvider.Ins.DB.HOADON.Where(x=>x.NGHD==SelectedDate);
            foreach (var his in Selling)
            {
                var historysell = DataProvider.Ins.DB.CTHD.Where(x => x.SOHD == his.SOHD);
                foreach (var item in historysell)
                {
                    var loaisell = DataProvider.Ins.DB.SANPHAM.Where(x => x.MASP == item.MASP);
                    foreach (var loai in loaisell)
                    {
                        if (loai.LOAI == "Appliances")
                        {
                            countGiaDung += (int)item.SL;
                        }
                        if (loai.LOAI == "Foods")
                        {
                            countThucPham += (int)item.SL;
                        }
                        if (loai.LOAI == "Cosmetic")
                        {
                            countHoaMyPham += (int)item.SL;
                        }
                        if (loai.LOAI == "Others")
                        {
                            countKhac += (int)item.SL;
                        }
                    }
                }
            }

            var nhap= DataProvider.Ins.DB.NHAPHANG.Where(x =>(DateTime) x.NGNH == SelectedDate);
            foreach(var item in nhap)
            {
                var sp = DataProvider.Ins.DB.SANPHAM.Where(x => x.MASP == item.MASP);
                foreach (var child in sp)
                {
                    switch (child.LOAI)
                    {
                        case "Appliances":
                            countGiaDung1 += (int)item.SLNHAPHANG;
                            break;
                        case "Foods":
                            countThucPham1 += (int)item.SLNHAPHANG;
                            break;
                        case "Cosmetic":
                            countHoaMyPham1 += (int)item.SLNHAPHANG;
                            break;

                        case "Others":
                            countKhac1 += (int)item.SLNHAPHANG;
                            break;
                        default:
                            break;
                    }
                }
                    

            }

            SeriesProduct = new SeriesCollection();
            ColumnSeries lns0 = new ColumnSeries
            {
                Title = "Import",
                Values = new ChartValues<int> { countGiaDung1, countThucPham1,countHoaMyPham1, countKhac1 },
                DataLabels = true,


            };

            SeriesProduct.Add(lns0);
            ColumnSeries lns1 = new ColumnSeries
            {
                Title = "Sell",
                Values = new ChartValues<int> { countGiaDung, countThucPham, countHoaMyPham, countKhac },
                DataLabels = true,


            };

            SeriesProduct.Add(lns1);
            Kinds = new[] { "Appliances", "Foods", "Cosmetic", "Others" };


            zFormatter = value => value.ToString("F");


        }


        private void loadLineChartProfitMonth()
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
                        jan += (decimal)item.TRIGIA - ((decimal)item.TRIGIA * (decimal)(item.GIAMGIA / 100));
                        break;
                    case 2:
                        feb += (decimal)item.TRIGIA - ((decimal)item.TRIGIA * (decimal)(item.GIAMGIA / 100));
                        break;
                    case 3:
                        mar += (decimal)item.TRIGIA - ((decimal)item.TRIGIA * (decimal)(item.GIAMGIA / 100));
                        break;
                    case 4:
                        apr += (decimal)item.TRIGIA - ((decimal)item.TRIGIA * (decimal)(item.GIAMGIA / 100));
                        break;
                    case 5:
                        may += (decimal)item.TRIGIA - ((decimal)item.TRIGIA * (decimal)(item.GIAMGIA / 100));
                        break;
                    case 6:
                        jun += (decimal)item.TRIGIA - ((decimal)item.TRIGIA * (decimal)(item.GIAMGIA / 100));
                        break;
                    case 7:
                        jul += (decimal)item.TRIGIA - ((decimal)item.TRIGIA * (decimal)(item.GIAMGIA / 100));
                        break;
                    case 8:
                        aug += (decimal)item.TRIGIA - ((decimal)item.TRIGIA * (decimal)(item.GIAMGIA / 100));
                        break;
                    case 9:
                        sep += (decimal)item.TRIGIA - ((decimal)item.TRIGIA * (decimal)(item.GIAMGIA / 100));
                        break;
                    case 10:
                        oct += (decimal)item.TRIGIA - ((decimal)item.TRIGIA * (decimal)(item.GIAMGIA / 100));
                        break;
                    case 11:
                        nov += (decimal)item.TRIGIA - ((decimal)item.TRIGIA * (decimal)(item.GIAMGIA / 100));
                        break;
                    case 12:
                        dec += (decimal)item.TRIGIA - ((decimal)item.TRIGIA * (decimal)(item.GIAMGIA / 100));
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
                Title = "Import",
                Values = new ChartValues<double> { decimal.ToDouble(jan1), decimal.ToDouble(feb1), decimal.ToDouble(mar1), decimal.ToDouble(apr1), decimal.ToDouble(may1), decimal.ToDouble(jun1), decimal.ToDouble(jul1), decimal.ToDouble(aug1), decimal.ToDouble(sep1), decimal.ToDouble(oct1), decimal.ToDouble(nov1), decimal.ToDouble(dec1) },
                DataLabels = true,

            };
            SeriesMoney.Add(lns0);
            LineSeries lns1 = new LineSeries
            {
                Title = "Sell",
                Values = new ChartValues<double> { decimal.ToDouble(jan), decimal.ToDouble(feb), decimal.ToDouble(mar), decimal.ToDouble(apr), decimal.ToDouble(may), decimal.ToDouble(jun), decimal.ToDouble(jul), decimal.ToDouble(aug), decimal.ToDouble(sep), decimal.ToDouble(oct), decimal.ToDouble(nov), decimal.ToDouble(dec) },
                PointGeometry = DefaultGeometries.Square,
                DataLabels = true,

            };
            SeriesMoney.Add(lns1);
            Lables = new[] { "Jan", "Ferb", "Mar", "Apr","May","Jun","Jul","Aug","Sep","Oct","Nov","Dec" };
            yFormatter = value => value.ToString("F");
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
                DateTime time = (DateTime)his.NGHD;
                int year = time.Year;
                if (year != SelectedDate.Year)
                {
                    break;
                }
                int day = time.Day;
                int month = time.Month;

                var historysell = DataProvider.Ins.DB.CTHD.Where(x => x.SOHD == his.SOHD);
                foreach(var item in historysell)
                {
                    var loaisell = DataProvider.Ins.DB.SANPHAM.Where(x => x.MASP == item.MASP);
                    foreach(var loai in loaisell)
                    {
                        if (loai.LOAI == "Appliances")
                        {
                            countGiaDung +=(int) item.SL;
                        }
                        if (loai.LOAI == "Foods")
                        {
                            countThucPham += (int)item.SL;
                        }
                        if (loai.LOAI == "Cosmetic")
                        {
                            countHoaMyPham += (int)item.SL;
                        }
                        if (loai.LOAI == "Others")
                        {
                            countKhac += (int)item.SL;
                        }
                    }
                }
            }

            SeriesMostProduct = new SeriesCollection();
            PieSeries giadungseries = new PieSeries
            {
                Title = "Appliances",
                Values = new ChartValues<ObservableValue> { new ObservableValue(countGiaDung) },
                DataLabels = true
            };
            SeriesMostProduct.Add(giadungseries);

            PieSeries thucphamseries = new PieSeries()
            {
                Title = "Foods",
                Values = new ChartValues<ObservableValue> { new ObservableValue(countThucPham) },
                DataLabels = true
            };
            SeriesMostProduct.Add(thucphamseries);


            PieSeries hoathucphamseries = new PieSeries
            {
                Title = "Cosmetic",
                Values = new ChartValues<ObservableValue> { new ObservableValue(countHoaMyPham) },
                DataLabels = true

            };
            SeriesMostProduct.Add(hoathucphamseries);

            PieSeries hoaseries = new PieSeries
            {
                Title = "Others",
                Values = new ChartValues<ObservableValue> { new ObservableValue(countKhac) },
                DataLabels = true
            };
            SeriesMostProduct.Add(hoaseries);


        }
    }
}
