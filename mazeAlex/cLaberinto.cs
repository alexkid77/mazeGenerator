using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace mazeAlex
{
    class cPunto
    {
        public int r { get; set; }
        public int c { get; set; }
        public cPunto(int r, int c)
        {
            this.r = r;
            this.c = c;
        }

    }
    class cLaberinto
    {
        int height = 0;
        int width = 0;
        int[,] maze;
        cPunto entrada;
        public cLaberinto(int N)
        {
            this.height = N;
            this.width = N;
            maze = generateMaze();
            this.maze[1, 0] = 0;
            this.maze[height - 2, width - 1] = 0;
            this.entrada = new cPunto(1, 0);
        }
        public void resolver()
        {
            Stack<cPunto> pila = new Stack<cPunto>();
            this.maze[entrada.r, entrada.c] = 2;
            pila.Push(this.entrada);
            while (pila.Count > 0)
            {
                cPunto punto = pila.Pop();
                if ((punto.r == this.height - 2) && (punto.c == this.width - 1))
                    break;
                  
                List<cPunto> ll = getCaminos(punto);
                if (ll.Count > 0)
                {
                    pila.Push(punto);
                    cPunto hh = new cPunto(ll[0].r, ll[0].c);
                    pila.Push(hh);
                    this.maze[hh.r, hh.c] = 2;
                }
                else
                {
                    this.maze[punto.r, punto.c] = 3;
                }

                this.dibujar();
                Thread.Sleep(50);
            }


        }

        public List<cPunto> getCaminos(cPunto p)
        {
            List<cPunto> res = new List<cPunto>();

            try
            {
                if (this.maze[p.r + 1, p.c] == 0)
                    res.Add(new cPunto(p.r + 1, p.c));
            }
            catch
            {
            }

            try
            {
                if (this.maze[p.r - 1, p.c] == 0)
                    res.Add(new cPunto(p.r - 1, p.c));
            }
            catch
            {
            }

            try
            {
                if (this.maze[p.r, p.c + 1] == 0)
                    res.Add(new cPunto(p.r, p.c + 1));
            }
            catch
            {
            }


            try
            {
                if (this.maze[p.r, p.c - 1] == 0)
                    res.Add(new cPunto(p.r, p.c - 1));
            }
            catch
            {
            }
            return res;
        }

        public int[,] generateMaze()
        {
            this.maze = new int[height, width];
            // Initialize
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    this.maze[i, j] = 1;

            Random rand = new Random();
            // r for row、c for column
            // Generate random r
            int r = rand.Next(height);
            while (r % 2 == 0)
            {
                r = rand.Next(height);
            }
            // Generate random c
            int c = rand.Next(width);
            while (c % 2 == 0)
            {
                c = rand.Next(width);
            }
            // Starting cell
            this.maze[r, c] = 0;

            //　Allocate the maze with recursive method
            recursion(r, c);

            return maze;
        }


        public void recursion(int r, int c)
        {
            // 4 random directions
            int[] randDirs = generateRandomDirections();
            // Examine each direction
            for (int i = 0; i < randDirs.Length; i++)
            {

                switch (randDirs[i])
                {
                    case 1: // Up
                            //　Whether 2 cells up is out or not
                        if (r - 2 <= 0)
                            continue;
                        if (maze[r - 2, c] != 0)
                        {
                            maze[r - 2, c] = 0;
                            maze[r - 1, c] = 0;
                            recursion(r - 2, c);
                        }
                        break;
                    case 2: // Right
                            // Whether 2 cells to the right is out or not
                        if (c + 2 >= width - 1)
                            continue;
                        if (maze[r, c + 2] != 0)
                        {
                            maze[r, c + 2] = 0;
                            maze[r, c + 1] = 0;
                            recursion(r, c + 2);
                        }
                        break;
                    case 3: // Down
                            // Whether 2 cells down is out or not
                        if (r + 2 >= height - 1)
                            continue;
                        if (maze[r + 2, c] != 0)
                        {
                            maze[r + 2, c] = 0;
                            maze[r + 1, c] = 0;
                            recursion(r + 2, c);
                        }
                        break;
                    case 4: // Left
                            // Whether 2 cells to the left is out or not
                        if (c - 2 <= 0)
                            continue;
                        if (maze[r, c - 2] != 0)
                        {
                            maze[r, c - 2] = 0;
                            maze[r, c - 1] = 0;
                            recursion(r, c - 2);
                        }
                        break;
                }
            }
        }

        public int[] generateRandomDirections()
        {
            List<int> randoms = new List<int>();
            for (int i = 0; i < 4; i++)
                randoms.Add(i + 1);
            randoms.Shuffle();

            return randoms.ToArray();
        }

        public void dibujar()
        {
            Console.Clear();
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (this.maze[i, j] == 0)
                    {

                        Console.Write(" ");

                        //  numeroVecinos(i, j);
                    }
                    else if (this.maze[i, j] == 2)
                        Console.Write(".");
                    else if (this.maze[i, j] == 3)
                        Console.Write(" ");
                    else
                        Console.Write("*");
                }
                Console.Write("\n");
            }


        }
        /*  void numeroVecinos(int i, int j)
          {
            //  int c = this.maze[i, j] + this.maze[i, j - 1] + this.maze[i, j - 1] + this.maze[i - 1, j] + this.maze[i + 1, j];


              if (this.maze[i, j - 1] == 0 && this.maze[i, j + 1] == 0)
                  Console.Write("|");
              else if ((this.maze[i - 1, j] == 1 && this.maze[i, j - 1] == 1) || (this.maze[i, j + 1] == 1 && this.maze[i + 1, j] == 1) || (this.maze[i, j - 1] == 1 && this.maze[i + 1, j] == 1) || (this.maze[i - 1, j] == 1 && this.maze[i, j + 1] == 1))
                  Console.Write("+");
              else
                  Console.Write("-");




          }*/
    }

    static class MyExtensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
    public static class ThreadSafeRandom
    {
        [ThreadStatic]
        private static Random Local;

        public static Random ThisThreadsRandom
        {
            get { return Local ?? (Local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
        }
    }
}
