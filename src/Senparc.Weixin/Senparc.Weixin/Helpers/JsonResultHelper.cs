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
    
    文件名：JsonResultHelper.cs
    文件功能描述：JsonResult 帮助类
    
    创建标识：Senparc - 20220731

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using Senparc.Weixin.Exceptions;
using System.Text.RegularExpressions;

namespace Senparc.Weixin.Helpers
{
    /// <summary>
    /// JsonResult 帮助类
    /// </summary>
    public static class JsonResultHelper
    {
        /// <summary>
        /// 获取错误信息中的 rid 信息
        /// </summary>
        /// <param name="errmsg">errmsg</param>
        /// <returns></returns>
        public static string GetRid(string errmsg)
        {
            var regex = new Regex("rid:(?<rid>[^\"]+)");
            var ridResult = regex.Match(errmsg);
            if (ridResult.Success)
            {
                return ridResult.Groups["rid"].Value;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取错误信息中的 rid 信息
        /// </summary>
        /// <param name="wxJsonResult">WxJsonResult</param>
        /// <returns></returns>
        public static string GetRid(this WxJsonResult wxJsonResult)
        {
            return GetRid(wxJsonResult.errmsg);
        }

        /// <summary>
        /// 获取错误信息中的 rid 信息
        /// </summary>
        /// <param name="ex">ErrorJsonResultException</param>
        /// <returns></returns>
        public static string GetRid(this ErrorJsonResultException ex)
        {
            return ex.JsonResult.GetRid();
        }
    }
}
