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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if (Session.GetUser().Role == Role.User)
            {
                UsersControl.IsEnabled = false;
                AddUser.IsEnabled = false;
            }
        }

        private void ChangePasswd_Click(object sender, RoutedEventArgs e)
        {
            ChangePasswordWindow changePasswordWindow = new ChangePasswordWindow();
            if (!changePasswordWindow.IsActive)
            {
                changePasswordWindow.Show();
            }
            else
                MessageBox.Show("Окно изменения пароля открыто", "Предупреждение");
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void UserControl_Click(object sender, RoutedEventArgs e)
        {
            UsersControlWindow usersControlWindow = new UsersControlWindow();
            if (!usersControlWindow.IsActive)
                usersControlWindow.Show();
            else
                MessageBox.Show("Окно контроля пользователями открыто", "Предупреждение");
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            AddUserWindow addUserWindow = new AddUserWindow();
            if (!addUserWindow.IsActive)
                addUserWindow.Show();
            else
                MessageBox.Show("Окно добавления пользователя открыто", "Предупреждение");
        }
    }
}
