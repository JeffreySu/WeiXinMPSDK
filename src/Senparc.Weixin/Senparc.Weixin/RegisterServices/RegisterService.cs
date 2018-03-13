/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc

    文件名：RegisterService.cs
    文件功能描述：Senparc.Weixin SDK 快捷注册流程


    创建标识：Senparc - 20180222

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.RegisterServices
{
    /// <summary>
    /// 快捷注册接口
    /// </summary>
    public interface IRegisterService
    {

    }

    /// <summary>
    /// 快捷注册类，IRegisterService的默认实现
    /// </summary>
    public class RegisterService : IRegisterService
    {
        /// <summary>
        /// 开始流程
        /// </summary>
        /// <returns></returns>
        public static RegisterService Start()
        {
            var register = new RegisterService();

            //如果不注册此线程，则AccessToken、JsTicket等都无法使用SDK自动储存和管理。
            register.RegisterThreads();//默认把线程注册好

            return register;
        }
    }
}
