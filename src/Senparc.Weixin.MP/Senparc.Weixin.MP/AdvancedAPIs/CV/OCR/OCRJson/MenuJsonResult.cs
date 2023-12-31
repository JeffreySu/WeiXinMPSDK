/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：MenuJsonResult.cs
    文件功能描述：OCR 菜单识别返回结果
    
    
    创建标识：yaofeng - 20231204

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.CV.OCR
{
    /// <summary>
    /// 菜单识别
    /// </summary>
    public class MenuJsonResult : WxJsonResult
    {
        /// <summary>
        /// {\"menu_items\": [{\"name\": \"酱鸭\", \"price\": 48.0}, {\"name\": \"龙虾\", \"price\": 80.0}, {\"name\": \"芝麻鸭块\", \"price\": 48.0}, {\"name\": \"皮皮虾\", \"price\": 70.0}, {\"name\": \"特色辣鸭脚\", \"price\": 46.0}, {\"name\": \"花甲\", \"price\": 30.0}, {\"name\": \"小鸭翅\", \"price\": 46.0}, {\"name\": \"鸭肠\", \"price\": 40.0}, {\"name\": \"微辣大鸭脚\", \"price\": 43.0}, {\"name\": \"八宝\", \"price\": 10.0}, {\"name\": \"大鸭翅\", \"price\": 43.0}, {\"name\": \"千叶豆腐\", \"price\": 10.0}, {\"name\": \"鸭脖\", \"price\": 35.0}, {\"name\": \"毛豆\", \"price\": 15.0}, {\"name\": \"鸭锁骨\", \"price\": 35.0}, {\"name\": \"螺丝\", \"price\": 15.0}, {\"name\": \"特色蟹脚\", \"price\": 55.0}, {\"name\": \"藕片\", \"price\": 15.0}, {\"name\": \"鱿鱼须\", \"price\": 70.0}, {\"name\": \"腐竹\", \"price\": 15.0}, {\"name\": \"鸭下巴\", \"price\": 70.0}, {\"name\": \"鸭头\", \"price\": 5.0}, {\"name\": \"无骨鸭脚\", \"price\": 70.0}, {\"name\": \"辣椒酱\", \"price\": 10.0}, {\"name\": \"鸭舌\", \"price\": 138.0}, {\"name\": \"花生米\", \"price\": 5.0}, {\"name\": \"鸭胗\", \"price\": 55.0}, {\"name\": \"萝卜干\", \"price\": 12.0}, {\"name\": \"牛肉串\", \"price\": 65.0}, {\"name\": \"盐菜\", \"price\": 12.0}]}
        /// </summary>
        public string content { get; set; }
    }
}
