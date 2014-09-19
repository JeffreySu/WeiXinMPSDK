using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.Configuration
{
    /// <summary>
    /// 微信的配置类
    /// 这样接口中。就可以直接调用WXConfiguration来得到配置信息
    /// <configuration>
    ///     <configSections>
    ///         <section name="WXConfiguration" type="Senparc.Weixin.MP.Configuration.WXConfiguration, Senparc.Weixin.MP"/>
    ///     </configSections>
    ///     <WXConfiguration appID="提供appID" appsecret="提供appsecret" Token="自定义的Token" />
    /// </configuration>
    /// </summary>
    class WXConfiguration : ConfigurationSection
    {
        /// <summary>
        /// 在配置文件中。定义appID配置属性为appID
        /// </summary>
        public const string WX_APPID = "appID";
        /// <summary>
        /// 在配置文件中。定义appsecret配置属性为appsecret
        /// </summary>
        public const string WX_APPSecret = "appsecret";
        /// <summary>
        /// 
        /// </summary>
        public const string WX_Token = "Token";
        public static WXConfiguration Config
        {
            get
            {
                return ConfigurationManager.GetSection("WXConfiguration") as WXConfiguration;
            }
        }
        /// <summary>
        /// 微信接口的appID
        /// </summary>
        [ConfigurationProperty(WX_APPID, IsRequired = true)]
        public string APPID
        {
            get
            {
                return this[WX_APPID] as string;
            }
        }
        /// <summary>
        /// 微信接口的appsecret
        /// </summary>
        [ConfigurationProperty(WX_APPSecret, IsRequired = true)]
        public string APPSecret
        {
            get
            {
                return this[WX_APPSecret] as string;
            }
        }
        /// <summary>
        /// 微信接口用的Token
        /// </summary>
        [ConfigurationProperty(WX_Token, IsRequired = true)]
        public string Token
        {
            get
            {
                return this[WX_Token] as string;
            }
        }
    }
}
