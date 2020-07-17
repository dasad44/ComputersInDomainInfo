using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation.Language;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComputersInDomainInfo
{
    class FileDialog
    {
        public List<string> serverList = new List<string>();
        public List<string> machineNameList = new List<string>();
        public List<string> domainList = new List<string>();
        public string fileName = string.Empty;

        public void splitServerList()
        {
            for (int i = 0; i < serverList.Count; i++)
            {
                var serverName = serverList[i].Split('.');
                machineNameList.Add(serverName[0]);
                int l = serverName[0].Length + 1;
                string domain = serverList[i].Remove(0,l) ;
                domainList.Add(domain);
            }
        }

        public List<string> GetTextFromPath()
        {

            var fileContent = string.Empty;
            var filePath = string.Empty;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;

                    var fileStream = openFileDialog.OpenFile();
                    fileName = Path.GetFileName(filePath);

                    try
                    {
                        using (StreamReader sr = new StreamReader(fileStream))
                        {
                            string line;
                            while ((line = sr.ReadLine()) != null)
                            {
                                serverList.Add(line);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("The file could not be read: " + e.Message);
                    }
                }
                return serverList;
            }
           // MessageBox.Show(fileContent, "File Content at path: " + filePath, MessageBoxButtons.OK);
        }


    }
}
