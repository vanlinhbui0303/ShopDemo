using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopDemo.Models;
using PagedList;
using PagedList.Mvc;

namespace ShopDemo.Controllers
{
    public class GiayController : Controller
    {
        // GET: Giay
        QLBanGiayEntities db = new QLBanGiayEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ViewResult XemChiTiet( String MaHH="0")
        {
            HangHoa giay = db.HangHoas.SingleOrDefault(n => n.MaHH == MaHH);
            if (giay ==null)
            {
                // trả về trang báo lỗi
                ViewBag.HangHoa = "Không tìm thấy sản phẩm";
                return null;
            }
            if (TempData["ThongBao"]!=null)
            {
                ViewBag.ThongBao= TempData["ThongBao"].ToString();
            }
            return View(giay);
        }
        public ViewResult SanPham(int? page) 
        {
            //tạo biến số sản phầm trên trang 
            int pageSize = 12;
            //tạo biến số  trang
            int pageNumber = (page ?? 1);
            return View(db.HangHoas.ToList().ToPagedList(pageNumber ,pageSize));        
        }

        public ViewResult GiayTheoNhaCC(int mancc = 0)
        {
            NhaCungCap ncc = db.NhaCungCaps.SingleOrDefault(n => n.MaNCC == mancc);
               if(ncc==null) {
                ViewBag.hanghoa = "Không có sản phẩm nào";
                return null;
            }
            List<HangHoa> lstHangHoa = db.HangHoas.Where(n => n.MaNCC == mancc).OrderBy(n => n.TenHH).ToList();
            if (lstHangHoa.Count==0)
            {
                ViewBag.hanghoa = "Không tìm thấy sản phẩm";
            }

            return View(lstHangHoa);
        }
        public ViewResult GiayTheoDanhMuc(int madm = 0)
        {
            DanhMuc dm = db.DanhMucs.SingleOrDefault(n => n.MaDM == madm);
            if (dm == null)
            {
                ViewBag.hanghoa = "Không có sản phẩm nào";
             
                return null;
            }
            List<HangHoa> lstHangHoa = db.HangHoas.Where(n => n.MaDM == madm).OrderBy(n => n.TenHH).ToList();
            if (lstHangHoa.Count == 0)
            {
                ViewBag.hanghoa = "Không tìm thấy sản phẩm";
            }

            return View(lstHangHoa);
        }

        public PartialViewResult GiayTT(String MaHH = "0")
        {
            HangHoa giay = db.HangHoas.SingleOrDefault(n => n.MaHH == MaHH);
            int madm = giay.MaDM;
            List<HangHoa> lstHangHoa = db.HangHoas.Where(n => n.MaDM == madm).OrderBy(n => n.TenHH).ToList();
            if (lstHangHoa.Count == 0)
            {
                ViewBag.hanghoa = "Không tìm thấy sản phẩm tương tự";
            }

            return PartialView(lstHangHoa);
        }
    }
}