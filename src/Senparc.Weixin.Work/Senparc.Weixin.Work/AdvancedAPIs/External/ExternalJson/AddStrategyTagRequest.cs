using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.External.ExternalJson
{
    /// <summary>
    /// 添加企业客户标签 请求参数
    /// <para>注意：</para>
    /// <para>1、如果填写了group_id参数，则group_name和标签组的order参数会被忽略。</para>
    /// <para>2、如果填写的group_name和此规则组下的其他标签组同名，则会将相关标签加入已存在的同名标签组下。</para>
    /// <para>3、不支持创建空标签组。</para>
    /// <para>4、标签组内的标签不可同名，如果传入多个同名标签，则只会创建一个。</para>
    /// </summary>
    public class AddStrategyTagRequest
    {
        /// <summary>
        /// 	规则组id
        /// </summary>
        public int strategy_id { get; set; }
        /// <summary>
        /// 标签组id
        /// </summary>
        public string group_id { get; set; }
        /// <summary>
        /// 标签组名称，最长为30个字符
        /// </summary>
        public string group_name { get; set; }
        /// <summary>
        /// 标签组次序值。order值大的排序靠前。有效的值范围是[0, 2^32)
        /// </summary>
        public int? order { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public IList<AddStrategyTagRequest_Tag> tag { get; set; }
        /// <summary>
        /// 授权方安装的应用agentid。仅旧的第三方多应用套件需要填此参数
        /// </summary>
        public int? agentid { get; set; }
    }

    public class AddStrategyTagRequest_Tag
    {
        /// <summary>
        /// 添加的标签名称，最长为30个字符
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 标签次序值。order值大的排序靠前。有效的值范围是[0, 2^32)
        /// </summary>
        public long order { get; set; }
    }

}
