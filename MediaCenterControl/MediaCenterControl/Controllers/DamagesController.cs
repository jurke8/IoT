using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MediaCenterControl.Models;
using MediaCenterControl.Context;

namespace MediaCenterControl.Controllers
{
    public class DamagesController : Controller
    {
        private ApplicationDBContext db = new ApplicationDBContext();

        // GET: Damages
        public ActionResult Index()
        {
            if (Session["UserId"] != null)
            {
                var userId = Guid.Parse(Session["UserId"].ToString());

                var damages = db.Damages
                    .Where(d => d.IsDeleted == false)
                    .Where(d => d.IsArchived == false)
                    .Where(d => d.UserCreated.Equals(userId))
                    .ToList();

                return View(damages);
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Damages/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Damage damage = db.Damages.Find(id);
            if (damage == null)
            {
                return HttpNotFound();
            }
            return View(damage);
        }

        // GET: Damages/Create
        public ActionResult Create()
        {
            ViewBag.CarBrands = new List<string> { "Volkswagen", "Ford", "BMW", "Mercedes", "Volvo", "Fiat", "Renault", "Škoda", "Peugeot" };
            ViewBag.ServiceCenters = new List<string> { "Zagreb", "Osijek", "Rijeka", "Split", "Dubrovnik" };

            return View();
        }

        // POST: Damages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CarBrand,CarModel,Description,ServiceCenter,ServiceDate")] Damage damage)
        {
            ViewBag.CarBrands = new List<string> { "Volkswagen", "Ford", "BMW", "Mercedes", "Volvo", "Fiat", "Renault", "Škoda", "Peugeot", "Audi" };
            ViewBag.ServiceCenters = new List<string> { "Zagreb", "Osijek", "Rijeka", "Split", "Dubrovnik" };

            if (damage.ServiceDate.HasValue && damage.ServiceDate.Value.Date < DateTime.Now.Date)
            {
                ModelState.AddModelError(string.Empty, string.Format("Datum servisa ne smije biti prije datuma unosa ({0:d.M.yyyy.})", DateTime.Now));
            }

            if (ModelState.IsValid)
            {
                damage.DateCreated = DateTime.Now;
                damage.UserCreated = Guid.Parse(Session["UserId"].ToString());

                db.Damages.Add(damage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(damage);
        }

        // GET: Damages/Edit/5
        public ActionResult Edit(Guid? id)
        {
            ViewBag.CarBrands = new List<string> { "Volkswagen", "Ford", "BMW", "Mercedes", "Volvo", "Fiat", "Renault", "Škoda", "Peugeot", "Audi" };
            ViewBag.ServiceCenters = new List<string> { "Zagreb", "Osijek", "Rijeka", "Split", "Dubrovnik" };

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Damage damage = db.Damages.Find(id);
            if (damage == null)
            {
                return HttpNotFound();
            }
            return View(damage);
        }

        // POST: Damages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CarBrand,CarModel,Description,ServiceCenter,ServiceDate,DateCreated,UserCreated,IsArchived,IsDeleted")] Damage damage)
        {
            ViewBag.CarBrands = new List<string> { "Volkswagen", "Ford", "BMW", "Mercedes", "Volvo", "Fiat", "Renault", "Škoda", "Peugeot", "Audi" };
            ViewBag.ServiceCenters = new List<string> { "Zagreb", "Osijek", "Rijeka", "Split", "Dubrovnik" };

            if (damage.ServiceDate.HasValue && damage.ServiceDate.Value.Date < damage.DateCreated.Date)
            {
                ModelState.AddModelError(string.Empty, string.Format("Datum servisa ne smije biti prije datuma unosa ({0:d.M.yyyy.})", damage.DateCreated));
            }

            if (ModelState.IsValid)
            {
                db.Entry(damage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(damage);
        }

        // GET: Damages/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Damage damage = db.Damages.Find(id);
            if (damage == null)
            {
                return HttpNotFound();
            }
            return View(damage);
        }

        // POST: Damages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Damage damage = db.Damages.Find(id);
            damage.IsDeleted = true;
            db.Entry(damage).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Damages/ToArchive/5
        public ActionResult ToArchive(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Damage damage = db.Damages.Find(id);

            if (damage == null)
            {
                return HttpNotFound();
            }
            return View(damage);
        }

        // POST: Damages/ToArchive/5
        [HttpPost, ActionName("ToArchive")]
        [ValidateAntiForgeryToken]
        public ActionResult ArchiveConfirmed(Guid id)
        {
            Damage damage = db.Damages.Find(id);

            if (damage.ServiceDate == null)
            {
                ModelState.AddModelError(string.Empty, "Kvar se ne može arhivirati jer vozilo nije popravljeno - nema datuma servisa.");
            }

            if (ModelState.IsValid)
            {
                damage.IsArchived = true;
                db.Entry(damage).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(damage);
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
