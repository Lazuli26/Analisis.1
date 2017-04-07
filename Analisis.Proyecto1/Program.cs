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
            int opc;
            while (true)
            {
                Write("Orden de la matriz: ");
                if (int.TryParse(ReadLine(), out opc))
                {
                    //Juego juego = new Juego(opc);
                    Juego juego = new Juego(new Fijas().llenarMatriz2(), 3);

                    WriteLine("\n\n-----------------------------------------------------");
                    WriteLine("\nLISTA DE OPCIONES EN EL CASO PROMEDIO: desordenado...");
                    juego.desordenarLista();
                    juego.fuerza();
                    juego.descarte();
                    juego.tanteo();
                    ReadKey();

                    WriteLine("\n\n-----------------------------------------------------");
                    WriteLine("\nLISTA DE OPCIONES EN EL PEOR CASO: inverso...");
                    juego.invertirLista();
                    juego.fuerza();
                    juego.descarte();
                    juego.tanteo();
                    ReadKey();

                    WriteLine("\n\n-----------------------------------------------------");
                    WriteLine("\nLISTA DE OPCIONES EN EL MEJOR CASO: ordenado...");
                    juego.ordenarLista();
                    juego.fuerza();
                    juego.descarte();
                    juego.tanteo();
                    ReadKey();
                }
            }
        }
    }
}
