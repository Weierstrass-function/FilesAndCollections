using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FilesAndCollections
{
    // Класс работы с файлами
    internal class FileMgr
    {
        public static List<string> importLines(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Файл не найден.", filePath);
            }

            return new List<string>(File.ReadAllLines(filePath));
        }

        public static void exportLines(string filePath, List<string> lines) 
        {
            File.WriteAllLines(filePath, lines);
        }

        public static List<int> import(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Файл не найден.", filePath);
            }

            List<int> nums = [];

            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (BinaryReader reader = new BinaryReader(fs))
            {
                while (fs.Position < fs.Length)
                {
                    int num = reader.ReadInt32();
                    nums.Add(num);
                }
            }

            return nums;
        }

        public static void export(string filePath, List<int> nums)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            using (BinaryWriter writer = new BinaryWriter(fs))
            {
                foreach (int num in nums)
                {
                    writer.Write(num);
                }
            }
        }

        public static void serializeToys(string filePath, List<Toy> toys)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Toy>));
            using (FileStream fs = File.OpenWrite(filePath))
            {
                serializer.Serialize(fs, toys);
            }
        }

        public static List<Toy> DeserializeToys(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Файл не найден.", filePath);
            }

            XmlSerializer serializer = new XmlSerializer(typeof(List<Toy>));
            using (FileStream fs = File.OpenRead(filePath))
            {
                object? v = serializer.Deserialize(fs);
                return v as List<Toy>;
            }
        }
    }
}
