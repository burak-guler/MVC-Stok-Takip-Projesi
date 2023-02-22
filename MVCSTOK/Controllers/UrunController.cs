using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
//entity yani veritabanının dosya yolunu verdik.
using MVCSTOK.Models.Entity;

namespace MVCSTOK.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        //veritabanından db adında bir nesne oluşturduk.
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index()
        {
            //urunler tablosunu liste şeklinde değerler degişkene atarak return view de terar çalışmasını sağladık.
            var degerler = db.TBLURUNLER.ToList();
            return View(degerler);
        }

        [HttpGet]   
        public ActionResult Yeniurun()
        {
            //dropdownlist e kategorileri yazdırmak için linq sorgusu kullandık.
            List<SelectListItem> degerler = ( from i in db.TBLKATEGORILER.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = i.KATEGORIAD,
                                                  Value = i.KATEGORIID.ToString()
                                              }).ToList();
            ViewBag.dgr = degerler;
            return View();  
        }
        [HttpPost]  
        public ActionResult Yeniurun( TBLURUNLER p1)
        {
            var ktg = db.TBLKATEGORILER.Where(m=>m.KATEGORIID == p1.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
            p1.TBLKATEGORILER = ktg;
            db.TBLURUNLER.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SIL(int id)
        {
            var urun = db.TBLURUNLER.Find(id);
            db.TBLURUNLER.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("index");
        }

        public ActionResult UrunGetir(int id)
        {
            var urun = db.TBLURUNLER.Find(id);

            //dropdownlist e kategorileri yazdırmak için linq sorgusu kullandık.
            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,
                                                 Value = i.KATEGORIID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;

            return View("UrunGetir", urun);
        }

        public ActionResult Guncelle(TBLURUNLER p)
        {
            var urun = db.TBLURUNLER.Find(p.URUNID);
            urun.URUNAD = p.URUNAD;
            urun.MARKA = p.MARKA;   
            urun.STOK = p.STOK; 
            urun.FIYAT = p.FIYAT;
            // urun.URUNKATEGORI = p.URUNKATEGORI;
            var ktg = db.TBLKATEGORILER.Where(m => m.KATEGORIID == p.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
            urun.URUNKATEGORI = ktg.KATEGORIID;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}