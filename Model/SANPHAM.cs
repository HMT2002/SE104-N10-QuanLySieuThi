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
    using System;
    using System.Collections.Generic;
    
    public partial class SANPHAM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SANPHAM()
        {
            this.CTHD = new HashSet<CTHD>();
        }
    
        public string MASP { get; set; }
        public string TENSP { get; set; }
        public string DVT { get; set; }
        public string MACC { get; set; }
        public Nullable<decimal> GIA { get; set; }
        public Nullable<int> SL { get; set; }
        public byte[] PICBI { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTHD> CTHD { get; set; }
        public virtual NHACUNGCAP NHACUNGCAP { get; set; }
    }
}
