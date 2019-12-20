using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopDemo.Models;


namespace ShopDemo.Controllers
{
    public class HomeController : Controller
    {
        QLBanGiayEntities db = new QLBanGiayEntities();
        
        // GET: Home
        public ActionResult Index()
        {
          
           
            return View(db.HangHoas.ToList());
        }
        public PartialViewResult HeaderBotPartial()
        {
          
            return PartialView(db.DanhMucs.ToList());
        }
        public PartialViewResult HeaderMidPartial()
        {

            return PartialView(db.NhaCungCaps.ToList());
        }
      public PartialViewResult SidebarLeftPartial()
        {

            //var listNhaCungCap = from hhs in db.HangHoas
            //                     join nccs in db.NhaCungCaps on hhs.MaNCC equals nccs.MaNCC
            //                     select new
            //                     {
            //                         TenNcc = nccs.TenNCC,
            //                         MaNcc = nccs.MaNCC,
            //                         Mahh = hhs.MaHH
            //                     };
                return PartialView(db.NhaCungCaps.ToList());
        }
        public PartialViewResult HeaderTopPartial()
        {
            
                ThanhVien tv = Session["ThanhVien"] as ThanhVien;

                return PartialView(tv);
            
        }

    }
}