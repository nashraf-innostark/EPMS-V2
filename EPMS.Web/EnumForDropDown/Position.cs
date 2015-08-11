using System.ComponentModel.DataAnnotations;
using EPMS.WebModels.Resources.Shared;

namespace EPMS.Web.EnumForDropDown
{
    public enum Position
    {
        [Display(ResourceType = typeof (Common), Name = "PositionTop")]
        Top = 1,
        [Display(ResourceType = typeof (Common), Name = "PositionBottom")]
        Bottom = 2,
        [Display(ResourceType = typeof (Common), Name = "PositionLeft")]
        Left = 3,
        [Display(ResourceType = typeof (Common), Name = "PositionRight")]
        Right = 4
    }
}