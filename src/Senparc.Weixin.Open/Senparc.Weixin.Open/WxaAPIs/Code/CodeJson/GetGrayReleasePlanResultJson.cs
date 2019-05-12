using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.WxaAPIs
{
    [Serializable]
    public class GetGrayReleasePlanResultJson : CodeResultJson
    {
        public GrayReleasePlan gray_release_plan { get; set; }
    }

    [Serializable]
    public class GrayReleasePlan
    {
        /// <summary>
        /// 0:初始状态 1:执行中 2:暂停中 3:执行完毕 4:被删除
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public long create_timestamp { get; set; }

        public int gray_percentage { get; set; }
    }
}
