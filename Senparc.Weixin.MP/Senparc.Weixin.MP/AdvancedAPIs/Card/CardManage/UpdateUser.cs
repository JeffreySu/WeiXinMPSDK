/************************************************************************ 
 * 项目名称 :  Senparc.Weixin.MP.AdvancedAPIs.Card.CardManage   
 * 项目描述 :      
 * 类 名 称 :  UpdateUser 
 * 版 本 号 :  v1.0.0.0  
 * 说    明 :      
 * 作    者 :  Victor_Lo 
 * 创建时间 :  2015/8/2 17:59:18 
 * 更新时间 :  2015/8/2 17:59:18 
************************************************************************ 
 * Copyright @ Vapps 2015. All rights reserved. 
************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.AdvancedAPIs.Card.CardManage
{
    public class UpdateUser
    {
        /// <summary>
        /// 卡券Code码
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 卡券ID
        /// </summary>
        public string card_id { get; set; }

        /// <summary>
        /// 需要变更的积分，扣除积分用“-“表示
        /// </summary>
        public int add_bonus { get; set; }

        /// <summary>
        /// 商家自定义积分消耗记录，不超过14个汉字
        /// </summary>
        public string record_bonus { get; set; }

        /// <summary>
        /// 需要变更的余额，扣除金额用“-”表示。单位为分。
        /// </summary>
        public int add_balance { get; set; }

        /// <summary>
        /// 商家自定义金额消耗记录，不超过14个汉字。
        /// </summary>
        public string record_balance { get; set; }

        /// <summary>
        /// 创建时字段custom_field1定义类型的最新数值，限制为4个汉字，12字节。
        /// </summary>
        public string custom_field_value1 { get; set; }

        /// <summary>
        /// 创建时字段custom_field2定义类型的最新数值，限制为4个汉字，12字节。
        /// </summary>
        public string custom_field_value2 { get; set; }

        /// <summary>
        /// 创建时字段custom_field3定义类型的最新数值，限制为4个汉字，12字节。
        /// </summary>
        public string custom_field_value3 { get; set; }
    }
}
