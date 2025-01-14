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
    
    public partial class CARD_INFO
    {
        public long REC_ID { get; set; }
        public string CARD_NUMBER { get; set; }
        public string SERIAL_NUMBER { get; set; }
        public Nullable<System.DateTime> ISSUE_DATE { get; set; }
        public Nullable<System.DateTime> EXPIRE_DATE { get; set; }
        public string CARD_STATE { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        public Nullable<int> CREATE_USER { get; set; }
        public Nullable<int> OWNER_USER { get; set; }
        public byte[] EXPONENT { get; set; }
        public byte[] MODULUS { get; set; }
        public string CERTIFICATE_FILE { get; set; }
        public Nullable<bool> IS_ACTIVE { get; set; }
        public Nullable<bool> IS_PRINTED { get; set; }
        public string PICTURE_PATH { get; set; }
        public byte[] CERTIFICATE_BIN { get; set; }
        public byte[] PRIVATE_N { get; set; }
        public byte[] PRIVATE_D { get; set; }
        public string PIN { get; set; }
    
        public virtual USER USER { get; set; }
        public virtual USER USER1 { get; set; }
    }
}
