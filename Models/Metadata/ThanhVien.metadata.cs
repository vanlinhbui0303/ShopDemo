using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShopDemo.Models
{
    [MetadataType(typeof(ThanhVienMetadata))]
    public partial class ThanhVien
    {
        internal sealed class ThanhVienMetadata
        {
            public int MaTV { get; set; }


            [Required(ErrorMessage = "Tên Đăng Nhập Không Được Để Trống")]
            public string TenDangNhap { get; set; }

            [DataType(DataType.Password)]
            [Required(ErrorMessage = "Mật Khẩu Không Được Để Trống")]
            public string MatKhau { get; set; }


            [Compare("MatKhau", ErrorMessage = "Nhập Lại Mật khẩu không đúng")]
            [DataType(DataType.Password)]
            public string ReMatKhau { get; set; }



            [Required(ErrorMessage = "Họ Tên Không Được Để Trống")]
            public string HoTen { get; set; }
            public string DiaChi { get; set; }
            public string SoDienThoai { get; set; }
            public Nullable<System.DateTime> NgaySinh { get; set; }
            public string GioiTinh { get; set; }
            public string MaQuyen { get; set; }
        }
    }
}