using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CochesCutresApp.Models;

namespace CochesCutresApp.Controllers
{
    public class VehiculosController : Controller
    {
        private CochesCutresEntities db = new CochesCutresEntities();

        // GET: Vehiculos
        public ActionResult Index()
        {
            var vehiculos = db.Vehiculos.Include(v => v.TipoVehiculos);
            return View(vehiculos.ToList());
        }

        // GET: Vehiculos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehiculos vehiculos = db.Vehiculos.Find(id);
            if (vehiculos == null)
            {
                return HttpNotFound();
            }
            return View(vehiculos);
        }

        // GET: Vehiculos/Create
        public ActionResult Create()
        {
            ViewBag.TipoID = new SelectList(db.TipoVehiculos, "TipoID", "Tipo");
            return View();
        }

        // POST: Vehiculos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VehiculoId,Marca,Modelo,Puertas,Color,TipoID,MesesGarantia,Stock,Foto")] Vehiculos vehiculos)
        {
            if (ModelState.IsValid)
            {
                db.Vehiculos.Add(vehiculos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TipoID = new SelectList(db.TipoVehiculos, "TipoID", "Tipo", vehiculos.TipoID);
            return View(vehiculos);
        }

        // GET: Vehiculos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehiculos vehiculos = db.Vehiculos.Find(id);
            if (vehiculos == null)
            {
                return HttpNotFound();
            }
            ViewBag.TipoID = new SelectList(db.TipoVehiculos, "TipoID", "Tipo", vehiculos.TipoID);
            return View(vehiculos);
        }

        // POST: Vehiculos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VehiculoId,Marca,Modelo,Puertas,Color,TipoID,MesesGarantia,Stock,Foto")] Vehiculos vehiculos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehiculos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TipoID = new SelectList(db.TipoVehiculos, "TipoID", "Tipo", vehiculos.TipoID);
            return View(vehiculos);
        }

        // GET: Vehiculos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehiculos vehiculos = db.Vehiculos.Find(id);
            if (vehiculos == null)
            {
                return HttpNotFound();
            }
            return View(vehiculos);
        }

        // POST: Vehiculos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vehiculos vehiculos = db.Vehiculos.Find(id);
            db.Vehiculos.Remove(vehiculos);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
