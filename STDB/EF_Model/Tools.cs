//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace STDB.EF_Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tools
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tools()
        {
            this.O_rder = new HashSet<O_rder>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> Amount { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<O_rder> O_rder { get; set; }
    }
}