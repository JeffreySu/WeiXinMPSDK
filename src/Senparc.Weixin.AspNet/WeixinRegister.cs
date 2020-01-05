#if NETSTANDARD2_0 || NETSTANDARD2_1 || NETCOREAPP3_0
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
#endif

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.AspNet
{
    public static class WeixinRegister
    {
#if NETSTANDARD2_0 || NETSTANDARD2_1 || NETCOREAPP3_0
        /// <summary>
        /// <para>开始包含 CO2NET 注册在内的 Senparc.Weixin SDK 初始化参数流程</para>
        /// <para>注意：本方法集成了 CON2ET 全局注册以及 Senparc.Weixin SDK 微信注册过程，提供给对代码行数有极限追求的开发者使用，常规情况下为了提高代码可读性和可维护性，并不推荐使用此方法。</para>
        /// </summary>
        /// <param name="registerService"></param>
        /// <param name="senparcWeixinSetting"></param>
        /// <param name="registerConfigure"></param>
        /// <returns></returns>
        public static IRegisterService UseSenparcWeixin(this IApplicationBuilder app,
#if NETSTANDARD2_0 || NETSTANDARD2_1
            IHostingEnvironment env,
#else
            IWebHostEnvironment env,
#endif
            SenparcSetting senparcSetting, SenparcWeixinSetting senparcWeixinSetting, Action<IRegisterService> globalRegisterConfigure, Action<IRegisterService> weixinRegisterConfigure,
             //CO2NET 全局设置
             bool autoScanExtensionCacheStrategies = false, Func<IList<IDomainExtensionCacheStrategy>> extensionCacheStrategiesFunc = null
            )
        {
            //注册 CO2NET 全局
            var register = app.UseSenparcGlobal(env, senparcSetting, globalRegisterConfigure, autoScanExtensionCacheStrategies, extensionCacheStrategiesFunc);
            //注册微信
            register.UseSenparcWeixin(senparcWeixinSetting, weixinRegisterConfigure);
            return register;
        }
#endif

    }
}
