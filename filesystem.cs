using System;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace FirstOS
{
    class filesystem
    {
        public static void writeFile(string Adr, string[] data) //Write text to a file
        {
        File.WriteAllLines(Adr, data);
        }
        public static string[] readFile(string Adr) //Read text from a file
        {
        string[] read;
        read = File.ReadAllLines(Adr);
        return read;
        }
        public static void createDir(string Adr) //Create a folder
        {
        Directory.CreateDirectory(Adr);
        }
        public static void deleteDir(string Adr) //Delete a folder
        {
        Directory.Delete(Adr);
        }
        public static string[] readFiles(string Adr) //get files from address
        {
            string[] Files = new string[256];
            if (Directory.GetFiles(Adr).Length > 0)
                Files = Directory.GetFiles(Adr);
            else
                Files[0] = "No Files Found";
            return Files;
        }
        public static string[] getDirectories(string Adr)
        {
            var Dirs = Directory.GetDirectories(Adr);
            return Dirs;
        }
        public string ReadText(string FileAddr) //returns the file in single string
        {
            string contents = "";
            contents = File.ReadAllText(FileAddr);
            return contents;
        }
        public static void writeText(string Adr, string testo)
        {
            File.WriteAllText(Adr, testo);
        }
        public static void createFile(string Adr)
        {
            File.Create(Adr);
        }
        public static void readText(string Adr)
        {
            File.ReadAllText(Adr);
        }
        public static string[] readDirectories(string Adr) // Get Directories From Address
        {
            var Dirs = Directory.GetDirectories(Adr);
            return Dirs;
        }
    }
}
