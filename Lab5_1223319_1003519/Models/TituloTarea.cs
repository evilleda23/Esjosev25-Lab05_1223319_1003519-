using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab5_1223319_1003519.Models
{
    public class TituloTarea
    {
        public string Titulo { get; set; }
        public int Prioridad { get; set; }

        public static Comparison<TituloTarea> CompararPrioridad = delegate (TituloTarea t1, TituloTarea t2)
        {
            return t1.Prioridad.CompareTo(t2.Prioridad);
        };
    }
}