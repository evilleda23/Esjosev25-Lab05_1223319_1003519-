using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasesGenericas.Estructuras
{
    public class NodoLineal<T>
    {
        public NodoLineal<T> Siguiente { get; set; }
        public NodoLineal<T> Anterior { get; set; }
        public T Valor { get; set; }
    }
}
