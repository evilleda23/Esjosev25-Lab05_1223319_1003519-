using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasesGenericas.Estructuras
{
    public class ColaPrioridad<T>
    {
        private Nodo<T> Raiz { get; set; }
        public int Count { get; set; } = 0;

        public void Add(T nuevo, Comparison<T> comparer)
        {
            if (Count == 0)
                Raiz = new Nodo<T> { Valor = nuevo };
            else
            {
                Nodo<T> posicion = Raiz;
                int n = Count + 1;
                Pila<int> direcciones = new Pila<int>();
                while (n > 1)
                {
                    direcciones.Push(n % 2);
                    n /= 2;
                }
                while (direcciones.Count > 1)
                {
                    if (direcciones.Pop() == 0)
                        posicion = posicion.Izquierda;
                    else
                        posicion = posicion.Derecha;
                }
                if (direcciones.Pop() == 0)
                {
                    posicion.Izquierda = new Nodo<T> { Valor = nuevo };
                    posicion.Izquierda.Padre = posicion;
                    posicion = posicion.Izquierda;
                }
                else
                {
                    posicion.Derecha = new Nodo<T> { Valor = nuevo };
                    posicion.Derecha.Padre = posicion;
                    posicion = posicion.Derecha;
                }
                while (posicion.Padre != null)
                {
                    if (comparer.Invoke(posicion.Valor, posicion.Padre.Valor) < 0)
                    {
                        T aux = posicion.Valor;
                        posicion.Valor = posicion.Padre.Valor;
                        posicion.Padre.Valor = aux;
                    }
                    posicion = posicion.Padre;
                }
            }
            Count++;
        }

        public T Remove(Comparison<T> comparer)
        {
            if (Count > 0)
            {
                T valorPorDevolver = Raiz.Valor;
                if (Count == 1)
                    Raiz = null;
                else
                {
                    Nodo<T> posicion = Raiz;
                    int n = Count;
                    Pila<int> direcciones = new Pila<int>();
                    while (n > 1)
                    {
                        direcciones.Push(n % 2);
                        n /= 2;
                    }
                    while (direcciones.Count > 1)
                    {
                        if (direcciones.Pop() == 0)
                            posicion = posicion.Izquierda;
                        else
                            posicion = posicion.Derecha;
                    }
                    if (direcciones.Pop() == 0)
                    {
                        posicion.Izquierda = null;
                    }
                    else
                    {
                        posicion.Derecha = null;
                    }
                    Raiz.Valor = posicion.Valor;
                    posicion = Raiz;
                    while (posicion.Izquierda != null)
                    {
                        if (posicion.Derecha != null)
                        {
                            if (comparer.Invoke(posicion.Valor, posicion.Izquierda.Valor) < 0 && comparer.Invoke(posicion.Valor, posicion.Derecha.Valor) < 0)
                                posicion = new Nodo<T>();
                            else
                            {
                                if (comparer.Invoke(posicion.Derecha.Valor, posicion.Izquierda.Valor) < 0)
                                {
                                    T aux = posicion.Valor;
                                    posicion.Valor = posicion.Derecha.Valor;
                                    posicion.Derecha.Valor = aux;
                                }
                                else
                                {
                                    T aux = posicion.Valor;
                                    posicion.Valor = posicion.Izquierda.Valor;
                                    posicion.Izquierda.Valor = aux;
                                }
                            }
                        }
                        else
                        {
                            if (comparer.Invoke(posicion.Valor, posicion.Izquierda.Valor) < 0)
                                posicion = new Nodo<T>();
                            else
                            {
                                T aux = posicion.Valor;
                                posicion.Valor = posicion.Izquierda.Valor;
                                posicion.Izquierda.Valor = aux;
                            }
                        }
                    }
                }
                Count--;
                return valorPorDevolver;
            }
            else
                return default(T);
        }

        public void Delete(Comparison<T> comparer)
        {
            Remove(comparer);
        }

        public T Get()
        {
            if (Raiz != null)
                return Raiz.Valor;
            else
                return default(T);
        }
    }
}
