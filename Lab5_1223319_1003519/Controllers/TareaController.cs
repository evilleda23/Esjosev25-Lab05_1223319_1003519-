using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lab5_1223319_1003519.Helpers;
using Lab5_1223319_1003519.Models;
using System.IO;
using ClasesGenericas.Estructuras;

namespace Lab5_1223319_1003519.Controllers
{
    public class TareaController : Controller
    {
        // GET: Tarea
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            if (UsuarioExiste(collection["name"], collection["password"]))
            {
                if (Storage.Instance.PrimeraSesion)
                {
                    if (LlenarTablaHash())
                        Storage.Instance.PrimeraSesion = false;
                }
                LlenarCola();
                if (Storage.Instance.admin)
                    return RedirectToAction("Admin");
                else
                    return RedirectToAction("ProximaTarea");
            }
            else
                return RedirectToAction("Index");
        }

        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(FormCollection collection)
        {
            if (NombreUsuarioValido(collection["name"]))
            {
                if (collection["password"] == collection["password2"] && !collection["password"].Contains(','))
                {
                    if (Storage.Instance.PrimeraSesion)
                    {
                        if (LlenarTablaHash())
                            Storage.Instance.PrimeraSesion = false;
                    }
                    AgregarUsuario(collection["name"], collection["password"]);
                    LlenarCola();
                    return RedirectToAction("ProximaTarea");
                }
                else
                    return RedirectToAction("SignIn");
            }
            else
                return RedirectToAction("SignIn");
        }

        // GET: Tarea/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Tarea/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tarea/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Tarea/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Tarea/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Tarea/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Tarea/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ProximaTarea()
        {
            if (Storage.Instance.tareasUsuario.Get() != null)
            {
                if (Storage.Instance.infoTareas.Search(Storage.Instance.tareasUsuario.Get().ToInfoTarea(), tarea => tarea.Titulo) != null)
                    return View("ProximaTarea", Storage.Instance.infoTareas.Search(Storage.Instance.tareasUsuario.Get().ToInfoTarea(), tarea => tarea.Titulo).ToTareas());
                else
                    return RedirectToAction("NuevaTarea");
            }
            else
                return RedirectToAction("NuevaTarea");
        }

        public ActionResult NuevaTarea()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NuevaTarea(FormCollection collection)
        {

            try
            {
                InfoTarea nuevo = new InfoTarea { Desarrollador = Storage.Instance.name, Titulo = collection["Titulo"], Descripcion = collection["Descripcion"],
                    Proyecto = collection["Proyecto"], Entrega = DateTime.Parse(collection["Entrega"]), Prioridad = int.Parse(collection["Prioridad"]) };
                if (Storage.Instance.infoTareas.Search(nuevo, tarea => tarea.Titulo) == null && nuevo.Prioridad >= 0)
                {
                    Storage.Instance.infoTareas.Add(nuevo, tarea => tarea.Titulo);
                    Storage.Instance.tareasUsuario.Add(nuevo.ToTituloTarea(), TituloTarea.CompararPrioridad);
                    Sobreescribir();
                    return RedirectToAction("ProximaTarea");
                }
                else
                    return RedirectToAction("NuevaTarea");
            }
            catch
            {
                return RedirectToAction("NuevaTarea");
            }
        }

        public ActionResult Finalizada()
        {
            Storage.Instance.infoTareas.Delete(Storage.Instance.tareasUsuario.Remove(TituloTarea.CompararPrioridad).ToInfoTarea(), tarea => tarea.Titulo);
            Sobreescribir();
            return RedirectToAction("ProximaTarea");
        }

        public ActionResult Admin()
        {
            List<InfoTarea> infoTareas = Storage.Instance.infoTareas.Items();
            List<InfoDesarrollador> tareas = new List<InfoDesarrollador>();
            foreach(InfoTarea tarea in infoTareas)
            {
                tareas.Add(tarea.ToInfoDesarrollador());
            }
            tareas.Sort();
            return View(tareas);
        }

