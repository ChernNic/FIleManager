using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileManager
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            string path = @"C:\";

            string[] Options = FileController.GetDirectoryInfo(path);

            for (int i = 0; i < Options.Length; i++)
            {
                Console.WriteLine(Options[i]);
            }
            Console.ReadLine();
        }
    }
}
