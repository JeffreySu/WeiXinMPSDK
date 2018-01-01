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
  
    文件名：ReturnResult.cs
    文件功能描述：微微嗨开发者信息
    
    
    创建标识：Senparc - 20150319
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.AppStore
{
    /// <summary>
    /// 微微嗨开发者信息。申请开发者：http://www.weiweihi.com/User/Developer/Apply
    /// </summary>
    public class DeveloperInfo
    {
        ///// <summary>
        ///// 微微嗨开发者的AppKey，可以在 http://www.weiweihi.com/Developer/Developer 找到
        ///// </summary>
        //public string AppKey { get; set; }

        ///// <summary>
        ///// 微微嗨开发者的Secret，可以在 http://www.weiweihi.com/Developer/Developer 找到
        ///// </summary>
        //public string AppSecret { get; set; }

        /// <summary>
        /// 在www.weiweihi.com对接微信公众号之后，自动生成的WeiweihiKey。
        /// </summary>
        public string WeiweihiKey { get; set; }
    }
}
