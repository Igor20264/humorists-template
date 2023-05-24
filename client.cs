
using System;
using System.Collections.Generic;

class Program
{
    static Random random = new Random();

    static (string, (int, int, int, int)) Gen(int complexity)
    {
        int MaxInt = 100;

        byte[] randomChar(List<char> listChar, int count)
        {
            List<char> r = new List<char>();
            for (int i = 0; i <= count; i++)
            {
                r.Add(listChar[random.Next(0, listChar.Count)]);
            }
            return new string(r.ToArray());
        }

        (string, int) Line(List<char> listChar, int countInt)
        {
            List<int> Int = new List<int>();
            for (int i = 0; i < countInt - 1; i++)
            {
                Int.Add(RandomInt(1, MaxInt));
            }

            string chars = randomChar(listChar, countInt - 1);
            string strg = "";
            for (int i = 0; i < chars.Length; i++)
            {
                strg += $"{Int[i]}{chars[i]}";
            }
            strg += $"{Int[Int.Count - 1]}";

            int gs = Evaluate(strg);
            if (0 < gs && gs < MaxInt && AllCharsCount(strg, listChar))
            {
                return (strg, gs);
            }
            else
            {
                return Line(listChar, countInt);
            }
        }

        bool AllCharsCount(string strg, List<char> listChar)
        {
            foreach (char c in listChar)
            {
                if (CountOccurrences(strg, c) == 0)
                    return false;
            }
            return true;
        }

        int CountOccurrences(string str, char c)
        {
            int count = 0;
            foreach (char ch in str)
            {
                if (ch == c)
                    count++;
            }
            return count;
        }

        int Evaluate(string strg)
        {
            return (int)new System.Data.DataTable().Compute(strg, null);
        }

        int RandomInt(int min, int max)
        {
            return random.Next(min, max);
        }

        // выбор сложности.
        List<char> listChar = new List<char>() { '-', '+' };
        int countInt = 3;

        if (complexity == 0)
        {
            countInt = 3;
        }
        else if (complexity == 1)
        {
            countInt = 3;
            listChar.Add('*');
            MaxInt = 200;
        }
        else if (complexity == 2)
        {
            countInt = 4;
            listChar.Add('*');
            MaxInt = 350;
        }
        else if (complexity == 3)
        {
            countInt = 4;
            listChar.Add('*');
            MaxInt = 500;
        }

        (string, int) primer = Line(listChar, countInt);

        (int, int, int, int) false_ether(int true_otvet)
        {
            int fls0 = true_otvet + RandomInt(-50, 50);
            double s = RandomDouble();
            int fls1 = (int)Math.Round(true_otvet * s);
            int fls2 = (int)Math.Round(true_otvet / 1.5) - RandomInt(0, 10);
            return (fls0, fls1, fls2, true_otvet);
        }

        (int, int, int, int) l = false_ether(primer.Item2);
        return (primer.Item1, (l.Item1, l.Item2, l.Item3, l.Item4));
    }

    static double RandomDouble()
    {
        return random.NextDouble();
    }

    static void Main()
    {
        Console.WriteLine(Gen(0));
        Console.WriteLine(Gen(1));
        Console.WriteLine(Gen(2));
        Console.WriteLine(Gen(3));
    }
}
