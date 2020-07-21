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
            //MessageBox.Show(machineIP);
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
            try
            {
                ObjectQuery Oquery = new ObjectQuery(query);
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, Oquery);
                ManagementObjectCollection queryCollection = searcher.Get();
                foreach (ManagementObject m in queryCollection)
                {
                    //MessageBox.Show(m[getWMIObject(query)].ToString());
                    return m[getWMIObject(query)].ToString();

                }
            }
            catch (Exception e)
            {
                MessageBox.Show("WMI Problem: " + e.Message);
            }
            return null;
        }

        public List<string> GetValuesArray(string query)
        {
            List<string> elementList = new List<string>();
            try
            {
                ObjectQuery Oquery = new ObjectQuery(query);
                ManagementObjectSearcher obj = new ManagementObjectSearcher(scope, Oquery);
                ManagementObjectCollection colDisks = obj.Get();
                foreach (ManagementObject objDisk in colDisks)
                {
                    elementList.Add(objDisk[getWMIObject(query)].ToString());
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("WMI Problem: " + e.Message);
            }
            return elementList;
        }
    }
}
