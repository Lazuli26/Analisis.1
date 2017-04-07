using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analisis.Proyecto1
{
    class Fijas
    {
        List<Item> matriz1 = new List<Item>();
        List<Item> matriz2 = new List<Item>();

        public List<Item> llenarMatriz1()
        {
            //arriba, abajo, izquierda, derecha
            matriz1.Add(new Item(3, 2, 1, 4));
            matriz1.Add(new Item(5, 0, 4, 0));
            matriz1.Add(new Item(7, 6, 0, 5));

            matriz1.Add(new Item(2, 7, 6, 8));
            matriz1.Add(new Item(0, 9, 8, 3));
            matriz1.Add(new Item(6, 9, 3, 9));

            matriz1.Add(new Item(7, 1, 5, 2));
            matriz1.Add(new Item(9, 0, 2, 4));
            matriz1.Add(new Item(9, 3, 4, 0));
            return matriz1;
        }

        public List<Item> llenarMatriz2()
        {
            //arriba, abajo, izquierda, derecha
            matriz2.Add(new Item(7, 7, 2, 7));
            matriz2.Add(new Item(8, 1, 7, 9));
            matriz2.Add(new Item(4, 1, 9, 4));

            matriz2.Add(new Item(7, 4, 5, 0));
            matriz2.Add(new Item(1, 2, 0, 3));
            matriz2.Add(new Item(1, 3, 3, 6));

            matriz2.Add(new Item(4, 8, 0, 1));
            matriz2.Add(new Item(2, 9, 1, 8));
            matriz2.Add(new Item(3, 7, 8, 0));
            return matriz2;
        }
    }
}
