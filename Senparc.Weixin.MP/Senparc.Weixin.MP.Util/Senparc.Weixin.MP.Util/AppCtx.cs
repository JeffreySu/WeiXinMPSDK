using System;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Text.RegularExpressions;
using Senparc.Weixin.MP.Util.Conf;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Util.Content;

namespace Senparc.Weixin.MP.Util
{
    public class AppCtx
    {
        private readonly string _appKey;
        private static readonly Regex AppKeyRegexp = new Regex("/app-(.+?)(/*$|/)");
        private MenuFull_ButtonGroup _buttonGroup;
        private IAppCustomHandler _handler;
        private CustomMessageHandler _contextHandler;


        private AppCtx(String appKey)
        {
            this._appKey = appKey;
        }

        public static AppCtx Current
        {
            get
            {

                HttpContext context = HttpContext.Current;

                Object cacheObj = context.Items["wx_ctx"];
                AppCtx cacheCtx = cacheObj as AppCtx;
                if (cacheCtx != null)
                {
                    return cacheCtx;
                }

                String appQuery = context.Request["app"];
                if (!String.IsNullOrEmpty(appQuery))
                {
                    return CacheCtx(context, new AppCtx(appQuery));
                }

                String path = context.Request.Path;
                if (AppKeyRegexp.IsMatch(path))
                {
                    Match match = AppKeyRegexp.Match(path);
                    String appKey = match.Groups[1].Value;
                    if (ConfigManager.GetKeys().Contains(appKey))
                    {
                        return CacheCtx(context, new AppCtx(appKey));
                    }
                }

                return CacheCtx(context, new AppCtx(ConfigManager.GetKeys().First()));
            }
        }

        private static AppCtx CacheCtx(HttpContext context, AppCtx appCtx)
        {
            context.Items["wx_ctx"] = appCtx;
            return appCtx;
        }


        public ConfigItem GetConfig()
        {
            return ConfigManager.GetConfig(this._appKey);
        }

        public IAppCustomHandler GetHandler()
        {
            if (this._handler == null)
            {
                // this._handler = new IAppCustomerHandler();
                String classFullName = this.GetConfig().CustomHandlerClassName;
                Object obj = Assembly.GetCallingAssembly().CreateInstance(classFullName);
                if (obj == null || (this._handler = obj as IAppCustomHandler) == null)
                {
                    throw new Exception("Can't convert " + classFullName + " as IAppCustomHandler");
                }
            }
            return this._handler;
        }


        /// <summary>
        /// 设置上下文处理对象
        /// </summary>
        /// <param name="handler"></param>
        public void SetContextHandler(CustomMessageHandler handler)
        {
            this._contextHandler = handler;
        }


        /// <summary>
        /// 上下文处理对象
        /// </summary>
        public CustomMessageHandler ContextHandler
        {
            get
            {
                if (this._contextHandler == null)
                {
                    throw new ArgumentNullException("please use SetContextHandler() first!");
                }
                return this._contextHandler;
            }
        }

        /// <summary>
        /// 根据当前的RequestMessage创建指定类型的ResponseMessage
        /// </summary>
        /// <typeparam name="T">基于ResponseMessageBase的响应消息类型</typeparam>
        /// <returns></returns>
        public T CreateResponseMessage<T>() where T : ResponseMessageBase
        {
            return this._contextHandler.CreateResponseMessage<T>();
        }
    }
}
