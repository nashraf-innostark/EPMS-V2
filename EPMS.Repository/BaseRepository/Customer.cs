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
    
    public partial class Customer
    {
        public Customer()
        {
            this.AspNetUsers = new HashSet<AspNetUser>();
            this.Complaints = new HashSet<Complaint>();
            this.Orders = new HashSet<Order>();
            this.Projects = new HashSet<Project>();
            this.ProjectTasks = new HashSet<ProjectTask>();
            this.Quotations = new HashSet<Quotation>();
            this.ItemReleases = new HashSet<ItemRelease>();
        }
    
        public long CustomerId { get; set; }
        public string CustomerNameE { get; set; }
        public string CustomerNameA { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerMobile { get; set; }
        public string RecCreatedBy { get; set; }
        public Nullable<System.DateTime> RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public Nullable<System.DateTime> RecLastUpdatedDt { get; set; }
    
        public virtual ICollection<AspNetUser> AspNetUsers { get; set; }
        public virtual ICollection<Complaint> Complaints { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<ProjectTask> ProjectTasks { get; set; }
        public virtual ICollection<Quotation> Quotations { get; set; }
        public virtual ICollection<ItemRelease> ItemReleases { get; set; }
    }
}
