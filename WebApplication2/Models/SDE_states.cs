//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SDE_states
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SDE_states()
        {
            this.SDE_table_registry = new HashSet<SDE_table_registry>();
        }
    
        public long state_id { get; set; }
        public string owner { get; set; }
        public System.DateTime creation_time { get; set; }
        public Nullable<System.DateTime> closing_time { get; set; }
        public long parent_state_id { get; set; }
        public long lineage_name { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SDE_table_registry> SDE_table_registry { get; set; }
    }
}
