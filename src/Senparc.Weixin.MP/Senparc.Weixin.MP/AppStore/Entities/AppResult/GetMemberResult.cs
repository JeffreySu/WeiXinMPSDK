#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2018 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2018 Senparc
  
    文件名：GetMemberResult.cs
    文件功能描述：获取微信会员信息结果
    
    
    创建标识：Senparc - 20150319
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.AppStore
{
    /// <summary>
    /// 获取微信会员信息结果
    /// </summary>
    public class GetMemberResult : AppResult<NormalAppData>
    {
        public string OpenId { get; set; }
        public string NickName { get; set; }
        public WeixinSex Sex { get; set; }
        /// <summary>
        /// Base64格式的头像信息，当提供HeadImageUrl时不再提供HeadImageBase64
        /// </summary>
        public string HeadImageBase64 { get; set; }
        /// <summary>
        /// 头像URL地址
        /// </summary>
        public string HeadImageUrl { get; set; }
    }
}
