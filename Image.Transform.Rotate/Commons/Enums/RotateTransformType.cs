using System.ComponentModel.DataAnnotations;

namespace Image.Transform.Rotate.Commons.Enums
{
    /// <summary>
    /// 影像旋轉類型。
    /// </summary>
    public enum RotateTransformType
    {
        /// <summary>
        /// 自訂影像旋轉。
        /// </summary>
        [Display(Name = "自訂")]
        Custom = 0,

        /// <summary>
        /// GDI+ 影像旋轉。
        /// </summary>
        [Display(Name = "GDI+")]
        GdiPlus = 1,
    }
}