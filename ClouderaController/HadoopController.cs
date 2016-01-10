using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSHWrapper;
using System.Configuration;
using System.IO;

namespace Hadoop
{
	// hadoop class
    public class HadoopController
    {

        public void TransferWebLogFilesToRemote(string filesPath)
        {
            // Sending the the weblogs
            var webLogsFiles = Directory.GetFiles(@"C:\Users\Matan\Desktop\BigDataCourse\Task3\Web_logs");

            for (int i = 0; i < webLogsFiles.Length; i++)
            {
                SshController.TransferFileToMachine(webLogsFiles[i], "/home/training/ex4run/WebLogs/" + "Log" + i + ".txt");
            }
        }

        public void TransferJavaFilesToRemote(string javaFilePath)
        {
            // Adding the files to hadoop hdfs

            // Sending the javafiles
            var javaFiles = Directory.GetFiles(@"C:\Users\Matan\Desktop\BigDataCourse\Task3\JavaFiles");
            string[] fileNames = Directory.GetFiles(@"C:\Users\Matan\Desktop\BigDataCourse\Task3\JavaFiles", "*.java")
                                     .Select(path => Path.GetFileName(path))
                                     .ToArray();

            for (int i = 0; i < javaFiles.Length; i++)
            {
                SshController.TransferFileToMachine(javaFiles[i], "/home/training/ex4run/" + fileNames[i]);
                
            }
        }

        public void CompilingJavaFilesOnRemote()
        {

            // Compiling the javafiles
            SshController.ExecuteSingleCommand("javac -cp /usr/lib/hadoop/*:/usr/lib/hadoop/client-0.20/*:/usr/lib/hadoop/lib/* -d /home/training/ex4run/ /home/training/ex4run/*.java");
            
            // Creating the jar
            SshController.ExecuteSingleCommand("cd /home/training/ex4run/ && jar -cvf  /home/training/ex4run/ex4.jar -c solution/*.class;");
        }

        public void RunHadoopOnRemote()
        {
            // Making all the folders
            SshController.ExecuteSingleCommand("hadoop fs -mkdir ex4");

            SshController.ExecuteSingleCommand("hadoop fs -mkdir ex4/input");

            SshController.ExecuteSingleCommand("hadoop fs -mkdir ex4/output");

            // Upload files to hadoop HDFS
            SshController.ExecuteSingleCommand("hadoop fs -copyFromLocal /home/training/ex4run/WebLogs/*.txt ex4/input/");

            // Running the map reduce function from the jar
            SshController.ExecuteSingleCommand("hadoop jar /home/training/ex4run/ex4.jar solution.WebsiteLogCount /user/training/ex4/input/* /user/training/ex4/output/");

            // Handle output
            SshController.ExecuteSingleCommand("hadoop fs -get /user/training/ex4/output/part-r-00000  /home/training/ex4run/output");
        }

        public void TransferOutputFilesFromRemoteMachine(string filePath)
        {
            SshController.TransferFileFromMachine(filePath, "part-r-00000");
        }
    }
}
