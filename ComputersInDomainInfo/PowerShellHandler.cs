using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ComputersInDomainInfo
{
    class PowerShellHandler
    {
        FileDialog fd = new FileDialog();
        string command;

        public void createCommand()
        {
            command = "(Get-ADComputer '" + fd.machineNameList + "' -Properties IPv4Address).IPv4Address";
        }

        public void executeCommand()
        {
            using (PowerShell PowerShellInstance = PowerShell.Create())
            {
                PowerShellInstance.AddScript(command);
                var localComputer = Environment.GetEnvironmentVariable("COMPUTERNAME");
                PowerShellInstance.AddParameter("param1", localComputer);
                var remoteComputer = "[Remote Compter Name Goes Here]";
                PowerShellInstance.AddParameter("param2", remoteComputer);

                Collection<PSObject> PSOutput = PowerShellInstance.Invoke();

                foreach (PSObject outputItem in PSOutput)
                {
                    if (outputItem != null)
                    {
                        MessageBox.Show(outputItem.ToString());
                    }
                }
            }
        }
            
    }
}
