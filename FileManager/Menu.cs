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
        private string[] Options;
        private string Label;
        private string FullOrder;

        public Menu(string label, string[] options, string fullOrder)
        {
            Label = label;
            Options = options;
            SelectedIndex = 0;
            FullOrder = fullOrder;
        }
        public void Display_Options()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(Label);
            Console.SetCursorPosition(0, 1);
            
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
                Console.WriteLine(SelectedOption);
            }
            Console.ResetColor();

        }
        public int Run()
        {
            ConsoleKey Key_Pressed;
            do
            {
                
                new Thread(Display_Options).Start();

                ConsoleKeyInfo Key_Inf = Console.ReadKey(true);
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
            } while (Key_Pressed != ConsoleKey.Enter);



            return SelectedIndex;
        }
    }
}
