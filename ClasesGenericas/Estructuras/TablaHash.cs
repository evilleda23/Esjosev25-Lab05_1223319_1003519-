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

        public void Add(T value, Func<T,string> llave)
        {
            Arreglo[FuncionHash(llave(value))].Add(value);
        }

        public T Remove(T value, Func<T, string> llave)
        {
            T resultado = default(T);
            foreach (T item in Arreglo[FuncionHash(llave(value))])
            {
                if (llave(item).Equals(llave(value)))
                {
                    resultado = item;
                    Arreglo[FuncionHash(llave(value))].Remove(item);
                }
            }
            return resultado;
        }

        public void Delete(T value, Func<T, string> llave)
        {
            Remove(value, llave);
        }

        public T Search(T value, Func<T, string> llave)
        {
            T resultado = default(T);
            foreach (T item in Arreglo[FuncionHash(llave(value))])
            {
                if (llave(item).Equals(llave(value)))
                    resultado = item;
            }
            return resultado;
        }

        public List<T> Items()
        {
            List<T> valores = new List<T>();
            foreach (List<T> list in Arreglo)
            {
                foreach (T item in list)
                {
                    valores.Add(item);
                }
            }
            return valores;
        }

        private int FuncionHash(string llave)
        {
            return (llave.Length * 7) % 20;
        }
    }
}
