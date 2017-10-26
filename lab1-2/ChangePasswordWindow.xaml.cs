using lab1_2.Helpers;
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
using System.Windows.Shapes;

namespace lab1_2
{
    /// <summary>
    /// Interaction logic for ChangePasswordWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow : Window
    {
       
        public ChangePasswordWindow()
        {
            InitializeComponent();
        }

        private void Ok_btn_Click(object sender, RoutedEventArgs e)
        {
            string new_passwd = NewPasswd_pswd.Password;
            string rep_passwd = RepPasswd_pswd.Password;

            if (!PasswordHelper.CheckPasswd(new_passwd) && Session.GetUser().Lock)
            {
                MessageBox.Show("Введите корректный пароль(как минимум 1 буква и 1 цифра)", "Предупреждение");
            }
            else if (!new_passwd.Equals(rep_passwd))
            {
                MessageBox.Show("Пароли не эквивалентны", "Предупреждение");
            }
            else
            {
                if (Session.GetUserContext().ChangePasswd(new_passwd))
                    MessageBox.Show("Пароль изменен", "Информация");
                else
                    MessageBox.Show("Произошла ошибка во время изменения информации", "Информация");

                Close();
            }

        }

        private void Cancel_btn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
