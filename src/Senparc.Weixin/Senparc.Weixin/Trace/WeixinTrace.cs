/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：WeixinTrace.cs
    文件功能描述：跟踪日志相关
    
    
    创建标识：Senparc - 20151012

    修改标识：Senparc - 20161225
    修改描述：v4.9.7 1、使用同步锁
                     2、修改日志储存路径，新路径为/App_Data/WeixinTraceLog/SenparcWeixinTrace-yyyyMMdd.log
                     3、添加WeixinExceptionLog方法

    修改标识：Senparc - 20161231
    修改描述：v4.9.8 将SendLog方法改名为SendApiLog，添加SendCustomLog方法

----------------------------------------------------------------*/

using System;
using System.Diagnostics;
using System.IO;
using Senparc.Weixin.Cache;
using Senparc.Weixin.Exceptions;

namespace Senparc.Weixin
{
    /// <summary>
    /// 微信日志跟踪
    /// </summary>
    public static class WeixinTrace
    {
        private static TraceListener _traceListener = null;

        const string LockName = "WeixinTraceLock";

        private static IObjectCacheStrategy Cache
        {
            get
            {
                //使用工厂模式或者配置进行动态加载
                return CacheStrategyFactory.GetObjectCacheStrategyInstance();
            }
        }

        /// <summary>
        /// 记录ErrorJsonResultException日志时需要执行的任务
        /// </summary>
        public static Action<ErrorJsonResultException> OnErrorJsonResultExceptionFunc;

        /// <summary>
        /// 执行所有日志记录操作时执行的任务（发生在Senparc.Weixin记录日志之后）
        /// </summary>
        public static Action OnLogFunc;

        internal static void Open()
        {
            Close();

            using (Cache.BeginCacheLock(LockName, ""))
            {
                var logDir = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "App_Data", "WeixinTraceLog");

                if (!Directory.Exists(logDir))
                {
                    Directory.CreateDirectory(logDir);
                }

                string logFile = Path.Combine(logDir, string.Format("SenparcWeixinTrace-{0}.log", DateTime.Now.ToString("yyyyMMdd")));

                System.IO.TextWriter logWriter = new System.IO.StreamWriter(logFile, true);
                _traceListener = new TextWriterTraceListener(logWriter);
                System.Diagnostics.Trace.Listeners.Add(_traceListener);
                System.Diagnostics.Trace.AutoFlush = true;
            }
        }

        internal static void Close()
        {
            using (Cache.BeginCacheLock(LockName, ""))
            {
                if (_traceListener != null && System.Diagnostics.Trace.Listeners.Contains(_traceListener))
                {
                    _traceListener.Close();
                    System.Diagnostics.Trace.Listeners.Remove(_traceListener);
                }
            }
        }

        #region 私有方法

        /// <summary>
        /// 统一时间格式
        /// </summary>
        private static void TimeLog()
        {
            Log("[{0}]", DateTime.Now);
        }

        private static void Unindent()
        {
            using (Cache.BeginCacheLock(LockName, ""))
            {
                System.Diagnostics.Trace.Unindent();
            }
        }

        private static void Indent()
        {
            using (Cache.BeginCacheLock(LockName, ""))
            {
                System.Diagnostics.Trace.Indent();
            }
        }

        private static void Flush()
        {
            using (Cache.BeginCacheLock(LockName, ""))
            {
                System.Diagnostics.Trace.Flush();
            }
        }

        private static void LogBegin(string title = null)
        {
            Open();
            Log("");
            if (title != null)
            {
                Log("[{0}]", title);
            }
            TimeLog();
            Indent();
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="messageFormat">日志内容格式</param>
        /// <param name="args">日志内容参数</param>
        public static void Log(string messageFormat, params object[] args)
        {
            using (Cache.BeginCacheLock(LockName, ""))
            {
                System.Diagnostics.Trace.WriteLine(string.Format(messageFormat, args));
            }
        }

        private static void LogEnd()
        {
            Unindent();
            Flush();
            Close();

            if (OnLogFunc != null)
            {
                OnLogFunc();
            }
        }

        #endregion

        #region 日志记录

        /// <summary>
        /// 记录日志（建议使用SendXXLog()方法，以符合统一的记录规则）
        /// </summary>
        /// <param name="message">日志内容</param>
        public static void Log(string message)
        {
            using (Cache.BeginCacheLock(LockName, ""))
            {
                System.Diagnostics.Trace.WriteLine(message);
            }
        }


        /// <summary>
        /// 自定义日志
        /// </summary>
        /// <param name="typeName">日志类型</param>
        /// <param name="content">日志内容</param>
        public static void SendCustomLog(string typeName, string content)
        {
            if (!Config.IsDebug)
            {
                return;
            }

            LogBegin(string.Format("[[{0}]]", typeName));
            Log(content);
            LogEnd();
        }

        /// <summary>
        /// API请求日志
        /// </summary>
        /// <param name="url"></param>
        /// <param name="returnText"></param>
        public static void SendApiLog(string url, string returnText)
        {
            if (!Config.IsDebug)
            {
                return;
            }

            LogBegin("[[接口调用]]");
            Log("URL：{0}", url);
            Log("Result：\r\n{0}", returnText);
            LogEnd();
        }

        #endregion

        #region WeixinException 相关日志

        /// <summary>
        /// WeixinException 日志
        /// </summary>
        /// <param name="ex"></param>
        public static void WeixinExceptionLog(WeixinException ex)
        {
            if (!Config.IsDebug)
            {
                return;
            }

            LogBegin(ex.GetType().Name);
            Log("Message", ex.Message);
            Log("StackTrace", ex.StackTrace);
            if (ex.InnerException != null)
            {
                Log("InnerException", ex.InnerException.Message);
                Log("InnerException", ex.InnerException.StackTrace);
            }
            LogEnd();

        }

        /// <summary>
        /// ErrorJsonResultException 日志
        /// </summary>
        /// <param name="ex"></param>
        public static void ErrorJsonResultExceptionLog(ErrorJsonResultException ex)
        {
            if (!Config.IsDebug)
            {
                return;
            }

            LogBegin("ErrorJsonResultException");
            Log("URL：{0}", ex.Url);
            Log("errcode：{0}", ex.JsonResult.errcode);
            Log("errmsg：{0}", ex.JsonResult.errmsg);
            LogEnd();

            if (OnErrorJsonResultExceptionFunc != null)
            {
                OnErrorJsonResultExceptionFunc(ex);
            }
        }

        #endregion
    }
}
