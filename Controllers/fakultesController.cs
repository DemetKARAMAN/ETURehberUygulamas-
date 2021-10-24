using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using etu_rehber.Models;


namespace etu_rehber.Controllers
{
    [Authorize(Roles = "Seyfi Kalaycıoğlu")]
    public class fakultesController : Controller
    {
        private etu_rehberEntities db = new etu_rehberEntities();
         
        // GET: fakultes
        public ActionResult Index()
        {
            return View(db.fakulte.ToList());
        }

        
        public ActionResult fakulte(int id)
        {
            return View(db.bolum.Where(x => x.fak_id== id).ToList());
        }

        // GET: fakultes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fakulte fakulte = db.fakulte.Find(id);
            if (fakulte == null)
            {
                return HttpNotFound();
            }
            return View(fakulte);
        }

        // GET: fakultes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: fakultes/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "fakulte_id,fakulte_adi")] fakulte fakulte)
        {
            if (ModelState.IsValid)
            {
                db.fakulte.Add(fakulte);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fakulte);
        }

        // GET: fakultes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fakulte fakulte = db.fakulte.Find(id);
            if (fakulte == null)
            {
                return HttpNotFound();
            }
            return View(fakulte);
        }

        // POST: fakultes/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "fakulte_id,fakulte_adi")] fakulte fakulte)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fakulte).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fakulte);
        }

        // GET: fakultes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fakulte fakulte = db.fakulte.Find(id);
            if (fakulte == null)
            {
                return HttpNotFound();
            }
            return View(fakulte);
        }

        // POST: fakultes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            fakulte fakulte = db.fakulte.Find(id);
            db.fakulte.Remove(fakulte);
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
