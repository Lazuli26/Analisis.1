using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Analisis.Proyecto1
{
    class Program
    {
        static void Main(string[] args)
        {
            var juego = new Juego(6);
            juego.tanteo();
        }
    }
}
