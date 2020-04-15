using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab5_1223319_1003519.Models
{
    public class InfoTarea
    {
        public string Desarrollador { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Proyecto { get; set; }
        public int Prioridad { get; set; }
        public DateTime Entrega { get; set; }

        public TituloTarea ToTituloTarea()
        {
            return new TituloTarea { Titulo = this.Titulo, Prioridad = this.Prioridad };
        }
        
        public string String()
        {
            string texto = Desarrollador + ",";
            if (Titulo.Contains(','))
                texto += "\"" + Titulo + "\"" + ",";
            else
                texto += Titulo + ",";
            if (Descripcion.Contains(','))
                texto += "\"" + Descripcion + "\"" + ",";
            else
                texto += Descripcion + ",";
            if (Proyecto.Contains(','))
                texto += "\"" + Proyecto + "\"" + ",";
            else
                texto += Proyecto + ",";
            texto += Entrega.ToString("MM/dd/yyyy") + ",";
            texto += Prioridad.ToString();
            return texto;
        }

        public Tareas ToTareas()
        {
            return new Tareas { Titulo = this.Titulo, Descripcion = this.Descripcion, Proyecto = this.Proyecto, Entrega = this.Entrega, Prioridad = this.Prioridad };
        }
    }
}