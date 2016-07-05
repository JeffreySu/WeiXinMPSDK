using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 添加邮费模板返回结果
    /// </summary>
    public class AddExpressResult : WxJsonResult
    {
        /// <summary>
        /// 邮费模板ID
        /// </summary>
        public long template_id { get; set; }
    }

    /// <summary>
    /// 获取指定ID的邮费模板返回结果
    /// </summary>
    public class GetByIdExpressResult : WxJsonResult
    {
        public Template_Info template_info { get; set; }
    }

    public class Template_Info
    {
        /// <summary>
        /// 邮费模板ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 邮费模板名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 支付方式(0-买家承担运费, 1-卖家承担运费)
        /// </summary>
        public int Assumer { get; set; }
        /// <summary>
        /// 计费单位(0-按件计费, 1-按重量计费, 2-按体积计费，目前只支持按件计费，默认为0)
        /// </summary>
        public int Valuation { get; set; }
        /// <summary>
        /// 具体运费计算
        /// </summary>
        public List<TopFeeItem> TopFee { get; set; }
    }

    /// <summary>
    /// 获取所有邮费模板
    /// </summary>
    public class GetAllExpressResult : WxJsonResult
    {
        /// <summary>
        /// 所有邮费模板集合
        /// </summary>
        public List<Template_Info> templates_info { get; set; } 
    }
}

