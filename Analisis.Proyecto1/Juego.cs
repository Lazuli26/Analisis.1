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
                        respuesta[x][y].arriba = rnd.Next(0, 10);
                    else
                        respuesta[x][y].arriba = respuesta[x - 1][y].abajo;
                    if (y == 0)
                        respuesta[x][y].izquierda = rnd.Next(0, 10);
                    else
                        respuesta[x][y].izquierda = respuesta[x][y - 1].derecha;
                    respuesta[x][y].abajo = rnd.Next(0, 10);
                    respuesta[x][y].derecha = rnd.Next(0, 10);
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
            WriteLine("");
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
            List<List<List<int>>> contenedor = new List<List<List<int>>>();
            imprimirLista(opciones);

            for (int i = 0; i < 10; i++)
            {
                contenedor.Add(new List<List<int>>());
                for (int j = 0; j < 4; j++)
                {
                    contenedor[i].Add(new List<int>());
                }
            }

            //insercion a la matriz 3d
            for (int i = 0; i < opciones.Count; i++)
            {
                contenedor[opciones[i].arriba][0].Add(i);
                contenedor[opciones[i].abajo][1].Add(i);
                contenedor[opciones[i].izquierda][2].Add(i);
                contenedor[opciones[i].derecha][3].Add(i);
            }

            WriteLine();
            imprimir3D(contenedor);

            //creacion de grupos
            List<List<int>> grupos = new List<List<int>>();
            for (int i = 0; i < 5; i++)
            {
                grupos.Add(new List<int>());
            }

            //grupos: arriba, abajo, izq, der, comunes

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (contenedor[i][j*2].Count == 0 && contenedor[i][(j*2)+1].Count != 0)
                    {
                        for (int k = 0; k < contenedor[i][(j * 2) + 1].Count; k++)
                        {
                            grupos[(j * 2) + 1].Add(contenedor[i][(j * 2) + 1][k]);
                        }
                    }
                    else if (contenedor[i][j * 2].Count != 0 && contenedor[i][(j * 2) + 1].Count == 0)
                    {
                        for (int k = 0; k < contenedor[i][j * 2].Count; k++)
                        {
                            grupos[j * 2].Add(contenedor[i][j * 2][k]);
                        }
                    }
                    else
                    {
                        for (int k = 0; k < contenedor[i][(j * 2) + 1].Count; k++)
                        {
                            if(!grupos[4].Contains(contenedor[i][(j * 2) + 1][k]))
                                grupos[4].Add(contenedor[i][(j * 2) + 1][k]);
                        }
                        for (int k = 0; k < contenedor[i][j * 2].Count; k++)
                        {
                            if (!grupos[4].Contains(contenedor[i][j * 2][k]))
                                grupos[4].Add(contenedor[i][j * 2][k]);
                        }
                    }
                }
            }

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < grupos[i].Count; j++)
                {
                    Write(grupos[i][j]+", ");
                }
                WriteLine();
            }
            ReadKey();

        }

        void imprimir3D(List<List<List<int>>> contenedor)
        {
            for (int i = 0; i < contenedor.Count; i++)
            {
                Write(i+ ": \t");
                for (int j = 0; j < contenedor[i].Count; j++)
                {
                    for (int k = 0; k < contenedor[i][j].Count; k++)
                    {
                        Write(contenedor[i][j][k]+",");
                    }
                    Write("\t\t|");
                }
                WriteLine();
            }
            
        }

        public bool tanteoRec()
        {
            

            return false;
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
            WriteLine();
        }
    }
}
