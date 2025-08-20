#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2025 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2025 Senparc
    
    文件名：ResponseErrorJsonResult.cs
    文件功能描述：响应消息中的错误信息
    
    
    创建标识：Senparc - 20210828
  
    修改标识：Senparc - 20250819
    修改描述：v2.2.1 处理 ResponseErrorJsonResult 中 value 值类型，由 int 改为 object

----------------------------------------------------------------*/

namespace Senparc.Weixin.TenPayV3.Entities
{
    /// <summary>
    /// API 返回错误时，附带的错误信息
    /// </summary>
    public class ResponseErrorJsonResult
    {
        public string code { get; set; }
        public Detail detail { get; set; }
        public string message { get; set; }
    }

    public class Detail
    {
        public object location { get; set; }
        //public string[] value { get; set; }
        public object value { get; set; }
    }

}
