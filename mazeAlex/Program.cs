using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Drawing;

namespace mazeAlex
{
    class Program
    {
       
        static void Main(string[] args)
        {
            cLaberinto lab = new cLaberinto(19);
       
            lab.resolver();
            lab.dibujar();
            Console.WriteLine("Laberinto resuelto");
            Console.ReadKey();
        }
    }
}
