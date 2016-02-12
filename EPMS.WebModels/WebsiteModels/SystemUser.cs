using System;

namespace EPMS.WebModels.WebsiteModels
{
    public class SystemUser
    {
        public string KeyId { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string Telephone { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public string ImageName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Qualification { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string RoleId { get; set; }
        public string Role { get; set; }
        public string Username { get; set; }
        public string EmpEmail { get; set; }
    }
}