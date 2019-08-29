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

    文件名：Post.cs
    文件功能描述：Post


    创建标识：Senparc - 20150211

    修改标识：Senparc - 20150303
    修改描述：整理接口

    修改标识：Senparc - 20150312
    修改描述：开放代理请求超时时间

    修改标识：zhanghao-kooboo - 20150316
    修改描述：增加

    修改标识：Senparc - 20150407
    修改描述：发起Post请求方法修改，为了上传永久视频素材
 
    修改标识：Senparc - 20160720
    修改描述：增加了PostFileGetJsonAsync的异步方法（与之前的方法多一个参数）

    修改标识：Senparc - 20170409
    修改描述：v4.11.9 修改Download方法

    修改标识：Senparc - 20180928
    修改描述：将 CO2NET 已经移植的方法标记为过期
    
    修改标识：Senparc - 20190129
    修改描述：统一 CommonJsonSend.Send<T>() 方法请求接口

    修改标识：Senparc - 20190602
    修改描述：添加 Config.ThrownWhenJsonResultFaild 判断

----------------------------------------------------------------*/



using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Exceptions;
using Senparc.CO2NET.Helpers;

#if NET45
using System.Web.Script.Serialization;
using Senparc.Weixin.HttpUtility;
#else
using System.Net.Http;
#endif

namespace Senparc.Weixin.HttpUtility
{
    /// <summary>
    /// Post 请求处理
    /// </summary>
    public static class Post
    {
        /// <summary>
        /// 获取Post结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="returnText"></param>
        /// <returns></returns>
        public static T GetResult<T>(string returnText)
        {
            if (returnText.Contains("errcode"))
            {
                //可能发生错误
                WxJsonResult errorResult = SerializerHelper.GetObject<WxJsonResult>(returnText);

                ErrorJsonResultException ex = null;
                if (errorResult.errcode != ReturnCode.请求成功)
                {
                    //发生错误，记录异常
                    ex =  new ErrorJsonResultException(
                        string.Format("微信Post请求发生错误！错误代码：{0}，说明：{1}",
                                      (int)errorResult.errcode,
                                      errorResult.errmsg),
                        null, errorResult);
                }

                if (Config.ThrownWhenJsonResultFaild && ex != null)
                {
                    throw ex;//抛出异常
                }
            }

            T result = SerializerHelper.GetObject<T>(returnText);

            //TODO:加入特殊情况下的回调处理

            return result;
        }
    }
}
