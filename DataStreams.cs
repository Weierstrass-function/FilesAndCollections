using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FilesAndCollections
{
    public struct Toy
    {
        public string Name;
        public int Price;
        public int MinAge;
        public int MaxAge;
    }

    internal class DataStreams
    {
        
        public static List<int> getNums(string filePath)
        {
            List<string> lines = FileMgr.importLines(filePath);
            List<int> nums = [];
            foreach (string line in lines)
            {
                string[] words = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                foreach (string word in words)
                {
                    if (int.TryParse(word, out int num))
                    {
                        nums.Add(num);
                    }
                    else
                    {
                        Console.WriteLine($"'{word}' не является числом и будет пропущено.");
                    }
                }
            }

            return nums;
        }

        public static List<int> getNums(int size, int minVal, int maxVal)
        {
            List<int> nums = new List<int>();
            Random random = new Random();

            for (int i = 0; i < size; i++)
            {
                nums.Add(random.Next(minVal, maxVal + 1));
            }

            return nums;
        }


        public struct CellSchedule
        {
            public TimeSpan OriginTime;
            public SortedList<TimeSpan, string> SortedNamesByTime;
        }

        public static CellSchedule getCellSchedule(List<string> input)
        {
            if (!TimeSpan.TryParse(input[0], out TimeSpan initialTime))
            {
                throw new FormatException($"Ошибка при разборе начального времени: {input[0]}");
            }

            SortedList<TimeSpan, string> sortedNamesByTime = new SortedList<TimeSpan, string>();

            // 2 первые строки больше не нужны
            for (int i = 2; i < input.Count; i++)
            {
                string[] parts = input[i].Split(' ');
                if (parts.Length != 2)
                {
                    throw new FormatException($"Неверный формат строки: {input[i]}");
                }

                string name = parts[0];
                string timeString = parts[1];

                if (!TimeSpan.TryParse(timeString, out TimeSpan time))
                {
                    throw new FormatException($"Ошибка при разборе времени: {timeString}");
                }

                sortedNamesByTime.Add(time, name);
            }

            return new CellSchedule
            {
                OriginTime = initialTime,
                SortedNamesByTime = sortedNamesByTime
            };
        }
    }
}
