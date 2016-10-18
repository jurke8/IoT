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
    public class ArchiveController : Controller
    {
        private DamageDBContext db = new DamageDBContext();

        // GET: Archive
        public ActionResult Index()
        {
            var archivedDamages = db.Damages
                .Where(d => d.IsDeleted == false)
                .Where(d => d.IsArchived == true)
                .ToList();

            return View(archivedDamages);
        }

        // GET: Archive/Details/5
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
