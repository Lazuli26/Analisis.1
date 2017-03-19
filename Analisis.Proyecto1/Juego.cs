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

        public void descarte()
        {
            descarteRec(opciones.ToList(),0,0,0,tablero.ToList());
            WriteLine("\n\nMatriz originaal...");
            imprimir(respuesta);

            WriteLine("\n\nMatriz calculada...");
            imprimir(tablero);
            ReadKey();
        }

        public bool descarteRec(List<Item> listaOpciones, int x, int y, int recursividad, List<List<Item>> tableRec)
        {
            
            if (listaOpciones.Count != 0)
            {
                bool correcto = false;
                for (int i = 0; i < listaOpciones.Count; i++)
                {
                    tableRec[x][y] = listaOpciones[i];
                    

                    if (x == 0)
                    {
                        correcto = true;
                    }
                    else
                    {
                        if (tableRec[x - 1][y].abajo == listaOpciones[i].arriba)
                        {
                            correcto = true;
                        }
                    }

                    if (y != 0 && correcto)
                    {
                        if (tableRec[x][y-1].derecha != listaOpciones[i].izquierda)
                        {
                            correcto = false;
                        }
                    }

                    if (correcto)
                    {

                        List<Item> listaInf = listaOpciones.ToList();
                        listaInf.RemoveAt(i);

                        if (y == tamañoMatriz - 1)
                        {
                            y = 0;
                            if (descarteRec(listaInf, x+1, y,recursividad+1, tableRec.ToList()))
                                return true;
                        }
                        else
                        {
                            if (descarteRec(listaInf,x,y+1,recursividad+1, tableRec.ToList()))
                                return true;
                        }
                    }
                }
                return false;
            }
            else
            {
                tablero = tableRec;

                WriteLine("\n\nMatriz tablero...");
                imprimir(tablero);
                return true;
            }
        }



        public void fuerzaBruta()
        {
            fuerzaBrutaRec(opciones, 0,0);
        }

        public void fuerzaBrutaRec(List<Item> listaOpciones, int x, int y)
        {

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
