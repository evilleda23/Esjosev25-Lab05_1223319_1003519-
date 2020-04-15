using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasesGenericas.Estructuras
{
    public class TablaHash<T>
    {
        private List<T>[] Arreglo = new List<T>[20];
        
        public TablaHash()
        {
            for (int i = 0; i < Arreglo.Length; i++)
                Arreglo[i] = new List<T>();
        
        
        }


        public void Add(T value, Func<T,int> llave)
        {
            
           //if( Arreglo[llave(value)] != null){
            Arreglo[llave(value)].Add(value);
            
        }
     

        public T Remove(T value, Func<T, int> llave)
        {
            return default(T);
        }

        public void Delete(T value, Func<T, int> llave)
        {
            Remove(value, llave);
        }

        public T Search(T value, Func<T, int> llave)
        {
            return default(T);
        }
    }
}
