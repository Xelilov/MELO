﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MelogisEntities : DbContext
    {
        public MelogisEntities()
            : base("name=MelogisEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ADDRESS> ADDRESSes { get; set; }
        public virtual DbSet<BORDER> BORDERs { get; set; }
        public virtual DbSet<BUILDING> BUILDINGs { get; set; }
        public virtual DbSet<BUILDING1> BUILDINGS1 { get; set; }
        public virtual DbSet<CASPIAN> CASPIANs { get; set; }
        public virtual DbSet<CHANNEL> CHANNELs { get; set; }
        public virtual DbSet<CHANNEL_1> CHANNEL_1 { get; set; }
        public virtual DbSet<CHANNEL1> CHANNELS1 { get; set; }
        public virtual DbSet<COAST_LINE> COAST_LINE { get; set; }
        public virtual DbSet<DEPARTMENT> DEPARTMENTS { get; set; }
        public virtual DbSet<DEVICE> DEVICEs { get; set; }
        public virtual DbSet<DISTRICT> DISTRICTs { get; set; }
        public virtual DbSet<DRENAJ> DRENAJs { get; set; }
        public virtual DbSet<EXPLOITATION_ROAD> EXPLOITATION_ROAD { get; set; }
        public virtual DbSet<FOREST> FORESTs { get; set; }
        public virtual DbSet<GDB_ITEMRELATIONSHIPS> GDB_ITEMRELATIONSHIPS { get; set; }
        public virtual DbSet<GDB_ITEMS> GDB_ITEMS { get; set; }
        public virtual DbSet<HYDRO_OBJECT> HYDRO_OBJECT { get; set; }
        public virtual DbSet<HYDRO_POLY> HYDRO_POLY { get; set; }
        public virtual DbSet<i10> i10 { get; set; }
        public virtual DbSet<i11> i11 { get; set; }
        public virtual DbSet<i12> i12 { get; set; }
        public virtual DbSet<i13> i13 { get; set; }
        public virtual DbSet<i14> i14 { get; set; }
        public virtual DbSet<i15> i15 { get; set; }
        public virtual DbSet<i16> i16 { get; set; }
        public virtual DbSet<i17> i17 { get; set; }
        public virtual DbSet<i18> i18 { get; set; }
        public virtual DbSet<i19> i19 { get; set; }
        public virtual DbSet<i2> i2 { get; set; }
        public virtual DbSet<i20> i20 { get; set; }
        public virtual DbSet<i21> i21 { get; set; }
        public virtual DbSet<i22> i22 { get; set; }
        public virtual DbSet<i23> i23 { get; set; }
        public virtual DbSet<i24> i24 { get; set; }
        public virtual DbSet<i25> i25 { get; set; }
        public virtual DbSet<i26> i26 { get; set; }
        public virtual DbSet<i27> i27 { get; set; }
        public virtual DbSet<i28> i28 { get; set; }
        public virtual DbSet<i29> i29 { get; set; }
        public virtual DbSet<i3> i3 { get; set; }
        public virtual DbSet<i30> i30 { get; set; }
        public virtual DbSet<i31> i31 { get; set; }
        public virtual DbSet<i32> i32 { get; set; }
        public virtual DbSet<i33> i33 { get; set; }
        public virtual DbSet<i34> i34 { get; set; }
        public virtual DbSet<i35> i35 { get; set; }
        public virtual DbSet<i36> i36 { get; set; }
        public virtual DbSet<i37> i37 { get; set; }
        public virtual DbSet<i4> i4 { get; set; }
        public virtual DbSet<i5> i5 { get; set; }
        public virtual DbSet<i6> i6 { get; set; }
        public virtual DbSet<i7> i7 { get; set; }
        public virtual DbSet<i8> i8 { get; set; }
        public virtual DbSet<i9> i9 { get; set; }
        public virtual DbSet<POI> POIs { get; set; }
        public virtual DbSet<PUMPSTATION> PUMPSTATIONs { get; set; }
        public virtual DbSet<REGION_BORDER> REGION_BORDER { get; set; }
        public virtual DbSet<REGION_CENTER> REGION_CENTER { get; set; }
        public virtual DbSet<REGION_CENTER_1> REGION_CENTER_1 { get; set; }
        public virtual DbSet<REGION> REGIONS { get; set; }
        public virtual DbSet<RESIDENTIAL_AREA> RESIDENTIAL_AREA { get; set; }
        public virtual DbSet<RESIDENTIALAREAFORFILTER> RESIDENTIALAREAFORFILTERs { get; set; }
        public virtual DbSet<RIVERBAND> RIVERBANDs { get; set; }
        public virtual DbSet<RIVER> RIVERS { get; set; }
        public virtual DbSet<ROAD> ROADS { get; set; }
        public virtual DbSet<SABIRABAD> SABIRABADs { get; set; }
        public virtual DbSet<SDE_archives> SDE_archives { get; set; }
        public virtual DbSet<SDE_column_registry> SDE_column_registry { get; set; }
        public virtual DbSet<SDE_dbtune> SDE_dbtune { get; set; }
        public virtual DbSet<SDE_geometry_columns> SDE_geometry_columns { get; set; }
        public virtual DbSet<SDE_GEOMETRY1> SDE_GEOMETRY1 { get; set; }
        public virtual DbSet<SDE_GEOMETRY10> SDE_GEOMETRY10 { get; set; }
        public virtual DbSet<SDE_GEOMETRY11> SDE_GEOMETRY11 { get; set; }
        public virtual DbSet<SDE_GEOMETRY12> SDE_GEOMETRY12 { get; set; }
        public virtual DbSet<SDE_GEOMETRY13> SDE_GEOMETRY13 { get; set; }
        public virtual DbSet<SDE_GEOMETRY14> SDE_GEOMETRY14 { get; set; }
        public virtual DbSet<SDE_GEOMETRY15> SDE_GEOMETRY15 { get; set; }
        public virtual DbSet<SDE_GEOMETRY16> SDE_GEOMETRY16 { get; set; }
        public virtual DbSet<SDE_GEOMETRY17> SDE_GEOMETRY17 { get; set; }
        public virtual DbSet<SDE_GEOMETRY18> SDE_GEOMETRY18 { get; set; }
        public virtual DbSet<SDE_GEOMETRY19> SDE_GEOMETRY19 { get; set; }
        public virtual DbSet<SDE_GEOMETRY2> SDE_GEOMETRY2 { get; set; }
        public virtual DbSet<SDE_GEOMETRY20> SDE_GEOMETRY20 { get; set; }
        public virtual DbSet<SDE_GEOMETRY21> SDE_GEOMETRY21 { get; set; }
        public virtual DbSet<SDE_GEOMETRY22> SDE_GEOMETRY22 { get; set; }
        public virtual DbSet<SDE_GEOMETRY23> SDE_GEOMETRY23 { get; set; }
        public virtual DbSet<SDE_GEOMETRY24> SDE_GEOMETRY24 { get; set; }
        public virtual DbSet<SDE_GEOMETRY25> SDE_GEOMETRY25 { get; set; }
        public virtual DbSet<SDE_GEOMETRY26> SDE_GEOMETRY26 { get; set; }
        public virtual DbSet<SDE_GEOMETRY27> SDE_GEOMETRY27 { get; set; }
        public virtual DbSet<SDE_GEOMETRY28> SDE_GEOMETRY28 { get; set; }
        public virtual DbSet<SDE_GEOMETRY29> SDE_GEOMETRY29 { get; set; }
        public virtual DbSet<SDE_GEOMETRY3> SDE_GEOMETRY3 { get; set; }
        public virtual DbSet<SDE_GEOMETRY30> SDE_GEOMETRY30 { get; set; }
        public virtual DbSet<SDE_GEOMETRY31> SDE_GEOMETRY31 { get; set; }
        public virtual DbSet<SDE_GEOMETRY32> SDE_GEOMETRY32 { get; set; }
        public virtual DbSet<SDE_GEOMETRY4> SDE_GEOMETRY4 { get; set; }
        public virtual DbSet<SDE_GEOMETRY5> SDE_GEOMETRY5 { get; set; }
        public virtual DbSet<SDE_GEOMETRY6> SDE_GEOMETRY6 { get; set; }
        public virtual DbSet<SDE_GEOMETRY7> SDE_GEOMETRY7 { get; set; }
        public virtual DbSet<SDE_GEOMETRY8> SDE_GEOMETRY8 { get; set; }
        public virtual DbSet<SDE_GEOMETRY9> SDE_GEOMETRY9 { get; set; }
        public virtual DbSet<SDE_layer_locks> SDE_layer_locks { get; set; }
        public virtual DbSet<SDE_layer_stats> SDE_layer_stats { get; set; }
        public virtual DbSet<SDE_layers> SDE_layers { get; set; }
        public virtual DbSet<SDE_lineages_modified> SDE_lineages_modified { get; set; }
        public virtual DbSet<SDE_locators> SDE_locators { get; set; }
        public virtual DbSet<SDE_logfile_pool> SDE_logfile_pool { get; set; }
        public virtual DbSet<SDE_metadata> SDE_metadata { get; set; }
        public virtual DbSet<SDE_object_ids> SDE_object_ids { get; set; }
        public virtual DbSet<SDE_object_locks> SDE_object_locks { get; set; }
        public virtual DbSet<SDE_process_information> SDE_process_information { get; set; }
        public virtual DbSet<SDE_raster_columns> SDE_raster_columns { get; set; }
        public virtual DbSet<SDE_server_config> SDE_server_config { get; set; }
        public virtual DbSet<SDE_spatial_references> SDE_spatial_references { get; set; }
        public virtual DbSet<SDE_state_lineages> SDE_state_lineages { get; set; }
        public virtual DbSet<SDE_state_locks> SDE_state_locks { get; set; }
        public virtual DbSet<SDE_states> SDE_states { get; set; }
        public virtual DbSet<SDE_table_locks> SDE_table_locks { get; set; }
        public virtual DbSet<SDE_table_registry> SDE_table_registry { get; set; }
        public virtual DbSet<SDE_tables_modified> SDE_tables_modified { get; set; }
        public virtual DbSet<SDE_version> SDE_version { get; set; }
        public virtual DbSet<SDE_versions> SDE_versions { get; set; }
        public virtual DbSet<SDE_xml_columns> SDE_xml_columns { get; set; }
        public virtual DbSet<SDE_xml_index_tags> SDE_xml_index_tags { get; set; }
        public virtual DbSet<SDE_xml_indexes> SDE_xml_indexes { get; set; }
        public virtual DbSet<tblUser> tblUsers { get; set; }
        public virtual DbSet<VEGETATION> VEGETATIONs { get; set; }
        public virtual DbSet<WELL> WELLs { get; set; }
        public virtual DbSet<GDB_ITEMRELATIONSHIPTYPES> GDB_ITEMRELATIONSHIPTYPES { get; set; }
        public virtual DbSet<GDB_ITEMTYPES> GDB_ITEMTYPES { get; set; }
        public virtual DbSet<GDB_REPLICALOG> GDB_REPLICALOG { get; set; }
        public virtual DbSet<GDB_TABLES_LAST_MODIFIED> GDB_TABLES_LAST_MODIFIED { get; set; }
    }
}