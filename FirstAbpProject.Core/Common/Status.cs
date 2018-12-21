using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAbpProject.Common
{
    /// <summary>
    /// represents working status
    /// </summary>
    public enum Status : int
    {
        [Description("正常")]
        Alive = 0,

        [Description("预警")]
        Warning = 1,

        [Description("警报")]
        Dead = 2
    }
}
