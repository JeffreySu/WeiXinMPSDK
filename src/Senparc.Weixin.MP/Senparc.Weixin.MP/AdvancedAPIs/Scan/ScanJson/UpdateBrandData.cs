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
    
    文件名：UpdateBrandData.cs
    文件功能描述：更新商品信息数据
    
    
    创建标识：Senparc - 20181008
    
    

----------------------------------------------------------------*/

using Senparc.CO2NET.Helpers.Serializers;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Helpers;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 更新商品信息数据
    /// </summary>
    public class UpdateBrandData
    {
        public string keystandard { get; set; }
        public string keystr { get; set; }
        public Brand_Info brand_info { get; set; }
    }

    public class Brand_Info
    {
        public Action_Info action_info { get; set; }
    }

    public class Action_Info
    {
        public Action_List[] action_list { get; set; }
    }

    public class Action_List
    {
        public string type { get; set; }
        public string name { get; set; }
        public string link { get; set; }
        public string image { get; set; }
        public string showtype { get; set; }
        public string appid { get; set; }
        public string text { get; set; }
    }

    /// <summary>
    /// 更新商品信息返回结果
    /// </summary>
    public class UpdateBrandResultJson : WxJsonResult
    {
        public string pid { get; set; }
    }
}
