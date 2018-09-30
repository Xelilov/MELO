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
    
    public partial class SDE_table_registry
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SDE_table_registry()
        {
            this.SDE_archives1 = new HashSet<SDE_archives>();
            this.SDE_xml_columns = new HashSet<SDE_xml_columns>();
            this.SDE_states = new HashSet<SDE_states>();
        }
    
        public int registration_id { get; set; }
        public string database_name { get; set; }
        public string table_name { get; set; }
        public string owner { get; set; }
        public string rowid_column { get; set; }
        public string description { get; set; }
        public int object_flags { get; set; }
        public int registration_date { get; set; }
        public string config_keyword { get; set; }
        public Nullable<int> minimum_id { get; set; }
        public string imv_view_name { get; set; }
    
        public virtual SDE_archives SDE_archives { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SDE_archives> SDE_archives1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SDE_xml_columns> SDE_xml_columns { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SDE_states> SDE_states { get; set; }
    }
}
