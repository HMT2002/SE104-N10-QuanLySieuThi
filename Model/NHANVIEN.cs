//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SE104_N10_QuanLySieuThi.Model
{
    using SE104_N10_QuanLySieuThi.ViewModel;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public partial class NHANVIEN:BaseViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NHANVIEN()
        {
            this.HOADON = new HashSet<HOADON>();
        }
    
        public string MANV { get; set; }
        public string HOTEN { get; set; }
        public string SODT { get; set; }
        public string NGVL { get; set; }
        public Nullable<decimal> LUONG { get; set; }
        public string ACC { get; set; }
        public byte[] PICBI { get; set; }
        public string MAIL { get; set; }
        public string POSITION { get; set; }
        public string CMND { get; set; }
        public string NGSINH { get; set; }
        public string GENDER { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HOADON> HOADON { get; set; }
    }
}
