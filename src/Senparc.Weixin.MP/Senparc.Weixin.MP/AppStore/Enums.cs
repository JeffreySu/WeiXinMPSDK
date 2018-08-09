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
  
    文件名：Enums.cs
    文件功能描述：返回结果枚举类型
    
    
    创建标识：Senparc - 20150319
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.AppStore
{
    /// <summary>
    /// P2P返回结果类型
    /// </summary>
    public enum AppResultKind
    {
        成功 = 0,
        账户验证失败 = -1000,

        账户被停用 = -2000,
        账户被停用_尚未通过审核 = -2001,
        账户被停用_已关闭 = -2002,
        账户被停用_状态异常 = -2003,

        包含违法信息 = -3000,

        执行过程发生异常 = -4000,
        执行过程发生异常_API地址错误 = -4001,
        执行过程发生异常_积分不足 = -4002,

        操作用户信息失败 = -5000,
        操作用户信息失败_用户不存在 = -5001,
    }

    ///// <summary>
    ///// 性别
    ///// </summary>
    //public enum WeixinSex
    //{
    //    未设置 = 0,
    //    男 = 1,
    //    女 = 2,
    //}
}
