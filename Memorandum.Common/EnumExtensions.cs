using System.ComponentModel;

namespace Memorandum.Common
{
    public static class EnumExtensions
    {
        /// <summary>
        /// 取得Description
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attribute.Length == 0 ? value.ToString() : attribute[0].Description;
        }
    }
}
