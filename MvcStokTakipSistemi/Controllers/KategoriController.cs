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
    public class KategoriController : Controller
    {
        MvcStokTakipSistemiEntities db = new MvcStokTakipSistemiEntities();
        // GET: Kategori
       
        public ActionResult Index(string aramametni,int sayfa=1)
        {
            return View("Index", db.TBLKATEGORI.Where(m => m.KATEGORIAD.Contains(aramametni) || aramametni == null).ToList().ToPagedList(sayfa, 4));
        }
        [HttpGet]
        public ActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Ekle(TBLKATEGORI p)
        {
            if (!ModelState.IsValid)
            {
                return View("Ekle");
            }
            db.TBLKATEGORI.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Sil(int id)
        {
            var kategori = db.TBLKATEGORI.Find(id);
            db.TBLKATEGORI.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Guncelle(int id)
        {
            var kategori = db.TBLKATEGORI.Find(id);
            return View("Guncelle", kategori);
        }
        public ActionResult GuncelleYap(TBLKATEGORI p)
        {
            var kategori = db.TBLKATEGORI.Find(p.KATEGORIID);
            kategori.KATEGORIAD = p.KATEGORIAD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}