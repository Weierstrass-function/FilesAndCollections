
using FilesAndCollections;
using System.Collections.Generic;
using static FilesAndCollections.DataStreams;

class Program
{
    static void Main()
    {
        Methods.generateRandomFiles();
        Methods.generateFileOfToys();

        // Задания 1-4:
        Console.WriteLine($"Средеарифметическое чисел = {Methods.avgNumsInFile("input1.txt")}");
        Console.WriteLine($"Произведение нечетных элементов = {Methods.prodOddInFile("input2.txt")}");
        Methods.copyLinesWithoutLetters("input3.txt", "output3.txt");
        Console.WriteLine($"Максимальный модуль числа нанечетной позиции = {Methods.maxAbsInOddPositions("input4")}");

        // Задание 5:
        List<Toy> toys = Methods.getToysFor4To5Years("input5");
        foreach (var toy in toys)
        {
            Console.WriteLine($"{toy.Name}, {toy.Price} деревянных, Рекомендовано детям: {toy.MinAge}-{toy.MaxAge} лет");
        }

        // Задание 6:
        List<int> l1 = DataStreams.getNums(5, 0, 6);
        List<int> l2 = DataStreams.getNums(5, 0, 6);
        Console.WriteLine($"Список l1: { string.Join(" ", l1)}");
        Console.WriteLine($"Список l2: {string.Join(" ", l2)}");
        Console.WriteLine($"l1 - l2: {string.Join(" ", Methods.sub(l1,l2))}");

        // Задание 7:
        LinkedList<int> l7 = new LinkedList<int>(new int[] { 1, 2, 3, 2, 1 });
        Console.WriteLine($"Симметричен с 1 по 3 {Methods.isSublistSymmetric(l7, 1, 3)}");
        Console.WriteLine($"Симметричен с 0 по 3 {Methods.isSublistSymmetric(l7, 0, 3)}");

        // Задача 8:
        HashSet<string> allChocolates = new HashSet<string> { "Dark", "Milk", "White", "GigaChocolate", "CrocodiloChocolito" };
        List<HashSet<string>> individualPreferences = new List<HashSet<string>>
        {
            new HashSet<string> { "Dark", "Milk", "GigaChocolate" },
            new HashSet<string> { "Milk", "White", "GigaChocolate" },
            new HashSet<string> { "GigaChocolate"  }
        };
        HashSet<string>[] analyzedPref = Methods.analyzeChocolatePreferences(allChocolates, individualPreferences);
        Console.WriteLine($"Нравится всем: {string.Join(", ", analyzedPref[0])}");
        Console.WriteLine($"Нравится кому-то: {string.Join(", ", analyzedPref[1])}");
        Console.WriteLine($"Нравится всем: {string.Join(", ", analyzedPref[2])}");

        // Задача 9:
        Console.WriteLine($"Было потеряно букв {Methods.countMissingCyrillicLetters("input9.txt")}");

        // Задача 10:
        CellSchedule result = getCellSchedule(FileMgr.importLines("input10.txt"));
        Console.WriteLine("C вещами из cell на выход:");
        foreach (var entry in result.SortedNamesByTime)
        {
            if (entry.Key <= result.OriginTime.Add(TimeSpan.FromHours(2)))
            {
                Console.WriteLine($"{entry.Value}");
            }
        }
    }
}