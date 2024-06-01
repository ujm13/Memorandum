using System.ComponentModel;

namespace Memorandum.Common.Enums
{
    /// <summary>
    /// PriorityEnum
    /// </summary>
    public enum PriorityEnum
    {
        [Description("低")]
        Low = 0,
        [Description("中")]
        Medium = 1,
        [Description("高")]
        High = 2,
        [Description("緊急")]
        Critical = 3
    }
}
