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
  
    文件名：AppResult.cs
    文件功能描述：获取App返回结果
    
    
    创建标识：Senparc - 20150319
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.AppStore
{
    public interface IAppResult<T> where T : IAppResultData
    {
        AppResultKind Result { get; set; }
        string ErrorMessage { get; set; }
        T Data { get; set; }
        long RunTime { get; set; }
    }

    public interface IAppResultData
    {
    }

    public class AppResultData : IAppResultData
    {

    }

    /// <summary>
    /// JSON返回结果（用于菜单接口等）
    /// </summary>
    public class AppResult<T> : IAppResult<T> where T : IAppResultData
    {
        public AppResultKind Result { get; set; }
        /// <summary>
        /// 如果结果未成功，则Data为期望的类型
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// 错误信息，如果结果为成功，错误信息为Null
        /// </summary>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// Api执行时间
        /// </summary>
        public long RunTime { get; set; }
        //public T SData
        //{
        //    get { return Data as T; }
        //}
    }
}
