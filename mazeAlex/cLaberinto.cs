using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace mazeAlex
{
    class cLaberinto
    {
        int height = 0;
        int width = 0;
        int[,] maze;
        public cLaberinto(int N)
        {
            this.height = N;
            this.width = N;
            maze = generateMaze();
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
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (this.maze[i, j] == 1)
                    {
                        if (i == 0 || i == height - 1)
                            Console.Write("=");
                        else if (j == 0 || j == width - 1)
                            Console.Write("|");
                        else
                            numeroVecinos(i, j);
                    }
                    else
                        Console.Write(" ");
                }
                Console.Write("\n");
            }


        }
        void numeroVecinos(int i, int j)
        {
            int c = this.maze[i, j] + this.maze[i, j - 1] + this.maze[i, j - 1] + this.maze[i - 1, j] + this.maze[i + 1, j];


            if (this.maze[i, j - 1] == 0 && this.maze[i, j+1] == 0)
                Console.Write("|");
           else if((this.maze[i-1, j] == 1 && this.maze[i, j - 1]==1)|| (this.maze[i , j+1] == 1 && this.maze[i+1, j ] == 1) || (this.maze[i, j - 1] == 1 && this.maze[i + 1, j] == 1) || (this.maze[i-1, j ] == 1 && this.maze[i, j+1] == 1)) 
                Console.Write("+");
            else
                Console.Write("-");




        }
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
