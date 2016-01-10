using Hadoop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunProject
{
    public class Program
    {
        static void Main(string[] args)
        {

            HadoopController hadoop = new HadoopController();

//            hadoop.TransferWebLogFilesToRemote(@"C:\Users\Matan\Desktop\BigDataCourse\Task3\Web_logs");

  //          hadoop.TransferJavaFilesToRemote(@"C:\Users\Matan\Desktop\BigDataCourse\Task3\JavaFiles");

      //      hadoop.CompilingJavaFilesOnRemote();
            
            //hadoop.RunHadoopOnRemote();

            hadoop.TransferOutputFilesFromRemoteMachine("/home/training/ex4run/output/");
        }
    }
}
