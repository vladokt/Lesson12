using System.IO;
using System.IO.Compression;

namespace ZipAnalyzer
{
    public class Program
    {
        public static void Main()
        {
            string zipFilePath = Directory.GetCurrentDirectory() + "\\";
            string zipFileName = "archive.zip";

            Console.Clear();
            Console.WriteLine("Analyzing ZIP-archive...");
            Console.WriteLine();
            Console.WriteLine($"File path: {zipFilePath + zipFileName}");

            if (Directory.Exists(zipFilePath + "archive"))
            {
                Console.WriteLine($"Directory {zipFilePath + "archive"} alredy exists");
                Console.Write("Continue? (y/n): ");
                string input = Console.ReadLine();
                if (input == "n")
                {
                    return;
                }
                else if (input != "y")
                {
                    return;
                }
            }
            else
            {
                try
                {
                    Directory.CreateDirectory(zipFilePath + "archive");
                    ZipFile.ExtractToDirectory(zipFilePath + zipFileName, zipFilePath + "archive");
                }
                catch (IOException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine();

                    return;
                }
            }

            List<ItemInfo> items = new();
            ItemInfo item;

            string[] names = Directory.GetDirectories(zipFilePath + "archive");

            if(names.Length > 0)
            {
                for (int i = 0; i < names.Length; i++)
                {
                    item = new(ItemInfo.Type.Directory, names[i].Substring(names[i].LastIndexOf('\\') + 1), 
                        Directory.GetLastAccessTime(names[i]));
                    items.Add(item);
                }
            }

            names = Directory.GetFiles(zipFilePath + "archive");
            if (names.Length > 0)
            {
                for (int i = 0; i < names.Length; i++)
                {
                    item = new(ItemInfo.Type.File, names[i].Substring(names[i].LastIndexOf('\\') + 1), 
                        Directory.GetLastAccessTime(names[i]));
                    items.Add(item);
                }
            }

            Console.WriteLine();
            Console.WriteLine($"File {zipFileName} contains:");
            foreach (var itemInfo in items)
            {
                Console.WriteLine(itemInfo.ToString());
            }

            using(StreamWriter stream = new (zipFilePath + "zipinfo.csv", false))
            {
                try
                {
                    for (int i = 0; i < items.Count; i++)
                    {
                        if (i < items.Count - 1)
                        {
                            stream.Write(items[i].ToString() + "\t");
                        }
                        else if (i == items.Count - 1)
                        {
                            stream.Write(items[i].ToString());
                        }
                    }
                }
                catch(IOException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadLine();
                    return;
                }
            }

            Console.WriteLine();
            Console.WriteLine("File zipinfo.csv saved successfully");
            Console.WriteLine();

            Directory.Delete(zipFilePath + "archive", true);

            using(StreamWriter stream = new(zipFilePath + "Lesson12Homework.txt", false))
            {
                stream.Write(zipFilePath + "zipinfo.csv");
            }

            Console.WriteLine("Press any key");
            Console.ReadLine();
        }
    }
}