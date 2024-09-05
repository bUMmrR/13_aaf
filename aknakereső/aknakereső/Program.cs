using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aknakereső
{
    internal class Program
    {
        static List<List<char>> t = new List<List<char>>(); 
        static void Main(string[] args)
        {
            tombFeltoltes(6, 10, 10);
            tombKiiras();
            korBekeres();

            Console.ReadLine();
        }

        private static void korBekeres()
        {
            Console.WriteLine("Add meg a a helyed (x,y kordináta szóközzel elválasztva)");
            string input = Console.ReadLine();
            int x = input.Split('');
        }

        private static void tombKiiras()
        {
            for (int i = 0; i < t.Count; i++)
            {
                for (int j = 0; j < t[0].Count; j++)
                {
                    Console.Write(t[i][j] +"\t");
                }
                Console.WriteLine();
            }
        }

        private static void tombFeltoltes(int length, int height, int chance)
        {
            Random rnd = new Random();
            for (int i = 0; i < length; i++)
            {
                List<char> segedList = new List<char>();
                for (int j = 0; j < height; j++)
                {
                    if (rnd.Next(0,chance) == 1)
                    {
                        segedList.Add('x');
                    }
                    else
                    {
                        segedList.Add('-');
                    }
                }
                t.Add(segedList);
            }
        }
    }
}
