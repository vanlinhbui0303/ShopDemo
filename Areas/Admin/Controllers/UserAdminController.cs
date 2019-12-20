using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopDemo.Models;



namespace ShopDemo.Areas.Admin.Controllers
{
    public class UserAdminController : Controller
    {
        QLBanGiayEntities db = new QLBanGiayEntities();
        // GET: Admin/UserAdmin
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            string sATaiKhoan = f["txtAdminTaiKhoan"].ToString();
            string sAMatKhau = f.Get("txtAdminMatKhau").ToString();
            Models.Admin ad = db.Admins.SingleOrDefault(n => n.TenDN == sATaiKhoan && n.MatKhau == sAMatKhau);
            if (ad != null)
            {
                ViewBag.ThongBaoThanhCong = "Đăng Nhập Thành Công";
                Session["AdminTV"] = ad;
                ViewBag.ThongBaoDangNhap = "Đăng Nhập Thành Công";
              
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ThongBao = "Tài Khoản hoặc Mật Khẩu không đúng";
            return View();
        }
        public ActionResult DangXuat(string strUrl)
        {
            Session["AdminTV"] = null;
            return Redirect(strUrl);
        }
    }
}