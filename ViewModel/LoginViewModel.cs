using SE104_N10_QuanLySieuThi.classes;
using SE104_N10_QuanLySieuThi.Model;
using SE104_N10_QuanLySieuThi.windows;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace SE104_N10_QuanLySieuThi.ViewModel
{
    class LoginViewModel : BaseViewModel
    {

        public KhachHang kh = new KhachHang();

        public bool IsLogin { get; set; }
        private string _UserName;
        public string UserName { get => _UserName; set { _UserName = value; OnPropertyChanged(); } }
        private string _MailAdress;
        public string MailAdress { get => _MailAdress; set { _MailAdress = value; OnPropertyChanged(); } }
        private string _NewPassword;
        public string NewPassword { get => _NewPassword; set { _NewPassword = value; OnPropertyChanged(); } }
        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }
        private string _RePassword;
        public string RePassword { get => _RePassword; set { _RePassword = value; OnPropertyChanged(); } }
        private string _Name;
        public string Name { get => _Name; set { _Name = value; OnPropertyChanged(); } }
        private string VerifyCode;
        private string _ReVerifyCode;
        public string ReVerifyCode { get => _ReVerifyCode; set { _ReVerifyCode = value; OnPropertyChanged(); } }
        private string _Phone;
        public string Phone { get => _Phone; set { _Phone = value; OnPropertyChanged(); } }
        private bool _IsMale;
        public bool IsMale { get => _IsMale; set { _IsMale = value; OnPropertyChanged(); } }
        private bool _IsFeMale;
        public bool IsFeMale { get => _IsFeMale; set { _IsFeMale = value; OnPropertyChanged(); } }
        private DateTime _Joineddate;
        public DateTime Joineddate { get => _Joineddate; set { _Joineddate = value; OnPropertyChanged(); } }

        private DateTime _Birthday;
        public DateTime Birthday { get => _Birthday; set { _Birthday = value; OnPropertyChanged(); } }

        public string ConfirmPassword { get => _ConfirmPassword; set { _ConfirmPassword = value; OnPropertyChanged(); } }
        private string _ConfirmPassword;
        public string CurrentPassword { get => _CurrentPassword; set { _CurrentPassword = value; OnPropertyChanged(); } }
        private string _CurrentPassword;
        public ICommand LoginCmd { get; set; }
        public ICommand RegistCmd { get; set; }
        public ICommand CreateAccountCmd { get; set; }
        public ICommand SendVerifyCmd { get; set; }
        public ICommand VerifyCmd { get; set; }

        public ICommand BackCmd { get; set; }

        public ICommand CloseCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand RePasswordChangedCommand { get; set; }
        public ICommand ChangepasswordCommand { get; set; }
        public ICommand CheckedGenderCmd { get; set; }
        public ICommand PickImage { get; set; }

        private Account acc;
        public Account Acc { get => acc; set => acc = value; }

        public SqlConnection ketnoi = new SqlConnection(@"Data Source=LAPTOP-H3DR409O\MSSQLSERVER01;Initial Catalog=QuanLySieuThi;Integrated Security=True");

        private Random random = new Random();

        public string RandomString(int length)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private void Imagepick(Button p)
        {
            kh.chooseImg();
            Bitimg = kh.Bitimg;
            imgAvatar.Source = kh.Bitimg;
            p.Content = imgAvatar;
        }
        public Button btnAvatar = new Button();
        public Image imgAvatar = new Image();
        public BitmapImage Bitimg { get => bitimg; set => bitimg = value; }
        private BitmapImage bitimg = new BitmapImage();
        private string _Gender;
        public string Gender { get => _Gender; set { _Gender = value; OnPropertyChanged(); } }
        public LoginViewModel()
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            Acc = new Account();
            IsLogin = false;
            Password = "";
            RePassword = "";
            UserName = "";
            MailAdress = "";
            Name = "";
            Phone = "";
            IsMale = true;
            VerifyCode = "";
            ReVerifyCode = "";
            Birthday = DateTime.Now;
            Joineddate = DateTime.Now;
            LoginCmd = new RelayCommand<Window>((p) => { return true; }, (p) => { Login(p); });
            RegistCmd = new RelayCommand<Window>((p) => { return true; }, (p) => { Regist(p); });
            CreateAccountCmd = new RelayCommand<Window>((p) => { return true; }, (p) => { CreateAccount(p); });
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { p.Close(); });
            ChangepasswordCommand = new RelayCommand<object>((parameter) => true, (parameter) => ChangePW());
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });
            RePasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { RePassword = p.Password; });
            BackCmd = new RelayCommand<Window>((p) => { return true; }, (p) => { Back(p); });
            SendVerifyCmd = new RelayCommand<Window>((p) => { return true; }, (p) => { SendVerifyMail(p); });
            VerifyCmd = new RelayCommand<Window>((p) => { return true; }, (p) => { VerifyMail(p); });
            PickImage = new RelayCommand<Button>((p) => { btnAvatar = p; return true; }, (p) => { Imagepick(p); });

            CheckedGenderCmd = new RelayCommand<object>((p) => { return true; }, (p) => { CheckGender(); });
            IsMale = true;
            IsFeMale = false;
            Gender = "Male";

        }
        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        private void VerifyMail(Window p)
        {
            if (VerifyCode.CompareTo(ReVerifyCode) != 0)
            {
                return;
            }
            winRegistInformation win = new winRegistInformation();
            win.ShowDialog();
            p.Close();

        }

        private void SendVerifyMail(Window p)
        {
            VerifyCode = RandomString(8);
            if (!IsValidEmail(MailAdress))
            {
                return;
            }
            SendVerifyCode(VerifyCode);
            winVerifyMail win = new winVerifyMail();
            win.ShowDialog();
            p.Close();

        }
        private void CheckGender()
        {
            if (IsMale == false)
            {
                IsMale = true;
                IsFeMale = false;
                Gender = "Male";
            }
            else if (IsMale == true)
            {
                IsMale = false;
                IsFeMale = true;
                Gender = "Female";

            }

        }
        private void SendVerifyCode(string verifyCode, Attachment file = null)
        {

            Thread thread = new Thread(() =>
            {
                file = null;
                //try
                //{
                //    //FileInfo file = new FileInfo(txtFile.Text);
                //    //file = new Attachment(txtFile.Text);
                //}
                //catch
                //{ }
                string email = MailAdress;
                string message = "Here is your verify code for supermarket account: " + VerifyCode;
                GuiMail("20520850@gm.uit.edu.vn", email, "Verify account code", message, file);

            });
            thread.Start();
            MessageBox.Show("Sent verify code!, Plesea check your email");

        }

        void GuiMail(string from, string to, string sub, string message, Attachment file = null)
        {
            MailMessage mailmess = new MailMessage(from, to, sub, message);

            if (file != null)
            {
                mailmess.Attachments.Add(file);
            }

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;

            client.Credentials = new NetworkCredential("se104storemanage@gmai.com", "storepass");
            client.Send(mailmess);
        }

        void Back(Window p)
        {
            if (p == null)
                return;
            Window win = new winLogin();

            switch (p.Name)
            {
                case "RegistWin":
                    break;
                case "VerifyWin":
                    win = new winRegister();
                    win.ShowDialog();

                    break;
                case "RegistInformationWin":
                    win = new winVerifyMail();
                    win.ShowDialog();

                    break;

                default:
                    break;
            }
            Password = "";
            RePassword = "";
            UserName = "";
            Name = "";
            MailAdress = "";
            Phone = "";
            p.Close();
        }
        private void ChangePW()
        {

        }
        void Regist(Window p)
        {
            if (p == null)
                return;
            Password = "";
            RePassword = "";
            UserName = "";
            winRegister win = new winRegister();
            win.ShowDialog();
            p.Close();
        }


        void CreateAccount(Window p)
        {
            if (p == null)
                return;

            KhachHang kh = new KhachHang();
            kh.Name = Name;
            kh.Phone = Phone;
            kh.Mail = MailAdress;
            kh.Birth = Birthday.ToString("dd/MM/yyyy");
            kh.Startdate = DateTime.Now.ToString("dd/MM/yyyy");
            kh.Bitimg = null;
            kh.Img = null;
            kh.Id = RandomString(5);
            while (kh.randomSamedetected(kh.Id))
            {
                kh.Id = RandomString(5);
            }
            if (IsMale)
            {
                kh.Gender = "Male";
            }
            else
            {
                kh.Gender = "Female";
            }
            kh.Acc = new Account();
            kh.Acc.Acc = UserName;
            //if (!kh.RegistCustomer()){
            //    return;
            //}
            try
            {
                string khid = Converter.Instance.RandomString(5);
                while (DataProvider.Ins.DB.KHACHHANG.Where(x => x.MAKH == khid).Count() > 0)
                {
                    khid = Converter.Instance.RandomString(5);
                }
                var nv = new KHACHHANG() { HOTEN = Name, MAKH = khid, SODT = Phone, MAIL = MailAdress, PICBI = Converter.Instance.ConvertBitmapImageToBytes(Bitimg), GENDER = Gender, NGSINH = Birthday, ACC = UserName, DOANHSO = 0,NGDK=DateTime.Now };
                DataProvider.Ins.DB.KHACHHANG.Add(nv);
                var acc = new ACCOUNT() { PASS = Converter.Instance.MD5Encrypt(Converter.Instance.Base64Encode(Password)),ACC=UserName };
                DataProvider.Ins.DB.ACCOUNT.Add(acc);

                DataProvider.Ins.DB.SaveChanges();

            }
            catch (Exception ex)
            {
                return;
            }

            IsLogin = true;
            p.Close();
        }






        void Login(Window p)
        {
            if (p == null)
                return;
            Acc = new Account(UserName, Converter.Instance.MD5Encrypt(Converter.Instance.Base64Encode(Password)));
            string pass = Converter.Instance.MD5Encrypt(Converter.Instance.Base64Encode(Password));
            var accCountKhach = DataProvider.Ins.DB.ACCOUNT.Where(x => x.ACC == UserName && x.PASS == pass).SingleOrDefault();
            var accCountNhanVien = DataProvider.Ins.DB.ACCOUNT.Where(x => x.ACC == UserName && x.PASS == pass).SingleOrDefault();
            if (accCountKhach!=null)
            {
                var user = DataProvider.Ins.DB.NHANVIEN.Where(x => x.ACC == UserName).SingleOrDefault();

                MainViewModel._currentUser = user.MANV;
                MainWindow win = new MainWindow();

                win.Show();
                p.Close();
            }
            else if (accCountNhanVien!=null)
            {
                var user = DataProvider.Ins.DB.NHANVIEN.Where(x => x.ACC == UserName).SingleOrDefault();
                MainViewModel._currentUser = user.MANV;
                if (accCountNhanVien.PRI == 1)
                {
                    MainWindow win = new MainWindow();
                    win.Show();
                    p.Close();
                }
                else
                {
                    MainWindow win = new MainWindow();
                    win.Show();
                    p.Close();
                }

            }
            else
            {
                IsLogin = false;
                MessageBox.Show("Incorrect password or username");
            }
        }

    }

}
