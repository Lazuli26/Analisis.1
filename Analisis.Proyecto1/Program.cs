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
        static int tamano=7;
        static List<List<Item>> respuesta = new List<List<Item>>();
        static void Main(string[] args)
        {
            Random rnd = new Random();
            for (int x = 0; x < tamano; x++)
            {
                respuesta.Add(new List<Item>());
                for (int y = 0; y < tamano; y++)
                {
                    respuesta[x].Add (new Item());
                    if (x == 0)
                        respuesta[x][y].arriba = rnd.Next(1, 9);
                    else
                        respuesta[x][y].arriba = respuesta[x - 1][y].abajo;
                    if (y == 0)
                        respuesta[x][y].izquierda = rnd.Next(1, 9);
                    else
                        respuesta[x][y].izquierda = respuesta[x][y - 1].derecha;
                    respuesta[x][y].abajo = rnd.Next(1, 9);
                    respuesta[x][y].derecha = rnd.Next(1, 9);
                }
            }
            for (int fila = 0; fila < tamano; fila++)
            {
                for (int pos = 0; pos < 3; pos++)
                {
                    WriteLine();
                    for (int col = 0; col < tamano; col++)
                    {
                        if (pos == 0)
                            Write("  " + respuesta[fila][col].arriba.ToString() + "  ");
                        if (pos == 1)
                            Write(" "+respuesta[fila][col].izquierda.ToString() + " " + respuesta[fila][col].derecha.ToString()+" ");
                        if (pos == 2)
                            Write("  " + respuesta[fila][col].abajo.ToString() + "  ");
                    }
                }
            }
            ReadKey();
        }
    }
}
