using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using etu_rehber.Models;
using System.Web.Security;


namespace etu_rehber.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {

        private etu_rehberEntities db = new etu_rehberEntities();
    
        // GET: Login
     
        public ActionResult Index()
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult Index(liste ad)
        {
            
            var bilgiler = db.liste.FirstOrDefault(x => x.mail == ad.mail && x.sifre == ad.sifre) ;
            if(bilgiler != null )
            {
                Session["user_id"] = bilgiler.admin_id;
                Session["birim_id"] = bilgiler.birim.birim_id;
                Session["fakulte_id"] = bilgiler.fakulte.fakulte_id;

                FormsAuthentication.SetAuthCookie(bilgiler.mail, false);
                Session["mail"] = bilgiler.mail.ToString();
                return RedirectToAction("Index", "Home");
            }

           
            else
            {
                ViewBag.Message = "Hatalı Giriş Yaptınız..!!";
                return View();
            }

            

            
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("/Index");
        }

        
        public ActionResult Admin()
        {
            ViewData["fakultee"] = db.fakulte.ToList();
            ViewData["bolumm"] = db.bolum.ToList();
            ViewData["adminn"] = db.admin.ToList();
            ViewData["listee"] = db.liste.ToList();
            ViewData["birimm"] = db.birim.ToList();
            ViewData["yetkii"] = db.yetki.ToList();
            return View();
        }

        
        public ActionResult Mühlogin()
        {
            ViewData["fakultee"] = db.fakulte.ToList();
            ViewData["bolumm"] = db.bolum.ToList();
            ViewData["adminn"] = db.admin.ToList();
            ViewData["listee"] = db.liste.ToList();
            ViewData["birimm"] = db.birim.ToList();
            ViewData["yetkii"] = db.yetki.ToList();
            return View();
        }
       
    }
}