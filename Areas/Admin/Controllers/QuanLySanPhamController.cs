using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopDemo.Models;
using PagedList.Mvc;
using PagedList;
using System.IO;

namespace ShopDemo.Areas.Admin.Controllers
{
    public class QuanLySanPhamController : Controller
    {
        QLBanGiayEntities db = new QLBanGiayEntities();

        //kiểm tra đăng nhập admin
        public Boolean kiemTraAdmin()
        {
            if (Session["AdminTV"] == null || Session["AdminTV"].ToString() == "")
            {
                return false;
            }
            else return true;
        }
        // GET: Admin/QuanLySanPham
        public ActionResult Index(int? page)
        {
            if (kiemTraAdmin() == false)
            {
                ViewBag.ThongBaoCanDangNhap = "Cần Đăng Nhập để tiếp tục";
                return RedirectToAction("DangNhap", "UserAdmin");
            }
            int pageNumber = (page ?? 1);
            int pageSize = 10;

            return View(db.HangHoas.ToList().ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult ThemMoi()
        {

            if (kiemTraAdmin() == false)
            {
                ViewBag.ThongBaoCanDangNhap = "Cần Đăng Nhập để tiếp tục";
                return RedirectToAction("DangNhap", "UserAdmin");
            }
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.ToList().OrderBy(n => n.TenNCC), "MaNCC", "TenNCC");
            ViewBag.MaDM = new SelectList(db.DanhMucs.ToList().OrderBy(n => n.TenDM), "MaDM", "TenDM");
            return View();
        }
        [HttpPost]
        public ActionResult ThemMoi(HangHoa hanghoa,HttpPostedFileBase fileUpload)
        {
          
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.ToList().OrderBy(n => n.TenNCC), "MaNCC", "TenNCC");
            ViewBag.MaDM = new SelectList(db.DanhMucs.ToList().OrderBy(n => n.TenDM), "MaDM", "TenDM");
            //kiểm tra ảnh bìa
            if (fileUpload==null)
            {
                ViewBag.ThongBaoAnhTonTai = "Hình ảnh không được để trống";
                return View();
            }
            if (db.HangHoas.Any(x => x.MaHH == hanghoa.MaHH))
            {
                ViewBag.ThongBaoLoi = "Mã Hàng đã tồn tại";
                return View("ThemMoi", hanghoa);
            }
            //thêm vào CSDL
            if (ModelState.IsValid)
            {
                //lưu tên của file ảnh
                var fileName = Path.GetFileName(fileUpload.FileName);
                // lưu đường dẫn của file
                var path = Path.Combine(Server.MapPath("~/images/Item"), fileName);
                //kiểm tra hình ảnh 
                if (System.IO.File.Exists(path))
                {
                    ViewBag.ThongBaoAnhTonTai = "Ảnh đã tồn tại";
                }
                else
                {
                    fileUpload.SaveAs(path);
                }
                hanghoa.AnhBia = fileUpload.FileName;
                hanghoa.NgayNhap = DateTime.Now;

                db.HangHoas.Add(hanghoa);
                
                ViewBag.Success = "Thêm Hàng Thành Công";
                db.SaveChanges();
            }
            return View();
        }

        [HttpGet]
        public ActionResult Update(string Mahh)
        {

            if (kiemTraAdmin() == false)
            {
                ViewBag.ThongBaoCanDangNhap = "Cần Đăng Nhập để tiếp tục";
                return RedirectToAction("DangNhap", "UserAdmin");
            }

            HangHoa hh = db.HangHoas.SingleOrDefault(n => n.MaHH == Mahh);
            if (hh==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.ToList().OrderBy(n => n.TenNCC), "MaNCC", "TenNCC",hh.MaNCC);
            ViewBag.MaDM = new SelectList(db.DanhMucs.ToList().OrderBy(n => n.TenDM), "MaDM", "TenDM",hh.MaDM);

            return View(hh);
        }
        [HttpPost]
        public ActionResult Update(HangHoa hanghoa)
        {
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.ToList().OrderBy(n => n.TenNCC), "MaNCC", "TenNCC",hanghoa.MaNCC);
            ViewBag.MaDM = new SelectList(db.DanhMucs.ToList().OrderBy(n => n.TenDM), "MaDM", "TenDM",hanghoa.MaDM);
            if (ModelState.IsValid)
            {
                HangHoa hh1 = db.HangHoas.SingleOrDefault(n => n.MaHH == hanghoa.MaHH);
                //hh1.NgayNhap = DateTime.Now;
                hh1.TenHH = hanghoa.TenHH;
                hh1.GiaBan = hanghoa.GiaBan;
                hh1.GiaNhap = hanghoa.GiaNhap;
                hh1.GiaKhuyenMai = hanghoa.GiaKhuyenMai;
                hh1.MaNCC = hanghoa.MaNCC;
                hh1.MaDM = hanghoa.MaDM;
                hh1.SoLuong = hanghoa.SoLuong;
                hh1.NgayNhap = DateTime.Now;
                db.SaveChanges();
                ViewBag.Success = "Cập nhập thành công";
                return View();
            }

            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.ToList().OrderBy(n => n.TenNCC), "MaNCC", "TenNCC", hanghoa.MaNCC);
            ViewBag.MaDM = new SelectList(db.DanhMucs.ToList().OrderBy(n => n.TenDM), "MaDM", "TenDM", hanghoa.MaDM);
            return View();
        }
        [HttpGet]
        public ActionResult XoaHang(String mahh)
        {

            if (kiemTraAdmin() == false)
            {
                ViewBag.ThongBaoCanDangNhap = "Cần Đăng Nhập để tiếp tục";
                return RedirectToAction("DangNhap", "UserAdmin");
            }

            HangHoa hanghoa = db.HangHoas.SingleOrDefault(n => n.MaHH == mahh);
            if (hanghoa==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(hanghoa);
        }

        [HttpPost,ActionName("XoaHang")]
        public ActionResult XacNhanXoa(string mahh)
        {
            HangHoa hanghoa = db.HangHoas.SingleOrDefault(n => n.MaHH == mahh);
            if (hanghoa==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.HangHoas.Remove(hanghoa);
            db.SaveChanges();
            return RedirectToAction("Index", "QuanLySanPham");
        }
    }
}