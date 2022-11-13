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
            int SelectedIndex;

            string[] dirs = Directory.GetDirectories(@"C:\");

            ConsoleKeyInfo PressedKey;
            do
            {
                Menu menu = new Menu("afafaf", dirs, "dadadadd");
                SelectedIndex = menu.Run();
                PressedKey = Console.ReadKey();
                dirs = Directory.GetDirectories(dirs[SelectedIndex]);
            } while(PressedKey.Key != ConsoleKey.Escape);


        }
    }
}
