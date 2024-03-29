﻿  using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab5_1223319_1003519.Models
{
    public class Tareas
    {
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Proyecto { get; set; }
        public int Prioridad { get; set; }
        public DateTime Entrega { get; set; }

        public InfoTarea ToInfoTarea(string desarrollador)
        {
            return new InfoTarea { Desarrollador = desarrollador, Titulo = this.Titulo, Descripcion = this.Descripcion, Proyecto = this.Proyecto, Entrega = this.Entrega, Prioridad = this.Prioridad };
        }
    }
}