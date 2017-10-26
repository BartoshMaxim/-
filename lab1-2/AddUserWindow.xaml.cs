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
    /// Interaction logic for AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        public AddUserWindow()
        {
            InitializeComponent();

        }

        private void AddName_Click(object sender, RoutedEventArgs e)
        {
            string enterLogin = NameBox.Text;
            if (NameBox.Text.Equals(string.Empty))
            {
                MessageBox.Show("Имя не может быть пустым", "Предупреждение");
            }
            else if (NameBox.Text.Length > 50)
            {
                MessageBox.Show("Длина имени не может превышать 50 символов", "Предупреждение");
            }
            else if (Session.GetUserContext().IsExist(NameBox.Text))
            {
                MessageBox.Show("Пользователь с таким именем уже существует", "Предупреждение");
            }
            else
            {
                Session.GetUserContext().Register(new User { UserLogin = enterLogin, UserPassword = "" });
                MessageBox.Show("Пользователь добавлен", "Успешно");
                Close();
            }

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
