using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab5_1223319_1003519.Models
{
    public class InfoDesarrollador : IComparable
    {
        public string Desarrollador { get; set; }
        public string Titulo { get; set; }
        public int Prioridad { get; set; }

        public int CompareTo(object obj)
        {
            return this.Prioridad.CompareTo(((InfoDesarrollador)obj).Prioridad);
        }
    }
}