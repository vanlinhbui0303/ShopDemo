using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopDemo.Models;
using PagedList.Mvc;
using PagedList;

namespace ShopDemo.Controllers
{
    
    public class TimKiemController : Controller
    {
        QLBanGiayEntities db = new QLBanGiayEntities();
        // GET: TimKiem
        [HttpPost]
        public ActionResult KetQuaTimKiem(FormCollection f ,int? page)
        {
            string sTuKhoa = f["txtTimKiem"].ToString();
            string sKieuTimKiem = f["txtKieuTimKiem"].ToString();


            //phân trang
            
            if (sKieuTimKiem =="tenSanPham")
            {
                List<HangHoa> listHH = db.HangHoas.Where(n => n.TenHH.Contains(sTuKhoa)).ToList();
                //phân trang
                int pageNumber = (page ?? 1);
                int pageSize = 12;
                return View(listHH.OrderBy(n=>n.TenHH).ToPagedList(pageNumber,pageSize));
            }
            if (sKieuTimKiem =="tenHang")
            {
                //phân trang
                int pageNumber = (page ?? 1);
                int pageSize = 12;
                List<HangHoa> listHH = db.HangHoas.Where(n => n.NhaCungCap.TenNCC.Contains(sTuKhoa)).ToList();
                return View(listHH.OrderBy(n => n.TenHH).ToPagedList(pageNumber, pageSize));
            }
            return View();
        }
    }
}