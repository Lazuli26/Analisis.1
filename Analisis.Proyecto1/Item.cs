using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Analisis.Proyecto1
{
    class Item
    {
        public int arriba, abajo, izquierda, derecha;
        public Item(int ar,int ab,int iz,int der)
        {
            arriba = ar;
            abajo = ab;
            izquierda = iz;
            derecha = der;
        }
        public Item()
        {

        }

        public void imprimirItem()
        {
            WriteLine("Pos arriba: " + arriba + " - Pos abajo: " + abajo + " - Pos izquierda: " + izquierda + " - Pos derecha: " + derecha);
        }
    }
}
