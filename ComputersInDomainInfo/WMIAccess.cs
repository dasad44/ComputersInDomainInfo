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
        string username;
        string password;
        string authority;
        string query;
        string machineIP;
        ConnectionOptions options = new ConnectionOptions();
        ManagementScope scope = null;

        public void configureConnections()
        {
            options.Impersonation = System.Management.ImpersonationLevel.Impersonate;
            options.Username = "Administrator";
            options.Password = "777Dasad777";
            options.Authority = "ntlmdomain:edu.pl";
            scope = new ManagementScope("\\\\192.168.0.3\\root\\cimv2", options);
        }

        public void OpenConnection()
        {
            scope.Connect();
        }


        public void GetValue()
        {
            ObjectQuery query = new ObjectQuery("SELECT lastbootuptime FROM Win32_OperatingSystem");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);

            ManagementObjectCollection queryCollection = searcher.Get();
            foreach (ManagementObject m in queryCollection)
            {
                MessageBox.Show(m["lastbootuptime"].ToString());
            }
        }
    }
}
