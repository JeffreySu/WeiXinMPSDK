#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2024 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2024 Senparc
    
    文件名：DraftApi.cs
    文件功能描述：新建草稿接口 返回结果
    
    
    创建标识：dupeng0811 - 20220227

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.Draft.DraftJson
{
    /// <summary>
    /// 创建草稿的返回
    /// </summary>
    public class AddDraftResultJson  : WxJsonResult
    {
        /// <summary>
        /// 上传后的获取标志，长度不固定，但不会超过 128 字符
        /// </summary>
        public string media_id { get; set; }
    }
}