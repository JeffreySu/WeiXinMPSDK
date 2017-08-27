#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2017 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2017 Senparc
    
    文件名：SnsApi.cs
    文件功能描述：小程序微信登录
    
    创建标识：Senparc - 20170827

----------------------------------------------------------------*/

using System;
using System.IO;
using System.Threading.Tasks;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.Open.WxaAPIs.Template.TemplateJson;

namespace Senparc.Weixin.Open.WxaAPIs.Template
{
    /// <summary>
    /// 小程序模板消息接口
    /// </summary>
    public static class TemplateApi
    {
        #region 同步请求


        #region 模板快速设置

        /// <summary>
        /// 获取小程序模板库标题列表
        /// </summary>
        /// <param name="accessToken">接口调用凭证</param>
        /// <param name="offset">offset和count用于分页，表示从offset开始，拉取count条记录，offset从0开始，count最大为20。</param>
        /// <param name="count">offset和count用于分页，表示从offset开始，拉取count条记录，offset从0开始，count最大为20。</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        public static LibraryListJsonResult LibraryList(string accessToken, int offset, int count, int timeOut = Config.TIME_OUT)
        {
            const string urlFormat = "https://api.weixin.qq.com/cgi-bin/wxopen/template/library/list?access_token={0}";
            var data = new
            {
                offset = offset,
                count = count
            };
            return CommonJsonSend.Send<LibraryListJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
        }

        #endregion


        #endregion

        #region 异步请求

     

        #endregion
    }
}