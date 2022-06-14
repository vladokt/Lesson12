using ZipAnalyzer;

namespace ZipInfoReader
{
    public class Program
    {
        public static void Main()
        {
            Console.Clear();

            string currentDirectory = Directory.GetCurrentDirectory() + "\\";

            if (!File.Exists(currentDirectory + "Lesson12Homework.txt"))
            {
                Console.WriteLine("File Lesson12Homework.txt not found");
                Console.ReadLine();
                return;
            }

            string zipInfoFileName;
            string zipInfoFilePath;

            using (StreamReader stream = new(currentDirectory + "Lesson12Homework.txt"))
            {
                string str = stream.ReadToEnd();

                zipInfoFileName = str.Substring(str.LastIndexOf('\\') + 1) + "\\";
                zipInfoFilePath = str.Substring(0, str.LastIndexOf('\\'));
            }

            if (!File.Exists(zipInfoFilePath + zipInfoFileName))
            {
                Console.WriteLine($"File {zipInfoFileName} not found");
                Console.ReadLine();
                return;
            }

            string[] strings;

            using (StreamReader stream = new (zipInfoFilePath + zipInfoFileName))
            {
                strings = stream.ReadToEnd().Split('\t');
            }

            List<ItemInfo> items = new();
            ItemInfo item;
            ItemInfo.Type itemType;
            DateTime updateTime;
            string[] s;
            string[] date;
            string[] time;

            foreach (var str in strings)
            {
                s = str.Split(' ');

                if (s[0] == "Directory")
                {
                    itemType = ItemInfo.Type.Directory;
                }
                else if (s[0] == "File")
                {
                    itemType = ItemInfo.Type.File;
                }
                else
                {
                    Console.WriteLine("Wrong item type");
                    return;
                }

                date = s[2].Split('.');
                time = s[3].Split(':');
                updateTime = new(Int32.Parse(date[2]), Int32.Parse(date[1]), Int32.Parse(date[0]),
                    Int32.Parse(time[0]), Int32.Parse(time[1]), Int32.Parse(time[2]));

                item = new(itemType, s[1], updateTime);
                items.Add(item);
            }

            Console.WriteLine($"File {zipInfoFileName} contains {items.Count} items:");
            Console.WriteLine();

            var sortedItems = from i in items orderby i.ItemUpdateTime select i;

            foreach (var i in sortedItems)
            {
                Console.WriteLine(i.ToString());
            }

            File.Delete(currentDirectory + "Lesson12Homework.txt");

            Console.WriteLine();
            Console.WriteLine("Press any key");
            Console.ReadLine();
        }
    }
}