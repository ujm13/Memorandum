using System.ComponentModel;

namespace Memorandum.Common.Enums
{
    /// <summary>
    /// StatusEnum
    /// </summary>
    public enum StatusEnum
    {
        [Description("已完成")]
        Completed,
        [Description("進行中")]
        Ongoing,
        [Description("未開始")]
        NotStarted
    }
}
