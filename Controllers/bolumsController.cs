using etu_rehber.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace etu_rehber.Controllers
{

    [Authorize(Roles = "Seyfi Kalaycıoğlu")]
    public class bolumsController : Controller
    {
        private etu_rehberEntities db = new etu_rehberEntities();

        // GET: bolums
        public ActionResult Index()
        {
            
            var bolum = db.bolum.Include(b => b.fakulte);
            return View(bolum.ToList());
        }

       
        // GET: bolums/Create
        public ActionResult Create()
        {
            ViewBag.fak_id = new SelectList(db.fakulte, "fakulte_id", "fakulte_adi");
            return View();
        }

        // POST: bolums/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "bolum_id,bolum_adi,fak_id")] bolum bolum)
        {
            if (ModelState.IsValid)
            {
                db.bolum.Add(bolum);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.fak_id = new SelectList(db.fakulte, "fakulte_id", "fakulte_adi", bolum.fak_id);
            return View(bolum);
        }


        // GET: bolums/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bolum bolum = db.bolum.Find(id);
            if (bolum == null)
            {
                return HttpNotFound();
            }
            return View(bolum);
        }

        // GET: bolums/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bolum bolum = db.bolum.Find(id);
            if (bolum == null)
            {
                return HttpNotFound();
            }
            ViewBag.fak_id = new SelectList(db.fakulte, "fakulte_id", "fakulte_adi", bolum.fak_id);
            return View(bolum);
        }

        // POST: bolums/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "bolum_id,bolum_adi,fak_id")] bolum bolum)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bolum).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fak_id = new SelectList(db.fakulte, "fakulte_id", "fakulte_adi", bolum.fak_id);
            return View(bolum);
        }

        // GET: bolums/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bolum bolum = db.bolum.Find(id);
            if (bolum == null)
            {
                return HttpNotFound();
            }
            return View(bolum);
        }

        // POST: bolums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bolum bolum = db.bolum.Find(id);
            db.bolum.Remove(bolum);
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
