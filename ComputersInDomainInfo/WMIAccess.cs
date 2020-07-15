using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ComputersInDomainInfo
{
    class WMIAccess
    {
        public void GetValue()
        {
            ManagementScope scope = null;
            ConnectionOptions options = new ConnectionOptions();
            options.Impersonation = System.Management.ImpersonationLevel.Impersonate;
            options.Username = "Administrator";
            options.Password = "777Dasad777";
            options.Authority = "ntlmdomain:edu.pl";


            scope = new ManagementScope("\\\\192.168.0.3\\root\\cimv2", options);
            scope.Connect();

            ObjectQuery query = new ObjectQuery("Select TotalPhysicalMemory FROM Win32_PhysicalMemory");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);

            ManagementObjectCollection queryCollection = searcher.Get();
            foreach (ManagementObject m in queryCollection)
            {
                MessageBox.Show(m["TotalPhysicalMemory"].ToString());
            }
        }
    }
}
