#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2019 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：ActivateUserFormSetData.cs
    文件功能描述：会员卡设置开卡字段需要的数据
    
    
    创建标识：Senparc - 20150910
 
    修改标识：Senparc - 20160808
    修改描述：修改BaseForm
----------------------------------------------------------------*/



using System.Collections.Generic;

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

        /// <summary>
        /// 自定义富文本类型
        /// </summary>
        public List<RichField> rich_field_list { get; set; }
    }

    /// <summary>
    /// 自定义富文本类型，包含以下三个字段
    /// 富文本类型
    /// FORM_FIELD_RADIO 自定义单选
    /// FORM_FIELD_SELECT 自定义选择项
    /// FORM_FIELD_CHECK_BOX 自定义多选
    /// </summary>
    public class RichField
    {
        public RichFieldType type { get; set; }
        /// <summary>
        /// 否 string(32)  职业  字段名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 否 arry    见上述示例 选择项
        /// </summary>
        public string[] values { get; set; }
    }

    /// <summary>
    /// 富文本类型
    /// </summary>
    public enum RichFieldType
    {
        /// <summary>
        /// 自定义单选
        /// </summary>
        FORM_FIELD_RADIO = 0,
        /// <summary>
        /// 自定义选择项
        /// </summary>
        FORM_FIELD_SELECT = 1,
        /// <summary>
        /// 自定义多选
        /// </summary>
        FORM_FIELD_CHECK_BOX
    }
}
