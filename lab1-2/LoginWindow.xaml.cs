using lab1_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lab1_2
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        //login: ADMIN
        //passwd: ""
        public LoginWindow()
        {
            InitializeComponent();
            Session.Initialize();
            string errors = Helper.CheckInfo();
            if (errors != string.Empty)
            {
                MessageBox.Show(errors);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            User user = Session.GetUserContext().Login(Login.Text, Passwd.Password);
            if (user!=null)
            {
                Session.SetUser(user);
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
        }
    }
}
