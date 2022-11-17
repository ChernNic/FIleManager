using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace FileManager
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            //string path = @"C:\";

            //string[] Options = Directory.GetFileSystemEntries(path);

            //for (int i = 0; i < Options.Length; i++)
            //{
            //    Console.WriteLine(Options[i]);
            //}|—

            //Console.ReadLine();

            Menu menu = new Menu();
            menu.MainMenu();
        }
        
    }
}

