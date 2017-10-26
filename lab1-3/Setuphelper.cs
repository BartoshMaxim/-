using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Windows;
using Microsoft.VisualBasic.Devices;

namespace lab1_3
{
    public static class SetupHelper
    {
        private readonly static string _sourceProgramName = "res/lab1-2.exe";

        private readonly static string _studentName = "Bartosh";

        [DllImport("user32.dll")]
        private static extern int GetKeyboardType(int nTypeFlag);

        public static void SetupProgram(string name, string path)
        {
            if (Directory.Exists(path))
            {
                if (name != string.Empty)
                {
                    string folderPath = path + "\\" + name;
                    Directory.CreateDirectory(folderPath);
                    string filePath = folderPath + "\\" + name + ".exe";

                    File.Copy(_sourceProgramName, filePath);
                    System.Windows.Forms.MessageBox.Show("Программа успешно установлена");
                }
                else
                    System.Windows.Forms.MessageBox.Show("Имя программы пустое");
            }
            else
                System.Windows.Forms.MessageBox.Show("Такой папки не существует: " + path);
        }

        public static void WriteInfo(string path)
        {
            string keyName = @"SOFTWARE\" + _studentName;

            RegistryKey rk = Registry.CurrentUser.OpenSubKey(keyName, true);
            if (rk == null)
                rk = Registry.CurrentUser.CreateSubKey(keyName, true);

            string driveName = Path.GetPathRoot(path);

            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady && drive.Name == driveName)
                {
                    rk.SetValue("driveMemory", drive.TotalSize);
                }
            }

            rk.SetValue("userName", Environment.UserName);
            rk.SetValue("computerName", Environment.MachineName);
            rk.SetValue("windowFolder", Environment.GetFolderPath(Environment.SpecialFolder.Windows));
            rk.SetValue("specialFolder", Environment.GetFolderPath(Environment.SpecialFolder.System));
            rk.SetValue("keyboardType", GetKeyboardType(0));
            rk.SetValue("keyboardSubtype", GetKeyboardType(1));
            rk.SetValue("screenHeight", SystemParameters.PrimaryScreenHeight);
            rk.SetValue("totalDriveMemory", new ComputerInfo().TotalPhysicalMemory);
            rk.Close();
        }

        private static string checkInfo()
        {
            string errors = string.Empty;
            string keyName = @"SOFTWARE\" + _studentName;

            RegistryKey rk = Registry.CurrentUser.OpenSubKey(keyName, true);
            if (rk == null)
                errors += "Key not created\n";
            else
            {
                string driveName = Path.GetPathRoot(System.Reflection.Assembly.GetExecutingAssembly().Location);

                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    if (drive.IsReady && drive.Name == driveName)
                    {
                        if (!rk.GetValue("driveMemory").Equals(drive.TotalSize))
                            errors += "Key not created\n";
                    }
                }

                if (!rk.GetValue("userName").Equals(Environment.UserName))
                    errors += "different UserName\n";

                if (!rk.GetValue("computerName").Equals(Environment.MachineName))
                    errors += "different MachineName\n";

                if (!rk.GetValue("windowFolder").Equals(Environment.SpecialFolder.Windows))
                    errors += "different WindowFolder\n"; ;

                if (!rk.GetValue("specialFolder").Equals(Environment.SpecialFolder.System))
                    errors += "different specialFolder\n";

                if (!rk.GetValue("keyboardType").Equals(GetKeyboardType(0)))
                    errors += "different keyboardType\n";

                if (!rk.GetValue("keyboardSubtype").Equals(GetKeyboardType(1)))
                    errors += "different keyboardSubtype\n";

                if (!rk.GetValue("screenHeight").Equals(SystemParameters.PrimaryScreenHeight))
                    errors += "different screenHeight\n";

                if (!rk.GetValue("totalDriveMemory").Equals(new ComputerInfo().TotalPhysicalMemory))
                    errors += "different totalDriveMemory\n";
            }
            rk.Close();
            return errors;
        }
    }
}
