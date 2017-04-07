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
        static Juego juego;
        static void secureFuerza(int opc)
        {
            if (opc > 3)
            {
                WriteLine("El tamaño de la matriz es demasiado grande para fuerza bruta, probar fuerza bruta? s/n");
                string cont = ReadKey().KeyChar.ToString();
                while (cont != "s" && cont != "n")
                {
                    cont = ReadKey().KeyChar.ToString();
                }
                if (cont == "s")
                {
                    juego.fuerza();
                    ReadKey();
                }
            }
        }
        static void Main(string[] args)
        {
            int opc;
            while (true)
            {
                Write("Orden de la matriz: ");
                if (int.TryParse(ReadLine(), out opc))
                {
                    juego = new Juego(opc);

                    WriteLine("\n\n-----------------------------------------------------");
                    WriteLine("\nLISTA DE OPCIONES EN EL MEJOR CASO: ordenado...");
                    juego.ordenarLista();
                    secureFuerza(opc);
                    juego.ordenarLista();
                    juego.descarte();
                    ReadKey();
                    juego.ordenarLista();
                    juego.tanteo();
                    ReadKey();

                    WriteLine("\n\n-----------------------------------------------------");
                    WriteLine("\nLISTA DE OPCIONES EN EL PEOR CASO: inverso...");
                    juego.invertirLista();
                    secureFuerza(opc);
                    juego.invertirLista();
                    juego.descarte();
                    ReadKey();
                    juego.invertirLista();
                    juego.tanteo();
                    ReadKey();

                    WriteLine("\n\n-----------------------------------------------------");
                    WriteLine("\nLISTA DE OPCIONES EN EL CASO PROMEDIO: desordenado...");
                    juego.desordenarLista();
                    secureFuerza(opc);
                    juego.desordenarLista();
                    juego.descarte();
                    ReadKey();
                    juego.desordenarLista();
                    juego.tanteo();
                    ReadKey();

                }
            }
        }
    }
}
