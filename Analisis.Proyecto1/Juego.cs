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
        List<List<List<List<int>>>> contenedor = new List<List<List<List<int>>>>();
        int tamañoMatriz;
        long asig, comp;
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
            WriteLine("____________________________");
            WriteLine("\nALGORITMO POR DESCARTE...");

            descarteRec(opciones, 0, 0, tablero);   asig += 2;

            WriteLine("\nMatriz calculada...");
            imprimir(tablero);

            WriteLine();
            WriteLine("Comparaciones: " + comp + "\nAsignaciones: " + asig);
        }
        public bool descarteRec(List<Item> listaOpciones, int x, int y, List<List<Item>> tableRec)
        {
            for (int i = 0; i < listaOpciones.Count; i++)
            {comp++;asig++;
                bool xT = false;    asig++;
                bool yT = false;    asig++;
                comp++; if (x == 0)
                {
                    xT = true; asig++;
                }
                else
                {
                    comp++; if (tableRec[x - 1][y].abajo == listaOpciones[i].arriba)
                    {
                        xT = true; asig++;
                    }
                }
                comp++; if (y == 0)
                {
                    yT = true; asig++;
                }
                else
                {
                    comp++; if (tableRec[x][y - 1].derecha == listaOpciones[i].izquierda)
                    {
                        yT = true; asig++;
                    }
                }
                comp+=2;if (xT && yT)
                {
                    tableRec[x][y] = listaOpciones[i];  asig++;
                    comp++; if (listaOpciones.Count == 1)
                    {
                        return true;
                    }
                    else
                    {
                        Item temp = listaOpciones[i];   asig++;
                        listaOpciones.RemoveAt(i);      asig++;
                        comp++;asig += 2; if (descarteRec(listaOpciones, x + (y / (tableRec[x].Count - 1)), (y + 1) % tableRec[x].Count, tableRec))
                        {
                            listaOpciones.Insert(i, temp); asig++;
                            return true;
                        }
                        listaOpciones.Insert(i, temp);  asig++;
                    }
                }
            }comp++;
            return false;
        }
//------------------------------------------------------------------------------------------------------------------       
        public void fuerza()
        {
            asig = 0;
            comp = 0;

            WriteLine("____________________________");
            WriteLine("\nALGORITMO POR FUERZA BRUTA...");
            fuerzaRec(opciones, 0, 0, tablero);     asig += 2;

            WriteLine();
            WriteLine("Comparaciones: " + comp + "\nAsignaciones: " + asig);
        }
        /// <summary>
        /// Aspecto recursivo de fuerza bruta
        /// </summary>
        /// <param name="listaOpciones">Las opciones que tiene disponible el nivel de recursividad, al insertar una, esta es removida temporalmente de la lista</param>
        /// <param name="x">Parte 1 de la tupla de direccionamiento de la matriz</param>
        /// <param name="y">Parte 2 de la tupla de direccionamiento de la matriz</param>
        /// <param name="tableRec">Tabla que se modifica en la recursividad, pasada por referencia en un parametro para mas comodidad</param>
        /// <returns>Retorna true cuando se encuentre un resultado, para acabar con la recursividad en forma de cascada</returns>
        public bool fuerzaRec(List<Item> listaOpciones, int x, int y, List<List<Item>> tableRec)
        {
            comp++; if (listaOpciones.Count != 0)
            {
                for (int i = 0; i < listaOpciones.Count; i++)
                {comp++;asig++;
                    tableRec[x][y] = listaOpciones[i];      asig++;
                    Item temp =listaOpciones[i];            asig++;
                    listaOpciones.RemoveAt(i);              asig++;

                    asig += 3; bool rt=fuerzaRec(listaOpciones, x + (y / (tableRec[x].Count - 1)), (y + 1) % tableRec[x].Count, tableRec);
                    listaOpciones.Insert(i, temp);      asig++;
                    comp++; if (rt)
                        return true;
                }comp++;
            }
            else
            {
                bool prueba = true;     asig++;
                for (int i = 0; i < tableRec.Count && prueba; i++)
                {comp+=2;asig++;
                    for (int j = 0; j < tableRec[i].Count && prueba; j++)
                    {comp+=2;asig++;
                        comp++;if (i != 0)
                        {
                            comp++;if (tableRec[i - 1][j].abajo != tableRec[i][j].arriba)
                            {
                                prueba = false;     asig++;
                            }
                        }
                        comp++;if (j != 0)
                        {
                            comp++;if (tableRec[i][j - 1].derecha != tableRec[i][j].izquierda)
                            {
                                prueba = false;asig++;
                            }
                        }
                    }comp++;
                }comp++;
                comp++; if (prueba)
                {
                    Console.WriteLine();
                    WriteLine("Se ha encontrado una coincidencia");
                    imprimir(tableRec);
                    return true;
                }
            }
            return false;
        }
