//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AccountancyCRUD.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class project
    {
        public int id { get; set; }
        public string project_name { get; set; }
        public decimal cost { get; set; }
        public Nullable<int> department_id { get; set; }
        public Nullable<System.DateTime> date_begin { get; set; }
        public Nullable<System.DateTime> date_end { get; set; }
        public Nullable<System.DateTime> date_end_real { get; set; }
    
        public virtual department department { get; set; }
    }
}
