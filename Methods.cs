using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FilesAndCollections
{
    // Все
    internal class Methods
    {
        public static void generateRandomFiles()
        {
            List<int> rndNums = DataStreams.getNums(5, -3, 10);
            Console.WriteLine($"Случайно сгенерированная последовательность: {rndNums}");
            FileMgr.exportLines("input1.txt", rndNums.ConvertAll(num => num.ToString()));
            List<string> numsOnOneLine = [];
            numsOnOneLine.Add(string.Join(" ", rndNums));
            FileMgr.exportLines("input2.txt", numsOnOneLine);
            FileMgr.export("input4", rndNums);
        }

        public static void generateFileOfToys()
        {
            List<Toy> toys = new List<Toy>()
            {
                new Toy { Name = "Юла", Price = 0, MinAge = 21, MaxAge = 30 },
                new Toy { Name = "Промышленное артилерийское орудие", Price = 0, MinAge = 3, MaxAge = 7 },
                new Toy { Name = "Белколет", Price = 200, MinAge = 3, MaxAge = 7 },
                new Toy { Name = "Desert Eagle", Price = 300, MinAge = 0, MaxAge = 6 },
                new Toy { Name = "Радий фасованный", Price = 0, MinAge = 0, MaxAge =  66 },
                new Toy { Name = "Надстольная игра Выйти из IT нормально", Price = 1666, MinAge = 12, MaxAge = 66 },
                new Toy { Name = "Тонкие структуры", Price = 137, MinAge = -137, MaxAge = 137 },
                new Toy { Name = "Фрезерный ЧПУ станок", Price = 137, MinAge = 3, MaxAge = 5 }
            };
            FileMgr.serializeToys("input5", toys);
        }

        // Задание 1:
        public static double avgNumsInFile(string filePath)
        {
            List<int> nums = DataStreams.getNums(filePath);
            double sum = 0;
            foreach (int num in nums)
            {
                sum += num;
            }

            return sum / nums.Count;
        }

        // Задание 2:
        public static int prodOddInFile(string filePath)
        {
            List<int> nums = DataStreams.getNums(filePath);
            int prod = 1;
            foreach (int num in nums)
            {
                if (num % 2 != 0)
                {
                    prod *= num;
                }
            }

            return prod;
        }

        // Задание 3:
        static bool containsLetter(string str)
        {
            foreach (char c in str)
            {
                if (char.IsLetter(c))
                {
                    return true;
                }
            }
            return false;
        }
        public static void copyLinesWithoutLetters(string inputPath, string outputPath)
        {
            List<string> lines = FileMgr.importLines(inputPath);
            lines.RemoveAll(containsLetter);
            FileMgr.exportLines(outputPath, lines);
        }

        // Задание 4:
        public static int maxAbsInOddPositions(string filePath)
        {
            List<int> nums = FileMgr.import(filePath);
            int maxAbsValue = int.MinValue;

            for (int i = 0; i < nums.Count; i += 2)
            {
                int absValue = Math.Abs(nums[i]);
                if (absValue > maxAbsValue)
                {
                    maxAbsValue = absValue;
                }
            }

            return maxAbsValue;
        }

        // Задание 5:
        static bool notFor4To5Years(Toy toy)
        {
            return toy.MinAge > 4 || toy.MaxAge < 5;
        }
        public static List<Toy> getToysFor4To5Years(string filePath)
        {
            List<Toy> toys = FileMgr.DeserializeToys(filePath);
            toys.RemoveAll(notFor4To5Years);

            return toys;
        }

        // Задание 6:
        public static List<T> sub<T>(List<T> L1, List<T> L2)
        {
            List<T> result = new List<T>();

            foreach (var item in L1)
            {
                if (!L2.Contains(item))
                {
                    result.Add(item);
                }
            }

            return result;
        }

        // Задание 7:
        public static bool isSublistSymmetric<T>(LinkedList<T> list, int i, int j)
        {
            if (i < 0 || j >= list.Count || i > j)
            {
                throw new ArgumentException("Некорректные индексы.");
            }

            var left = list.First;
            var right = list.Last;

            for (int k = 0; k < i; k++)
            {
                left = left.Next;
            }

            for (int k = list.Count - 1; k > j; k--)
            {
                right = right.Previous;
            }

            while (left != null && right != null && left != right && left.Previous != right)
            {
                if (!EqualityComparer<T>.Default.Equals(left.Value, right.Value))
                {
                    return false;
                }
                left = left.Next;
                right = right.Previous;
            }

            return true;
        }

        // Задание 8:
        public static HashSet<string>[] analyzeChocolatePreferences(HashSet<string> allChocolates, List<HashSet<string>> preferences)
        {
            HashSet<string> likedByAll = new HashSet<string>(preferences[0]);
            HashSet<string> likedBySome = new HashSet<string>();
            HashSet<string> likedByNone = new HashSet<string>(allChocolates);

            foreach (var preference in preferences)
            {
                likedByAll.IntersectWith(preference);
                likedByNone.ExceptWith(preference);
            }

            foreach (var chocolate in allChocolates)
            {
                bool likedByAny = false;

                foreach (var preference in preferences)
                {
                    if (preference.Contains(chocolate))
                    {
                        likedByAny = true;
                        break;
                    }
                }

                if (likedByAny && !likedByAll.Contains(chocolate))
                {
                    likedBySome.Add(chocolate);
                }
            }

            return new HashSet<string>[] { likedByAll, likedBySome, likedByNone };
        }

        // Задание 9:
        public static int countMissingCyrillicLetters(string filePath)
        {
            HashSet<char> presentLetters = new HashSet<char>();
            string cyrilicLetters = "йцукенгшщзхъфывапролджэячсмитьбюё";

            List<string> lines = FileMgr.importLines(filePath);
            foreach (string line in lines)
            {
                foreach (char c in line.ToLower())
                {
                    if (cyrilicLetters.Contains(c.ToString()))
                    {
                        presentLetters.Add(c);
                    }
                }
            }

            return cyrilicLetters.Length - presentLetters.Count;
        }
    }
}
