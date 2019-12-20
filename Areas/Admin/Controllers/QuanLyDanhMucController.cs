using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopDemo.Models;
using PagedList.Mvc;
using PagedList;

namespace ShopDemo.Areas.Admin.Controllers
{
    public class QuanLyDanhMucController : Controller
    {
        public Boolean kiemTraAdmin()
        {
            if (Session["AdminTV"] == null || Session["AdminTV"].ToString() == "")
            {
                return false;
            }
            else return true;
        }
        QLBanGiayEntities db = new QLBanGiayEntities();
        // GET: Admin/QuanLyDanhMuc
        public ActionResult Index(int? page)
        {
            if (kiemTraAdmin() == false)
            {
                ViewBag.ThongBaoCanDangNhap = "Cần Đăng Nhập để tiếp tục";
                return RedirectToAction("DangNhap", "UserAdmin");
            }
            int pageNumber = (page ?? 1);
            int pageSize = 10;

            return View(db.DanhMucs.ToList().ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult ThemMoi()
        {
            if (kiemTraAdmin() == false)
            {
                ViewBag.ThongBaoCanDangNhap = "Cần Đăng Nhập để tiếp tục";
                return RedirectToAction("DangNhap", "UserAdmin");
            }
            return View();
        }
        public ActionResult ThemMoi(DanhMuc dm)
        {
            if (kiemTraAdmin() == false)
            {
                ViewBag.ThongBaoCanDangNhap = "Cần Đăng Nhập để tiếp tục";
                return RedirectToAction("DangNhap", "UserAdmin");
            }
            if (dm.TenDM == null)
            {
                ViewBag.ThongBaoLoi = "Không Được Để trống";
                return View();
            }
            if (ModelState.IsValid)
            {
                @ViewBag.Success = "Thêm Danh Mục Thành Công";
                db.DanhMucs.Add(dm);
                db.SaveChanges();
            }
            return View();
        }


        [HttpGet]
        public ActionResult Update(int madm)
        {
            if (kiemTraAdmin() == false)
            {
                ViewBag.ThongBaoCanDangNhap = "Cần Đăng Nhập để tiếp tục";
                return RedirectToAction("DangNhap", "UserAdmin");
            }
            DanhMuc dm = db.DanhMucs.SingleOrDefault(n => n.MaDM == madm);
            if (dm == null)
            {
                Response.StatusCode = 404;
                return null;
            }


            return View(dm);
        }
        [HttpPost]
        public ActionResult Update(DanhMuc dm)
        {
            if (ModelState.IsValid)
            {
                DanhMuc dmm = db.DanhMucs.SingleOrDefault(n => n.MaDM == dm.MaDM);
                
                dmm.TenDM = dm.TenDM;
                db.SaveChanges();
                @ViewBag.Success = "Cập Nhập Thành Công Thành Công";
                return View();
            }
            else
            {
                ViewBag.ThongBaoLoi = "Cập Nhập Thất Bại";
                return View();
            }
        }

        [HttpGet]
        public ActionResult Delete(int madm)
        {
            if (kiemTraAdmin() == false)
            {
                ViewBag.ThongBaoCanDangNhap = "Cần Đăng Nhập để tiếp tục";
                return RedirectToAction("DangNhap", "UserAdmin");
            }
            DanhMuc dmm = db.DanhMucs.SingleOrDefault(n => n.MaDM == madm);
            

            if (dmm == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(dmm);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult XacNhanXoa(int madm)
        {
            DanhMuc dmm = db.DanhMucs.SingleOrDefault(n => n.MaDM ==madm);

           
            if (dmm == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.DanhMucs.Remove(dmm);
            db.SaveChanges();
            return RedirectToAction("Index", "QuanLyDanhMuc");
        }

    }
}