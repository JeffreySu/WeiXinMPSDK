
using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.WxApp.WxAppJson
{
    /// <summary>
    /// 文本是否含有违法违规内容返回结果
    /// </summary>
    public class MsgSecCheckResult : WxJsonResult
    {
        /// <summary>
        /// 详细检测结果
        /// </summary>
        public List<MsgSecCheck_Detail> detail { get; set; }

        /// <summary>
        /// 唯一请求标识，标记单次请求
        /// </summary>
        public string trace_id { get; set; }

        /// <summary>
        /// 综合结果
        /// </summary>
        public MsgSecCheck_Result result { get; set; }
    }

    [Serializable]
    public class MsgSecCheck_Detail
    {
        /// <summary>
        /// 策略类型
        /// </summary>
        public string strategy { get; set; }

        /// <summary>
        /// 错误码，仅当该值为0时，该项结果有效
        /// </summary>
        public int errcode { get; set; }

        /// <summary>
        /// 建议，有risky、pass、review三种值
        /// </summary>
        public string suggest { get; set; }

        /// <summary>
        /// 命中标签枚举值，100 正常；10001 广告；20001 时政；20002 色情；20003 辱骂；20006 违法犯罪；20008 欺诈；20012 低俗；20013 版权；21000 其他
        /// </summary>
        public int label { get; set; }

        /// <summary>
        /// 命中的自定义关键词
        /// </summary>
        public string keyword { get; set; }

        /// <summary>
        /// 0-100，代表置信度，越高代表越有可能属于当前返回的标签（label）
        /// </summary>
        public int prob { get; set; }
    }

    [Serializable]
    public class MsgSecCheck_Result
    {
        /// <summary>
        /// 建议，有risky、pass、review三种值
        /// </summary>
        public string suggest { get; set; }

        /// <summary>
        /// 命中标签枚举值，100 正常；10001 广告；20001 时政；20002 色情；20003 辱骂；20006 违法犯罪；20008 欺诈；20012 低俗；20013 版权；21000 其他
        /// </summary>
        public int label { get; set; }
    }
}
