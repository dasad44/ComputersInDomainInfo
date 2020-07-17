using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Threading;

namespace ComputersInDomainInfo
{
    public partial class MainWindow : Window
    {
        FileDialog filedialog = new FileDialog();
        PowerShellHandler powershell = new PowerShellHandler();
        static string serverNameQuery = "Select Caption FROM Win32_ComputerSystem";
        static string procesorTypeQuery = "Select Caption FROM Win32_Processor";
        static string amountOfRamQuery = "SELECT Capacity FROM Win32_PhysicalMemory";
        static string diskCapacityQuery = "SELECT Size FROM Win32_LogicalDisk";
        static string diskFreeSpaceQuery = "SELECT FreeSpace FROM Win32_LogicalDisk";
        static string lastRebootQuery = "SELECT lastbootuptime FROM Win32_OperatingSystem";
        string server, serverName, processorType, RAM, diskCapacity, freeDiskSpace, lastReboot;
        int serversCount = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                progressbar.Maximum = filedialog.serverList.Count;
                WMIAccess wmi = new WMIAccess(username.Text, password.Password);
                for (int i = 0; i < filedialog.serverList.Count; i++)
                {
                    progressbar.Value = i + 1;
                    wmi.authority = filedialog.domainList[i];
                    wmi.machineIP = powershell.executeCommand(powershell.getMachineIp(filedialog.machineNameList[i]));
                    wmi.configureConnections();
                    wmi.OpenConnection();
                    server = filedialog.serverList[i];
                    serverName = wmi.GetValue(serverNameQuery);
                    processorType = wmi.GetValue(procesorTypeQuery);
                    RAM = wmi.GetValue(amountOfRamQuery);
                    RAM = kbToMBConvert(RAM);
                    diskCapacity = wmi.GetValue(diskCapacityQuery);
                    diskCapacity = kbToGBConvert(diskCapacity);
                    freeDiskSpace = wmi.GetValue(diskFreeSpaceQuery);
                    freeDiskSpace = kbToGBConvert(freeDiskSpace);
                    lastReboot = wmi.GetValue(lastRebootQuery);
                    lastReboot = getRebootDate(lastReboot);
                    listview.Items.Add(new ServerElements { Server = server, ServerName = serverName, Processor = processorType, RAM = RAM + "MB", DiskSpace = diskCapacity + "GB", FreeDiskSpace = freeDiskSpace + "GB", LastReboot = lastReboot });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem with showing servers components: " + ex.Message);
            }
        }
        static string getRebootDate(string unConvertedDate)
        {
            string day, month, year, hour, minute, second, convertedDate;
            year = unConvertedDate.Remove(4);
            month = unConvertedDate[4].ToString() + unConvertedDate[5].ToString();
            day = unConvertedDate[6].ToString() + unConvertedDate[7].ToString();
            hour = unConvertedDate[8].ToString() + unConvertedDate[9].ToString();
            minute = unConvertedDate[10].ToString() + unConvertedDate[11].ToString();
            second = unConvertedDate[12].ToString() + unConvertedDate[13].ToString();
            convertedDate = year + "/" + month + "/" + day + " " + hour + ":" + minute + ":" + second;
            return convertedDate;
        }

        static string kbToGBConvert(string number)
        {
            long _temp = Int64.Parse(number);
            _temp = _temp / (1024 * 1024 * 1024);
            return _temp.ToString();
        }
        static string kbToMBConvert(string number)
        {
            long _temp = Int64.Parse(number);
            _temp = _temp / (1024 * 1024);
            return _temp.ToString();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            filedialog.GetTextFromPath();
            filedialog.splitServerList();
            filename.Text = filedialog.fileName;
        }
    }
}
