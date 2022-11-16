using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileManager
{
    internal class Menu
    {
        public int SelectedIndex;
        private string Label = "NAME                                                  DATE                                                                        ";
        private string[] Options;
        private string[] PrevOptions;
        private string[] Paths;
        private string[] PrevPaths;

        public void MainMenu()
        {
            Console.CursorVisible = false;
            Paths = FileController.GetPaths(@"C:\");
            string[] Dirs = FileController.GetDirectoryInfo(@"C:\");
            string[] Files = FileController.GetFileInfo(@"C:\");
            Options = Dirs.Concat(Files).ToArray();
            Run();
        }

        public void Display_Options()
        {
            Console.ResetColor();
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine(Label);
            Console.ResetColor();
            Console.SetCursorPosition(0, 2);
            
            for (int i = 0; i < Options.Length; i++)
            {
                string SelectedOption = Options[i];

                if (i == SelectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                Console.SetCursorPosition(0, i + 1);
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
                        SelectedIndex = Options.Length - 1;
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

                switch(Key_Pressed)
                {
                    case ConsoleKey.Escape:
                        Paths = PrevPaths;
                        Options = PrevOptions;
                        break;

                    case ConsoleKey.Enter:
                        PrevOptions = Options;
                        PrevPaths = Paths;

                        string[] Dirs = FileController.GetDirectoryInfo(Paths[SelectedIndex]);
                        string[] Files = FileController.GetFileInfo(Paths[SelectedIndex]);
                        Options = Dirs.Concat(Files).ToArray();

                        MenuClear();
                        break;
                }

            } while (true);
        }
        public void MenuClear()
        {
            for (int i = 0; i < PrevOptions.Length; i++)
            {
                Console.SetCursorPosition(0, 2);
                Console.ForegroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(0, i + 1);
                Console.WriteLine(PrevOptions[i]);
            }
            Console.ResetColor();
        }
    }
}
