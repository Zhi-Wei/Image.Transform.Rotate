using Image.Transform.Rotate.Services.Interfaces;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Image.Transform.Rotate.Services
{
    /// <summary>
    /// GDI+ 影像轉換服務。
    /// </summary>
    public class GdiPlusTransformService : IImageTransformService
    {
        /// <summary>
        /// 旋轉影像。
        /// </summary>
        /// <param name="sourceBitmap">要旋轉的影像。</param>
        /// <param name="degrees">旋轉角度，–360 至 360 度。</param>
        /// <returns>旋轉後的影像。</returns>
        public Bitmap RotateImage(Bitmap sourceBitmap, double degrees)
        {
            Bitmap returnBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);
            using (Graphics graphics = Graphics.FromImage(returnBitmap))
            {
                graphics.TranslateTransform(
                    sourceBitmap.Width / 2.0f, sourceBitmap.Height / 2.0f);
                graphics.RotateTransform(Convert.ToSingle(degrees));
                graphics.TranslateTransform(
                    -sourceBitmap.Width / 2.0f, -sourceBitmap.Height / 2.0f);
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(sourceBitmap, 0, 0);
            }
            return returnBitmap;
        }
    }
}