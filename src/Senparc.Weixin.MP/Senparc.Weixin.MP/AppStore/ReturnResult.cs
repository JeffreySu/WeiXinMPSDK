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
    文件功能描述：返回结果类型
    
    
    创建标识：Senparc - 20150319

    修改标识：Senparc - 20180901
    修改描述：支持 NeuChar

----------------------------------------------------------------*/

using Senparc.NeuChar;

namespace Senparc.Weixin.MP.AppStore
{
   public class ReturnResult
    {
        /// <summary>
        /// 如果>0则进入某个APP状态，如果=0则维持当前状态不变，如果>0则退出某个App状态
        /// </summary>
       public AppStoreState AppStoreState { get; set; }
       /// <summary>
       /// 改变状态的AppId
       /// </summary>
       public int AppId { get; set; }
       /// <summary>
       /// 错误信息
       /// </summary>
       public string ErrorMessage { get; set; }
    }
}
