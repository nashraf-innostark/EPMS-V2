//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EPMS.Repository.BaseRepository
{
    using System;
    using System.Collections.Generic;
    
    public partial class NotificationRecipient
    {
        public long Id { get; set; }
        public long NotificationId { get; set; }
        public bool IsRead { get; set; }
        public string UserId { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public Nullable<long> EmployeeId { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Notification Notification { get; set; }
    }
}
