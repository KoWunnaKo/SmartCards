//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SmartCardDesc.EntityModel.EntityModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class WAREHOUSE_DTL
    {
        public int REC_ID { get; set; }
        public int PARENT_ID { get; set; }
        public string OP_TYPE { get; set; }
        public Nullable<decimal> TR_AMOUNT { get; set; }
        public Nullable<System.DateTime> TR_DATE { get; set; }
        public Nullable<int> STATE { get; set; }
        public Nullable<int> quantity { get; set; }
        public Nullable<int> CREATE_USER { get; set; }
    
        public virtual USER USER { get; set; }
        public virtual WAREHOUSE WAREHOUSE { get; set; }
    }
}
