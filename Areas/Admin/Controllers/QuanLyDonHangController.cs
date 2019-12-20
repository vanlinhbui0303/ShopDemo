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
    
    public class QuanLyDonHangController : Controller
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
        // GET: Admin/QuanLyDonHang
        public ActionResult Index(int? page)
        {
            if (kiemTraAdmin() == false)
            {
                ViewBag.ThongBaoCanDangNhap = "Cần Đăng Nhập để tiếp tục";
                return RedirectToAction("DangNhap", "UserAdmin");
            }
            int pageNumber = (page ?? 1);
            int pageSize = 10;

            return View(db.DonHangs.OrderBy(n=>n.TrangThai).ToList().ToPagedList(pageNumber, pageSize));
        }
        
        [HttpGet]
        public ActionResult XemChiTiet(int madh)
        {
            if (kiemTraAdmin() == false)
            {
                ViewBag.ThongBaoCanDangNhap = "Cần Đăng Nhập để tiếp tục";
                return RedirectToAction("DangNhap", "UserAdmin");
            }
            List<ChiTietDonHang> ctdh = new List<ChiTietDonHang>();
            foreach (var item in db.ChiTietDonHangs)
            {
                if (item.MaDH==madh)
                {
                    ctdh.Add(item);
                }
            }
            
            return View(ctdh);

        }
        [HttpPost]
        public ActionResult XacNhan(int mactdh)
        {
            DonHang dh = db.DonHangs.SingleOrDefault(n => n.MaDH == mactdh);
            if (dh==null)
            {
                ViewBag.ThongBaoLoi = "Xác nhận Đơn Hàng Thất Bại";

                return View();
            }
            dh.TrangThai = (int)2;
            db.SaveChanges();
            ViewBag.ThongBao = "Xác Nhận Đơn Hàng Thành Công";
            return RedirectToAction("Index", "QuanLyDonHang");

        }
        [HttpGet]
        public ActionResult Delete(int madh)
        {
            if (kiemTraAdmin() == false)
            {
                ViewBag.ThongBaoCanDangNhap = "Cần Đăng Nhập để tiếp tục";
                return RedirectToAction("DangNhap", "UserAdmin");
            }
            DonHang dh = db.DonHangs.SingleOrDefault(n => n.MaDH == madh);

            if (dh == null)
            { 
                Response.StatusCode = 404;
                return null;
            }
            List<ChiTietDonHang> ctdh = new List<ChiTietDonHang>();
            foreach (var item in db.ChiTietDonHangs)
            {
                if (item.MaDH == madh)
                {
                    ctdh.Add(item);
                }
            }

            return View(ctdh);
        }

        [HttpPost]
        public ActionResult XacNhanXoa(int mactdh)
        {
            DonHang dh = db.DonHangs.SingleOrDefault(n => n.MaDH == mactdh);

            if (dh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
            {
                List<ChiTietDonHang> ctdh = new List<ChiTietDonHang>();
                foreach (var item in db.ChiTietDonHangs)
                {
                    if (item.MaDH == dh.MaDH)
                    {
                        ctdh.Remove(item);
                        db.SaveChanges();
                    }
                }
                db.DonHangs.Remove(dh);
                db.SaveChanges();
                return RedirectToAction("Index", "QuanLyDonHang");
            }
        }
    }
}