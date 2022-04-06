﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn_LTW.Models;

namespace DoAn_LTW.Controllers
{
    public class NguoiDungController : Controller
    {
        Model db = new Model();
        // GET: NguoiDung
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(FormCollection collection, KhachHang kh)
        {
            var hoten = collection["hoten"];
            var email = collection["email"];
            var matkhau = collection["matkhau"];
            var MatKhauXacNhan = collection["MatKhauXacNhan"];
            var diachi = collection["diachi"];
            var dienthoai = collection["dienthoai"];
            var loaitv = collection["loaitv"];


            KhachHang check = db.KhachHang.SingleOrDefault(s => s.email == email.ToString());

            if (check == null)
            {
                ViewBag.ThongBao = "Tạo Tại Khoản Thành Công";
                if (String.IsNullOrEmpty(MatKhauXacNhan))
                {
                    ViewData["NhapMMKXN"] = "Phải nhập mật khẩu xác nhận!";
                }
                 else
                {
                    if (!matkhau.Equals(MatKhauXacNhan))
                    {
                    ViewData["MatKhauGiongNhau"] = "Mật khẩu và mật khẩu xác nhận phải giống nhau";
                    }
                    else
                     {
                        kh.hoten = hoten;
                        kh.email = email;
                        kh.matkhau = matkhau;
                        kh.diachi = diachi;
                        kh.dienthoai = dienthoai;
                        kh.loaitv = 2;


                        db.KhachHang.Add(kh);
                        db.SaveChanges();
                        return View();
                     }
                 }
            }
            ViewBag.ThongBao = " Email Đã Tồn Tại";
            return View();
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            string tendangnhap = collection["email"];
            string matkhau = collection["matkhau"];
            KhachHang kh = db.KhachHang.SingleOrDefault(n => n.email == tendangnhap && n.matkhau == matkhau);
            if (kh != null)
            {
                Session["email"] = kh;
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult DangXuat()
        {
            Session["email"] = null;
            return RedirectToAction("Index", "Home");
        }

    }
}