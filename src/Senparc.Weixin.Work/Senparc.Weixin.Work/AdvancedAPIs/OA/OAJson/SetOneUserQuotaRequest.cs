using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.OA.OAJson
{
    /// <summary>
    /// 请求参数
    /// </summary>
    public class SetOneUserQuotaRequest
    {
        /// <summary>
        /// 需要修改假期余额的成员的userid
        /// </summary>
        public string userid { get; set; }

        /// <summary>
        /// 假期id
        /// </summary>
        public string vacation_id { get; set; }

        /// <summary>
        /// 设置的假期余额,单位为秒
        /// 不能大于1000天或24000小时，当假期时间刻度为按小时请假时，必须为360整倍数，即0.1小时整倍数，按天请假时，必须为8640整倍数，即0.1天整倍数
        /// </summary>
        public int leftduration { get; set; }

        /// <summary>
        /// 假期时间刻度：0-按天请假；1-按小时请假
        /// 主要用于校验，必须等于企业假期管理配置中设置的假期时间刻度类型
        /// </summary>
        public int time_attr { get; set; }

        /// <summary>
        /// 修改备注，用于显示在假期余额的修改记录当中，可对修改行为作说明，不超过200字符
        /// 非必填
        /// </summary>
        public string remarks { get; set; }
    }
}
