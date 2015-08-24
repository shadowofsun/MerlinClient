using System;
using System.Windows.Forms;
using Microsoft.Win32;

namespace MerlinClient.Controller
{
    class AutoStartup
    {
        public static bool Set(bool enabled)
        {
            try
            {
                string path = Application.ExecutablePath;
                RegistryKey runKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                if (enabled)
                {
                    runKey.SetValue("MerlinClient", path);
                }
                else
                {
                    runKey.DeleteValue("MerlinClient");
                }
                runKey.Close();
                return true;
            }
            catch (Exception e)
            {
                //Logging.LogUsefulException(e);
                return false;
            }
        }

        public static bool Check()
        {
            try
            {
                string path = Application.ExecutablePath;
                RegistryKey runKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
                string[] runList = runKey.GetValueNames();
                runKey.Close();
                foreach (string item in runList)
                {
                    if (item.Equals("MerlinClient"))
                        return true;
                }
                return false;
            }
            catch (Exception e)
            {
                //Logging.LogUsefulException(e);
                return false;
            }
        }
    }
}
