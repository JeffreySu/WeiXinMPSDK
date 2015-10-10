using System.Collections.Generic;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 邮费模板信息
    /// </summary>
    public class BaseExpressData
    {
        public string delivery_template { get; set; }
    }

    public class DeliveryTemplate
    {
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

    public class TopFeeItem
    {
        /// <summary>
        /// 快递类型ID(参见增加商品/快递列表)
        /// </summary>
        public long Type { get; set; }
        /// <summary>
        /// 默认邮费计算方法
        /// </summary>
        public Normal Normal { get; set; }
        /// <summary>
        /// 指定地区邮费计算方法
        /// </summary>
        public List<CustomItem> Custom { get; set; }
    }

    public class Normal
    {
        /// <summary>
        /// 起始计费数量(比如计费单位是按件, 填2代表起始计费为2件)
        /// </summary>
        public int StartStandards { get; set; }
        /// <summary>
        /// 起始计费金额(单位: 分）
        /// </summary>
        public int StartFees { get; set; }
        /// <summary>
        /// 递增计费数量
        /// </summary>
        public int AddStandards { get; set; }
        /// <summary>
        /// 递增计费金额(单位 : 分)
        /// </summary>
        public int AddFees { get; set; }
    }

    public class CustomItem
    {
        /// <summary>
        /// 起始计费数量
        /// </summary>
        public int StartStandards { get; set; }
        /// <summary>
        /// 起始计费金额(单位: 分）
        /// </summary>
        public int StartFees { get; set; }
        /// <summary>
        /// 递增计费数量
        /// </summary>
        public int AddStandards { get; set; }
        /// <summary>
        /// 递增计费金额(单位 : 分)
        /// </summary>
        public int AddFees { get; set; }
        /// <summary>
        /// 指定国家
        /// </summary>
        public string DestCountry { get; set; }
        /// <summary>
        /// 指定省份
        /// </summary>
        public string DestProvince { get; set; }
        /// <summary>
        /// 指定城市
        /// </summary>
        public string DestCity { get; set; }
    }
    /// <summary>
    /// 增加邮费模板
    /// </summary>
    public class AddExpressData : BaseExpressData
    {
    }

    /// <summary>
    /// 修改邮费模板Post数据
    /// </summary>
    public class UpDateExpressData : BaseExpressData
    {
        /// <summary>
        /// 邮费模板Id
        /// </summary>
        public int template_id { get; set; }
    }
}

