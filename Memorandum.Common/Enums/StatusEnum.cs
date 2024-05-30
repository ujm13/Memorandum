using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
