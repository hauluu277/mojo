//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace mojoPortal.Model.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class mp_UserRoles
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public Nullable<System.Guid> UserGuid { get; set; }
        public Nullable<System.Guid> RoleGuid { get; set; }
    }
}
