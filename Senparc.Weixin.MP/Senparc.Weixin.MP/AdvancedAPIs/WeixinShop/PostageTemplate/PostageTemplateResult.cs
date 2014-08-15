using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    public class PostageTemplateResult
    {
        public int errcode { get; set; }//错误码
        public string errmsg { get; set; }//错误信息
    }

    /// <summary>
    /// 添加邮费模板返回结果
    /// </summary>
    public class AddPostageTemplateResult : PostageTemplateResult
    {
        public int template_id { get; set; }//邮费模板ID
    }

    /// <summary>
    /// 删除邮费模板返回结果
    /// </summary>
    public class DeletePostageTemplateResult : PostageTemplateResult
    {
    }

    /// <summary>
    /// 修改邮费模板返回结果
    /// </summary>
    public class UpdatePostageTemplateResult : PostageTemplateResult
    {
    }

    /// <summary>
    /// 获取指定ID的邮费模板返回结果
    /// </summary>
    public class GetByIdPostageTemplateResult : PostageTemplateResult
    {
        public Template_Info template_info { get; set; }
    }

    public class Template_Info
    {
        public int Id { get; set; }
        public string Name { get; set; }//邮费模板名称
        public int Assumer { get; set; }//支付方式(0-买家承担运费, 1-卖家承担运费)
        public int Valuation { get; set; }//计费单位(0-按件计费, 1-按重量计费, 2-按体积计费，目前只支持按件计费，默认为0)
        public List<TopFeeItem> TopFee { get; set; }//具体运费计算
    }

    public class TopFeeItem
    {
        public int Type { get; set; }//快递类型ID(参见增加商品/快递列表)
        public Normal Normal { get; set; }//默认邮费计算方法
        public List<CustomItem> Custom { get; set; }//指定地区邮费计算方法
    }

    public class Normal
    {
        public int StartStandards { get; set; }//起始计费数量(比如计费单位是按件, 填2代表起始计费为2件)
        public int StartFees { get; set; }//起始计费金额(单位: 分）
        public int AddStandards { get; set; }//递增计费数量
        public int AddFees { get; set; }//递增计费金额(单位 : 分)
    }

    public class CustomItem
    {
        public int StartStandards { get; set; }//起始计费数量
        public int StartFees { get; set; }//起始计费金额(单位: 分）
        public int AddStandards { get; set; }//递增计费数量
        public int AddFees { get; set; }//递增计费金额(单位 : 分)
        public string DestCountry { get; set; }//指定国家
        public string DestProvince { get; set; }//指定省份
        public string DestCity { get; set; }//指定城市
    }

    /// <summary>
    /// 获取所有邮费模板
    /// </summary>
    public class GetAllPostageTemplateResult : PostageTemplateResult
    {
        public List<Template_Info> templates_info { get; set; } //所有邮费模板集合
    }
}

