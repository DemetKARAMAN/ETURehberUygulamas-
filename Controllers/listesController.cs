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
    public class listesController : Controller
    {
        private etu_rehberEntities db = new etu_rehberEntities();

        // GET: listes
        public ActionResult Index()
        {
            var liste = db.liste.Include(l => l.admin).Include(l => l.birim).Include(l => l.bolum).Include(l => l.fakulte);
            return View(liste.ToList());
        }

        // GET: listes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            liste liste = db.liste.Find(id);
            if (liste == null)
            {
                return HttpNotFound();
            }
            return View(liste);
        }

        // GET: listes/Create
        public ActionResult Create()
        {
            int user_id = Convert.ToInt32(Session["user_id"].ToString());
            ViewData["yetkiler_table"] = db.yetki.Where(x => x.admin_id == user_id);

            var adminList = from x in db.admin
                            where x.id == user_id
                            select x;

            var fakulteList = from x in db.liste
                              join z in db.yetki
                              on x.admin_id equals z.admin_id
                              join y in db.fakulte
                              on z.fak_id equals y.fakulte_id
                              where x.admin_id == user_id
                              select y;

            var birimList = from x in db.liste
                            join z in db.yetki
                            on x.admin_id equals z.admin_id
                            join y in db.birim
                            on z.birim_id equals y.birim_id
                            where x.admin_id == user_id
                            select y;

            ViewBag.admin_id = new SelectList(adminList, "id", "adi");
            ViewBag.birim_id = new SelectList(birimList, "birim_id", "birim_adi");
            ViewBag.fak_id = new SelectList(fakulteList, "fakulte_id", "fakulte_adi");
            ViewBag.bolum_id = new SelectList(db.bolum, "bolum_id", "bolum_adi");
            return View();
        }

        // POST: listes/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "liste_id,ad,soyad,tel,mail,gorevi,tarih,entity,admin_id,fak_id,birim_id,bolum_id")] liste liste)
        {
            if (ModelState.IsValid)
            {
                db.liste.Add(liste);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.admin_id = new SelectList(db.admin, "id", "adi", liste.admin_id);
            ViewBag.birim_id = new SelectList(db.birim, "birim_id", "birim_adi", liste.birim_id);
            ViewBag.bolum_id = new SelectList(db.bolum, "bolum_id", "bolum_adi", liste.bolum_id);
            ViewBag.fak_id = new SelectList(db.fakulte, "fakulte_id", "fakulte_adi", liste.fak_id);
            return View(liste);
        }

        public JsonResult getfakulteList(int? user_id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var fakulteList = from x in db.liste
                              join z in db.yetki
                              on x.admin_id equals z.admin_id
                              join y in db.fakulte
                              on z.fak_id equals y.fakulte_id
                              where x.admin_id == user_id
                              select y;
            return Json(fakulteList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getbirimList(int? user_id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var birimList = from x in db.liste
                            join z in db.yetki
                            on x.admin_id equals z.admin_id
                            join y in db.birim
                            on z.birim_id equals y.birim_id
                            where x.admin_id == user_id
                            select y;

            return Json(birimList, JsonRequestBehavior.AllowGet);
        }

        // GET: listes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            liste liste = db.liste.Find(id);
            if (liste == null)
            {
                return HttpNotFound();
            }
            ViewBag.admin_id = new SelectList(db.admin, "id", "adi", liste.admin_id);
            ViewBag.birim_id = new SelectList(db.birim, "birim_id", "birim_adi", liste.birim_id);
            ViewBag.bolum_id = new SelectList(db.bolum, "bolum_id", "bolum_adi", liste.bolum_id);
            ViewBag.fak_id = new SelectList(db.fakulte, "fakulte_id", "fakulte_adi", liste.fak_id);
            return View(liste);
        }

        // POST: listes/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "liste_id,ad,soyad,tel,mail,gorevi,tarih,entity,admin_id,fak_id,birim_id,bolum_id")] liste liste)
        {
            if (ModelState.IsValid)
            {
                db.Entry(liste).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.admin_id = new SelectList(db.admin, "id", "adi", liste.admin_id);
            ViewBag.birim_id = new SelectList(db.birim, "birim_id", "birim_adi", liste.birim_id);
            ViewBag.bolum_id = new SelectList(db.bolum, "bolum_id", "bolum_adi", liste.bolum_id);
            ViewBag.fak_id = new SelectList(db.fakulte, "fakulte_id", "fakulte_adi", liste.fak_id);
            return View(liste);
        }

        // GET: listes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            liste liste = db.liste.Find(id);
            if (liste == null)
            {
                return HttpNotFound();
            }
            return View(liste);
        }

        // POST: listes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            liste liste = db.liste.Find(id);
            db.liste.Remove(liste);
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
