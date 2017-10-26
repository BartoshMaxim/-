using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using Microsoft.VisualBasic.Devices;
namespace lab1_2
{
    public static class Helper
    {
        private readonly static string _studentName = "Bartosh";

        [DllImport("user32.dll")]
        private static extern int GetKeyboardType(int nTypeFlag);

        public static string CheckInfo()
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
                        if (!long.Parse(rk.GetValue("driveMemory").ToString()).Equals(drive.TotalSize))
                            errors += "different driveMemory\n";
                    }
                }

                if (!(rk.GetValue("userName") as string).Equals(Environment.UserName))
                    errors += "different UserName\n";

                if (!(rk.GetValue("computerName") as string).Equals(Environment.MachineName))
                    errors += "different MachineName\n";

                if (!(rk.GetValue("windowFolder") as string).Equals(Environment.GetFolderPath(Environment.SpecialFolder.Windows)))
                    errors += "different WindowFolder\n"; ;

                if (!(rk.GetValue("specialFolder") as string).Equals(Environment.GetFolderPath(Environment.SpecialFolder.System)))
                    errors += "different specialFolder\n";

                if (!int.Parse(rk.GetValue("keyboardType").ToString()).Equals(GetKeyboardType(0)))
                    errors += "different keyboardType\n";

                if (!int.Parse(rk.GetValue("keyboardSubtype").ToString()).Equals(GetKeyboardType(1)))
                    errors += "different keyboardSubtype\n";

                if (!double.Parse(rk.GetValue("screenHeight").ToString()).Equals(SystemParameters.PrimaryScreenHeight))
                    errors += "different screenHeight\n";

                if (!ulong.Parse(rk.GetValue("totalDriveMemory").ToString()).Equals(new ComputerInfo().TotalPhysicalMemory))
                    errors += "different totalDriveMemory\n";
                rk.Close();
            }

            return errors;
        }
    }
}
