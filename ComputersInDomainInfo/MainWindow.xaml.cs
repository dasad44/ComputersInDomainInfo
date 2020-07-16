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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WMIAccess wmi = new WMIAccess();
            wmi.configureConnections();
            wmi.OpenConnection();
            wmi.GetValue();


            PowerShellHandler psh = new PowerShellHandler();
            psh.createCommand();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FileDialog opendialog = new FileDialog();
            opendialog.GetTextFromPath();
            opendialog.splitServerList();
        }
    }
}