//------------------------------------------------------------------------------------------------------------------
        
        /// <summary>
        /// Arranca el proceso de Seleccion y tanteo, crea los grupos primarios y el contenedor,
        /// el cual categoriza las piezas segun los numeros de sus lados,
        /// tambien crea un conteo de la cantidad de elementos disponibles en cada categoria
        /// </summary>
        public void tanteo()
        {
            //decalra el contenedor, que categoriza las piezas y mantiene un conteo de aquellas que estan disponibles
            asig = 0; comp = 0;
            contenedor = new List<List<List<List<int>>>>();     asig++;
            for (int i = 0; i < 10; i++) 
            {comp++; asig++;
                contenedor.Add(new List<List<List<int>>>());    asig++;

                for (int j = 0; j < 4; j++)
                {comp++; asig++;
                    contenedor[i].Add(new List<List<int>>());   asig++;
                    for (int k = 0; k < 2; k++)
                    {comp ++; asig++;
                        contenedor[i][j].Add(new List<int>());  asig++;
                    }comp++;
                    contenedor[i][j][1].Add(0);                 asig++;
                }comp++;
            }comp++;
            //conteo
            for (int i = 0; i < opciones.Count; i++)
            {comp++; asig++;
                contenedor[opciones[i].arriba][0][0].Add(i);    asig++;
                contenedor[opciones[i].abajo][1][0].Add(i);     asig++;
                contenedor[opciones[i].izquierda][2][0].Add(i); asig++;
                contenedor[opciones[i].derecha][3][0].Add(i);   asig++;
                contenedor[opciones[i].arriba][0][1][0]++;      asig++;
                contenedor[opciones[i].abajo][1][1][0]++;       asig++;
                contenedor[opciones[i].izquierda][2][1][0]++;   asig++;
                contenedor[opciones[i].derecha][3][1][0]++;     asig++;
            }comp++;
            WriteLine("____________________________");
            WriteLine("\nALGORITMO POR SELECCION Y TANTEO...");
            imprimirLista(opciones);
            imprimir3D(contenedor);
            asig+=2; if (tanteoRec(tablero, opciones, creargrupos(), 0, 0))
            {
                WriteLine("\nMatriz calculada...");
                imprimir(tablero);
                WriteLine();
                WriteLine("Comparaciones: " + comp + "\nAsignaciones: " + asig);

            }
            else
                WriteLine("No se ha podido encontrar una solucion");

        }
        /// <summary>
        /// El aspecto recursivo de seleccion y tanteo, cada nivel de profundidad en la recursividad representa una casilla distinta de la 
        /// matriz
        /// </summary>
        /// <param name="tableRec"> La tabla que se modifica durante la recursividad</param>
        /// <param name="listaOpciones">La lista de las fichas existentes, cada ficha posee un atributo de uso</param>
        /// <param name="grupos">Los grupos en los que se clasifican las fichas, lateral o centralmente</param>
        /// <param name="x">Parte 1 de la tupla para direccionamiento en matriz</param>
        /// <param name="y">Parte 2 de la tupla para direccionamiento en matriz</param>
        /// <returns></returns>
        public bool tanteoRec(List<List<Item>> tableRec, List<Item> listaOpciones, List<List<int>> grupos, int x, int y)
        {
            //para establecer el orden en que los grupos serán evaluados
            List<int> ordenIndices = new List<int>(); asig++;
            comp += 2; if (x == 0 || x == tableRec.Count - 1)
            {
                ordenIndices.Add(x/(tableRec.Count - 1));   asig++;
                ordenIndices.Add(2);                        asig++;
                ordenIndices.Add(3);                        asig++;
                ordenIndices.Add(4);                        asig++;
            }
            else
            {
                comp+=2; if (y == 0 || y == tableRec.Count - 1)
                {
                    ordenIndices.Add(y/(tableRec.Count - 1));   asig++;
                    ordenIndices.Add(0);                        asig++;
                    ordenIndices.Add(1);                        asig++;
                    ordenIndices.Add(4);                        asig++;
                }
                else
                {
                    ordenIndices.Add(4);                        asig++;
                } 
            }

            //para determinar cuales itemes se ven prometedores a corto plazo, ej itemes que encajan en la siguente casilla
            List<int> prometedores = new List<int>();

            for (int i = 0; i < ordenIndices.Count; i++)
            {comp++; asig++;
                for (int j = 0; j < grupos[ordenIndices[i]].Count; j++)
                {comp++; asig++;
                    comp++; if (listaOpciones[grupos[ordenIndices[i]][j]].colocado)
                    {
                        continue;
                    }
                    bool xT = false;    asig++;
                    bool yT = false;    asig++;
                    comp++; if (x == 0)
                    {
                        xT = true;      asig++;
                    }
                        
                    else
                    {
                        comp++; if (tableRec[x - 1][y].abajo == listaOpciones[grupos[ordenIndices[i]][j]].arriba)
                        {
                            xT = true;  asig++;
                        }
                        else
                            continue;
                    }

                    comp++; if (y == 0)
                    {
                        yT = true; asig++;
                    }
                    else
                    {
                        comp++; if (tableRec[x][y - 1].derecha == listaOpciones[grupos[ordenIndices[i]][j]].izquierda)
                        {
                            yT = true; asig++;
                        }
                        else
                            continue;
                    }

                    comp+=2; if (xT && yT)
                    {
                        prometedores.Add(grupos[ordenIndices[i]][j]);   asig++;
                    }
                }comp++;
            }comp++;
            //recorre los elementos prometedores, insertando a cada uno de ellos en la misma casilla, llamando al siguiente nivel de 
            //recursividad en cada insercion
            for (int item = 0; item < prometedores.Count; item++)
            {comp++; asig++;
                tableRec[x][y] = listaOpciones[prometedores[item]];     asig++;
                //se reduce el contador de las categorias en contador que incluian este item
                contenedor[listaOpciones[prometedores[item]].arriba][0][1][0]--;    asig++;
                contenedor[listaOpciones[prometedores[item]].abajo][1][1][0]--;     asig++;
                contenedor[listaOpciones[prometedores[item]].izquierda][2][1][0]--; asig++;
                contenedor[listaOpciones[prometedores[item]].derecha][3][1][0]--;   asig++;
                //comprueba si el item insertado completó el tablero, significando que se ha acabado el juego y la recursividad
                comp+=2; if (x == tableRec.Count - 1 && y == tableRec.Count - 1)
                {
                    imprimirGrupos(grupos);
                    return true;
                }
                else
                {
                    //asigna el item a la casilla, despues llama a la recursividad para que pruebe bajo sus propias variables
                    listaOpciones[prometedores[item]].colocado = true;              asig++;

                    //la recursividad tiene una funcion intermediaria, la cual calcula los grupos para la nueva recursividad
                    //a partir de los grupos actuales
                    asig+=3; bool rt = actgrupotanteorec(tableRec, listaOpciones, grupos, x +(y / (tableRec[x].Count - 1)), (y + 1) % tableRec[x].Count,listaOpciones[prometedores[item]]);
                    //siempre es preferible devolver todo aquello que no sea el tablero de juego a su estado original
                    listaOpciones[prometedores[item]].colocado = false;                 asig++;
                    contenedor[listaOpciones[prometedores[item]].arriba][0][1][0]++;    asig++;
                    contenedor[listaOpciones[prometedores[item]].abajo][1][1][0]++;     asig++;
                    contenedor[listaOpciones[prometedores[item]].izquierda][2][1][0]++; asig++;
                    contenedor[listaOpciones[prometedores[item]].derecha][3][1][0]++;   asig++;

                    comp++;if (rt)
                    {
                        //fin de recursividad por cascada
                        return true;
                    }
                }
            }comp++;
            //fin del nivel de recursividad por resultado insatisfactorio
            return false;
        }
        /// <summary>
        /// Crea y llena los grupos de clasificacion lateral de las fichas
        /// </summary>
        /// <returns></returns>
        public List<List<int>> creargrupos()
        {
            asig++; List<List<int>> grupos = new List<List<int>>();
            
            for (int i = 0; i < 5; i++)
            {asig++; comp++;
                grupos.Add(new List<int>()); asig ++;
            }comp++;

            //grupos: 
            // primera iteracion: arriba, abajo, 
            // segunda iteracion: izq, der, 
            // dos iteraciones: comunes

            asig++; List<int> restaurar = new List<int>();
            //Para asignar un item a algun grupo especial, no debe haber ningun item disponible que funcione como contraparte a uno de los lados del item en cuestion
            for (int i = 0; i < 10; i++)
            {comp++; asig++;
                for (int j = 0; j < 3; j = j + 2)
                {comp++; asig++;
                    comp+=2; if (contenedor[i][j][1][0] == 0 && contenedor[i][j + 1][1][0] != 0)
                    {
                        for (int k = 0; k < contenedor[i][j + 1][0].Count; k++)
                        {comp++; asig++;
                            comp++; if (!opciones[contenedor[i][j + 1][0][k]].colocado)
                            {
                                comp++; if (!grupos[j + 1].Contains(contenedor[i][j + 1][0][k]))
                                {
                                    opciones[contenedor[i][j + 1][0][k]].colocado = true;       asig++;
                                    grupos[j + 1].Add(contenedor[i][j + 1][0][k]);              asig++;
                                    restaurar.Add(contenedor[i][j + 1][0][k]);                  asig++;
                                }
                            }
                        }comp++;
                    }
                    else if (contenedor[i][j][1][0] != 0 && contenedor[i][j + 1][1][0] == 0)
                    {comp+=2;
                        for (int k = 0; k < contenedor[i][j][0].Count; k++)
                        {comp++; asig++;
                            comp++; if (!opciones[contenedor[i][j][0][k]].colocado)
                            {
                                comp++; if (!grupos[j].Contains(contenedor[i][j][0][k]))
                                {
                                    grupos[j].Add(contenedor[i][j][0][k]);                      asig++;
                                    opciones[contenedor[i][j][0][k]].colocado = true;           asig++;
                                    restaurar.Add(contenedor[i][j][0][k]);                      asig++;
                                }
                            }
                        }comp++;
                    }
                    else
                    {
                        for (int k = 0; k < contenedor[i][j + 1][0].Count; k++)
                        {comp++; asig++;
                            comp++; if (!opciones[contenedor[i][j + 1][0][k]].colocado)
                            {
                                comp++; if (!grupos[4].Contains(contenedor[i][j + 1][0][k]))
                                {
                                    grupos[4].Add(contenedor[i][j + 1][0][k]);                  asig++;
                                    opciones[contenedor[i][j + 1][0][k]].colocado = true;       asig++;
                                    restaurar.Add(contenedor[i][j + 1][0][k]);                  asig++;

                                }
                            }
                        }comp++;
                        for (int k = 0; k < contenedor[i][j][0].Count; k++)
                        {comp++;asig++;
                            comp++; if (!opciones[contenedor[i][j][0][k]].colocado)
                            {
                                comp++; if (!grupos[4].Contains(contenedor[i][j][0][k]))
                                {
                                    grupos[4].Add(contenedor[i][j][0][k]);                      asig++;
                                    opciones[contenedor[i][j][0][k]].colocado = true;           asig++;
                                    restaurar.Add(contenedor[i][j][0][k]);                      asig++;
                                }
                            }
                        }comp++;
                    }
                }comp++;
            }comp++;
            //se utilizó la bandera "colocado" para realizar los calculos de grupos, en esta parte se restauran
            for (int res = 0; res < restaurar.Count; res++)
            {comp++;asig++;
                opciones[restaurar[res]].colocado = false;      asig++;
            }comp++;
            imprimirGrupos(grupos);
            return grupos;
        }

        /// <summary>
        /// Funcion intermediaria para la recursividad de tanteo, actualiza los grupos para la nueva recursividad
        /// </summary>
        /// <param name="tableRec"> La tabla que se modifica durante la recursividad</param>
        /// <param name="listaOpciones">La lista de las fichas existentes, cada ficha posee un atributo de uso</param>
        /// <param name="grupos">Los grupos en los que se clasifican las fichas, lateral o centralmente</param>
        /// <param name="x">Parte 1 de la tupla para direccionamiento en matriz</param>
        /// <param name="y">Parte 2 de la tupla para direccionamiento en matriz</param>
        /// <param name="factor">Item del que se deducen los grupos que se deben actualizar</param>
        /// <returns>Retorna el valor que retorne la siguiente recursividad</returns>
        public bool actgrupotanteorec(List<List<Item>> tableRec, List<Item> listaOpciones, List<List<int>> grupos, int x, int y, Item factor)
        {
            List<int> indecesCont = new List<int>();    asig++;
            List<int> recuperar = new List<int>();      asig++;
            //asigna las casillas de contenedor que se van a evaluar
            indecesCont.Add(factor.arriba);             asig++;
            indecesCont.Add(factor.izquierda);          asig++;

            //Para el numero de arriba, comprueba que existan más fichas con ese numero arriba, en caso de que no hayan, las fichas 
            //con ese numero abajo se envian al grupo de abajo
            //caso similar para el numero de arriba
            //además se conservan los cambios realizados para recuperarlos despues de llamar a la recursividad
            for (int i = 0; i < indecesCont.Count; i = i + 2)
            {comp++;asig++;
                comp+=2; if (contenedor[indecesCont[i]][i][1][0] == 0 && contenedor[indecesCont[i]][i + 1][1][0] != 0)
                {
                    for (int j = 0; j < contenedor[indecesCont[i]][i + 1][0].Count; j++)
                    {comp++;asig++;
                        comp+=2; if ((!opciones[contenedor[indecesCont[i]][i + 1][0][j]].colocado) && grupos[4].Contains(contenedor[indecesCont[i]][i + 1][0][j]))
                        {
                            grupos[4].Remove(contenedor[indecesCont[i]][i + 1][0][j]);      asig++;
                            grupos[i + 1].Add(contenedor[indecesCont[i]][i + 1][0][j]);     asig++;
                            recuperar.Add(i + 1);                                           asig++;
                            recuperar.Add(contenedor[indecesCont[i]][i + 1][0][j]);         asig++;
                        }
                    }comp++;
                }
            }comp++;
            bool rt = tanteoRec(tableRec, listaOpciones, grupos, x, y); asig++;
            //recupera los cambios realizados en el grupo
            for (int i = 0; i < recuperar.Count; i = i + 2)
            {comp++;asig++;
                grupos[recuperar[i]].Remove(recuperar[i + 1]);          asig++;
                grupos[4].Add(recuperar[i + 1]);                        asig++;
            }comp++;
            return rt;
        }

