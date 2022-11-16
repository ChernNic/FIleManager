using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    static class FileController
    {
        public static string[] GetDirectoryInfo(string path)
        {
            string[] dirs = Directory.GetDirectories(path);

            string[] result = new string[dirs.Length];

            DateTime[] dirsCreationTime = new DateTime[dirs.Length];
            for (int i = 0; i < dirs.Length; i++)
            {
                dirsCreationTime[i] = Directory.GetCreationTime(dirs[i]);
            }

            for (int i = 0; i < dirs.Length; i++)
            {
                string gap = "                                                      ";
                result[i] = dirs[i].Substring(dirs[i].LastIndexOf(@"\") + 1) + gap.Substring(dirs[i].Substring(dirs[i].LastIndexOf(@"\") + 1).Length) + dirsCreationTime[i].ToString("f");
            }
            return result;
        }

        public static string[] GetFileInfo(string path)
        {
            string[] files = Directory.GetFiles(path);

            string[] result = new string[files.Length];

            DateTime[] filesCreationTime = new DateTime[files.Length];
            for (int i = 0; i < files.Length; i++)
            {
                filesCreationTime[i] = File.GetCreationTime(files[i]);
            }

            for (int i = 0; i < files.Length; i++)
            {
                string gap = "                                                      ";
                result[i] = files[i].Substring(files[i].LastIndexOf(@"\") + 1) + gap.Substring(files[i].Substring(files[i].LastIndexOf(@"\") + 1).Length) + filesCreationTime[i].ToString("f");
            }
            return result;
        }
        //
    }
}
