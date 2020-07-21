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
using System.Globalization;
using System.Threading;
using System.Security.Cryptography.X509Certificates;
using System.CodeDom;
using System.Data;
using System.Data.Odbc;
using System.Security.Cryptography;

namespace ComputersInDomainInfo
{
    public partial class MainWindow : Window
    {
        FileDialog filedialog = new FileDialog();
        PowerShellHandler powershell = new PowerShellHandler();
        static string serverNameQuery = "Select Caption FROM Win32_ComputerSystem";
        static string procesorTypeQuery = "Select Caption FROM Win32_Processor";
        static string amountOfRamQuery = "SELECT Capacity FROM Win32_PhysicalMemory";
        static string diskCapacityQuery = "SELECT Size FROM Win32_LogicalDisk WHERE DriveType = 3";
        static string diskFreeSpaceQuery = "SELECT FreeSpace FROM Win32_LogicalDisk WHERE DriveType = 3";
        static string lastRebootQuery = "SELECT lastbootuptime FROM Win32_OperatingSystem";
        static string diskIDQuery = "SELECT DeviceID FROM Win32_LogicalDisk WHERE DriveType = 3";
        string server, serverName, processorType, RAM, diskCapacity, freeDiskSpace, lastReboot;
        Export export = new Export();
        int i = 0, j = 0;
        List<string> diskNameList = new List<string>();
        List<string> diskCapacityList = new List<string>();
        List<string> freeDiskSpaceList = new List<string>();

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                export.ToCSV();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem with exporting to CSV file " + ex.Message);
            }
            MessageBox.Show("CSV file created");
        }

        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                export.clearDataTable();
            }
            catch(Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }

            try
            {
                export.createDataTable();
                progressbar.Maximum = filedialog.serverList.Count;
                progressbar.Value = 0;
                WMIAccess wmi = new WMIAccess(username.Text, password.Password);
                for (i = 0; i < filedialog.serverList.Count; i++)
                {
                    wmi.authority = filedialog.domainList[i];
                    wmi.machineIP = powershell.executeCommand(powershell.getMachineIp(filedialog.machineNameList[i]));
                    server = filedialog.serverList[i];
                    wmi.configureConnections();
                    wmi.OpenConnection();
                    serverName = wmi.GetValue(serverNameQuery);
                    processorType = wmi.GetValue(procesorTypeQuery);
                    RAM = wmi.GetValue(amountOfRamQuery);
                    RAM = kbToGBConvert(RAM);
                    lastReboot = wmi.GetValue(lastRebootQuery);
                    lastReboot = getRebootDate(lastReboot);


                    diskNameList = wmi.GetValuesArray(diskIDQuery);
                    diskCapacityList = wmi.GetValuesArray(diskCapacityQuery);
                    freeDiskSpaceList = wmi.GetValuesArray(diskFreeSpaceQuery);
                    for (j = 0; j < diskNameList.Count; j++)
                    {
                        diskCapacityList[j] = kbToGBConvert(diskCapacityList[j]);
                        freeDiskSpaceList[j] = kbToGBConvert(freeDiskSpaceList[j]);

                        export.addToTable(server, serverName, processorType, RAM, diskNameList[j], diskCapacityList[j], freeDiskSpaceList[j], lastReboot);
                        listview.Items.Add(new ServerElements { Server = server, ServerName = serverName, Processor = processorType, RAM = RAM + "GB", NameDisk = diskNameList[j], DiskSpace = diskCapacityList[j] + "GB", FreeDiskSpace = freeDiskSpaceList[j] + "GB", LastReboot = lastReboot });

                        //MessageBox.Show("d");
                    }
                    progressbar.Value = i + 1;
                    MessageBox.Show("");
                    percentblock.Text = getPercentStateValue(filedialog.serverList.Count, i + 1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem with showing servers components: " + ex.Message + " Server Name: " + server);
                percentblock.Text = "Failed";
            }
        }

        string getPercentStateValue(double maximumPercentVal, double actualPercentVal)
        {
            maximumPercentVal = progressbar.Maximum;

            actualPercentVal = progressbar.Value;
            string actualPercent = "";
            actualPercentVal = actualPercentVal / maximumPercentVal;
            actualPercent = actualPercentVal.ToString("P", CultureInfo.InvariantCulture);
            if (actualPercent == "100.00 %")
            {
                actualPercent = actualPercent + " Completed";
            }
            return actualPercent;
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            filedialog.GetTextFromPath();
            filedialog.splitServerList();
            filename.Text = filedialog.fileName;
        }
    }
}
