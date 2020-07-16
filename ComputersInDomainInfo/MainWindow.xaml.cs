using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Management;
using System.Management.Automation;
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

namespace ComputersInDomainInfo
{
    public partial class MainWindow : Window
    {
        FileDialog filedialog = new FileDialog();
        PowerShellHandler powershell = new PowerShellHandler();
        static string serverNameQuery = "Select * FROM Win32_ComputerSystem";
        static string procesorTypeQuery = "Select * FROM Win32_Processor";
        static string amountOfRamQuery = "SELECT Capacity FROM Win32_PhysicalMemory";
        static string discCapacityQuery = "SELECT Size FROM Win32_LogicalDisk";
        static string lastRebootQuery = "SELECT lastbootuptime FROM Win32_OperatingSystem";


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WMIAccess wmi = new WMIAccess(username.Text, password.Text);

            for(int i = 0; i < filedialog.serverList.Count;i++)
            {
                wmi.authority = filedialog.domainList[i];
                wmi.machineIP = powershell.getMachineIp(filedialog.machineNameList[i]);
                wmi.configureConnections();
                wmi.OpenConnection();
                wmi.GetValue(serverNameQuery);
                wmi.GetValue(procesorTypeQuery);
                wmi.GetValue(amountOfRamQuery);
                wmi.GetValue(discCapacityQuery);
                wmi.GetValue(lastRebootQuery);
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FileDialog opendialog = new FileDialog();
            opendialog.GetTextFromPath();
            opendialog.splitServerList();
        }
    }
}
