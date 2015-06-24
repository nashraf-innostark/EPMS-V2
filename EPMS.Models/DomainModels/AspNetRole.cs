using System.Collections.Generic;
using EPMS.Models.MenuModels;

namespace EPMS.Models.DomainModels
{
    public partial class AspNetRole
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int? RoleKey { get; set; }
        public virtual ICollection<AspNetUser> AspNetUsers{ get; set; }
        public virtual ICollection<MenuRight> MenuRights { get; set; }
    }
}
