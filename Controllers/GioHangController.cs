using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopDemo.Models;

namespace ShopDemo.Controllers
{
    public class GioHangController : Controller
    {
        QLBanGiayEntities db = new QLBanGiayEntities();
        // lấy giỏ hàng 
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                // nếu giỏ hàng chưa tồn tại, tiến hành khởi tạo list giỏ hàng 
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }
        // thêm giỏ hàng 
        public ActionResult ThemGioHang(String mahh, string strUrl)
        {
            HangHoa hh = db.HangHoas.SingleOrDefault(n => n.MaHH == mahh);
            if (hh == null)
            {
                Response.StatusCode = 404;
                return null;
            }


            List<GioHang> lstGioHang = LayGioHang();
            //kiểm tra mã hàng đã tồn tại trong giỏ hàng chưa   
            GioHang sanpham = lstGioHang.Find(n => n.sMaHH == mahh);
            if (sanpham == null)
            {
                sanpham = new GioHang(mahh);

                lstGioHang.Add(sanpham);
                return Redirect(strUrl);

            }
            else
            {
                sanpham.iSoLuong++;
                return Redirect(strUrl);
            }
        }

        // thêm giỏ hàng  2
        public ActionResult ThemGioHang2(String mahh, string strUrl, FormCollection f)
        {
            HangHoa hh = db.HangHoas.SingleOrDefault(n => n.MaHH == mahh);
            if (hh == null)
            {
                Response.StatusCode = 404;
                return null;
            }


            List<GioHang> lstGioHang = LayGioHang();
            //kiểm tra mã hàng đã tồn tại trong giỏ hàng chưa   
            GioHang sanpham = lstGioHang.Find(n => n.sMaHH == mahh && n.iSize == int.Parse(f["txtSize"].ToString()));

            //Kiểm tra số lượng trong kho
            if (hh.SoLuong < int.Parse(f["txtSoLuong"].ToString()))
            {
                TempData["ThongBao"] = "Sản Phẩm không còn đủ số lượng để đặt hàng. Xin Lỗi vì sự bất tiện này";

                return Redirect(strUrl);
            }
            if (sanpham == null)
            {
                sanpham = new GioHang(mahh);
               
                lstGioHang.Add(sanpham);
                sanpham.iSoLuong = int.Parse(f["txtSoLuong"].ToString());
                sanpham.iSize = int.Parse(f["txtSize"].ToString());
                return Redirect(strUrl);

            }

            else
            {
                //kiểm tra hàng hóa có đủ số lượng
                if (sanpham.iSoLuong + int.Parse(f["txtSoLuong"].ToString()) > hh.SoLuong)
                {
                    TempData["ThongBao"] = "Sản Phẩm không còn đủ số lượng để đặt hàng. Xin Lỗi vì sự bất tiện này";

                    return Redirect(strUrl);
                }
                else
                {
                    sanpham.iSoLuong = sanpham.iSoLuong + int.Parse(f["txtSoLuong"].ToString());
                    return Redirect(strUrl);
                }
            }

        }







        //cập nhập giỏ hàng
        public ActionResult CapNhapGioHang(string mahh,int size ,FormCollection f)
        {
            //kiểm tra masp
            HangHoa hh = db.HangHoas.SingleOrDefault(n => n.MaHH == mahh);
            //nếu  get sai mã hàng hóa  thì trả về trang lỗi
            if (hh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //lấy giỏ hàng ra từ session
            List<GioHang> lstGioHang = LayGioHang();
            //kiểm tra hàng hóa đã tồn tại trong session["GioHang"]
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.sMaHH == mahh && n.iSize== size);
            //nếu tồn tại ... sửa số lượng
            if (sanpham != null)
            {
                if (hh.SoLuong< int.Parse(f["txtSoLuong"].ToString()))
                {
                    TempData["ThongBao"] = "Sản Phẩm không còn đủ số lượng để đặt hàng. Xin Lỗi vì sự bất tiện này";
                    return RedirectToAction("GioHang");
                }
                sanpham.iSoLuong = int.Parse(f["txtSoLuong"].ToString());
            }
            if (sanpham.iSoLuong == 0)
            {
                lstGioHang.Remove(sanpham);
            }
            return RedirectToAction("GioHang");
        }
        //xóa giỏ hàng 
        public ActionResult XoaGioHang(String mahh, int size)
        {
            //kiểm tra masp
            HangHoa hh = db.HangHoas.SingleOrDefault(n => n.MaHH == mahh);
            //nếu  get sai mã hàng hóa  thì trả về trang lỗi
            if (hh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //lấy giỏ hàng ra từ session
            List<GioHang> ltsGioHang = LayGioHang();
            //kiểm tra hàng hóa đã tồn tại trong session["GioHang"]
            GioHang sanpham = ltsGioHang.SingleOrDefault(n => n.sMaHH == mahh && n.iSize==size);

            if (sanpham != null)
            {
                ltsGioHang.RemoveAll(n => n.sMaHH == mahh && n.iSize == size);


            }
            if (ltsGioHang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("GioHang");
        }




        //xóa giỏ hàng 2
        public ActionResult XoaGioHang2(String mahh, int size,string strUrl)
        {
            //kiểm tra masp
            HangHoa hh = db.HangHoas.SingleOrDefault(n => n.MaHH == mahh);
            //nếu  get sai mã hàng hóa  thì trả về trang lỗi
            if (hh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //lấy giỏ hàng ra từ session
            List<GioHang> ltsGioHang = LayGioHang();
            //kiểm tra hàng hóa đã tồn tại trong session["GioHang"]
            GioHang sanpham = ltsGioHang.SingleOrDefault(n => n.sMaHH == mahh && n.iSize==size);

            if (sanpham != null)
            {
                ltsGioHang.RemoveAll(n => n.sMaHH == mahh &&n.iSize==size);
                return Redirect(strUrl);

            }
            if (ltsGioHang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("GioHang");
        }



        //xây dựng trang giỏ hàng
        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> lstGioHang = LayGioHang();
            if (TempData["ThongBao"] != null)
            {
                ViewBag.ThongBao = TempData["ThongBao"].ToString();
            }

            return View(lstGioHang);
        }
        //tính tổng số lượng và tổng tiền

        //tính tổng số lượng
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                iTongSoLuong = lstGioHang.Sum(n => n.iSoLuong);

            }
            return iTongSoLuong;
        }
        // tính tổng tiền

        private double TongTien()
        {
            double dTongTien = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                dTongTien = lstGioHang.Sum(n => n.dThanhTien);

            }
            return dTongTien;
        }
        public PartialViewResult GioHangTTPartial()
        {
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            List<GioHang> lstGioHang = LayGioHang();
            return PartialView(lstGioHang);
        }

