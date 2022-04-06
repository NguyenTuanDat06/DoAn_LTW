using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn_LTW.Models;

namespace DoAn_LTW.Controllers
{
    public class SanPhamController : Controller
    {
        // GET: SanPham
        Model dbThucAn = new Model();
        public ActionResult Index()
        {
            var listThucAn = dbThucAn.ThucAn.ToList();
            return View(listThucAn);
        }
        public ActionResult SanPhamList()
        {
            return PartialView();
        }
        public ActionResult Detail(int id)
        {
            var D_thucAn = dbThucAn.ThucAn.Where(m => m.mathucan == id).First();
            return View(D_thucAn);
        }

        public ActionResult Edit(int id)
        {
            var E_Sach = dbThucAn.ThucAn.First(m => m.mathucan == id);
            return View(E_Sach);

        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var E_rubik = dbThucAn.ThucAn.First(m => m.mathucan == id);
            var E_maloai = Convert.ToInt32(collection["maloai"]);
            var E_ten = collection["tenthucan"];
            var E_mota = collection["mota"];
            var E_gia = Convert.ToDecimal(collection["giaban"]);
            var E_hinh = collection["hinh"];
            var E_soluongton = Convert.ToInt32(collection["soluongton"]);
            E_rubik.mathucan = id;
            if (string.IsNullOrEmpty(E_ten))
            {
                ViewData["Error"] = "Don't empty";
            }
            else
            {
                E_rubik.tenthucan = E_ten.ToString();
                E_rubik.mota = E_mota.ToString();
                E_rubik.giaban = E_gia;
                E_rubik.hinh = E_hinh.ToString();
                E_rubik.soluongton = E_soluongton;
                UpdateModel(E_rubik);
                dbThucAn.SaveChanges();

                return RedirectToAction("Index","Home");
            }
            return this.Edit(id);
        }
        public string ProcessUpload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return "";
            }
            file.SaveAs(Server.MapPath("~/Content/img/" +
            file.FileName));
            return "/Content/img/" + file.FileName;
        }

        public ActionResult Delete(int id)
        {
            var E_Sach = dbThucAn.ThucAn.First(m => m.mathucan == id);
            return View(E_Sach);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var D_Sach = dbThucAn.ThucAn.Where(m => m.mathucan == id).First();
            dbThucAn.ThucAn.Remove(D_Sach);
            dbThucAn.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Create()
        {
            ThucAn objCourse = new ThucAn();
            objCourse.lstTheLoai = dbThucAn.TheLoai.ToList();

            return View(objCourse);
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, ThucAn s)
        {
            var E_tensach = collection["tenthucan"];
            var E_maloai = Convert.ToInt32(collection["maloai"]);
            var E_mota = collection["mota"];
            var E_hinh = collection["hinh"];
            var E_giaban = Convert.ToDecimal(collection["giaban"]); 
            var E_soluongton = Convert.ToInt32(collection["soluongton"]); if (string.IsNullOrEmpty(E_tensach))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                s.tenthucan = E_tensach.ToString();
                s.hinh = E_hinh.ToString();
                s.giaban = E_giaban;
                s.soluongton = E_soluongton;
                dbThucAn.ThucAn.Add(s);
                dbThucAn.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return this.Create();
        }
    }
}