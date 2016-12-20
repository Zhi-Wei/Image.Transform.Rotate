using Image.Transform.Rotate.Commons.Enums;
using Image.Transform.Rotate.Services;
using Image.Transform.Rotate.Services.Interfaces;

namespace Image.Transform.Rotate.Factories
{
    /// <summary>
    /// 影像轉換策略工廠。
    /// </summary>
    public class ImageTransformStrategyFactory
    {
        /// <summary>
        /// 取得影像轉換策略的服務實體。
        /// </summary>
        /// <param name="type">影像旋轉類型。</param>
        /// <returns>影像轉換服務實體。</returns>
        public static IImageTransformService GetStrategy(RotateTransformType type)
        {
            IImageTransformService service;
            switch (type)
            {
                case RotateTransformType.GdiPlus:
                    service = new GdiPlusTransformService();
                    break;

                default:
                    service = new ImageTransformService();
                    break;
            }
            return service;
        }
    }
}