using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tamir.SharpSsh;

namespace SSHWrapper
{
    public class SshManager
    {
        static string USER_NAME = "training";
        static string PASSWORD = "training";
        static string MACHINE_IP = "192.168.59.131";

        public static void MakeNewDirectory(string dirName)
        {

            //create a new ssh stream
            using (SshStream ssh = new SshStream(MACHINE_IP, USER_NAME, PASSWORD))
            {
                //writing to the ssh channel
                ssh.Write("mkdir " + dirName);
            }
        }

        public static void TransferFileToMachine(string localFilePath, string remoteFilePath)
        {
            try
            {
                //Create a new SCP instance
                Scp scp = new Scp(MACHINE_IP, USER_NAME, PASSWORD);

                scp.Connect();

                //Copy a file from remote SSH server to local machine
                scp.To(localFilePath, remoteFilePath);

                scp.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void TransferFileFromMachine(string remoteFile, string localPath)
        {
            try
            {
                //Create a new SCP instance
                Scp scp = new Scp(MACHINE_IP, USER_NAME, PASSWORD);

                scp.Connect();

                //Copy a file from remote SSH server to local machine
                scp.From(remoteFile, localPath);

                scp.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void ExecuteSingleCommand(string command)
        {
            try
            {
                //create a new ssh stream
                SshExec ssh = new SshExec(MACHINE_IP, USER_NAME, PASSWORD);

                ssh.Connect();

                //writing to the ssh channel
               var str =  ssh.RunCommand(command);

                ssh.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
