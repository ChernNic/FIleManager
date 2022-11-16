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

            string[] dirs = Directory.GetDirectories(path);

            for (int i = 0; i < dirs.Length; i++)
            {
                string gap = "                                                      ";
                dirs[i] = dirs[i].Substring(dirs[i].LastIndexOf(@"\") + 1) + gap.Substring(dirs[i].Substring(dirs[i].LastIndexOf(@"\") + 1).Length) + Directory.GetCreationTime(dirs[i]).ToString("f");

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

            return result = dirs.Concat(files).ToArray();
        }

        public static string[] GetPaths(string path)
        {
            string[] result;
            string[] dirs = Directory.GetDirectories(path);
            string[] files = Directory.GetFiles(path);

            return result = dirs.Concat(files).ToArray();
        }

        public static void DeleteFile(string path)
        { 
            try
            {
                Directory.Delete(path,true);
            }
            catch (Exception)
            {
                File.Delete(path);                
            }
        }

        private static int SelectedIndex;
        private static DriveInfo[] AllDrives = DriveInfo.GetDrives();

        public static string DriveMenu()
        {
            Console.Clear();
            int selectedIndex = Run();
            return AllDrives[selectedIndex].Name;
        }

        private static void GetDrivesInfo()
        {
            string[] result = new string[AllDrives.Length];
            int j = 0;
            for (int i = 0; i < AllDrives.Length; i++)
            {

                if (AllDrives[i].IsReady == true)
                {
                    string drive = ($"\n{AllDrives[i].VolumeLabel}({AllDrives[i].Name})\nFile System: {AllDrives[i].DriveFormat}\n{AllDrives[i].TotalFreeSpace / 1073741824} GB free in {AllDrives[i].TotalSize / 1073741824} GB");

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
                    drive += "\n----------------------------------------------";

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
                    SelectedIndex--;
                    if (SelectedIndex < 1)
                    {
                        SelectedIndex = AllDrives.Length - 1;
                    }
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    SelectedIndex++;
                    if (SelectedIndex > AllDrives.Length)
                    {
                        SelectedIndex = 0;
                    }
                }
            } while (keyPressed != ConsoleKey.Enter);
            Console.Clear();
            return SelectedIndex;
        }
    }
}
