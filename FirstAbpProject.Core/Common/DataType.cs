using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAbpProject.Common
{
    /// <summary>
    /// represents datatype of each cooler
    /// </summary>
    public enum DataType : int
    {
        [Description("可口可乐")]
        Coke = 1,

        [Description("沃尔玛")]
        Walmart = 2
    }
}
