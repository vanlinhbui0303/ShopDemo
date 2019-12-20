using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShopDemo.Models
{
    [MetadataType(typeof(DonHangMetadata))]
    public partial class DonHang
    {
        internal sealed class DonHangMetadata
        {
            public int MaDH { get; set; }
            public System.DateTime NgayDat { get; set; }
            public Nullable<System.DateTime> NgayGiao { get; set; }
            public Nullable<int> ThanhToan { get; set; }
            public string TinhTrangGiaoHang { get; set; }
            public string HoTenNhan { get; set; }

            [Required(ErrorMessage = "Số Điện Thoại Không Được Để Trống")]
            public string SoDienThoaiNhan { get; set; }
            [Required(ErrorMessage = "Địa Chỉ Không Được Để Trống")]
            public string DiaChiNhan { get; set; }
            public int MaTV { get; set; }
            public Nullable<int> TrangThai { get; set; }
            public Nullable<decimal> TongTienDonHang { get; set; }
        }
    }
}