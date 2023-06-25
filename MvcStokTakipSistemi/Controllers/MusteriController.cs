using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStokTakipSistemi.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcStokTakipSistemi.Controllers
{
    public class MusteriController : Controller
    {
        MvcStokTakipSistemiEntities db = new MvcStokTakipSistemiEntities();

        // GET: Musteri

        public ActionResult Index(string aramametni, int sayfa = 1)
        {
            return View("Index", db.TBLMUSTERI.Where(m => m.MUSTERIAD.Contains(aramametni) || aramametni == null).ToList().ToPagedList(sayfa, 4));
        }
        [HttpGet]
        public ActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Ekle(TBLMUSTERI p)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            db.TBLMUSTERI.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sil(int id)
        {
            var musteri = db.TBLMUSTERI.Find(id);
            db.TBLMUSTERI.Remove(musteri);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Guncelle(int id)
        {
            var musteri = db.TBLMUSTERI.Find(id);
            return View("Guncelle", musteri);
        }
        public ActionResult GuncelleYap(TBLMUSTERI p)
        {
            var musteri = db.TBLMUSTERI.Find(p.MUSTERIID);
            musteri.MUSTERIAD = p.MUSTERIAD;
            musteri.MUSTERISOYAD = p.MUSTERISOYAD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}