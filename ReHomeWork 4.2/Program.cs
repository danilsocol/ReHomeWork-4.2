using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace ReHomeWork_4._2
{
    class Program
    {
        public static int numColumnSort { get; set; }
        public static bool TypeSortUp { get; set; }
        public static int countRow { get; set; }
        public static bool IsDigit { get; set; }
        public static int pow { get; set; }
        public static int delay { get; set; }
        public static void Main(string[] args)
        {
            string file = "testTable3.txt";

            countRow = File.ReadAllLines(file).Length;
            FindPow();

            DrawTable(file);
            WorkFile.ReadData(file);
            WorkFile.SaveData(file);

            for (int i = 0; i < pow; i++)
            {
                SplitThrough(i);
                Sort(i+1);
            }

            DrawNewTable(file);
            WorkFile.SaveNewTable(file);

            Console.ReadLine();
        }

        static void FindPow()
        {
            int count = countRow - 1;
            while (count != 1)
            {
                if (count % 2==1)
                {
                    Console.WriteLine("Недопустимое кол-во строк, не степень 2");
                    Thread.Sleep(200000);
                }
                count  = count / 2;
                pow++;
            }
        }

        static void DrawTable(string file)
        {
            for (int i = 0; i < countRow; i++)
            {
                string[] text = File.ReadAllLines(file)[i].Split(";");

                for (int j = 0; j < text.Length; j++)
                {
                    Console.Write($"{text[j]} ");
                }
                Console.WriteLine();
            }
        }
        static void SplitThrough(int pow)
        {
            WorkFile.ClearFile("B.txt");
            WorkFile.ClearFile("C.txt");
            Console.WriteLine($"Раскидываем по файлам B и С через {Math.Pow(2,pow)}");

            int j = 0;

            for (int i = 0; i < (countRow - 1) / Math.Pow(2,pow); i++)
            {
                Thread.Sleep(delay);
                if (i % 2 == 1)
                {
                    for (int k = 0; k < Math.Pow(2, pow); k++)
                    {

                        string text = File.ReadAllLines("A.txt")[j];

                        using (StreamWriter sw = new StreamWriter("B.txt", true, System.Text.Encoding.Default))
                        {
                            Console.WriteLine($"{text.Split(";")[0]} в B");
                            sw.WriteLine($"{text}");
                        }
                        j++;
                    }
                }
                else
                {
                    for (int k = 0; k < Math.Pow(2, pow); k++)
                    {
                        string text = File.ReadAllLines("A.txt")[j];

                        using (StreamWriter sw = new StreamWriter("C.txt", true, System.Text.Encoding.Default))
                        {
                            Console.WriteLine($"{text.Split(";")[0]} в C");
                            sw.WriteLine($"{text}");
                        }
                        j++;
                    }
                }
            }
            Console.WriteLine();

        }

        static void Sort(int num)
        {
            WorkFile.ClearFile("A.txt");

            int i = 0;
            int j = 0;
            string elB = "";
            string elC = "";
            int limit = 0;
            int countLinesInB = File.ReadAllLines("B.txt").Length;
            int countLinesInC = File.ReadAllLines("C.txt").Length;

            Console.WriteLine($"Соединяем из файлов B и C в файл А при этом сортирием их по { num} элементов из каждого файла"); ;

            for (int k = 0; k < (countRow - 1) / Math.Pow(2,num); k++)
            {

                elB = "";
                elC = "";

                limit += Convert.ToInt32(Math.Pow(2, num-1));                  // (k+1)*num*2-1;

                for (int n = 0; n < Math.Pow(2, num); n++)
                {
                    Thread.Sleep(delay);

                    if (i < limit && j < limit)      //j <= 2 * (k + 1) -1) // туууууууууууут
                    {
                        if (i < countLinesInB)
                        {
                            elB = File.ReadAllLines("B.txt")[i];
                        }
                        if (j < countLinesInC)
                        {
                            elC = File.ReadAllLines("C.txt")[j];
                        }
                        Console.WriteLine($"Сравниваем " + elB.Split(';')[0] + " из файла B и " + elC.Split(';')[0] + " из файла C");

                        SelectSort(elB, elC, ref i, ref j);
                    }
                    else
                    {
                        if (i < limit)
                        {
                            elB = File.ReadAllLines("B.txt")[i];
                            WriteSort(elB, "B");
                            i++;
                        }
                        else if (j < limit)
                        {
                            elC = File.ReadAllLines("C.txt")[j];
                            WriteSort(elC, "C");
                            j++;
                        }
                    }

                }
            }
            Console.WriteLine();
        }
        static void SelectSort(string elB, string elC, ref int i, ref int j)
        {
            if (IsDigit && TypeSortUp)
            {
                if (Convert.ToInt32(elB.Split(";")[0]) < Convert.ToInt32(elC.Split(";")[0]))
                {
                    WriteSort(elB, "B");
                    i++;
                }
                else
                {
                    WriteSort(elC, "C");
                    j++;
                }
            }
            else if (IsDigit && !TypeSortUp)
            {
                if (Convert.ToInt32(elB.Split(";")[0]) > Convert.ToInt32(elC.Split(";")[0]))
                {
                    WriteSort(elB, "B");
                    i++;
                }
                else
                {
                    WriteSort(elC, "C");
                    j++;
                }
            }
            else if (!IsDigit && TypeSortUp)
            {
                if ((int)elB.Split(";")[0][0] < (int)elC.Split(";")[0][0])
                {
                    WriteSort(elB, "B");
                    i++;
                }
                else
                {
                    WriteSort(elC, "C");
                    j++;
                }
            }
            else if (!IsDigit && !TypeSortUp)
            {
                if ((int)elB.Split(";")[0][0] > (int)elC.Split(";")[0][0])
                {
                    WriteSort(elB, "B");
                    i++;
                }
                else
                {
                    WriteSort(elC, "C");
                    j++;
                }
            }
        }
        static void WriteSort(string FirstEl, string file)
        {
            using (StreamWriter sw = new StreamWriter("A.txt", true, System.Text.Encoding.Default))
            {
                Console.WriteLine($"Записываем {FirstEl.Split(";")[0]} в A из файла {file}");
                sw.WriteLine($"{FirstEl}");
            }
        }
        static void DrawNewTable(string file)
        {
            int[] arrBanRow = new int[Program.countRow];
            int countBan = 0;

            int countRowIn = 1;

            string textTable = File.ReadAllLines(file)[0];
            Console.WriteLine(textTable);

            while (countRowIn != countRow )
            {
                string[] text = File.ReadAllLines("A.txt")[countRowIn - 1].Split(";");

                for (int j = 0; j < countRow; j++)
                {
                    textTable = File.ReadAllLines(file)[j];
                    if (text[0] == textTable.Split(";")[numColumnSort] && !WorkFile.IsBanRow(arrBanRow, j))
                    {
                        arrBanRow[countBan] = j;
                        countBan++;
                        Console.WriteLine(textTable);
                        countRowIn++;
                        break;
                    }
                }
            }
        }
    }
}