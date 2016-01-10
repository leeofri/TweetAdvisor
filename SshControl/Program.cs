//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Tamir.SharpSsh;

//namespace SshWrapper
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            Console.WriteLine(@"Please choose from 3 options
//                                1.Press 1 to create new dir in the machine
//                                2.Press 2 to transfer file to the machine
//                                3.Press 3 to transfer file to the machine");

//            int userChoise = Convert.ToInt32(Console.ReadLine());

//            while (true)
//            {
//                switch (userChoise)
//                {
//                    case (1):
//                        {
//                            SshWrapper.MakeNewDirectory("matan");
//                            break;
//                        }
//                    case (2):
//                        {
//                            SshWrapper.TransferFileToMachine();
//                            break;
//                        }
//                    case (3):
//                        {
//                            SshWrapper.TransferFileFromMachine();
//                            break;
//                        }
//                    default:
//                        {
//                            Console.WriteLine("Wrong code ");
//                            break;
//                        }

//                }


//                Console.WriteLine(@"Please choose from 3 options
//                                1.Press 1 to create new dir in the machine
//                                2.Press 2 to transfer file to the machine
//                                3.Press 3 to transfer file to the machine");

//                userChoise = Convert.ToInt32(Console.ReadLine());
//            }
//        }
//    }
//}