        public PartialViewResult GioHangPartial()
        {
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            List<GioHang> lstGioHang = LayGioHang();
            return PartialView(lstGioHang);
        }

        public PartialViewResult GioHangPayPartial()
        {
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            List<GioHang> lstGioHang = LayGioHang();
            return PartialView(lstGioHang);
        }


        [HttpGet]
        public ActionResult DatHang()
        {
            // kiểm tra đăng nhập
            if (Session["ThanhVien"] == null || Session["ThanhVien"].ToString() == "")
            {
                ViewBag.ThongBao = "Cần Đăng Nhập Để đặt hàng";
                return RedirectToAction("DangNhap", "User");
            }
            if (Session["GioHang"] == null)
            {
                RedirectToAction("index", "Home");
            }
            ThanhVien tv = Session["ThanhVien"] as ThanhVien;
            if (tv.DiaChi != null)
            {
                ViewBag.DiaChi = tv.DiaChi;
            }
            if (tv.HoTen != null || tv.HoTen != "")
            {
                ViewBag.HoTen = tv.HoTen;
            }
            if (tv.SoDienThoai != null)
            {
                ViewBag.SoDienThoai = tv.SoDienThoai;
            }
            DonHang dh = new DonHang();
            return View(dh);
        }


        [HttpPost]
        public ActionResult DatHang(DonHang dh)
        {
            // kiểm tra đăng nhập
            if (Session["ThanhVien"] == null || Session["ThanhVien"].ToString() == "")
            {
                ViewBag.ThongBao = "Cần Đăng Nhập Để đặt hàng";
                return RedirectToAction("DangNhap", "User");
            }
            if (Session["GioHang"] == null)
            {
                RedirectToAction("index", "Home");
            }
            //Thêm đơn hàng


            ThanhVien tv = Session["ThanhVien"] as ThanhVien;
            if (tv.DiaChi != null)
            {
                ViewBag.DiaChi = tv.DiaChi;
            }
            if (tv.HoTen != null || tv.HoTen != "")
            {
                ViewBag.HoTen = tv.HoTen;
            }
            if (tv.SoDienThoai != null)
            {
                ViewBag.SoDienThoai = tv.SoDienThoai;
            }
           
            List<GioHang> gh = LayGioHang();

            dh.MaTV = tv.MaTV;
            dh.NgayDat = DateTime.Now;
            dh.MaTV = tv.MaTV;
            dh.TrangThai = (int)1;

            db.DonHangs.Add(dh);
            db.SaveChanges();
            decimal tien = 0;
            foreach (var item in gh)
            {
                ChiTietDonHang ctdh = new ChiTietDonHang();
                ctdh.MaDH = dh.MaDH;
                ctdh.Size = item.iSize;
                ctdh.MaHH = item.sMaHH;
                
                ctdh.SoLuong = item.iSoLuong;
                HangHoa hhh = db.HangHoas.SingleOrDefault(n => n.MaHH == item.sMaHH);
                if (hhh != null)
                {
                    hhh.SoLuong = (hhh.SoLuong - item.iSoLuong);
                    db.SaveChanges();
                }
                ctdh.DonGia = (decimal)item.dDonGia;
                ctdh.TongTien = ctdh.SoLuong * ctdh.DonGia;
                db.ChiTietDonHangs.Add(ctdh);

                db.SaveChanges();
                tien = tien + (decimal)ctdh.TongTien;
            }
            dh.TongTienDonHang = tien;
            db.SaveChanges();
            Session["GioHang"] = null;
            ViewBag.ThongBao = "Đặt Hàng Thành công";
            return View();

        }


    }


}