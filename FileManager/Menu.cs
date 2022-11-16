using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileManager
{
    internal class Menu
    {
        public int SelectedIndex;
        private string Label = "NAME                                                  DATE                                                                ";

        private string CurrentPath;

        private string[] Options;
        private string[] PrevOptions;

        private string[] Paths;


        public void MainMenu()
        {
            Console.Title = "Console File Manager";
            CurrentPath = FileController.DriveMenu();
            Console.CursorVisible = false;
            Paths = Directory.GetDirectories(CurrentPath);
            Options = FileController.GetDirectoryInfo(CurrentPath);
            Run();
        }

        private void Display_Options()
        {
            Console.ResetColor();

            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine(Label);
            Console.ResetColor();

            Console.SetCursorPosition(0, 1);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("CURRENT DIRECTORY: " + CurrentPath);

            Console.ResetColor();

            
            for (int i = 0; i < Options.Length; i++)
            {
                string SelectedOption = Options[i];

                if (i == SelectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }
                else if (i >= Directory.GetDirectories(CurrentPath).Length)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                Console.SetCursorPosition(0, 2 + i);
                Console.WriteLine(SelectedOption);
            }
            Console.ResetColor();
        }
        public void Run()
        {
            ConsoleKey Key_Pressed;
            do
            {
                new Thread(Display_Options).Start();
                Thread.Sleep(10);
                ConsoleKeyInfo Key_Inf = Console.ReadKey(true);
                Thread.Sleep(10);
                Key_Pressed = Key_Inf.Key;

                if (Key_Pressed == ConsoleKey.UpArrow)
                {
                    SelectedIndex--;
                    if (SelectedIndex < 0)
                    {
                        SelectedIndex = Options.Length;
                    };
                }
                else if (Key_Pressed == ConsoleKey.DownArrow)
                {
                    SelectedIndex++;
                    if (SelectedIndex >= Options.Length)
                    {
                        SelectedIndex = 0;
                    };
                }
                Console.ForegroundColor = ConsoleColor.Black;

                switch (Key_Pressed)
                {
                    case ConsoleKey.Escape:
                        try
                        {
                            CurrentPath = Path.GetDirectoryName(CurrentPath);
                            Options = FileController.GetDirectoryInfo(CurrentPath);
                            MenuClear();
                        }
                        catch (System.ArgumentNullException)
                        {
                            CurrentPath = FileController.DriveMenu();
                        }
                        break;

                    case ConsoleKey.Enter:
                        PrevOptions = Options;
                        try
                        {
                            Paths = FileController.GetPaths(CurrentPath);
                            Options = FileController.GetDirectoryInfo(Paths[SelectedIndex]);
                            CurrentPath = Paths[SelectedIndex];
                            SelectedIndex = 0;
                            MenuClear();
                        }
                        catch (System.IO.IOException)
                        {
                            Process.Start(new ProcessStartInfo { FileName = Paths[SelectedIndex], UseShellExecute = true });
                        }
                        catch (System.UnauthorizedAccessException)
                        {

                        }
                        break;

                    case ConsoleKey.Delete:
                        FileController.DeleteFile(Paths[SelectedIndex]);
                        Options = FileController.GetDirectoryInfo(CurrentPath);
                        break;
                    case ConsoleKey.F1:
                        //FileController.CreateDirectory(Paths[SelectedIndex]);
                        //Options = FileController.GetDirectoryInfo(CurrentPath);
                        break;
                }
            } while (true);
        }
        private void MenuClear()
        {
            for (int i = 0; i < PrevOptions.Length; i++)
            {
                Console.SetCursorPosition(0, 1);
                Console.ForegroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(0, i + 1);
                Console.WriteLine(Label);
                Console.WriteLine(Label);
            }
            Console.ResetColor();
        }
    }
}
