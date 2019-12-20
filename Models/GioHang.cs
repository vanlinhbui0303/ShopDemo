using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopDemo.Models
{
    public class GioHang
    {

        QLBanGiayEntities db = new QLBanGiayEntities();
        public string sMaGioHang { get; set; }
        public string sMaHH { get; set; }

        public string sTenHH { get; set; }

        public string sAnhBia { get; set; }

        public double dDonGia { get; set; }

        public int iSoLuong { get; set; }
        public int iSize { get; set; }

        public double dThanhTien {
            get { return iSoLuong * dDonGia; }
        }

        //hàm tạo giỏ hàng 

        public GioHang(string mahh)
        {
            sMaHH = mahh;
            HangHoa hh = db.HangHoas.Single(n => n.MaHH == sMaHH);
            sTenHH = hh.TenHH;
            sAnhBia = hh.AnhBia;
            if (hh.GiaKhuyenMai == null)
            {
                dDonGia = double.Parse(hh.GiaBan.ToString());
            }
            else dDonGia = double.Parse( hh.GiaKhuyenMai.ToString());
            iSoLuong = 1;

        }
    }
}