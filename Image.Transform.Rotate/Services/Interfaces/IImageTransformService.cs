using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image.Transform.Rotate.Services.Interfaces
{
    /// <summary>
    /// 定義影像轉換服務方法。
    /// </summary>
    public interface IImageTransformService
    {
        /// <summary>
        /// 旋轉影像。
        /// </summary>
        /// <param name="degrees">旋轉角度，–360 至 360 度。</param>
        /// <returns>旋轉後的影像。</returns>
        Bitmap RotateImage(double degrees);
    }
}
