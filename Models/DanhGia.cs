//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShopDemo.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DanhGia
    {
        public int MaDG { get; set; }
        public string MaHH { get; set; }
        public Nullable<int> SoSao { get; set; }
        public string NhanXet { get; set; }
        public int MaTV { get; set; }
    
        public virtual ThanhVien ThanhVien { get; set; }
        public virtual HangHoa HangHoa { get; set; }
    }
}
