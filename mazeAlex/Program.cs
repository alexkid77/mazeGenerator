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

            lab.dibujar();
            lab.resolver();
            
           
            Console.WriteLine("Laberinto resuelto");
            Console.ReadKey();
        }
    }
}
