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
    public class QuanLyNhaCungCapController : Controller
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
        // GET: Admin/QuanLyNhaCungCap
        public ActionResult Index(int? page)
        {
            if (kiemTraAdmin() == false)
            {
                ViewBag.ThongBaoCanDangNhap = "Cần Đăng Nhập để tiếp tục";
                return RedirectToAction("DangNhap", "UserAdmin");
            }

            int pageNumber = (page ?? 1);
            int pageSize = 10;

            return View(db.NhaCungCaps.ToList().ToPagedList(pageNumber, pageSize));
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
        public ActionResult ThemMoi(NhaCungCap ncc)
        {
            if (ncc.TenNCC == null)
            {
                ViewBag.ThongBaoLoi = "Không Được Để trống";
                return View();
            }
            if (ModelState.IsValid)
            {
                @ViewBag.Success = "Thêm Nhà Cung Cấp Thành Công";
                db.NhaCungCaps.Add(ncc);
                db.SaveChanges();
            }
            return View();
        }

        [HttpGet]
        public ActionResult Update(int mancc)
        {
            if (kiemTraAdmin() == false)
            {
                ViewBag.ThongBaoCanDangNhap = "Cần Đăng Nhập để tiếp tục";
                return RedirectToAction("DangNhap", "UserAdmin");
            }
            NhaCungCap ncc = db.NhaCungCaps.SingleOrDefault(n => n.MaNCC == mancc);
            if (ncc == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            

            return View(ncc);
        }
        [HttpPost]
        public ActionResult Update(NhaCungCap ncc)
        {
            if (ModelState.IsValid)
            {
                NhaCungCap nc = db.NhaCungCaps.SingleOrDefault(n => n.MaNCC == ncc.MaNCC);
                nc.TenNCC = ncc.TenNCC;
                db.SaveChanges();
                @ViewBag.Success = "Chỉnh Sửa Thành Công Thành Công";
                return View();
            }
            else
            {
                ViewBag.ThongBaoLoi = "Cập Nhập Thất Bại";
                return View();
            }

           
        }
        [HttpGet]
        public ActionResult Delete(int mancc)
        {
            if (kiemTraAdmin() == false)
            {
                ViewBag.ThongBaoCanDangNhap = "Cần Đăng Nhập để tiếp tục";
                return RedirectToAction("DangNhap", "UserAdmin");
            }
            NhaCungCap ncc = db.NhaCungCaps.SingleOrDefault(n => n.MaNCC == mancc);
          
            if (ncc == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(ncc);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult XacNhanXoa(int mancc)
        {
            NhaCungCap ncc = db.NhaCungCaps.SingleOrDefault(n => n.MaNCC == mancc);
            if (ncc == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.NhaCungCaps.Remove(ncc);
            db.SaveChanges();
            return RedirectToAction("Index", "QuanLyNhaCungCap");
        }



    }

   
}