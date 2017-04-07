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
        int tamañoMatriz, asig, comp;
        Random rnd = new Random();

        public Juego(int tamaño)
        {
            tamañoMatriz = tamaño;
            crearMatriz(respuesta);

            for (int x = 0; x < tamaño; x++)
            {
                for (int y = 0; y < tamaño; y++)
                {
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
            crearMatriz(tablero);

            WriteLine("\n\nMatriz original...");
            imprimir(respuesta);
        }

        public Juego(List<Item> lista, int tamaño)
        {
            tamañoMatriz = tamaño;
            this.opciones = lista;
            crearMatriz(respuesta);

            for (int x = 0; x < tamañoMatriz; x++)
            {
                for (int y = 0; y < tamañoMatriz; y++)
                {
                    respuesta[x][y] = lista[y + (x * tamaño)];
                }
            }

            crearMatriz(tablero);

            WriteLine("\n\nMatriz original...");
            imprimir(respuesta);

            WriteLine("\nLista original...");
            imprimirLista(opciones);
        }
        public void crearMatriz(List<List<Item>> matriz)
        {
            for (int x = 0; x < tamañoMatriz; x++)
            {
                matriz.Add(new List<Item>());
                for (int y = 0; y < tamañoMatriz; y++)
                {
                    matriz[x].Add(new Item());          
                }
            }
        }
        public void desordenarLista()
        {
            Item aux;
            for (int i = 0; i < opciones.Count; i++)
            {
                int randint = rnd.Next(0, opciones.Count - 1);
                aux = opciones[i];
                opciones[i] = opciones[randint];
                opciones[randint] = aux;
            }
        }
        public void ordenarLista()
        {
            int i=0;
            for (int x = 0; x < tamañoMatriz; x++)
            {
                for (int y = 0; y < tamañoMatriz; y++)
                {
                    opciones[i] = respuesta[x][y];
                    i++;
                }
            }
        }
        public void invertirLista()
        {
            ordenarLista();
            opciones.Reverse();       
        }
//------------------------------------------------------------------------------------------------------------------       
        public void descarte()
        {
            asig = 0;
            comp = 0;
            descarteRec(opciones, 0, 0, tablero);

            WriteLine("____________________________");
            WriteLine("\nALGORITMO POR DESCARTE...");
            WriteLine("\nMatriz calculada...");
            imprimir(tablero);

            WriteLine();
            WriteLine("Comparaciones: " + comp + "\nAsignaciones: " + asig);
        }
        public bool descarteRec(List<Item> listaOpciones, int x, int y, List<List<Item>> tableRec)
        {
            for (int i = 0; i < listaOpciones.Count; i++)
            {
                comp++;
                bool xT = false;
                bool yT = false;
                asig += 2;
                if (x == 0)
                {
                    xT = true;
                    asig++;
                }
                else
                {
                    if (tableRec[x - 1][y].abajo == listaOpciones[i].arriba)
                    {
                        xT = true;
                        asig++;
                    }
                    comp++;
                } 
                comp++;
                if (y == 0)
                {
                    yT = true;
                    asig++;
                }
                else
                {
                    if (tableRec[x][y - 1].derecha == listaOpciones[i].izquierda)
                    {
                        yT = true;
                        asig++;
                        comp++;
                    }
                    comp++;
                }
                
                comp++;
                if (xT && yT)
                {
                    tableRec[x][y] = listaOpciones[i];
                    asig++;
                    if (listaOpciones.Count == 1)
                    {
                        return true;
                    }
                    else
                    {
                        Item temp = listaOpciones[i];
                        asig++;
                        listaOpciones.RemoveAt(i);
                        if (descarteRec(listaOpciones, x + (y / (tableRec[x].Count - 1)), (y + 1) % tableRec[x].Count, tableRec))
                        {
                            asig++;
                            listaOpciones.Insert(i, temp);
                            return true;
                        }
                        asig++;
                        listaOpciones.Insert(i, temp);
                    }
                    comp++;
                }
                comp++;
                asig++;
            }
            comp++;
            return false;
        }
//------------------------------------------------------------------------------------------------------------------       
        public void fuerza()
        {
            asig = 0;
            comp = 0;

            WriteLine("____________________________");
            WriteLine("\nALGORITMO POR FUERZA BRUTA...");
            fuerzaRec(opciones, 0, 0, tablero);

            WriteLine();
            WriteLine("Comparaciones: " + comp + "\nAsignaciones: " + asig);
        }
        public void fuerzaRec(List<Item> listaOpciones, int x, int y, List<List<Item>> tableRec)
        {
            comp++;
            if (listaOpciones.Count != 0)
            {
                for (int i = 0; i < listaOpciones.Count; i++)
                {
                    comp++;
                    tableRec[x][y] = listaOpciones[i];
                    Item temp =listaOpciones[i];
                    asig += 2;
                    listaOpciones.RemoveAt(i);
                    fuerzaRec(listaOpciones, x + (y / (tableRec[x].Count - 1)), (y + 1) % tableRec[x].Count, tableRec);
                    listaOpciones.Insert(i, temp);
                }
                comp++;
            }
            else
            {
                bool prueba = true;
                asig++;
                for (int i = 0; i < tableRec.Count && prueba; i++)
                {
                    comp++;
                    for (int j = 0; j < tableRec[i].Count && prueba; j++)
                    {
                        comp++;
                        comp++;
                        if (i != 0)
                        {
                            comp++;
                            if (tableRec[i - 1][j].abajo != tableRec[i][j].arriba)
                            {
                                prueba = false;
                                asig++;
                            }
                        }
                        comp++;
                        if (j != 0)
                        {
                            comp++;
                            if (tableRec[i][j - 1].derecha != tableRec[i][j].izquierda)
                            {
                                prueba = false;
                                asig++;
                            }
                        }
                    }
                    comp++;
                }
                comp++;
                if (prueba)
                {
                    Console.WriteLine();
                    WriteLine("Se ha encontrado una coincidencia");
                    imprimir(tableRec);
                }
                comp++;
            }
        }
//------------------------------------------------------------------------------------------------------------------       
        public void tanteo()
        {
            List<List<List<List<int>>>> contenedor = new List<List<List<List<int>>>>();
            asig = 0; comp = 0;

            for (int i = 0; i < 10; i++)
            {
                contenedor.Add(new List<List<List<int>>>());
                for (int j = 0; j < 4; j++)
                {
                    contenedor[i].Add(new List<List<int>>());
                    for (int k = 0; k < 2; k++)
                    {
                        contenedor[i][j].Add(new List<int>());

                    }
                    contenedor[i][j][1].Add(0);
                }
            }

            //insercion a la matriz 3d
            for (int i = 0; i < opciones.Count; i++)
            {
                contenedor[opciones[i].arriba][0][0].Add(i);
                contenedor[opciones[i].abajo][1][0].Add(i);
                contenedor[opciones[i].izquierda][2][0].Add(i);
                contenedor[opciones[i].derecha][3][0].Add(i);
                contenedor[opciones[i].arriba][0][1][0]++;
                contenedor[opciones[i].abajo][1][1][0]++;
                contenedor[opciones[i].izquierda][2][1][0]++;
                contenedor[opciones[i].derecha][3][1][0]++;
            }

            WriteLine();
            //imprimir3D(contenedor);

            //creacion de grupos
            List<List<int>> grupos = new List<List<int>>();
            for (int i = 0; i < 5; i++)
            {
                grupos.Add(new List<int>());
            }

            //grupos: 
            // primera iteracion: arriba, abajo, 
            // segunda iteracion: izq, der, 
            // dos iteraciones: comunes

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (contenedor[i][j * 2][0].Count == 0 && contenedor[i][(j * 2) + 1][0].Count != 0)
                    {
                        for (int k = 0; k < contenedor[i][(j * 2) + 1][0].Count; k++)
                        {
                            if(!opciones[contenedor[i][(j * 2) + 1][0][k]].colocado)
                            {
                                grupos[(j * 2) + 1].Add(contenedor[i][(j * 2) + 1][0][k]);
                                opciones[contenedor[i][(j * 2) + 1][0][k]].colocado = true;
                            }      
                        }
                    }
                    else if (contenedor[i][j * 2][0].Count != 0 && contenedor[i][(j * 2) + 1][0].Count == 0)
                    {
                        for (int k = 0; k < contenedor[i][j * 2][0].Count; k++)
                        {
                            if (!opciones[contenedor[i][(j * 2)][0][k]].colocado)
                            {
                                grupos[j * 2].Add(contenedor[i][j * 2][0][k]);
                                opciones[contenedor[i][(j * 2)][0][k]].colocado = true;
                            }
                                
                        }
                    }
                    //esquina podria ser factible
                    else
                    {
                        for (int k = 0; k < contenedor[i][(j * 2) + 1][0].Count; k++)
                        {
                            if (!opciones[contenedor[i][(j * 2) + 1][0][k]].colocado)
                            {
                                grupos[4].Add(contenedor[i][(j * 2) + 1][0][k]);
                                opciones[contenedor[i][(j * 2) + 1][0][k]].colocado = true;
                            }
                                
                        }
                        for (int k = 0; k < contenedor[i][j * 2][0].Count; k++)
                        {
                            if (!opciones[contenedor[i][(j * 2)][0][k]].colocado)
                            {
                                grupos[4].Add(contenedor[i][j * 2][0][k]);
                                opciones[contenedor[i][(j * 2)][0][k]].colocado = true;
                            }  
                        }
                    }
                }
            }

            for (int i = 0; i < opciones.Count; i++)
            {
                opciones[i].colocado = false;
            }

            //for (int i = 0; i < 5; i++)
            //{
            //    Write("Pos "+i+": ");
            //    for (int j = 0; j < grupos[i].Count; j++)
            //    {
            //        Write(grupos[i][j]+", ");
            //    }
            //    WriteLine();
            //}
            //WriteLine();
            //ReadKey();   

            WriteLine("____________________________");
            WriteLine("\nALGORITMO POR SELECCION Y TANTEO...");
            tanteoRec(contenedor, tablero, opciones, grupos, 0, 0);
            WriteLine("\nMatriz calculada...");
            imprimir(tablero);

            WriteLine();
            WriteLine("Comparaciones: " + comp + "\nAsignaciones: " + asig);
        }
        public bool tanteoRec(List<List<List<List<int>>>> contenedor, List<List<Item>> tableRec, List<Item> listaOpciones, List<List<int>> grupos, int x, int y)
        {
            asig++;
            List<int> ordenIndices = new List<int>();
            comp++;
            if (x == 0)
            {
                ordenIndices.Add(0);
                ordenIndices.Add(2);
                ordenIndices.Add(3);
                ordenIndices.Add(4);

            }
            else
            {
                comp++;
                if (x == tableRec.Count - 1)
                {
                    ordenIndices.Add(1);
                    ordenIndices.Add(2);
                    ordenIndices.Add(3);
                    ordenIndices.Add(4);
                }
                else
                {
                    comp++;
                    if (y == 0)
                    {
                        ordenIndices.Add(2);
                        ordenIndices.Add(0);
                        ordenIndices.Add(1);
                        ordenIndices.Add(4);
                    }
                    else
                    {
                        comp++;
                        if (y == tableRec.Count - 1)
                        {
                            ordenIndices.Add(3);
                            ordenIndices.Add(0);
                            ordenIndices.Add(1);
                            ordenIndices.Add(4);
                        }
                        else
                        {
                            ordenIndices.Add(4);
                        }
                    } 
                } 
            }

            for (int i = 0; i < ordenIndices.Count; i++)
            {
                comp++;
                for (int j = 0; j < grupos[ordenIndices[i]].Count; j++)
                {
                    comp += 2;
                    if (listaOpciones[grupos[ordenIndices[i]][j]].colocado)
                    {
                        continue;
                    }

                    asig += 2;
                    bool xT = false;
                    bool yT = false;

                    comp++;
                    if (x == 0)
                    {
                        asig++;
                        xT = true;
                    }
                        
                    else
                    {
                        comp++;
                        if (tableRec[x - 1][y].abajo == listaOpciones[grupos[ordenIndices[i]][j]].arriba)
                        {
                            asig++;
                            xT = true;
                        }
                    }

                    comp++;                
                    if (y == 0)
                    {
                        asig++;
                        yT = true;
                    }
                    else
                    {
                        comp++;
                        if (tableRec[x][y - 1].derecha == listaOpciones[grupos[ordenIndices[i]][j]].izquierda)
                        {
                            asig++;
                            yT = true;
                        }
                    }

                    comp++;              
                    if (xT && yT)
                    {
                        asig += 6;
                        listaOpciones[grupos[ordenIndices[i]][j]].colocado = true;
                        tableRec[x][y] = listaOpciones[grupos[ordenIndices[i]][j]];
                        contenedor[listaOpciones[grupos[ordenIndices[i]][j]].arriba][0][1][0]--;
                        contenedor[listaOpciones[grupos[ordenIndices[i]][j]].abajo][1][1][0]--;
                        contenedor[listaOpciones[grupos[ordenIndices[i]][j]].izquierda][2][1][0]--;
                        contenedor[listaOpciones[grupos[ordenIndices[i]][j]].derecha][3][1][0]--;

                        comp++;
                        if (x == tableRec.Count -1 && y == tableRec.Count - 1)
                        {
                            return true;
                        }
                        else
                        {
                            asig += 6;
                            bool rt = tanteoRec(contenedor, tableRec, listaOpciones, grupos, x + (y / (tableRec[x].Count - 1)), (y + 1) % tableRec[x].Count);
                            listaOpciones[grupos[ordenIndices[i]][j]].colocado = false;
                            contenedor[listaOpciones[grupos[ordenIndices[i]][j]].arriba][0][1][0]++;
                            contenedor[listaOpciones[grupos[ordenIndices[i]][j]].abajo][1][1][0]++;
                            contenedor[listaOpciones[grupos[ordenIndices[i]][j]].izquierda][2][1][0]++;
                            contenedor[listaOpciones[grupos[ordenIndices[i]][j]].derecha][3][1][0]++;

                            comp++;
                            if (rt)
                            {
                                return true;
                            }
                        }
                    }
                }
                comp++;
            }
            comp++;         
            return false;
        }
//------------------------------------------------------------------------------------------------------------------
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
        void imprimir3D(List<List<List<List<int>>>> contenedor)
        {
            for (int i = 0; i < 10; i++)
            {
                Write(i + ": \t");
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < contenedor[i][j][0].Count; k++)
                    {
                        Write(contenedor[i][j][0][k] + ",");
                    }
                    Write("\t\t|");
                }
                WriteLine();
            }

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
