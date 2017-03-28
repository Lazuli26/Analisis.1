using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Analisis.Proyecto1
{
    class Juego
    {
        public List<List<Item>> respuesta = new List<List<Item>>();
        public List<List<Item>> tablero = new List<List<Item>>();
        public List<Item> opciones = new List<Item>();
        int tamañoMatriz;

        public Juego(int tamaño)
        {
            Random rnd = new Random();
            for (int x = 0; x < tamaño; x++)
            {
                respuesta.Add(new List<Item>());
                for (int y = 0; y < tamaño; y++)
                {
                    respuesta[x].Add(new Item());
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
                    opciones.Add(respuesta[x][y]);
                }
            }

            Item aux;
            for (int i = 0; i < opciones.Count; i++)
            {
                int randint = rnd.Next(0, opciones.Count - 1);
                aux = opciones[i];
                opciones[i] = opciones[randint];
                opciones[randint] = aux;
            }

            for (int x = 0; x < tamaño; x++)
            {
                tablero.Add(new List<Item>());
                for (int y = 0; y < tamaño; y++)
                {
                    tablero[x].Add(new Item());                  
                }
            }
            tamañoMatriz = tamaño;
        }

        public void imprimir(List<List<Item>> matriz)
        {
            for (int fila = 0; fila < tamañoMatriz; fila++)
            {
                for (int pos = 0; pos < 3; pos++)
                {
                    WriteLine();
                    for (int col = 0; col < tamañoMatriz; col++)
                    {
                        if (pos == 0)
                            Write("  " + matriz[fila][col].arriba.ToString() + "  ");
                        if (pos == 1)
                            Write(" " + matriz[fila][col].izquierda.ToString() + " " + matriz[fila][col].derecha.ToString() + " ");
                        if (pos == 2)
                            Write("  " + matriz[fila][col].abajo.ToString() + "  ");
                    }
                }
            }
        }
        /*
        public List<List<Item>> clonar(List<List<Item>> target)
        {
            List<List<Item>> clon = new List<List<Item>>();
            for (int i = 0; i < target.Count; i++)
            {
                clon.Add(new List<Item>());
                for (int x = 0; x < target[i].Count; x++)
                {
                    clon[i].Add(target[i][x]);
                }
            }
            return clon;
        }

        public List<Item> clonar(List<Item> target)
        {
            List<Item> clon = new List<Item>();
            for (int x = 0; x < target.Count; x++)
            {
                clon.Add(target[x]);
            }
            return clon;
        }
        */
        public void descarte()
        {
            descarteRec(opciones,0,0, tablero);

            WriteLine("\nMatriz calculada...");
            imprimir(tablero);

            WriteLine("\n\nMatriz original...");
            imprimir(respuesta);
            ReadKey();
        }

        public bool descarteRec(List<Item> listaOpciones, int x, int y, List<List<Item>> tableRec)
        {
            for (int i = 0; i < listaOpciones.Count; i++)
            {
                bool xT = false;
                bool yT = false;
                if (x == 0)
                    xT = true;
                else if (tableRec[x - 1][y].abajo == listaOpciones[i].arriba)
                    xT = true;
                if (y == 0)
                    yT = true;
                else if (tableRec[x][y - 1].derecha == listaOpciones[i].izquierda)
                    yT = true;
                if (xT && yT)
                {
                    tableRec[x][y] = listaOpciones[i];
                    if (listaOpciones.Count == 1)
                    {
                        this.tablero = tableRec;
                        return true;
                    }
                    else
                    {
                        Item temp = listaOpciones[i];
                        listaOpciones.RemoveAt(i);
                        if (descarteRec(listaOpciones, x + (y / (tableRec[x].Count - 1)), (y + 1) % tableRec[x].Count, tableRec))
                        {
                            listaOpciones.Insert(i, temp);
                            return true;
                        }
                        listaOpciones.Insert(i, temp);
                    }
                }
            }
            return false;
        }
        public void fuerza()
        {
            fuerzaRec(opciones, 0, 0, tablero);
            WriteLine("\n\nMatriz original...");
            imprimir(respuesta);
            ReadKey();
        }

        public void fuerzaRec(List<Item> listaOpciones, int x, int y, List<List<Item>> tableRec)
        {
            if (listaOpciones.Count != 0)
            {
                for (int i = 0; i < listaOpciones.Count; i++)
                {
                    tableRec[x][y] = listaOpciones[i];
                    Item temp =listaOpciones[i];
                    listaOpciones.RemoveAt(i);
                    fuerzaRec(listaOpciones, x + (y / (tableRec[x].Count - 1)), (y + 1) % tableRec[x].Count, tableRec);
                    listaOpciones.Insert(i, temp);
                }
            }
            else
            {
                bool prueba = true;
                for (int i = 0; i < tableRec.Count && prueba; i++)
                {
                    for (int j = 0; j < tableRec[i].Count && prueba; j++)
                    {
                        if (i != 0)
                        {
                            if (tableRec[i - 1][j].abajo != tableRec[i][j].arriba)
                            {
                                prueba = false;
                            }
                        }
                        if (j != 0)
                        {
                            if (tableRec[i][j - 1].derecha != tableRec[i][j].izquierda)
                            {
                                prueba = false;
                            }
                        }
                    }
                }
                if (prueba)
                {
                    Console.WriteLine();
                    WriteLine("Se ha encontrado una coincidencia");
                    imprimir(tableRec);
                }
            }
        }

        public void tanteo()
        {

        }

        public void imprimirLista(List<Item> listaOpciones)
        {
            for (int i = 0; i < listaOpciones.Count; i++)
            {
                Write("  " + listaOpciones[i].arriba.ToString() + "  ");
            }
            WriteLine();
            for (int i = 0; i < listaOpciones.Count; i++)
            {
                Write(" " + listaOpciones[i].izquierda.ToString() + " " + listaOpciones[i].derecha.ToString() + " ");
            }
            WriteLine();

            for (int i = 0; i < listaOpciones.Count; i++)
            {
                Write("  " + listaOpciones[i].abajo.ToString() + "  ");
            }
        }
    }
}
