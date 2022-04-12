using SE104_N10_QuanLySieuThi.classes;
using SE104_N10_QuanLySieuThi.windows;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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
        private string _NewPassword;
        public string NewPassword { get => _NewPassword; set { _NewPassword = value; OnPropertyChanged(); } }
        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }
        private string _RePassword;
        public string RePassword { get => _RePassword; set { _RePassword = value; OnPropertyChanged(); } }

        public string ConfirmPassword { get => _ConfirmPassword; set { _ConfirmPassword = value; OnPropertyChanged(); } }
        private string _ConfirmPassword;
        public string CurrentPassword { get => _CurrentPassword; set { _CurrentPassword = value; OnPropertyChanged(); } }
        private string _CurrentPassword;
        public ICommand LoginCommand { get; set; }
        public ICommand RegistCommand { get; set; }
        public ICommand CreateAccountCommand { get; set; }

        public ICommand CloseCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand RePasswordChangedCommand { get; set; }
        public ICommand ChangepasswordCommand { get; set; }


        public SqlConnection ketnoi = new SqlConnection(@"Data Source=LAPTOP-H3DR409O\MSSQLSERVER01;Initial Catalog=QuanLySieuThi;Integrated Security=True");

        public LoginViewModel()
        {
            IsLogin = false;
            Password = "";
            RePassword = "";
            UserName = "";
            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { Login(p); });
            RegistCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { Regist(p); });
            CreateAccountCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { CreateAccount(p); });
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { p.Close(); });
            ChangepasswordCommand = new RelayCommand<object>((parameter) => true, (parameter) => ChangePW());
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });
            RePasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { RePassword = p.Password; });

        }
        private void ChangePW()
        {

        }
        void Regist(Window p)
        {
            if (p == null)
                return;
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
                Account acc = new Account(UserName, Password);

                if (acc.RegistCustomer() == true)
                {
                    IsLogin = true;
                    MessageBox.Show("Welcome " + UserName + " " + Password);
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

            Account acc = new Account(UserName, Password);

            if (acc.CheckUser()==true)
            {
                IsLogin = true;
                MessageBox.Show("Welcome "+UserName+" "+Password);
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
