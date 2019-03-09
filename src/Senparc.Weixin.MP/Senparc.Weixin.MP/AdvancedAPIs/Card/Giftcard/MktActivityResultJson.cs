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
    
    文件名：MktActivityResultJson.cs
    文件功能描述：社交立减金活动返回结果


    创建标识：Senparc - 20181008
    

----------------------------------------------------------------*/



using System.CodeDom;
using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.Card
{
    /// <summary>
    /// 创建支付后领取立减金活动接口返回结果
    /// </summary>
    public class CreateActivityResultJson : WxJsonResult
    {
        public string activity_id { get; set; }
    }

    /// <summary>
    /// 使用授权码换取公众号的授权信息返回结果
    /// </summary>
    public class ApiQueryAuthResultJson : WxJsonResult
    {
        public Authorization_Info authorization_info { get; set; }
    }

    public class Authorization_Info
    {
        public string authorizer_appid { get; set; }
        public string authorizer_access_token { get; set; }
        public int expires_in { get; set; }
        public string authorizer_refresh_token { get; set; }
        public Func_Info[] func_info { get; set; }
    }

    public class Func_Info
    {
        public Funcscope_Category funcscope_category { get; set; }
        public Confirm_Info confirm_info { get; set; }
    }

    public class Funcscope_Category
    {
        public int id { get; set; }
    }

    public class Confirm_Info
    {
        public int need_confirm { get; set; }
        public int already_confirm { get; set; }
    }

    /// <summary>
    /// 获取授权方的账户信息返回结果
    /// </summary>
    public class ApiGetAuthorizerInfoResultJson : WxJsonResult
    {
        public Authorizer_Info authorizer_info { get; set; }
        public string qrcode_url { get; set; }
        public Authorization_Info authorization_info { get; set; }
    }

    public class Authorizer_Info
    {
        public string nick_name { get; set; }
        public string head_img { get; set; }
        public Service_Type_Info service_type_info { get; set; }
        public Verify_Type_Info verify_type_info { get; set; }
        public string user_name { get; set; }
        public string alias { get; set; }
    }

    public class Service_Type_Info
    {
        public int id { get; set; }
    }

    public class Verify_Type_Info
    {
        public int id { get; set; }
    }

}
