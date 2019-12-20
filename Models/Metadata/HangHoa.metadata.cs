using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShopDemo.Models
{
    [MetadataType(typeof(HangHoaMetadata))]
    public partial class HangHoa
    {
        internal sealed class HangHoaMetadata
        {
            [Required(ErrorMessage = "Mã Hàng Không Được Để Trống")]
            public string MaHH { get; set; }

            [Required(ErrorMessage = "Tên Hàng Không Được Để Trống")]
            public string TenHH { get; set; }
          
            [Required(ErrorMessage = "Giá Nhập Không Được Để Trống")]
            public Nullable<decimal> GiaNhap { get; set; }


          
            [Required(ErrorMessage = "Giá Bán Không Được Để Trống")]
            public decimal GiaBan { get; set; }

           
            public Nullable<decimal> GiaKhuyenMai { get; set; }

            [Required(ErrorMessage = "Thông Tin Mô tả Không Được Để Trống")]
            public string ThongTin { get; set; }

            [Required(ErrorMessage = "Số Lượng Không Được Để Trống")]
            public int SoLuong { get; set; }


            public System.DateTime NgayNhap { get; set; }


            public string AnhBia { get; set; }

            [Required(ErrorMessage = "Nhà Cung Cấp Không Được Để Trống")]
            public int MaNCC { get; set; }

            [Required(ErrorMessage = "Danh Mục Không Được Để Trống")]
            public int MaDM { get; set; }
        }
    }
}