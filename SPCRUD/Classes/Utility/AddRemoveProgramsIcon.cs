using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPCRUD.Classes.Utility
{
    static class AddRemoveProgramsIcon
    {
        public static void SetAddRemoveProgramsIcon()
        {
            //This Icon is seen in control panel (uninstalling the app)
            if (ApplicationDeployment.IsNetworkDeployed && ApplicationDeployment.CurrentDeployment.IsFirstRun)
            {
                try
                {
                    //The icon located in: (Right click Project -> Properties -> Application (tab) -> Icon)
                    var iconSourcePath = Path.Combine(Application.StartupPath, "briefcase-4-fill.ico");
                    if (!File.Exists(iconSourcePath)) { return; }

                    var myUninstallKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Uninstall");
                    if (myUninstallKey == null) { return; }

                    var mySubKeyNames = myUninstallKey.GetSubKeyNames();
                    foreach (var subkeyName in mySubKeyNames)
                    {
                        var myKey = myUninstallKey.OpenSubKey(subkeyName, true);
                        var myValue = myKey.GetValue("DisplayName");
                        if (myValue != null && myValue.ToString() == "SP CRUD")
                        {
                            // same as in 'Product name:' field (Located in: Right click Project ->
                            // Properties -> Publish (tab) -> Options -> Description)
                            myKey.SetValue("DisplayIcon", iconSourcePath);
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Add Remove Programs Icon Error! \nError: " + ex.Message);
                }
            }
        }
    }
}
