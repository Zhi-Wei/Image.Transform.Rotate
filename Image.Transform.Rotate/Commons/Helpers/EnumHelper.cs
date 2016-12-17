using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Image.Transform.Rotate.Commons.Helpers
{
    /// <summary>
    /// 列舉助手。
    /// </summary>
    public class EnumHelper
    {
        /// <summary>
        /// 取得顯示名稱。
        /// </summary>
        /// <typeparam name="T">列舉型別。</typeparam>
        /// <param name="value">欲取得顯示名稱的列舉值。</param>
        /// <returns>顯示名稱。</returns>
        public static string GetDisplayName<T>(T value)
            where T : struct, IComparable, IFormattable, IConvertible
        {
            string valueString = value.ToString();
            FieldInfo fieldInfo = value.GetType()
                .GetField(valueString, BindingFlags.Public | BindingFlags.Static);

            IEnumerable<DisplayAttribute> displayAttributes =
                 fieldInfo.GetCustomAttributes<DisplayAttribute>(false);

            if (displayAttributes == null || displayAttributes.Any() == false)
            {
                return valueString;
            }
            return displayAttributes.First().Name;
        }

        /// <summary>
        /// 取得顯示名稱的索引鍵和值集合。
        /// </summary>
        /// <typeparam name="T">列舉型別。</typeparam>
        /// <returns>顯示名稱的索引鍵和值集合。</returns>
        public static IDictionary<T, string> GetDisplayNameDictionary<T>()
            where T : struct, IComparable, IFormattable, IConvertible
        {
            return Enum.GetValues(typeof(T))
                   .Cast<T>()
                   .ToDictionary(x => x, x => GetDisplayName(x));
        }
    }
}