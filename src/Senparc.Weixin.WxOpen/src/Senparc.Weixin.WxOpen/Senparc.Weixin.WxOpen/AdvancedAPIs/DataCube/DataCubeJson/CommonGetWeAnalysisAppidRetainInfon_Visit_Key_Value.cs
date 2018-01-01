using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.DataCube
{
    /// 公共类：小程序“数据分析”接口 - 访问留存：x留存 返回结果 - visit_uv及相关属性
    /// </summary>
    public class CommonGetWeAnalysisAppidRetainInfon_Visit_Key_Value
    {
        /// <summary>
        /// 标识。
        /// 日留存：0开始，0表示当天，1表示1天后，依此类推，key取值分别是：0,1,2,3,4,5,6,7,14,30
        /// 周留存：0开始，0表示当周，1表示1周后，依此类推，key取值分别是：0,1,2,3,4
        /// 月留存：标识，0开始，0表示当月，1表示1月后，key取值分别是：0,1
        /// </summary>
        public int key { get; set; }
        /// <summary>
        /// key对应日期的新增用户数/活跃用户数（key=0时）或留存用户数（k>0时）
        /// </summary>
        public int value { get; set; }
    }
}
