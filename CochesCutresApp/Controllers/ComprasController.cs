using CochesCutresApp.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CochesCutresApp.Controllers
{
    public class ComprasController : Controller
    {
        private CochesCutresEntities db = new CochesCutresEntities();

        // GET: Compras
        public ActionResult Index()
        {
            var compras = db.Compras.Include(c => c.Clientes).Include(c => c.Empleados).Include(c => c.TipoCompraVenta).Include(c => c.Vehiculos);
            return View(compras.ToList());
        }

        public ActionResult Search(string search)
        {
            var compras = db.Compras.Include(c => c.Clientes).Include(c => c.Empleados).Include(c => c.TipoCompraVenta).Include(c => c.Vehiculos);
            return View(compras.Where(x => x.Clientes.Nombre.StartsWith(search) || search == null).ToList());
        }

        // GET: Compras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compras compras = db.Compras.Find(id);
            if (compras == null)
            {
                return HttpNotFound();
            }
            return View(compras);
        }

        // GET: Compras/Create
        public ActionResult Create()
        {
            ViewBag.ClienteId = new SelectList(db.Clientes, "ClienteId", "NIF");
            ViewBag.EmpleadoId = new SelectList(db.Empleados, "EmpleadoId", "NIF");
            ViewBag.TipoID = new SelectList(db.TipoCompraVenta, "TipoId", "TipoCompraVenta1");
            ViewBag.VehiculoID = new SelectList(db.Vehiculos, "VehiculoId", "Marca");
            return View();
        }

        // POST: Compras/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CompraId,Fecha,TipoID,EmpleadoId,ClienteId,Precio,VehiculoID")] Compras compras)
        {
            if (ModelState.IsValid)
            {
                db.Compras.Add(compras);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClienteId = new SelectList(db.Clientes, "ClienteId", "NIF", compras.ClienteId);
            ViewBag.EmpleadoId = new SelectList(db.Empleados, "EmpleadoId", "NIF", compras.EmpleadoId);
            ViewBag.TipoID = new SelectList(db.TipoCompraVenta, "TipoId", "TipoCompraVenta1", compras.TipoID);
            ViewBag.VehiculoID = new SelectList(db.Vehiculos, "VehiculoId", "Marca", compras.VehiculoID);
            return View(compras);
        }

        // GET: Compras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compras compras = db.Compras.Find(id);
            if (compras == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClienteId = new SelectList(db.Clientes, "ClienteId", "NIF", compras.ClienteId);
            ViewBag.EmpleadoId = new SelectList(db.Empleados, "EmpleadoId", "NIF", compras.EmpleadoId);
            ViewBag.TipoID = new SelectList(db.TipoCompraVenta, "TipoId", "TipoCompraVenta1", compras.TipoID);
            ViewBag.VehiculoID = new SelectList(db.Vehiculos, "VehiculoId", "Marca", compras.VehiculoID);
            return View(compras);
        }

        // POST: Compras/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CompraId,Fecha,TipoID,EmpleadoId,ClienteId,Precio,VehiculoID")] Compras compras)
        {
            if (ModelState.IsValid)
            {
                db.Entry(compras).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClienteId = new SelectList(db.Clientes, "ClienteId", "NIF", compras.ClienteId);
            ViewBag.EmpleadoId = new SelectList(db.Empleados, "EmpleadoId", "NIF", compras.EmpleadoId);
            ViewBag.TipoID = new SelectList(db.TipoCompraVenta, "TipoId", "TipoCompraVenta1", compras.TipoID);
            ViewBag.VehiculoID = new SelectList(db.Vehiculos, "VehiculoId", "Marca", compras.VehiculoID);
            return View(compras);
        }

        // GET: Compras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compras compras = db.Compras.Find(id);
            if (compras == null)
            {
                return HttpNotFound();
            }
            return View(compras);
        }

        // POST: Compras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Compras compras = db.Compras.Find(id);
            db.Compras.Remove(compras);
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
