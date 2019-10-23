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

    文件名：WxJsonResult.cs
    文件功能描述：同于公众号的JSON返回结果基类（用于菜单接口等）


    创建标识：Senparc - 20150211

    修改标识：Senparc - 20150303
    修改描述：整理接口

    修改标识：Senparc - 20150303
    修改描述：添加QyJsonResult（企业号JSON返回结果）

    修改标识：Senparc - 20150706
    修改描述：调整位置，去除MP下的WxJsonResult
    
    修改标识：Senparc - 20161108
    修改描述：重写ToString()方法，快捷输出结果

    修改标识：Senparc - 20170702
    修改描述：将 IWxJsonResult 定义从 IJsonResult.cs 文件移入

    修改标识：Senparc - 20170702
    修改描述：v4.13.0 添加 ErrorCodeValue 属性。使用 BaseJsonResult 基类。
----------------------------------------------------------------*/

using System;

namespace Senparc.Weixin.Entities
{
    /// <summary>
    /// 包含 errorcode 的 Json 返回结果接口
    /// </summary>
    public interface IWxJsonResult : IJsonResult
    {
        /// <summary>
        /// 返回结果代码
        /// </summary>
        ReturnCode errcode { get; set; }
    }

    /// <summary>
    /// 公众号 JSON 返回结果（用于菜单接口等），子类必须具有不带参数的构造函数
    /// </summary>
    [Serializable]
    public class WxJsonResult : BaseJsonResult
    {
        //会造成循环引用
        //public WxJsonResult BaseResult
        //{
        //    get { return this; }
        //}

        public ReturnCode errcode { get; set; }

        /// <summary>
        /// 返回消息代码数字（同errcode枚举值）
        /// </summary>
        public override int ErrorCodeValue { get { return (int)errcode; } }

        /// <summary>
        /// 无参数的构造函数
        /// </summary>
        public WxJsonResult() { }


        public override string ToString()
        {
            return string.Format("WxJsonResult：{{errcode:'{0}',errcode_name:'{1}',errmsg:'{2}'}}",
                (int)errcode, errcode.ToString(), errmsg);
        }

        //public ReturnCode ReturnCode
        //{
        //    get
        //    {
        //        try
        //        {
        //            return (ReturnCode) errorcode;
        //        }
        //        catch
        //        {
        //            return ReturnCode.系统繁忙;//如果有“其他错误”的话可以指向其他错误
        //        }
        //    }
        //}
        //public void SerializingCallback()
        //{
        //}

        //public void SrializedCallback(string json)
        //{
        //    throw new NotImplementedException();
        //}

        //public void DeserializingCallback(string json)
        //{
        //    throw new NotImplementedException();
        //}

        //public void DeserializedCallback(string json)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
