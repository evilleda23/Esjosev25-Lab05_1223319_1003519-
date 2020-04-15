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
                    LlenarTablaHash();
                    Storage.Instance.PrimeraSesion = false;
                }
                CargarDatos();
                if (Storage.Instance.admin)
                    return View();
                else
                    return View();
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
            return View();
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
            return View();
        }

        private bool UsuarioExiste(string name, string password)
        {
            return false;
        }

        private void CargarDatos()
        {
            if (Storage.Instance.admin)
            {

            }
            else
            {

            }
        }

        private void LlenarTablaHash()
        {
            Tareas nuevo = new Tareas();
            Storage.Instance.infoTareas.Add(nuevo, tarea => (tarea.Titulo.Length * 7) % 20);
        }
    }
}
