using System.Diagnostics;
using System.IO;

namespace Common.Entities
{
    public class Logger
    {
        public static void Write(object obj)
        {
            Debug.WriteLine(obj);
        }

        public static void WriteAdd(object obj)
        {
            WriteFile("ADDED " + obj);
        }

        public static void WriteFile(object obj)
        {
            //Directory.CreateDirectory(@"D:\Temp");
            //using (var file = new StreamWriter(new FileStream(@"D:\Temp\log.txt", FileMode.Append), Encoding.UTF8))
            //{
            //    file.WriteLineAsync(obj.ToString());
            //}
        }

        public static void ClearLog()
        {
            File.WriteAllText(@"D:\Temp\log.txt", string.Empty);
        }

        public static void WriteChange(object obj)
        {
            WriteFile("CHANGED " + obj);
        }
    }
}