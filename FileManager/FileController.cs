using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace FileManager
{
    static class FileController
    {
        public static string[] GetDirectoryInfo(string path)
        {
            string[] result;

            string[] dirs = Directory.GetDirectories(path); //добавь исключение

            for (int i = 0; i < dirs.Length; i++)
            {
                string gap = "                                                      ";
                try
                {
                    dirs[i] = dirs[i].Substring(dirs[i].LastIndexOf(@"\") + 1) + gap.Substring(dirs[i].Substring(dirs[i].LastIndexOf(@"\") + 1).Length) + Directory.GetCreationTime(dirs[i]).ToString("f");
                }
                catch (System.ArgumentOutOfRangeException)
                {
                    dirs[i] = dirs[i].Substring(dirs[i].LastIndexOf(@"\") + 1);
                }
            }

            string[] files = Directory.GetFiles(path);

            for (int i = 0; i < files.Length; i++)
            {
                string gap = "                                                      ";
                try
                {
                    files[i] = files[i].Substring(files[i].LastIndexOf(@"\") + 1) + gap.Substring(files[i].Substring(files[i].LastIndexOf(@"\") + 1).Length) + File.GetCreationTime(files[i]).ToString("f");
                }
                catch (System.ArgumentOutOfRangeException)
                {
                    files[i] = files[i].Substring(files[i].LastIndexOf(@"\") + 1);
                }
                
            }

            return dirs.Concat(files).ToArray();
        }

        public static string[] GetPaths(string path)
        {
            string[] dirs = Directory.GetDirectories(path);
            string[] files = Directory.GetFiles(path);

            return dirs.Concat(files).ToArray();
        }

        public static void DeleteFile(string path)
        {
            try
            {
                try
                {
                    Directory.Delete(path, true);
                }
                catch (System.IO.IOException)
                {
                    try
                    {
                        File.Delete(path);
                    }
                    catch (Exception)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.SetCursorPosition(45, 9);
                        Console.WriteLine("                     ");
                        Console.SetCursorPosition(45, 10);
                        Console.WriteLine("       ERORR         ");
                        Console.SetCursorPosition(45, 11);
                        Console.WriteLine("                     ");
                        Console.ResetColor();
                        Thread.Sleep(250);
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    AccessException();
                }
            }
            catch (UnauthorizedAccessException)
            {
                AccessException();
            }
        }

        public static void  CreateFile(string path)
        {
            Console.CursorVisible = true;
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(85, 10);
            Console.WriteLine("Enter file name: ");
            Console.SetCursorPosition(85, 11);
            string fileName = Console.ReadLine();
            Console.CursorVisible = false;
            
            try
            {
                FileStream fileStream = File.Create(path + "\\" + fileName);
                fileStream.Dispose();
            }
            catch (UnauthorizedAccessException)
            {
                AccessException();
            }
            
        }

        public static void CreateDirectory(string path)
        {
            Console.CursorVisible = true;
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(85, 10);
            Console.WriteLine("Enter Folder name: ");
            Console.SetCursorPosition(85, 11);
            string dirName = Console.ReadLine();
            Console.CursorVisible = false;
            try
            {
                Directory.CreateDirectory(path + "\\" + dirName);
            }
            catch (UnauthorizedAccessException)
            {
                AccessException();
            }
        }

        public static void AccessException()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(45, 9);
            Console.WriteLine("                     ");
            Console.SetCursorPosition(45, 10);
            Console.WriteLine("    ACCESS DENIED    ");
            Console.SetCursorPosition(45, 11);
            Console.WriteLine("                     ");
            Console.ResetColor();
            Thread.Sleep(250);
        }

        private static int SelectedIndex;
        private static DriveInfo[] AllDrives;

        public static string DriveMenu()
        {
            AllDrives = DriveInfo.GetDrives();
            Console.Clear();
            int selectedIndex = Run();
            return AllDrives[selectedIndex].Name; 
        }

        private static void GetDrivesInfo()
        {
            int j = 0;
            for (int i = 0; i < AllDrives.Length; i++)
            {

                if (AllDrives[i].IsReady == true)
                {
                    string drive = ($"\n———————————————————————————————————————————————————————————\n{AllDrives[i].VolumeLabel}({AllDrives[i].Name})\nFile System: {AllDrives[i].DriveFormat}\n{AllDrives[i].TotalFreeSpace / 1073741824} GB free in {AllDrives[i].TotalSize / 1073741824} GB");

                    double condition = 0;
                    condition = Convert.ToDouble(AllDrives[i].TotalFreeSpace) / Convert.ToDouble(AllDrives[i].TotalSize);
                    condition = (Math.Round(condition, 1));

                    switch (condition)
                    {
                        case 0:
                            {
                                drive += "\n████████████████████████████████████████";
                                break;
                            }
                        case 0.1:
                            {
                                drive += "\n████████████████████████████████████░░░░";
                                break;
                            }
                        case 0.2:
                            {
                                drive += "\n████████████████████████████████░░░░░░░░";
                                break;
                            }
                        case 0.3:
                            {
                                drive += "\n████████████████████████████░░░░░░░░░░░░";
                                break;
                            }
                        case 0.4:
                            {
                                drive += "\n████████████████████████░░░░░░░░░░░░░░░░";
                                break;
                            }
                        case 0.5:
                            {
                                drive += "\n████████████████████░░░░░░░░░░░░░░░░░░░░";
                                break;
                            }
                        case 0.6:
                            {
                                drive += "\n████████████████░░░░░░░░░░░░░░░░░░░░░░░░ ";
                                break;
                            }
                        case 0.7:
                            {
                                drive += "\n████████████░░░░░░░░░░░░░░░░░░░░░░░░░░░░";
                                break;
                            }
                        case 0.8:
                            {
                                drive += "\n████████░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░";
                                break;
                            }
                        case 0.9:
                            {
                                drive += "\n████░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░";
                                break;
                            }
                        case 1:
                            {
                                drive += "\n░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░";
                                break;
                            }
                    }
                    drive += "\n———————————————————————————————————————————————————————————";

                    if (i == SelectedIndex)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.SetCursorPosition(0, j);
                    j += 5;
                    Console.Write($"{drive}");
                }
            }
        }

        private static int Run()
        {
            ConsoleKey keyPressed;
            do
            {
                new Thread(GetDrivesInfo).Start();

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.UpArrow)
                {
                    if (SelectedIndex > 0)
                    {
                        SelectedIndex--;
                    }
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    if (SelectedIndex < AllDrives.Length + 1)
                    {
                        SelectedIndex++;
                    }
                }
            } while (keyPressed != ConsoleKey.Enter);
            Console.Clear();
            return SelectedIndex;
        }
    }
}
