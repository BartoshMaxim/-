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
    /// Interaction logic for UsersControllWindow.xaml
    /// </summary>
    public partial class UsersControlWindow : Window
    {

        public UsersControlWindow()
        {
            InitializeComponent();
            lvUsers.ItemsSource = Session.GetUserContext().GetList();
        }

        private void Lock_Click(object sender, RoutedEventArgs e)
        {
            var item = ((MenuItem)sender).CommandParameter as User;
            Session.GetUserContext().Lock(item);
            RefreshList();
        }

        private void IsBlock_Click(object sender, RoutedEventArgs e)
        {
            var item = ((MenuItem)sender).CommandParameter as User;
            Session.GetUserContext().IsBlock(item);
            RefreshList();
        }

        private void ToTop_Click(object sender, RoutedEventArgs e)
        {
            var item = ((MenuItem)sender).CommandParameter as User;
            Session.GetUserContext().ToTop(item);
            RefreshList();
        }

        private void RefreshList()
        {
            lvUsers.ItemsSource = null;
            lvUsers.ItemsSource = Session.GetUserContext().GetList();
        }
    }
}