//------------------------------------------------------------------------------------------------------------------
//las funciones de abajo son para mera representacion en interfaz
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
                Write("Fichas con el numero " + i + "\n");
                for (int j = 0; j < 4; j++)
                {
                    if (j == 0)
                        Write("Arriba:\t\t");
                    else if (j == 1)
                        Write("Abajo:\t\t");
                    else if(j == 2)
                        Write("Izquierda:\t");
                    else if(j == 3)
                        Write("Derecha:\t");
                    for (int k = 0; k < contenedor[i][j][0].Count; k++)
                    {
                        if(k!=0)
                            Write(", "+contenedor[i][j][0][k]);
                        else
                            Write(contenedor[i][j][0][k]);

                    }
                    Write("\n");
                }
                WriteLine();
            }

        }
        public void imprimirLista(List<Item> listaOpciones)
        {
            WriteLine("LISTA DE FICHAS\n------------------------------------------");
            for (int i = 0; i < listaOpciones.Count; i+=5)
            {
                for (int x = i; x < i+5 && x< listaOpciones.Count; x++)
                {
                    Write("  " + listaOpciones[x].arriba.ToString() + "  ");

                }
                WriteLine();
                for (int x = i; x < i + 5 && x < listaOpciones.Count; x++)
                {
                    Write(" " + listaOpciones[x].izquierda.ToString() + " " + listaOpciones[x].derecha.ToString() + " ");

                }
                WriteLine();
                for (int x = i; x < i + 5 && x < listaOpciones.Count; x++)
                {
                    Write("  " + listaOpciones[x].abajo.ToString() + "  ");

                }
                WriteLine();
            }
            WriteLine("------------------------------------------");
        }
        public void imprimirGrupos(List<List<int>> grupos)
        {
            //solo para interfaz, no se tomaran en cuenta las asignaciones y comparaciones
            for (int i = 0; i < 5; i++)
            {
                Write("Pos " + i + ": ");
                for (int j = 0; j < grupos[i].Count; j++)
                {
                    if (j!=0)
                        Write(", "+grupos[i][j] );
                    else
                        Write(grupos[i][j]);

                }
                WriteLine();
            }
            WriteLine("\n");
        }
    }
}
