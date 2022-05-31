using PdfSharp.Drawing;
using PdfSharp.Pdf;
using SE104_N10_QuanLySieuThi.classes;
using SE104_N10_QuanLySieuThi.Model;
using SE104_N10_QuanLySieuThi.windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace SE104_N10_QuanLySieuThi.ViewModel
{
    public class SellViewModel : BaseViewModel
    {
        ListBox lstbxSelected = new ListBox();
        public ICommand LoadedItemCtrlCmd { get; set; }

        public ICommand BuyProductCmd { get; set; }
        public ICommand ConfirmPaymentCmd { get; set; }
        public ICommand CancelPrintBillCmd { get; set; }
        public ICommand ConfirmPrintBillCmd { get; set; }


        public ICommand LoadedBillCmd { get; set; }
        private WrapPanel _pnlBill = new WrapPanel();


        public ICommand listboxSelectedItem_SelectionChangedCmd { get; set; }

        public ICommand IncreaseSelectAmmountCmd { get; set; }
        public ICommand DecreaseSelectAmmountCmd { get; set; }

        private string _IdBill;
        public string IdBill { get => _IdBill; set { _IdBill = value; OnPropertyChanged(); } }

        private string _TextIdBill;
        public string TextIdBill { get => _TextIdBill; set { _TextIdBill = value; OnPropertyChanged(); } }

        private int _Stt;
        public int Stt { get => _Stt; set { _Stt = value; OnPropertyChanged(); } }
        private string _MaSP;
        public string MaSP { get => _MaSP; set { _MaSP = value; OnPropertyChanged(); } }
        private string _TenSP;
        public string TenSP { get => _TenSP; set { _TenSP = value; OnPropertyChanged(); } }
        private long _SoLuongSP;
        public long SoLuongSP { get => _SoLuongSP; set { _SoLuongSP = value; OnPropertyChanged(); } }
        private long _DonGia;
        public long DonGia { get => _DonGia; set { _DonGia = value; OnPropertyChanged(); } }

        private decimal _ThanhTien;
        public decimal ThanhTien { get => _ThanhTien; set { _ThanhTien = value; OnPropertyChanged(); } }

        private decimal _TienTra;
        public decimal TienTra { get => _TienTra; set { _TienTra = value;
                if (TienTra < ThanhTienCoGiamGia)
                {
                    TienThoi = 0;
                    TextTienThoi = "Trả lại: " + TienThoi.ToString();

                }
                else
                {
                    TienThoi = TienTra - ThanhTienCoGiamGia;
                    TextTienTra = "Khách trả: ";
                    TextTienThoi = "Trả lại: " + TienThoi.ToString();
                }
                OnPropertyChanged(); } }

        private decimal _TienThoi;
        public decimal TienThoi { get => _TienThoi; set { _TienThoi = value; OnPropertyChanged(); } }

        private string _TextThanhTien;
        public string TextThanhTien { get => _TextThanhTien; set { _TextThanhTien = value; OnPropertyChanged(); } }

        private string _TextTienTra;
        public string TextTienTra { get => _TextTienTra; set { _TextTienTra = value; OnPropertyChanged(); } }

        private string _TextTienThoi;
        public string TextTienThoi { get => _TextTienThoi; set { _TextTienThoi = value; OnPropertyChanged(); } }

        private decimal _ThanhTienCoGiamGia;
        public decimal ThanhTienCoGiamGia { get => _ThanhTienCoGiamGia; set { _ThanhTienCoGiamGia = value; OnPropertyChanged(); } }

        private string _TextThanhTienCoGiamGia;
        public string TextThanhTienCoGiamGia { get => _TextThanhTienCoGiamGia; set { _TextThanhTienCoGiamGia = value; OnPropertyChanged(); } }

        private float _GiamGia;
        public float GiamGia { get => _GiamGia; set { _GiamGia = value; OnPropertyChanged(); } }

        private List<string> _CategoryList = new List<string>() { "Tất cả", "Gia dụng", "Hoá mỹ phẩm", "Thực phẩm", "Khác" };

        public List<string> CategoryList { get => _CategoryList; set { _CategoryList = value; OnPropertyChanged(); } }

        private ObservableCollection<SanPham> _ListSelecteditems;
        public ObservableCollection<SanPham> ListSelecteditems { get => _ListSelecteditems; set { _ListSelecteditems = value; OnPropertyChanged(); } }

        public static ObservableCollection<SanPham> SelectedBillList { get; set; }


        public bool isMainLoaded = false;

        SanPham sp = new SanPham();
        private ObservableCollection<SanPham> _SanPhamList;

        public ObservableCollection<SanPham> SanPhamList { get => _SanPhamList; set { _SanPhamList = value; OnPropertyChanged(); } }

        public CollectionView view;

        private int _SelectAmmount = 1;
        public int SelectAmmount { get => _SelectAmmount; set { _SelectAmmount = value; OnPropertyChanged(); } }

        public KhachHang _Khachhang = new KhachHang();
        public KhachHang Khachhang { get => _Khachhang; set { _Khachhang = value; OnPropertyChanged(); } }

        public NhanVien _Nhanvien = new NhanVien();
        public NhanVien Nhanvien { get => _Nhanvien; set { _Nhanvien = value; OnPropertyChanged(); } }

        private string _EmployeeId;
        public string EmployeeId { get => _EmployeeId; set { _EmployeeId = value; OnPropertyChanged(); } }

        private string _CustomerId;
        public string CustomerId { get => _CustomerId; set { _CustomerId = value; OnPropertyChanged(); } }

        private DateTime _BillDate;
        public DateTime BillDate { get => _BillDate; set { _BillDate = value; OnPropertyChanged(); } }

        private string _SearchCategory;
        public string SearchCategory { get => _SearchCategory; set
            { 
                _SearchCategory = value;
                view.Filter = ProductFilter0;
                FilterItem();
                OnPropertyChanged(); 
            } 
        }


        private List<string> _SearchType = new List<string>() { "ID", "Name" };

        public List<string> SearchType { get => _SearchType; set { _SearchType = value; OnPropertyChanged(); } }


        private List<KhachHang> _ListCustomer = new List<KhachHang>();
        public List<KhachHang> ListCustomer { get => _ListCustomer; set { _ListCustomer = value; OnPropertyChanged(); } }

        private List<NhanVien> _ListEmployee = new List<NhanVien>();
        public List<NhanVien> ListEmployee { get => _ListEmployee; set { _ListEmployee = value; OnPropertyChanged(); } }
        private KhachHang _SeletedCustomer;
        public KhachHang SeletedCustomer
        {
            get => _SeletedCustomer; set
            {
                _SeletedCustomer = value;
                if (SeletedCustomer != null)
                {
                    CustomerId = SeletedCustomer.khachhang.MAKH;

                }
                OnPropertyChanged();
            }
        }
        private NhanVien _SeletedEmployee;
        public NhanVien SeletedEmployee
        {
            get => _SeletedEmployee; set
            {
                _SeletedEmployee = value;
                if (SeletedEmployee != null)
                {
                    EmployeeId = SeletedEmployee.nhanvien.MANV;

                }
                OnPropertyChanged();
            }
        }

        public SellViewModel()
        {
            ListSelecteditems = new ObservableCollection<SanPham>();
            LoadedItemCtrlCmd = new RelayCommand<ListBox>((p) => { lstbxSelected = p; return true; }, (p) => { if (!isMainLoaded) { LoadSanPhamData(); isMainLoaded = true; }; });
            BuyProductCmd = new RelayCommand<object>((p) => { return true; }, (p) => { openBill(); });
            ConfirmPaymentCmd = new RelayCommand<Window>((p) => { return true; }, (p) => { confirmPayment(p); });

            CancelPrintBillCmd = new RelayCommand<Window>((p) => { return true; }, (p) => { CancelPrintBill(p); });
            ConfirmPrintBillCmd = new RelayCommand<Window>((p) => { return true; }, (p) => { PrintBill(p); });

            IdBill = "";
            IncreaseSelectAmmountCmd = new RelayCommand<object>((p) => { return true; }, (p) => { Increase(p); });
            DecreaseSelectAmmountCmd = new RelayCommand<object>((p) => { return true; }, (p) => { Decrease(p); });

            LoadedBillCmd = new RelayCommand<WrapPanel>((p) => { return true; }, (p) => { _pnlBill = p; });
            ThanhTien = 0;
            TienTra = 0;
            TienThoi = 0;
            BillDate = DateTime.Now;
            SelectAmmount = 1;
            GiamGia = 0;
            loadListCustomer();
            loadListEmployee();
        }

        private void PrintBill(Window p)
        {
            if (_pnlBill == null )
            {
                return;
            }
            List<string> finame = new List<string>();

            finame.Add(@"bills/" + IdBill + ".png");
            finame.Add(@"bills/" + IdBill + ".pdf");

            SendBill(finame);

            winPrintBillConfirmation win = p as winPrintBillConfirmation;
            win.Close();
        }

        private void DrawPDFImage(XGraphics gfx, string finame, int x, int y, int width, int height)
        {
            XImage image = XImage.FromFile(finame);
            gfx.DrawImage(image, x, y, width, height);
            

        }

        private void CancelPrintBill(Window p)
        {
            winPrintBillConfirmation win = p as winPrintBillConfirmation;
            win.Close();
        }

        private void SendBill(List<string> repos = null)
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
                string emailnv = user.MAIL;
                string message = "Bill " + IdBill + " : ";
                GuiMail("20520850@gm.uit.edu.vn", emailnv, "Bill " + IdBill, message, atts);

                var khach = DataProvider.Ins.DB.KHACHHANG.Where(x => x.MAKH == Khachhang.khachhang.MAKH).SingleOrDefault();
                string emailkh = khach.MAIL;
                //GuiMail("20520850@gm.uit.edu.vn", emailkh, "Bill " + IdBill, message, atts);

            });
            thread.Start();
            System.Windows.MessageBox.Show("Sent bill!, Please check your email");

        }

        void GuiMail(string from, string to, string sub, string message, List<Attachment> atts = null)
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
            client.Credentials = new NetworkCredential("se104storemanage@gmai.com", "storepass");
            client.Send(mailmess);
        }

        private void Decrease(object p)
        {
            if (p is ListViewItem)
            {
                ListViewItem item = (ListViewItem)p;
                SelectedSelectItem = item.Content as SanPham;
            }
            SelectedSelectItem.Amount--;

            if (SelectedSelectItem.Amount <= 0)
            {

                ListSelecteditems.Remove(SelectedSelectItem);
                SelectedItem = null;
            }
        }

        public void Increase(object p)
        {

            if (p is ListViewItem)
            {
                ListViewItem item = (ListViewItem)p;
                SelectedSelectItem = item.Content as SanPham;
            }
            var sanpham = DataProvider.Ins.DB.SANPHAM.Where(x => x.MASP == SelectedSelectItem.sanpham.MASP).SingleOrDefault();
            if (sanpham.SL < SelectedSelectItem.Amount)
            {
                MessageBox.Show("đã hết hàng");
            }
            SelectedSelectItem.Amount++;

        }


        private void loadListCustomer()
        {
            var list = DataProvider.Ins.DB.KHACHHANG;
            foreach (var item in list)
            {
                KhachHang kh = new KhachHang();
                kh.khachhang = item;
                ListCustomer.Add(kh);
            }
        }

        private void loadListEmployee()
        {
            var list = DataProvider.Ins.DB.NHANVIEN;
            foreach (var item in list)
            {
                NhanVien nv = new NhanVien();
                nv.nhanvien = item;
                ListEmployee.Add(nv);
            }
        }

        private void confirmPayment(Window p)
        {
            if (TienTra < ThanhTienCoGiamGia)
            {
                return;
            }
            var hd = new HOADON() { SOHD = IdBill, NGHD = BillDate, MANV = MainViewModel._currentUser, MAKH = Khachhang.khachhang.MAKH, TRIGIA = ThanhTienCoGiamGia, GIAMGIA = GiamGia };
            foreach (SanPham sp in ListSelecteditems)
            {
                var cthd = new CTHD() { SOHD = IdBill, MASP = sp.sanpham.MASP, SL = sp.Amount, };
                DataProvider.Ins.DB.CTHD.Add(cthd);

                var sanpham = DataProvider.Ins.DB.SANPHAM.Where(x => x.MASP == sp.sanpham.MASP).SingleOrDefault();
                sanpham.SL -= sp.Amount;
            }
            DataProvider.Ins.DB.HOADON.Add(hd);

            Khachhang.khachhang.DOANHSO += ThanhTienCoGiamGia;
            DataProvider.Ins.DB.SaveChanges();


            RenderTargetBitmap bmp0 = new RenderTargetBitmap((int)_pnlBill.ActualWidth, (int)_pnlBill.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            bmp0.Render(_pnlBill);
            MemoryStream stream = new MemoryStream();
            BitmapEncoder encoder = new BmpBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp0));
            encoder.Save(stream);
            Bitmap bmp = new Bitmap(stream);
            bmp.Save(@"bills/" + IdBill + ".png", ImageFormat.Png);

            string pdffiname = @"bills/" + IdBill + ".png";
            PrintPDF(pdffiname);

            clearField();
            LoadSanPhamData();

            //System.Windows.Point toggleButtonPosition = panel.TranslatePoint(new System.Windows.Point(0, 0), panel);
            //Rectangle bounds = Screen.GetBounds(System.Drawing.Point.Empty);
            //using (Bitmap bitmap = new Bitmap((int)panel.Width, (int)panel.Height))
            //{
            //    panel.DrawToBitmap(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
            //    bitmap.Save("test.png", ImageFormat.Png);
            //}

            //RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap((int)_pnlBill.ActualWidth, (int)_pnlBill.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            //renderTargetBitmap.Render(_pnlBill);
            //PngBitmapEncoder pngImage = new PngBitmapEncoder();
            //pngImage.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
            //using (Stream fileStream = File.Create(@"test.png"))
            //{
            //    pngImage.Save(fileStream);
            //}


            winPrintBillConfirmation newwin = new winPrintBillConfirmation();
            newwin.Show();
            winBill win = p as winBill;
            win.Close();

        }
        private void PrintPDF(string finame)
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = "PDF Bill " + IdBill;
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            DrawPDFImage(gfx, finame, 0, 0, (int)page.Width, (int)page.Height);
            document.Save("bills/" + IdBill + ".pdf");
        }
        private void clearField()
        {
            ListSelecteditems = new ObservableCollection<SanPham>();
            ThanhTien = 0;
            ThanhTienCoGiamGia = 0;
            GiamGia = 0;
            TienTra = 0;
            TienThoi = 0;
            BillDate = DateTime.Now;
        }

        private void LoadSanPhamData()
        {
            SanPhamList = new ObservableCollection<SanPham>();

            var objectList = DataProvider.Ins.DB.SANPHAM;
            foreach (var item in objectList)
            {
                SanPham sanpham = new SanPham();
                sanpham.sanpham = item;
                sanpham.Amount = 0;
                sanpham.Bitimg = Converter.Instance.ConvertByteToBitmapImage(item.PICBI);
                sanpham.Img.Source = sanpham.Bitimg;
                SanPhamList.Add(sanpham);
            }
            view = (CollectionView)CollectionViewSource.GetDefaultView(SanPhamList);
            view.Filter = ProductFilter1;

        }
        public bool ProductFilter0(object obj)
        {
            if (string.IsNullOrEmpty(SearchCategory))
            {
                return true;
            }
            if(SearchCategory=="Tất cả")
            {
                return true;

            }

            return (obj as SanPham).sanpham.LOAI.IndexOf(SearchCategory, StringComparison.OrdinalIgnoreCase) >= 0;

        }

        public bool ProductFilter1(object obj)
        {
            if (string.IsNullOrEmpty(Search))
            {
                return true;
            }
            switch (SearchTypeItem)
            {
                case "Name":
                    return (obj as SanPham).sanpham.TENSP.IndexOf(Search, StringComparison.OrdinalIgnoreCase) >= 0;
                case "ID":
                    return (obj as SanPham).sanpham.MASP.IndexOf(Search, StringComparison.OrdinalIgnoreCase) >= 0;
                default:
                    break;
            }

            return true; ;
        }
        private string _Search;

        public string Search
        {
            get => _Search; set
            {
                _Search = value;
                view.Filter = ProductFilter1;
                FilterItem();
                OnPropertyChanged();
            }
        }
        private void FilterItem()
        {
            CollectionViewSource.GetDefaultView(SanPhamList).Refresh();
        }
        private string _SearchTypeItem;
        public string SearchTypeItem
        {
            get => _SearchTypeItem; set
            {
                _SearchTypeItem = value;
                view.Filter = ProductFilter0;
                FilterItem();
                OnPropertyChanged();
            }
        }
        private void openBill()
        {
            Khachhang.khachhang = DataProvider.Ins.DB.KHACHHANG.Where(x => x.MAKH == CustomerId).SingleOrDefault();
            Nhanvien.nhanvien = DataProvider.Ins.DB.NHANVIEN.Where(x => x.MANV == MainViewModel._currentUser).SingleOrDefault();
            if (Khachhang.khachhang == null || Nhanvien.nhanvien == null)
            {
                return;
            }
            SelectedBillList = ListSelecteditems;
            foreach (SanPham sp in ListSelecteditems)
            {
                ThanhTien += (decimal)sp.sanpham.GIA * sp.Amount;
            }
            applyDisccount();
            ThanhTienCoGiamGia = ThanhTien - (ThanhTien / 100 * (decimal)GiamGia);
            string sohd = Converter.Instance.RandomString(5);
            while (DataProvider.Ins.DB.HOADON.Where(x => x.SOHD == sohd).Count() > 0)
            {
                sohd = Converter.Instance.RandomString(5);
            }
            IdBill = sohd;

            TextThanhTien = "Tổng cộng: " + ThanhTien.ToString();
            TextThanhTienCoGiamGia = "Sau khi áp dụng giảm giá: " + ThanhTienCoGiamGia.ToString();
            TextIdBill = "Mã hoá đơn là: " + IdBill;
            TextTienTra = "Khách trả: ";
            TextTienThoi = "Trả lại: " + TienThoi.ToString();
            winBill win = new winBill();
            win.ShowDialog();
        }
        public void applyDisccount()
        {
            if (Khachhang.khachhang.DOANHSO >= 1000000)
            {
                GiamGia = 10;
            }
            if (Khachhang.khachhang.DOANHSO >= 3000000)
            {
                GiamGia = 20;
            }
            if (Khachhang.khachhang.DOANHSO >= 5000000)
            {
                GiamGia = 30;
            }
        }

        private SanPham _SelectedItem;
        public SanPham SelectedItem
        {
            get => _SelectedItem; set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    if (SelectedItem.sanpham.SL == null)
                    {
                        return;
                    }
                    if (SelectedItem.sanpham.SL <= 0)
                    {
                        return;
                    }
                    if (SelectedItem.Amount > 0)
                    {
                        return;
                    }
                    SelectedItem.Amount = 1;
                    ListSelecteditems.Add(SelectedItem);
                }
            }
        }
        private SanPham _SelectedSelectItem;
        public SanPham SelectedSelectItem
        {
            get => _SelectedSelectItem; set
            {
                _SelectedSelectItem = value;
                if (SelectedSelectItem != null)
                {

                }
                OnPropertyChanged();
            }
        }

    }
}
