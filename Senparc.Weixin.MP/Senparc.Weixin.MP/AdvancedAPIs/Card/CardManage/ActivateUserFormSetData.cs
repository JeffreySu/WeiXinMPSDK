/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：ActivateUserFormSetData.cs
    文件功能描述：会员卡设置开卡字段需要的数据
    
    
    创建标识：Senparc - 20150910
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.AdvancedAPIs.Card
{
    /// <summary>
    /// 会员卡设置开卡字段需要的数据
    /// </summary>
    public class ActivateUserFormSetData
    {
        /// <summary>
        /// 卡券ID
        /// </summary>
        public string card_id { get; set; }
        /// <summary>
        /// 会员卡激活时的必填选项
        /// </summary>
        public BaseForm required_form { get; set; }
        /// <summary>
        /// 会员卡激活时的选填项
        /// </summary>
        public BaseForm optional_form { get; set; }
    }

    public class BaseForm
    {
        /// <summary>
        /// 微信格式化的选项类型
        /// USER_FORM_INFO_FLAG_MOBILE	手机号
        /// USER_FORM_INFO_FLAG_NAME	姓名
        /// USER_FORM_INFO_FLAG_BIRTHDAY	生日
        /// USER_FORM_INFO_FLAG_IDCARD	身份证
        /// USER_FORM_INFO_FLAG_EMAIL	邮箱
        /// USER_FORM_INFO_FLAG_DETAIL_LOCATION	详细地址
        /// USER_FORM_INFO_FLAG_EDUCATION_BACKGROUND	教育背景
        /// USER_FORM_INFO_FLAG_CAREER	职业
        /// USER_FORM_INFO_FLAG_INDUSTRY	行业
        /// USER_FORM_INFO_FLAG_INCOME	收入
        /// USER_FORM_INFO_FLAG_HABIT	兴趣爱好
        /// </summary>
        public string[] common_field_id_list { get; set; }
        /// <summary>
        /// 自定义选项名称
        /// </summary>
        public string[] custom_field_list { get; set; }
    }
}
