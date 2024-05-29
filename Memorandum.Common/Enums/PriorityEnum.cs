using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memorandum.Common.Enums
{
    
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
