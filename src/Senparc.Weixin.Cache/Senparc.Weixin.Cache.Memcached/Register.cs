/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc

    文件名：Register.cs
    文件功能描述：Senparc.Weixin.Cache.Memcached 注册类


    创建标识：Senparc - 20180609

    修改标识：Senparc - 20180802
    修改描述：当前类所有方法支持 .net standard 2.0

    修改标识：Senparc - 20180802
    修改描述：v2.5.102 RegisterDomainCache() 方法重命名为 ActivityDomainCache()

    修改标识：Senparc - 20200105
    修改描述：v2.6.100 UseSenparcWeixinCacheMemcached() 扩展方法 this 类型由 IApplicationBuilder 改为 IRegisterService

----------------------------------------------------------------*/


using Senparc.CO2NET.RegisterServices;

namespace Senparc.Weixin.Cache.Memcached
{
    public static class Register
    {
        /// <summary>
        /// 注册 Senparc.Weixin.Cache.Memcached
        /// </summary>
        /// <param name="app"></param>
        public static IRegisterService UseSenparcWeixinCacheMemcached(this IRegisterService register)
        {
            ActivityDomainCache();
            return register;
        }

        /// <summary>
        /// 激活领域缓存
        /// </summary>
        public static void ActivityDomainCache()
        {
            var cache = MemcachedContainerCacheStrategy.Instance;
        }

    }
}
