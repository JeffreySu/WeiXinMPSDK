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
    public class CardActivateData
    {
        public string membership_number { get; set; }

        public string code { get; set; }

        public long? activate_begin_time { get; set; }

        public long? activate_end_time { get; set; }

        public string cardId { get; set; }

        public string init_bonus { get; set; }

        public string init_balance { get; set; }

        public string init_custom_field_value1 { get; set; }

        public string init_custom_field_value2 { get; set; }

        public string init_custom_field_value3 { get; set; }

    }
}
