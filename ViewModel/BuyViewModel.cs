using PdfSharp.Drawing;
using PdfSharp.Pdf;
using SE104_N10_QuanLySieuThi.classes;
using SE104_N10_QuanLySieuThi.Model;
using SE104_N10_QuanLySieuThi.windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SE104_N10_QuanLySieuThi.ViewModel
{
    public class BuyViewModel:BaseViewModel
    {
        private static BuyViewModel ins = new BuyViewModel();
        public ICommand LoadedGridItemCmd { get; set; }
        public ICommand openImportproductCmd { get; set; }
        public ICommand ReportImportCmd { get; set; }


        private ObservableCollection<NhapHang> _DealingHistoryList;

        public ObservableCollection<NhapHang> DealingHistoryList { get => _DealingHistoryList; set { _DealingHistoryList = value; OnPropertyChanged(); } }


        private NhapHang _SelectedImport;

        public NhapHang SelectedImport { get => _SelectedImport; set {
                _SelectedImport = value;
                if (SelectedImport != null)
                {
                    ImportID = SelectedImport.nhaphang.MANH;
                    loadReport();
                    SeletedProduct = SelectedImport.nhaphang.SANPHAM.TENSP;
                    ImportDate =(DateTime) SelectedImport.nhaphang.NGNH;
                    Price =(decimal) SelectedImport.nhaphang.SANPHAM.GIA;
                    Ammount = (int)(SelectedImport.nhaphang.SANPHAM.SL - SelectedImport.nhaphang.SLNHAPHANG);
                    AmmountImport = ((int)SelectedImport.nhaphang.SLNHAPHANG).ToString();
                    SummaryImport = ((decimal)SelectedImport.nhaphang.TRIGIA).ToString();
                    imgAvatar = new System.Windows.Controls.Image();
                    convertImgFromByte(SelectedImport.nhaphang.SANPHAM.PICBI);
                    imgAvatar.Source = Bitimg;
                    btnAvatar.Content = imgAvatar;
                    //winImportDetail win = new winImportDetail();
                    //win.ShowDialog();

                }
                OnPropertyChanged();
            }
        }

        SANPHAM curProduct = new SANPHAM();

        private string _ProductName;
        public string ProductName { get => _ProductName; set { _ProductName = value; OnPropertyChanged(); } }
        private string _ProductID;
        public string ProductID { get => _ProductID; set { _ProductID = value; OnPropertyChanged(); } }
        private string _ImportID;
        public string ImportID { get => _ImportID; set { _ImportID = value; OnPropertyChanged(); } }
        private DateTime _ImportDate;
        public DateTime ImportDate { get => _ImportDate; set { _ImportDate = value; OnPropertyChanged(); } }
        private List<string> _ListProduct = new List<string>();
        public List<string> ListProduct { get => _ListProduct; set { _ListProduct = value; OnPropertyChanged(); } }

        public BitmapImage Bitimg { get => bitimg; set => bitimg = value; }
        private BitmapImage bitimg = new BitmapImage();
        public ICommand LoadAvatarCmd { get; set; }
        public Button btnAvatar = new Button();
        public System.Windows.Controls.Image imgAvatar = new System.Windows.Controls.Image();
        public void convertImgFromByte(byte[] data)
        {
            using (var ms = new System.IO.MemoryStream(data))
            {
                Bitimg = new BitmapImage();
                Bitimg.BeginInit();
                Bitimg.CacheOption = BitmapCacheOption.OnLoad; // here
                Bitimg.StreamSource = ms;
                Bitimg.EndInit();
                return;
            }
        }

        private string _SeletedProduct;
        public string SeletedProduct
        {
            get => _SeletedProduct; set
            {
                _SeletedProduct = value;
                if (SeletedProduct != null)
                {
                    var ncc = DataProvider.Ins.DB.SANPHAM.Where(x => x.TENSP == SeletedProduct).SingleOrDefault();
                    if (ncc != null)
                    {
                        curProduct = ncc;
                        imgAvatar = new System.Windows.Controls.Image();
                        convertImgFromByte(ncc.PICBI);
                        imgAvatar.Source = Bitimg;
                        btnAvatar.Content = imgAvatar;
                        ProductID = curProduct.MASP;
                        ProductName = curProduct.TENSP;
                        Ammount = (int)curProduct.SL;
                        Price = (decimal)curProduct.GIA;


                    }
                    else
                    {
                        ProductID = "";
                        ProductName = "";
                        Ammount = 0;
                        Price = 0;
                        Ammount = 0;
                        SummaryImport = 0.ToString();
                    }
                }
                OnPropertyChanged();
            }
        }

        public void loadReport()
        {
            winImportReport win2 = new winImportReport();

            win2.Show();
        }
        private int _Ammount;
        public int Ammount
        {
            get => _Ammount; set
            {
                _Ammount = value;
                OnPropertyChanged();
            }
        }
        private string _SummaryImport;
        public string SummaryImport { get => _SummaryImport; set { 
                _SummaryImport = value;
                decimal n1;
                if (!decimal.TryParse(value, out n1))
                {
                    return;
                }
                OnPropertyChanged(); }
        }
        private string _AmmountImport;
        public string AmmountImport
        {
            get => _AmmountImport; set
            {

                _AmmountImport = value;
                int n2;
                if (!int.TryParse(value, out n2))
                {
                    SummaryImport = 0.ToString();
                    return;
                }
                SummaryImport = (int.Parse(value) * Price).ToString();
                OnPropertyChanged();
            }
        }
        private decimal _Price;
        public decimal Price
        {
            get => _Price; set
            {
                _Price = value;
                OnPropertyChanged();
            }
        }

        private List<string> _SearchType = new List<string>() { "ID", "Name" };

        public List<string> SearchType { get => _SearchType; set { _SearchType = value; OnPropertyChanged(); } }

        public CollectionView view;

        public ICommand LoadRptCmd { get; set; }

        public ICommand ImportProductCmd { get; set; }

        public ICommand LoadedImportCmd { get; set; }
        public ICommand LoadedPageCmd { get; set; }

        private WrapPanel _pnlImport = new WrapPanel();

        public BuyViewModel()
        {
            openImportproductCmd = new RelayCommand<object>((p) => { return true; }, (p) => { openWinAddSupplier(); });
            ImportProductCmd = new RelayCommand<object>((p) => { return true; }, (p) => { ImportProduct(p); });
            ReportImportCmd = new RelayCommand<object>((p) => { return true; }, (p) => { ReportImport(); });
            LoadedImportCmd = new RelayCommand<WrapPanel>((p) => { return true; }, (p) => { _pnlImport = p; });
            LoadedPageCmd = new RelayCommand<ItemsControl>((p) => { return true; }, (p) => { { LoadPage(); }; });
            LoadAvatarCmd = new RelayCommand<Button>((p) => { btnAvatar = p; return true; }, (p) => { CreateAvatar(p); });
            imgAvatar = new System.Windows.Controls.Image();


        }

        private void LoadPage()
        {
            NewImport();
            LoadProductList();
            loadHistory();
        }

        private void ReportImport()
        {

            string folername = @"imports/";
            drawBillBitMap(folername);


            string pdffiname = @"imports/" + ImportID + @"/" + ImportID + ".png";
            PrintPDF(pdffiname);

            List<string> finame = new List<string>();


            finame.Add(@"imports/" + ImportID + @"/" + ImportID + ".pdf");
            finame.Add(@"imports/" + ImportID + @"/" + ImportID + ".png");
            MessageBox.Show("Đã in báo cáo! ");

            //SendReport(finame);

        }


        private void SendReport(List<string> repos = null)
        {
            if (repos == null)
            {
                return;
            }
            Thread thread = new Thread(() =>
            {
                List<Attachment> atts = new List<Attachment>();
                foreach (string item in repos)
                {
                    Attachment att = new Attachment(item);
                    atts.Add(att);
                }

                var user = DataProvider.Ins.DB.NHANVIEN.Where(x => x.MANV == MainViewModel._currentUser).SingleOrDefault();
                string email = user.MAIL;
                string message = "Import of " + ImportID + " : ";
                //GuiMail("20520850@gm.uit.edu.vn", email, "Import " + ImportID, message, atts);

            });
            thread.Start();
            System.Windows.MessageBox.Show("Sent reports!, Please check your email");

        }

        void GuiMail(string from, string to, string sub, string message, List<Attachment> atts = null)
        {
            MailMessage mailmess = new MailMessage(from, to, sub, message);
            foreach (Attachment att in atts)
            {
                if (att != null)
                {
                    mailmess.Attachments.Add(att);
                }
            }

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("<mail>", "<pass>");
            client.Send(mailmess);
        }


        private void CreateAvatar(Button p)
        {

            p.Content = imgAvatar;
        }

        private void openWinAddSupplier()
        {
            NewImport();

            winImportProduct win = new winImportProduct();
            win.ShowDialog();

        }

        private void LoadProductList()
        {
            DealingHistoryList = new ObservableCollection<NhapHang>();
            ListProduct = new List<string>();
            var supplierList = DataProvider.Ins.DB.SANPHAM;
            foreach (var item in supplierList)
            {
                ListProduct.Add(item.TENSP);
            }


        }
        private void ImportProduct(object p)
        {

            decimal n1;

            if (!decimal.TryParse(SummaryImport, out n1))
            {
                MessageBox.Show("Enter summary import as number only");
                return;
            }
            int n2;

            if (!int.TryParse(AmmountImport, out n2))
            {
                MessageBox.Show("Enter ammount import as number only");
                return;
            }
            var nv = new NHAPHANG() { MANH = ImportID, MASP = curProduct.MASP, MANV = MainViewModel._currentUser, NGNH = ImportDate, SLNHAPHANG = Int32.Parse(AmmountImport), TRIGIA = Decimal.Parse(SummaryImport) };
            DataProvider.Ins.DB.NHAPHANG.Add(nv);

            var sp = DataProvider.Ins.DB.SANPHAM.Where(x => x.MASP == ProductID).SingleOrDefault();
            if (sp.SL == null)
            {
                sp.SL = 0;
            }
            sp.SL += Int32.Parse(AmmountImport);
            DataProvider.Ins.DB.SaveChanges();
            LoadProductList();
            loadHistory();
            winImportProduct win = p as winImportProduct;
            win.Close();
        }

        private void NewImport()
        {
            SeletedProduct = null;
            AmmountImport = 0.ToString();
            SummaryImport = 0.ToString();
            Ammount = 0;
            Price = 0;
            do
            {
                ImportID = Converter.Instance.RandomString(5);
            } while (DataProvider.Ins.DB.NHAPHANG.Where(x => x.MANH == ImportID).Count() > 0);
            imgAvatar = new System.Windows.Controls.Image();
            btnAvatar.Content = imgAvatar;
            ImportDate = DateTime.Now;
        }

        private void DrawPDFImage(XGraphics gfx, string finame, int x, int y, int width, int height)
        {
            XImage image = XImage.FromFile(finame);
            gfx.DrawImage(image, x, y, width, height);


        }

        private void PrintPDF(string finame)
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = "PDF Import " + ImportID;
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            DrawPDFImage(gfx, finame, 0, 0, (int)page.Width, (int)page.Height);
            document.Save("imports/" + ImportID + @"/" + ImportID + ".pdf");
        }

        public void drawBillBitMap(string folername)
        {
            RenderTargetBitmap bmp0 = new RenderTargetBitmap((int)_pnlImport.ActualWidth, (int)_pnlImport.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            bmp0.Render(_pnlImport);
            MemoryStream stream = new MemoryStream();
            BitmapEncoder encoder = new BmpBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp0));
            encoder.Save(stream);
            Bitmap bmp = new Bitmap(stream);
            string finame = folername + ImportID;
            // If directory does not exist, create it
            if (!Directory.Exists(finame))
            {
                Directory.CreateDirectory(finame);
            }
            bmp.Save(finame + @"/" + ImportID + ".png", ImageFormat.Png);
        }

        public void loadHistory()
        {
            DealingHistoryList = new ObservableCollection<NhapHang>();
            var dealing = DataProvider.Ins.DB.NHAPHANG;
            foreach (var item in dealing)
            {
                NhapHang nhaphang = new NhapHang();
                nhaphang.nhaphang = item;
                DealingHistoryList.Add(nhaphang);
            }
            view = (CollectionView)CollectionViewSource.GetDefaultView(DealingHistoryList);
            view.Filter = UserFilter;
        }

        private void FilterItem()
        {
            CollectionViewSource.GetDefaultView(DealingHistoryList).Refresh();
        }

        private string _SearchTypeItem;
        public string SearchTypeItem
        {
            get => _SearchTypeItem; set
            {
                _SearchTypeItem = value;
                view.Filter = UserFilter;
                OnPropertyChanged();
            }
        }
        private string _Search;
        public string Search
        {
            get => _Search; set
            {
                _Search = value;
                NewImport();
                view.Filter = UserFilter;
                FilterItem();
                OnPropertyChanged();
            }
        }
        public bool UserFilter(object obj)
        {
            if (string.IsNullOrEmpty(Search))
            {
                return true;
            }
            switch (SearchTypeItem)
            {
                case "Name":
                    return (obj as NhapHang).nhaphang.NHANVIEN.HOTEN.IndexOf(Search, StringComparison.OrdinalIgnoreCase) >= 0;

                case "ID":
                    return (obj as NhapHang).nhaphang.MANH.IndexOf(Search, StringComparison.OrdinalIgnoreCase) >= 0;

                default:
                    break;
            }

            return true; ;
        }

    }
}

