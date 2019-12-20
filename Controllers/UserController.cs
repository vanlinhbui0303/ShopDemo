using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopDemo.Models;
namespace ShopDemo.Controllers
{
    
    public class UserController : Controller
    {
        QLBanGiayEntities db = new QLBanGiayEntities();
        // GET: User
        [HttpGet]
       public ActionResult DangKy(int id = 0)
        {
            ThanhVien tv = new ThanhVien();
            
            return View(tv);
        }
        [HttpPost]
        public ActionResult DangKy(ThanhVien tv)
        {
            using (QLBanGiayEntities db = new QLBanGiayEntities())
            {
                if (db.ThanhViens.Any(x =>x.TenDangNhap == tv.TenDangNhap))
                {
                    ViewBag.DuplicateMessage = "Tên Đăng Nhập đã tồn tại";
                    return View("DangKy", tv);
                }
                tv.MaQuyen = "2";
                db.ThanhViens.Add(tv);
                db.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccessMesage = "Đăng Ký Thành Công";
            

            return View("DangKy",new ThanhVien() );
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            string sTaiKhoan = f["txtTaiKhoan"].ToString();
            string sMatKhau = f.Get("txtMatKhau").ToString();
            ThanhVien tv = db.ThanhViens.SingleOrDefault(n => n.TenDangNhap == sTaiKhoan && n.MatKhau == sMatKhau);
            if (tv!=null)
            {
                ViewBag.ThongBaoThanhCong = "Đăng Nhập Thành Công";
                Session["ThanhVien"] = tv;
                ViewBag.ThongBaoDangNhap = "Đăng Nhập Thành Công";
                return View();
                //return RedirectToAction("Index", "Home");
            }
            ViewBag.ThongBao = "Tài Khoản hoặc Mật Khẩu không đúng";
            return View();
        }

       public ActionResult DangXuat(string strUrl) {
            Session["ThanhVien"] = null;
            return Redirect(strUrl);
        }
    }

}