        private bool NombreUsuarioValido(string name)
        {
            try
            {
                if (name.Contains(","))
                    return false;
                else
                {
                    string path = Server.MapPath("/Usuarios.csv");
                    string texto = "";
                    using (StreamReader lector = new StreamReader(path))
                    {
                        texto = lector.ReadToEnd();
                    }
                    if (texto.Contains(name))
                        return false;
                    else
                    {
                        Storage.Instance.name = name;
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        private bool UsuarioExiste(string name, string password)
        {
            try
            {
                if (name.Contains(",") || password.Contains(","))
                    return false;
                else
                {
                    string path = Server.MapPath("/Usuarios.csv");
                    string texto = "";
                    using (StreamReader lector = new StreamReader(path))
                    {
                        texto = lector.ReadToEnd();
                    }
                    if (texto.IndexOf(name) >= 0)
                    {
                        texto = texto.Remove(0, texto.IndexOf(name));
                        texto = texto.Remove(0, texto.IndexOf(',') + 1);
                        if (texto.Contains("\r\n"))
                            texto = texto.Substring(0, texto.IndexOf("\r\n"));
                        if (texto == password)
                        {
                            Storage.Instance.name = name;
                            if (name == "admin")
                                Storage.Instance.admin = true;
                            else
                                Storage.Instance.admin = false;
                            return true;
                        }
                        else
                            return false;
                    }
                    else
                        return false;
                }
            }
            catch
            {
                return false;
            }
        }

        private void LlenarCola()
        {
            if (!Storage.Instance.admin)
            {
                Storage.Instance.tareasUsuario.Clear();
                List<InfoTarea> tareas = Storage.Instance.infoTareas.Items();
                foreach (InfoTarea tarea in tareas)
                {
                    if (tarea.Desarrollador == Storage.Instance.name)
                        Storage.Instance.tareasUsuario.Add(tarea.ToTituloTarea(), TituloTarea.CompararPrioridad);
                }
            }
        }

        private bool LlenarTablaHash()
        {
            try
            {
                string path = Server.MapPath("/Tareas.csv");
                string texto = "";
                using (StreamReader lector = new StreamReader(path))
                {
                    texto = lector.ReadToEnd();
                }
                string[] text = texto.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 1; i < text.Length; i++)
                {
                    if (text[i] != "")
                    {
                        InfoTarea nuevo = new InfoTarea
                        {
                            Desarrollador = text[i].Substring(0, text[i].IndexOf(","))
                        };
                        text[i] = text[i].Remove(0, text[i].IndexOf(",") + 1);
                        if (text[i].IndexOf('\"') == 0)
                        {
                            text[i] = text[i].Remove(0, 1);
                            nuevo.Titulo = text[i].Substring(0, text[i].IndexOf('\"'));
                            text[i] = text[i].Remove(0, text[i].IndexOf('\"') + 2);
                        }
                        else
                        {
                            nuevo.Titulo = text[i].Substring(0, text[i].IndexOf(','));
                            text[i] = text[i].Substring(text[i].IndexOf(',') + 1);
                        }
                        if (text[i].IndexOf('\"') == 0)
                        {
                            text[i] = text[i].Remove(0, 1);
                            nuevo.Descripcion = text[i].Substring(0, text[i].IndexOf('\"'));
                            text[i] = text[i].Remove(0, text[i].IndexOf('\"') + 2);
                        }
                        else
                        {
                            nuevo.Descripcion = text[i].Substring(0, text[i].IndexOf(','));
                            text[i] = text[i].Substring(text[i].IndexOf(',') + 1);
                        }
                        if (text[i].IndexOf('\"') == 0)
                        {
                            text[i] = text[i].Remove(0, 1);
                            nuevo.Proyecto = text[i].Substring(0, text[i].IndexOf('\"'));
                            text[i] = text[i].Remove(0, text[i].IndexOf('\"') + 2);
                        }
                        else
                        {
                            nuevo.Proyecto = text[i].Substring(0, text[i].IndexOf(','));
                            text[i] = text[i].Substring(text[i].IndexOf(',') + 1);
                        }
                        nuevo.Entrega = DateTime.Parse(text[i].Substring(0, text[i].IndexOf(',')));
                        text[i] = text[i].Substring(text[i].IndexOf(',') + 1);
                        nuevo.Prioridad = Int32.Parse(text[i]);
                        Storage.Instance.infoTareas.Add(nuevo, tarea => tarea.Titulo);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void Sobreescribir()
        {
            try
            {
                List<InfoTarea> tareas = Storage.Instance.infoTareas.Items();
                string path = Server.MapPath("/Tareas.csv");
                using (StreamWriter lector = new StreamWriter(path, false))
                {
                    lector.Write("desarrollador,titulo,descripcion,proyecto,fecha de entrega,prioridad");
                    foreach (InfoTarea tarea in tareas)
                        lector.Write("\r\n" + tarea.String());
                }
            }
            catch
            {
            }
        }

        private void AgregarUsuario(string name, string password)
        {
            try
            {
                string path = Server.MapPath("/Usuarios.csv");
                using (StreamWriter lector = new StreamWriter(path, true))
                {
                    lector.Write("\r\n" + name + "," + password);
                }
            }
            catch
            {
            }
        }
    }
}
