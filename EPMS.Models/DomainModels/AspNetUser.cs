﻿using System;
using System.Collections.Generic;

namespace EPMS.Models.DomainModels
{
    public partial class AspNetUser
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }
        public long? EmployeeId { get; set; }
        public long? CustomerId { get; set; }
        public string Address { get; set; }
        public string ImageName { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }

        public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ICollection<NotificationRecipient> NotificationRecipients { get; set; }
        public virtual ICollection<RFI> RFIs { get; set; }
        public virtual ICollection<UserPrefrence> UserPrefrences { get; set; }
        public virtual ICollection<AspNetRole> AspNetRoles { get; set; }
        public virtual ICollection<RIF> RIFs { get; set; }
        public virtual ICollection<DIF> DIFs { get; set; }
        public virtual ICollection<DIF> DIFManager { get; set; }
        public virtual ICollection<POF> POFs { get; set; }
        public virtual ICollection<POF> POFManager { get; set; }
        public virtual ICollection<TIR> TIRs { get; set; }
        public virtual ICollection<TIR> TIRManager { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrderManager { get; set; }
        public virtual ICollection<DIFHistory> DIFHistories { get; set; }
        public virtual ICollection<DIFHistory> DIFHistoryManager { get; set; }
        public virtual ICollection<ItemReleaseHistory> ItemReleaseHistories { get; set; }
        public virtual ICollection<ItemRelease> ItemReleases { get; set; }
        public virtual ICollection<RFIHistory> RFIHistories { get; set; }
        public virtual ICollection<ItemBarcode> ItemBarcodes { get; set; }
    }
}
