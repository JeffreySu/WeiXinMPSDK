/************************************************************************ 
 * 项目名称 :  Senparc.Weixin.MP.AdvancedAPIs.Card.CardManage   
 * 项目描述 :      
 * 类 名 称 :  CardActivate 
 * 版 本 号 :  v1.0.0.0  
 * 说    明 :      
 * 作    者 :  Victor_Lo 
 * 创建时间 :  2015/7/30 16:08:05 
 * 更新时间 :  2015/7/30 16:08:05 
************************************************************************ 
 * Copyright @ Vapps 2015. All rights reserved. 
************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.AdvancedAPIs.Card.CardManage
{
    /// <summary>
    /// 会员卡激活类
    /// </summary>
    public class CardActivateData
    {
        /// <summary>
        /// 会员卡编号，由开发者填入，作为序列号显示在用户的卡包里。可与Code码保持等值。
        /// </summary>
        public string membership_number { get; set; }

        /// <summary>
        /// 创建会员卡时获取的初始code。
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 激活后的有效起始时间。若不填写默认以创建时的 data_info 为准。Unix时间戳格式。
        /// </summary>
        public long? activate_begin_time { get; set; }

        /// <summary>
        /// 激活后的有效截至时间。若不填写默认以创建时的 data_info 为准。Unix时间戳格式
        /// </summary>
        public long? activate_end_time { get; set; }

        /// <summary>
        /// 卡Id
        /// </summary>
        //public string cardId { get; set; }

        /// <summary>
        /// 初始积分，不填为0。
        /// </summary>
        public string init_bonus { get; set; }

        /// <summary>
        /// 初始余额，不填为0。
        /// </summary>
        public string init_balance { get; set; }

        /// <summary>
        /// 创建时字段custom_field1定义类型的初始值，限制为4个汉字，12字节。
        /// </summary>
        public string init_custom_field_value1 { get; set; }

        /// <summary>
        /// 创建时字段custom_field1定义类型的初始值，限制为4个汉字，12字节。
        /// </summary>
        public string init_custom_field_value2 { get; set; }

        /// <summary>
        /// 创建时字段custom_field1定义类型的初始值，限制为4个汉字，12字节。
        /// </summary>
        public string init_custom_field_value3 { get; set; }

    }
}
