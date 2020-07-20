using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputersInDomainInfo
{
    class Export
    {
        DataTable components = new DataTable();

        public void clearDataTable()
        {
            components.Clear();
            components.Columns.Remove("Server");
            components.Columns.Remove("Server Name");
            components.Columns.Remove("Processor");
            components.Columns.Remove("RAM");
            components.Columns.Remove("Disk Space");
            components.Columns.Remove("Free Disk Space");
            components.Columns.Remove("Last Reboot");
        }
        public void createDataTable()
        {
            components.Columns.Add("Server", typeof(string));
            components.Columns.Add("Server Name", typeof(string));
            components.Columns.Add("Processor", typeof(string));
            components.Columns.Add("RAM", typeof(string));
            components.Columns.Add("Disk Space", typeof(string));
            components.Columns.Add("Free Disk Space", typeof(string));
            components.Columns.Add("Last Reboot", typeof(string));

        }

        public void addToTable(string server, string serverName, string processorType, string RAM, string diskCapacity, string freeDiskSpace, string lastReboot)
        {
            components.Rows.Add(server, serverName, processorType, RAM, diskCapacity, freeDiskSpace, lastReboot);
        }

        public void ToCSV()
        {
            StreamWriter sw = new StreamWriter(System.Reflection.Assembly.GetExecutingAssembly().Location + "ServersCSV.csv", false);
            //headers  
            for (int i = 0; i < components.Columns.Count; i++)
            {
                sw.Write(components.Columns[i]);
                if (i < components.Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
            foreach (DataRow dr in components.Rows)
            {
                for (int i = 0; i < components.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        string value = dr[i].ToString();
                        if (value.Contains(','))
                        {
                            value = String.Format("\"{0}\"", value);
                            sw.Write(value);
                        }
                        else
                        {
                            sw.Write(dr[i].ToString());
                        }
                    }
                    if (i < components.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();

        }
    }
}
