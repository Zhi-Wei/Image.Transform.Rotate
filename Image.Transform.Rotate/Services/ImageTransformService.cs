using Image.Transform.Rotate.Services.Interfaces;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Image.Transform.Rotate.Services
{
    /// <summary>
    /// 影像轉換服務。
    /// </summary>
    /// <seealso cref="Image.Transform.Rotate.Services.Interfaces.IImageTransformService" />
    public class ImageTransformService : IImageTransformService
    {
        /// <summary>
        /// 旋轉影像。
        /// </summary>
        /// <param name="sourceBitmap">要旋轉的影像。</param>
        /// <param name="degrees">旋轉角度，–360 至 360 度。</param>
        /// <returns>旋轉後的影像。</returns>
        public Bitmap RotateImage(Bitmap sourceBitmap, double degrees)
        {
            BitmapData sourceData =
                sourceBitmap.LockBits(new Rectangle(0, 0,
                 sourceBitmap.Width, sourceBitmap.Height),
                 ImageLockMode.ReadOnly,
                 PixelFormat.Format32bppArgb);

            int asbStride = Math.Abs(sourceData.Stride);

            byte[] pixelBuffer = new byte[asbStride *
                                          sourceData.Height];

            byte[] resultBuffer = new byte[asbStride *
                                           sourceData.Height];

            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);

            sourceBitmap.UnlockBits(sourceData);

            //Convert to Radians
            degrees = degrees * Math.PI / 180.0;

            //Calculate Offset in order to rotate on image middle
            int xOffset = (int)(sourceBitmap.Width / 2.0);
            int yOffset = (int)(sourceBitmap.Height / 2.0);

            int sourceXY = 0;
            int resultXY = 0;

            Point sourcePoint = new Point();
            Point resultPoint = new Point();

            Rectangle imageBounds = new Rectangle(0, 0,
                                    sourceBitmap.Width,
                                    sourceBitmap.Height);

            for (int row = 0; row < sourceBitmap.Height; row++)
            {
                for (int col = 0; col < sourceBitmap.Width; col++)
                {
                    sourceXY = row * asbStride + col * 4;

                    sourcePoint.X = col;
                    sourcePoint.Y = row;

                    if (sourceXY >= 0 && sourceXY + 3 < pixelBuffer.Length)
                    {
                        resultPoint = this.RotateXY(sourcePoint, degrees, xOffset, yOffset);

                        resultXY = (int)(Math.Round(
                            (resultPoint.Y * asbStride) +
                            (resultPoint.X * 4.0)));

                        if (imageBounds.Contains(resultPoint) && resultXY >= 0)
                        {
                            if (resultXY + 6 < resultBuffer.Length)
                            {
                                Array.Copy(pixelBuffer, sourceXY, resultBuffer, resultXY + 4, 3);
                                resultBuffer[resultXY + 7] = 255;
                            }

                            if (resultXY + 3 < resultBuffer.Length)
                            {
                                Array.Copy(pixelBuffer, sourceXY, resultBuffer, resultXY, 3);
                                resultBuffer[resultXY + 3] = 255;
                            }
                        }
                    }
                }
            }

            Bitmap resultBitmap = new Bitmap(sourceBitmap.Width,
                                     sourceBitmap.Height);

            BitmapData resultData =
                       resultBitmap.LockBits(new Rectangle(0, 0,
                       resultBitmap.Width, resultBitmap.Height),
                       ImageLockMode.WriteOnly,
                       PixelFormat.Format32bppArgb);

            Marshal.Copy(resultBuffer, 0, resultData.Scan0, resultBuffer.Length);

            resultBitmap.UnlockBits(resultData);

            return resultBitmap;
        }

        /// <summary>
        /// 旋轉 X 與 Y 的座標點。
        /// </summary>
        /// <param name="source">來源的點。</param>
        /// <param name="degrees">旋轉角度。</param>
        /// <param name="offsetX">中心點的 X 座標偏移量。</param>
        /// <param name="offsetY">中心點的 Y 座標偏移量。</param>
        /// <returns>旋轉後的座標點。</returns>
        private Point RotateXY(Point source, double degrees, int offsetX, int offsetY)
        {
            return new Point
            {
                X = (int)(Math.Round((source.X - offsetX) *
                    Math.Cos(degrees) - (source.Y - offsetY) *
                    Math.Sin(degrees))) + offsetX,

                Y = (int)(Math.Round((source.X - offsetX) *
                    Math.Sin(degrees) + (source.Y - offsetY) *
                    Math.Cos(degrees))) + offsetY
            };
        }
    }
}