using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;


namespace ShopDemo.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public Boolean kiemTraAdmin()
        {
            if (Session["AdminTV"] == null || Session["AdminTV"].ToString() == "")
            {
                return false;
            }
            else return true;
        }
        public ActionResult Index()
        {

            if (kiemTraAdmin() == false)
            {
                ViewBag.ThongBaoCanDangNhap = "Cần Đăng Nhập để tiếp tục";
                return RedirectToAction("DangNhap", "UserAdmin");
            }
            return View();
        }
    }

    
}