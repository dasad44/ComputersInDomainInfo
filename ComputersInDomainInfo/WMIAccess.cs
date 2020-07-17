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
        public string authority;
        public string machineIP;
        ConnectionOptions options = new ConnectionOptions();
        ManagementScope scope = null;

        public WMIAccess(string Username, string Password)
        {
            username = Username;
            password = Password;
        }

        public void configureConnections()
        {
            options.Impersonation = System.Management.ImpersonationLevel.Impersonate;
            options.Username = username;
            options.Password = password;
            options.Authority = "ntlmdomain:" + authority;
            scope = new ManagementScope("\\\\" + machineIP + "\\root\\cimv2", options);
        }

        public void OpenConnection()
        {
            scope.Connect();
        }

        private string getWMIObject(string query)
        {
            var objectToShow = query.Split(' ');
            return objectToShow[1];
        }


        public string GetValue(string query)
        {
            ObjectQuery Oquery = new ObjectQuery(query);
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, Oquery);
            ManagementObjectCollection queryCollection = searcher.Get();
            foreach (ManagementObject m in queryCollection)
            {
                try
                {
                    return m[getWMIObject(query)].ToString();
                }
                catch(Exception e)
                {
                    MessageBox.Show("WMI Problem: " + e.Message);
                }
            }
            return null;
        }
    }
}
