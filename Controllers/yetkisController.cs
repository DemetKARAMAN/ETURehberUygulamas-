using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using etu_rehber;
using etu_rehber.Models;

namespace etu_rehber.Controllers
{
    [Authorize(Roles = "Seyfi Kalaycıoğlu")]
    public class yetkisController : Controller
    {
        private etu_rehberEntities db = new etu_rehberEntities();

        
        public ActionResult Index()
        {
            var yetki = db.yetki.Include(y => y.admin).Include(y => y.birim).Include(y => y.fakulte);
            return View(yetki.ToList());
        }

        // GET: yetkis/Details/5
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            yetki yetki = db.yetki.Find(id);
            if (yetki == null)
            {
                return HttpNotFound();
            }
            return View(yetki);
        }

        // GET: yetkis/Create
        public ActionResult Create()
        {
            ViewBag.admin_id = new SelectList(db.admin, "id", "adi");
            ViewBag.birim_id = new SelectList(db.birim, "birim_id", "birim_adi");
            ViewBag.fak_id = new SelectList(db.fakulte, "fakulte_id", "fakulte_adi");
            return View();
        }

        // POST: yetkis/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "yetki_id,admin_id,fak_id,birim_id")] yetki yetki)
        {
            if (ModelState.IsValid)
            {
                db.yetki.Add(yetki);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.admin_id = new SelectList(db.admin, "id", "adi", yetki.admin_id);
            ViewBag.birim_id = new SelectList(db.birim, "birim_id", "birim_adi", yetki.birim_id);
            ViewBag.fak_id = new SelectList(db.fakulte, "fakulte_id", "fakulte_adi", yetki.fak_id);
            return View(yetki);
        }

        // GET: yetkis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            yetki yetki = db.yetki.Find(id);
            if (yetki == null)
            {
                return HttpNotFound();
            }
            ViewBag.admin_id = new SelectList(db.admin, "id", "adi", yetki.admin_id);
            ViewBag.birim_id = new SelectList(db.birim, "birim_id", "birim_adi", yetki.birim_id);
            ViewBag.fak_id = new SelectList(db.fakulte, "fakulte_id", "fakulte_adi", yetki.fak_id);
            return View(yetki);
        }

        // POST: yetkis/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "yetki_id,admin_id,fak_id,birim_id")] yetki yetki)
        {
            if (ModelState.IsValid)
            {
                db.Entry(yetki).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.admin_id = new SelectList(db.admin, "id", "adi", yetki.admin_id);
            ViewBag.birim_id = new SelectList(db.birim, "birim_id", "birim_adi", yetki.birim_id);
            ViewBag.fak_id = new SelectList(db.fakulte, "fakulte_id", "fakulte_adi", yetki.fak_id);
            return View(yetki);
        }

        // GET: yetkis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            yetki yetki = db.yetki.Find(id);
            if (yetki == null)
            {
                return HttpNotFound();
            }
            return View(yetki);
        }

        // POST: yetkis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            yetki yetki = db.yetki.Find(id);
            db.yetki.Remove(yetki);
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
