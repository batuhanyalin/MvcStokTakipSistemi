using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStokTakipSistemi.Models.Entity;
using PagedList.Mvc;
using PagedList;

namespace MvcStokTakipSistemi.Controllers
{
    public class UrunController : Controller
    {
        MvcStokTakipSistemiEntities db = new MvcStokTakipSistemiEntities();
        // GET: Urun


        public ActionResult Index()
        {
            var degerler = db.TBLURUN.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult Ekle()
        {
            List<SelectListItem> degerler = (from i in db.TBLKATEGORI.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,
                                                 Value = i.KATEGORIID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View();
        }
        [HttpPost]
        public ActionResult Ekle(TBLURUN p)
        {
            var ktg = db.TBLKATEGORI.Where(m => m.KATEGORIID == p.TBLKATEGORI.KATEGORIID).FirstOrDefault();
            p.TBLKATEGORI = ktg;
            db.TBLURUN.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Sil(int id)
        {
            var urun = db.TBLURUN.Find(id);
            db.TBLURUN.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Guncelle(int id)
        {
            var urun = db.TBLURUN.Find(id);
            List<SelectListItem> kategoriler = (from i in db.TBLKATEGORI.ToList()
                                                select new SelectListItem
                                                {
                                                    Text = i.KATEGORIAD,
                                                    Value = i.KATEGORIID.ToString()
                                                }).ToList();
            ViewBag.dgr = kategoriler;  
            return View("Guncelle", urun);
        }
        public ActionResult GuncellemeYap(TBLURUN p)
        {
            var urun = db.TBLURUN.Find(p.URUNID);
            urun.URUNAD = p.URUNAD;
            urun.MARKA = p.MARKA;
            var kategori = db.TBLKATEGORI.Where(m => m.KATEGORIID == p.TBLKATEGORI.KATEGORIID).FirstOrDefault();
            urun.URUNKATEGORI = kategori.KATEGORIID;
            urun.FIYAT = p.FIYAT;
            urun.STOK = p.STOK;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}