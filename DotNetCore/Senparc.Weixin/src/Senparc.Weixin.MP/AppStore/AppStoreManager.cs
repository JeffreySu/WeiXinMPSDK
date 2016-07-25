/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
  
    文件名：AppStoreManager.cs
    文件功能描述：AppStoreOAuth
    
    
    创建标识：Senparc - 20150319
----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.MP.AppStore.Api;

namespace Senparc.Weixin.MP.AppStore
{
    public class AppStoreManager
    {
        public const string DEFAULT_URL = "http://api.weiweihi.com:8080";//默认Api Url地址

        //private static string _appKey;
        //private static string _secret;
        private static PassportCollection _passportCollection;
        public static PassportCollection PassportCollection
        {
            get
            {
                if (_passportCollection == null)
                {
                    //LoadPassport();
                    _passportCollection = new PassportCollection();
                }
                return _passportCollection;
            }
            set { _passportCollection = value; }
        }

        public static PassportBag GetPassportBag(string appKey)
        {
            if (PassportCollection.ContainsKey(appKey))
            {
                return PassportCollection[appKey];
            }
            return null;
        }

        public const string BasicApiPath = "/App/Api/";

        /// <summary>
        /// 注册P2P应用基本信息（可以选择不立即载入Passport以节省系统启动时间）
        /// </summary>
        /// <param name="appKey">P2P后台申请到微信应用后的AppKey</param>
        /// <param name="secret">AppKey对应的Secret</param>
        /// <param name="url">API地址，建议使用默认值</param>
        /// <param name="getPassportImmediately">是否马上获取Passport，默认为False</param>
        private static void Register(string appKey, string secret, string url = DEFAULT_URL, bool getPassportImmediately = false)
        {
            //if (PassportCollection.BasicUrl != url)
            //{
            ////不使用默认地址
            PassportCollection.BasicUrl = url + BasicApiPath;
            //}

            PassportCollection[appKey] = new PassportBag(appKey, secret, url + BasicApiPath);
            if (getPassportImmediately)
            {
                ApplyPassport(appKey, secret, url);
            }
        }

        /// <summary>
        /// 申请新的通行证。
        /// 每次调用完毕前将有1秒左右的Thread.Sleep时间
        /// </summary>
        public static void ApplyPassport(string appKey, string appSecret, string url = DEFAULT_URL)
        {
            if (!PassportCollection.ContainsKey(appKey))
            {
                //如果不存在，则自动注册（注册之后PassportCollection[appKey]一定有存在值）
                Register(appKey, appSecret, url, true);
            }

            var passportBag = PassportCollection[appKey];

            var getPassportUrl = PassportCollection.BasicUrl + "GetPassport";
            var formData = new Dictionary<string, string>();
            formData["appKey"] = passportBag.AppKey;
            formData["secret"] = passportBag.AppSecret;
            var result = Post.PostGetJson<PassportResult>(getPassportUrl, formData: formData);
            if (result.Result != AppResultKind.成功)
            {
                throw new WeixinException("获取Passort失败！错误信息：" + result.Result, null);
            }

            passportBag.Passport = result.Data;
            passportBag.Passport.ApiUrl = PassportCollection.BasicUrl;
        }

        /// <summary>
        /// 清除当前的通行证
        /// </summary>
        public static void RemovePassport(string appKey)
        {
            try
            {
                PassportCollection.Remove(appKey);
            }
            catch
            {
            }
        }

        /// <summary>
        /// 获取appKey对应的接口集合。
        /// 调用此方法请确认此appKey已经成功使用SdkManager.Register(appKey, appSecret, appUrl)方法注册过。
        /// </summary>
        /// <param name="appKey"></param>
        /// <returns></returns>
        public static ApiContainer GetApiContainer(string appKey, string appSecret, string url = DEFAULT_URL)
        {
            return new ApiContainer(appKey, appSecret, url);
        }
    }
}
