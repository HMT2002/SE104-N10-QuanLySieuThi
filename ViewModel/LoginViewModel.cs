using SE104_N10_QuanLySieuThi.classes;
using SE104_N10_QuanLySieuThi.windows;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SE104_N10_QuanLySieuThi.ViewModel
{
    class LoginViewModel:BaseViewModel
    {
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

        public ICommand CheckedIsMaleCmd { get; set; }
        public ICommand CheckedIsFemaleCmd { get; set; }

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

        public LoginViewModel()
        {
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
            CheckedIsMaleCmd = new RelayCommand<object>((p) => { return true; }, (p) => { CheckGender(p); });
            CheckedIsFemaleCmd = new RelayCommand<object>((p) => { return true; }, (p) => { CheckGender(p); });

        }

        private void CheckGender(object p)
        {
            if (IsMale == false)
            {
                IsMale = true;
                IsFeMale = false;
            }
            else if (IsFeMale == false)
            {
                IsFeMale = true;
                IsMale = false;
            }
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
            Acc = new Account(UserName, Password);

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

            client.Credentials = new NetworkCredential("20520850@gm.uit.edu.vn", "tue6tri123");

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
            if (Password.CompareTo(RePassword) == 0)
            {
                Acc = new Account(UserName, Password);

                if (Acc.RegistCustomer() == true)
                {
                    KhachHang kh = new KhachHang();
                    kh.Name = Name;
                    kh.Phone = Phone;
                    kh.Mail = MailAdress;
                    kh.Birth = Birthday;
                    kh.Startdate = DateTime.Now;
                    kh.Bitimg = null;
                    kh.Img = null;
                    kh.Id = RandomString(5);
                    if (IsMale)
                    {
                        kh.Gender = "Nam";
                    }
                    else
                    {
                        kh.Gender = "Nữ";
                    }
                    kh.Acc = new Account();
                    kh.Acc.Acc = UserName;
                    if (!kh.RegistCustomer()){
                        return;
                    }
                    IsLogin = true;
                    p.Close();
                }
                else
                {
                    IsLogin = false;
                    MessageBox.Show("Account already exitst");
                }
            }
            else
            {
                IsLogin = false;
                MessageBox.Show("Unmatch password");
            }

        }

        void Login(Window p)
        {
            if (p == null)
                return;
            Acc = new Account(UserName, Password);

            if (Acc.CheckUser()==true)
            {
                IsLogin = true;
                p.Close();
            }
            else
            {
                IsLogin = false;
                MessageBox.Show("Incorrect password or username");
            }
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
