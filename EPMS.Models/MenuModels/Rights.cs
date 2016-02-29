namespace EPMS.Models.MenuModels
{
    public class Rights
    {
        public int MenuId { get; set; }
        public string MenuTitle { get; set; }
        public string MenuTitleA { get; set; }
        public bool IsSelected { get; set; }
        public string PermissionKey { get; set; }

        public bool IsParent { get; set; }
        public int? ParentId { get; set; }
    }
}
