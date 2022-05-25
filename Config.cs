using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SedentaryCountdown
{
    public class Config
    {
        /// <summary>
        /// 倒计时分钟数
        /// </summary>
        public double CountdownMinute { get; set; } = 60;

        /// <summary>
        /// 休息分钟数
        /// </summary>
        public double RestMinute { get; set; } = 5;
    }
}
