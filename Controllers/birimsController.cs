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
    
    public class birimsController : Controller
    {
        
        private etu_rehberEntities db = new etu_rehberEntities();
        [Authorize(Roles = "Seyfi Kalaycıoğlu")]
        // GET: birims
        public ActionResult Index()
        {
            return View(db.birim.ToList());
        }
        [Authorize(Roles = " Seyfi Kalaycıoğlu,Adem Tanas,Hasan Ali,Ahmet Yıldırım,Rasim Yıldırım,İlyas Arınç")]
        public ActionResult birimler(int? id)
        {
            
            ViewData["birimm"] = db.birim.ToList();
            ViewData["liste"] = db.liste.ToList();
            return View(db.liste.Where(x=>x.birim_id==id).ToList());

        }
        [Authorize(Roles = " Seyfi Kalaycıoğlu,Adem Tanas,Hasan Ali,Ahmet Yıldırım,Rasim Yıldırım,İlyas Arınç")]
        // GET: birims/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            birim birim = db.birim.Find(id);
            if (birim == null)
            {
                return HttpNotFound();
            }
            return View(birim);
        }
        [Authorize(Roles = " Seyfi Kalaycıoğlu,Adem Tanas,Hasan Ali,Ahmet Yıldırım,Rasim Yıldırım,İlyas Arınç")]
        // GET: birims/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: birims/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = " Seyfi Kalaycıoğlu,Adem Tanas,Hasan Ali,Ahmet Yıldırım,Rasim Yıldırım,İlyas Arınç")]
        public ActionResult Create([Bind(Include = "birim_id,birim_adi")] birim birim)
        {
            if (ModelState.IsValid)
            {
                db.birim.Add(birim);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(birim);
        }
        [Authorize(Roles = "Seyfi Kalaycıoğlu,Adem Tanas,Hasan Ali,Ahmet Yıldırım,Rasim Yıldırım,İlyas Arınç")]
        // GET: birims/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            birim birim = db.birim.Find(id);
            if (birim == null)
            {
                return HttpNotFound();
            }
            return View(birim);
        }
        [Authorize(Roles = " Seyfi Kalaycıoğlu,Adem Tanas,Hasan Ali,Ahmet Yıldırım,Rasim Yıldırım,İlyas Arınç")]
        // POST: birims/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "birim_id,birim_adi")] birim birim)
        {
            if (ModelState.IsValid)
            {
                db.Entry(birim).State = (System.Data.Entity.EntityState)System.Data.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(birim);
        }
        [Authorize(Roles = "Seyfi Kalaycıoğlu,Adem Tanas,Hasan Ali,Ahmet Yıldırım,Rasim Yıldırım,İlyas Arınç")]
        // GET: birims/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            birim birim = db.birim.Find(id);
            if (birim == null)
            {
                return HttpNotFound();
            }
            return View(birim);
        }
        [Authorize(Roles = "Seyfi Kalaycıoğlu,Adem Tanas,Hasan Ali,Ahmet Yıldırım,Rasim Yıldırım,İlyas Arınç")]
        // POST: birims/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            birim birim = db.birim.Find(id);
            db.birim.Remove(birim);
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
