using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace aknakereső
{
    internal class Program
    {


        /*
         * TODO 
         * 
         * kiiratas rendesen
         * rekurziv ha uresre nyomod
         * 
         */


        static List<List<cella>> t = new List<List<cella>>(); 
        static bool gameOver = false;

        static void Main(string[] args)
        {
            Console.WriteLine("A táblázat hossza:");
            int length = int.Parse(Console.ReadLine());
            Console.WriteLine("A táblázat magassága:");
            int height = int.Parse(Console.ReadLine());
            Console.WriteLine("Egy mezőnek hány százalék esélye legyen hogy bomba legyen");
            int chance = int.Parse(Console.ReadLine());
            tombFeltoltes(length, height, chance);
            adjacentCalc();
            //tombKiirasCheat();
            Console.Clear();

            while (gameOver != true)
            {
                Console.Clear();
                tombKiirasCheat();
                tombKiiras();
                kor();
            }



            Console.ReadLine();
        }

        private static void adjacentCalc()
        {
            for (int i = 0; i < t.Count; i++)
            {
                for (int j = 0; j < t[0].Count; j++)
                {
                    if (!t[i][j].bomba)
                    {
                        t[i][j].adjecent = CountAdjacentBombs(i, j);
                    }
                }
            }
        }

        private static int CountAdjacentBombs(int x, int y)
        {
            int bombaCount = 0;

            for (int i = x-1; i <= x+1; i++)
            {
                for (int j = y-1; j <= y+1; j++)
                {
                    if (i == x && j == y)
                    {
                        continue;
                    }

                    if (IsInBounds(i, j))
                    {
                        if (t[i][j].bomba)
                        {
                            bombaCount++;
                        }
                    }
                }
            }

            return bombaCount;
        }


        private static bool IsInBounds(int row, int col)
        {
            return row >= 0 && col >= 0 && row < t.Count && col < t[0].Count ;
        }

        private static void kor()
        {
            var (y,x) = lepesBekeres();

            if (bombaE(y, x))
            {
                Console.WriteLine("Vesztettél, gg");
                Console.ReadLine();
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("nem bomba");
                Reveal(y, x);
            }

            if (nyert())
            {
                Console.Clear();
                tombKiiras();
                Console.WriteLine("Gratulálok, nyertél!");
                Console.ReadLine();
                Environment.Exit(0);
            }
        }


        private static bool nyert()
        {
            for (int i = 0; i < t.Count; i++)
            {
                for (int j = 0; j < t[0].Count; j++)
                {
                    if (!t[i][j].bomba && !t[i][j].lathato)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static (int, int) lepesBekeres()
        {
            Console.WriteLine("Add meg a helyed (x, y koordináta szóközzel elválasztva):");
            string input = Console.ReadLine();
            int x = 0, y = 0;
            try
            {
                x = int.Parse(input.Split(' ')[0]) - 1;
                y = int.Parse(input.Split(' ')[1]) - 1;
            }
            catch
            {
                Console.WriteLine("Hibás bemenet, próbáld újra!");
            }
            return (x, y);
        }


        private static bool bombaE(int y, int x)
        {
            try
            {
                if (t[y][x].bomba == true)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }


        private static void Reveal(int y, int x)
        {
            if (!IsInBounds(y, x)) return;

            if (t[y][x].lathato || t[y][x].bomba) return;

            t[y][x].lathato = true;

            if (t[y][x].adjecent > 0) return;

            for (int i = y - 1; i <= y + 1; i++)
            {
                for (int j = x - 1; j <= x + 1; j++)
                {
                    if (i == y && j == x) continue;

                    Reveal(i, j);
                }
            }
        }


        private static void tombKiiras()
        {
            Console.WriteLine("\nA tábla jelen állapotában:\n");

            Console.Write("   ");
            for (int col = 0; col < t[0].Count; col++)
            {
                Console.Write($"{col + 1}  ");
            }
            Console.WriteLine();

            for (int i = 0; i < t.Count; i++)
            {
                Console.Write($"{i + 1} ".PadLeft(3));

                for (int j = 0; j < t[0].Count; j++)
                {
                    if (t[i][j].lathato)
                    {
                        if (t[i][j].bomba)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("B  ");
                        }
                        else
                        {
                            switch (t[i][j].adjecent)
                            {
                                case 1:
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    break;
                                case 2:
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    break;
                                case 3:
                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    break;
                                case 4:
                                    Console.ForegroundColor = ConsoleColor.Magenta;
                                    break;
                                case 5:
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    break;
                                default:
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    break;
                            }
                            Console.Write($"{t[i][j].adjecent}  ");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("X  ");
                    }
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }



        private static void tombKiirasCheat()
        {
            for (int i = 0; i < t.Count; i++)
            {
                for (int j = 0; j < t[0].Count; j++)
                {
                    if (t[i][j].bomba)
                    {
                        Console.Write("x \t");
                    }
                    else
                    {
                        Console.Write(t[i][j].adjecent +"\t");
                    }
                }
                Console.WriteLine();
            }
        }

        private static void tombFeltoltes(int length, int height, int chance)
        {
            Random rnd = new Random();
            for (int i = 0; i < length; i++)
            {
                List<cella> segedList = new List<cella>();
                for (int j = 0; j < height; j++)
                {
                    int randomNumber = rnd.Next(0, 101);
                    if (randomNumber <= chance)
                    {
                        cella temp = new cella(i,j,true);
                        segedList.Add(temp);
                    }
                    else
                    {
                        cella temp = new cella(i, j, false);
                        segedList.Add(temp);
                    }
                }
                t.Add(segedList);
            }
        }
    }
}
