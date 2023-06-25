using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStokTakipSistemi.Models.Entity;

namespace MvcStokTakipSistemi.Controllers
{
    public class SatisController : Controller
    {
        MvcStokTakipSistemiEntities db = new MvcStokTakipSistemiEntities();
        // GET: Satis
        [HttpGet]
        public ActionResult Index()
        {
            List<SelectListItem> urn = (from i in db.TBLURUN.ToList()
                                        select new SelectListItem
                                        {
                                            Text = i.URUNAD,
                                            Value = i.URUNID.ToString()
                                        }).ToList();
            List<SelectListItem> mst = (from x in db.TBLMUSTERI.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.MUSTERIAD,
                                            Value = x.MUSTERIID.ToString()
                                        }).ToList();
            ViewBag.urun = urn;
            ViewBag.musteri = mst;
            return View();
        }

        [HttpPost]
        public ActionResult Index(TBLSATIS p)
        {
            var mstr = db.TBLMUSTERI.Where(m => m.MUSTERIID == p.TBLMUSTERI.MUSTERIID).FirstOrDefault();
            p.TBLMUSTERI = mstr;
            var urn = db.TBLURUN.Where(m => m.URUNID == p.TBLURUN.URUNID).FirstOrDefault();        
            p.TBLURUN = urn;
            db.TBLSATIS.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}