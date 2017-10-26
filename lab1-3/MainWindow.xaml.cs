using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lab1_3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            SetupBtn.IsEnabled = false;
            CloseBtn.IsEnabled = false;
            setValueToSetupPath(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
        }

        private void ChooseFolderBtn_Click(object sender, RoutedEventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    setValueToSetupPath(fbd.SelectedPath);
                }
            }
        }

        private void SetupBtn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder directory = new StringBuilder(ProgramName.Text);

            string[] incorrectDirectoryStrings = { @"\", @"/", ":", "*", "\"", "?", "<", ">", "|", "\n" };
            foreach (string incorrectDirectoryString in incorrectDirectoryStrings)
                directory.Replace(incorrectDirectoryString, "");

            string programName = directory.ToString();

            string path = PathFolderLbl.Content as string;

            if (programName != String.Empty&& path!= String.Empty)
            {
                SetupHelper.WriteInfo(path);

                SetupHelper.SetupProgram(programName, path);
                CloseBtn.IsEnabled = true;
            }
            else
                SetupBtn.IsEnabled = false;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void setValueToSetupPath(string setupPath)
        {
            PathFolderLbl.Content = setupPath;
        }

        private void ProgramName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ProgramName.Text != String.Empty)
                SetupBtn.IsEnabled = true;
            else
                SetupBtn.IsEnabled = false;
        }
    }
}
