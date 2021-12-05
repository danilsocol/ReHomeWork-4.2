using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace ReHomeWork_4._2
{
    class WorkFile
    {
        public static void ReadData(string file)
        {
            Console.WriteLine("\n\nВведите название столбца по которому будете сортировать");
            string nameColumnSort = Console.ReadLine();
            Console.WriteLine();

            string[] text = File.ReadAllLines(file)[0].Split(";");
            for (int i = 0; i < text.Length; i++)
            {
                if (nameColumnSort == text[i])
                {
                    Program.numColumnSort = i;
                }
            }

            Console.WriteLine("Если хотите сортировать по возрастанию напишите UP, по убыванию DOWN ");
            string TypeSort = Console.ReadLine();
            Console.WriteLine();

            if (TypeSort == "UP")
            {
                Program.TypeSortUp = true;
            }
            else if (TypeSort == "DOWN")
            {
                Program.TypeSortUp = false;
            }

            Console.WriteLine("Введите задержку в милисекундах ");
            Program.delay = Convert.ToInt32(Console.ReadLine());
        }

        public static void SaveData(string file)
        {
            ClearFile("A.txt");

            using (StreamWriter sw = new StreamWriter("A.txt", true, System.Text.Encoding.Default))
            {
                Console.WriteLine("Записываем выбранный столбец в файл A");
                for (int i = 1; i < Program.countRow; i++)
                {
                    string[] text = File.ReadAllLines(file)[i].Split(";");
                    Console.Write($"{text[Program.numColumnSort]} ");
                    sw.WriteLine($"{text[Program.numColumnSort]};{i}");
                }
                Console.WriteLine();

                string[] type = File.ReadAllLines(file)[1].Split(";");
                Program.IsDigit = type[Program.numColumnSort].Length == type[Program.numColumnSort].Where(c => char.IsDigit(c)).Count();

                Console.WriteLine();
            }
        }
        public static void ClearFile(string file)
        {
            File.Delete(file);
            var myFile = File.Create(file);
            myFile.Close();
        }
        public static void SaveNewTable(string file)
        {
            ClearFile("NewTable.txt");

            using (StreamWriter sw = new StreamWriter("NewTable.txt", true, System.Text.Encoding.Default))
            {
                int[] arrBanRow = new int[Program.countRow];
                int countBan = 0;

                int countRowIn = 1;

                string textTable = File.ReadAllLines(file)[0];
                sw.WriteLine(textTable);

                while (countRowIn != Program.countRow )
                {
                    string[] text = File.ReadAllLines("A.txt")[countRowIn - 1].Split(";");

                    for (int j = 0; j < Program.countRow; j++)
                    {
                        textTable = File.ReadAllLines(file)[j];
                        if (text[0] == textTable.Split(";")[Program.numColumnSort] && !IsBanRow(arrBanRow,j))
                        {
                            arrBanRow[countBan] = j;
                            countBan++;
                            sw.WriteLine(textTable);
                            countRowIn++;
                            break;
                        }
                    }
                }
            }
        }

        public static bool IsBanRow(int[] arrBanRow, int numRow)
        {
            for (int i = 0; i < arrBanRow.Length; i++)
            {
                if (numRow == arrBanRow[i])
                    return true;
            }
            return false;
        }
    }
}
