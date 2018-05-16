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
    
    文件名：WeixinTrace.cs
    文件功能描述：跟踪日志相关
    
    
    创建标识：Senparc - 20151012

    修改标识：Senparc - 20161225
    修改描述：v4.9.7 1、使用同步锁
                     2、修改日志储存路径，新路径为/App_Data/WeixinTraceLog/SenparcWeixinTrace-yyyyMMdd.log
                     3、添加WeixinExceptionLog方法

    修改标识：Senparc - 20161231
    修改描述：v4.9.8 将SendLog方法改名为SendApiLog，添加SendCustomLog方法

    修改标识：Senparc - 20170101
    修改描述：v4.9.9 1、优化日志记录方法（围绕OnWeixinExceptionFunc为主）
                     2、输出AccessTokenOrAppId

    修改标识：Senparc - 20170304
    修改描述：Senparc.Wexin v4.11.3 日志中添加对线程的记录

----------------------------------------------------------------*/

using System;
using System.Diagnostics;
using System.IO;
using Senparc.Weixin.Cache;
using Senparc.Weixin.Exceptions;
using System.Threading;

namespace Senparc.Weixin
{
    /// <summary>
    /// 微信日志跟踪
    /// </summary>
    public static class WeixinTrace
    {
        /// <summary>
        /// TraceListener
        /// </summary>
#if NET35 || NET40 || NET45 || NET461
        private static TraceListener _traceListener = null;
#elif NETSTANDARD2_0 || NETCOREAPP2_0 || NETCOREAPP2_1
        private static TextWriterTraceListener _traceListener = null;
#endif

        /// <summary>
        /// 统一日志锁名称
        /// </summary>
        const string LockName = "WeixinTraceLock";

        /// <summary>
        /// Senparc.Weixin全局统一的缓存策略
        /// </summary>
        private static IObjectCacheStrategy Cache
        {
            get
            {
                //使用工厂模式或者配置进行动态加载
                return CacheStrategyFactory.GetObjectCacheStrategyInstance();
            }
        }

        /// <summary>
        /// 记录WeixinException日志时需要执行的任务
        /// </summary>
        public static Action<WeixinException> OnWeixinExceptionFunc;

        /// <summary>
        /// 执行所有日志记录操作时执行的任务（发生在Senparc.Weixin记录日志之后）
        /// </summary>
        public static Action OnLogFunc;

        /// <summary>
        /// 打开日志开始记录
        /// </summary>
        internal static void Open()
        {
            Close();

            using (Cache.BeginCacheLock(LockName, ""))
            {
                string logDir;
#if NET35
                logDir = Path.Combine(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "App_Data"), "WeixinTraceLog");
#else

#if NET40 || NET45 || NET461
                logDir = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "App_Data", "WeixinTraceLog");
#else
                //var logDir = Path.Combine(AppContext.BaseDirectory, "App_Data", "WeixinTraceLog");
                logDir = Path.Combine(Senparc.Weixin.Config.RootDictionaryPath, "App_Data", "WeixinTraceLog");
#endif
#endif

                if (!Directory.Exists(logDir))
                {
                    Directory.CreateDirectory(logDir);
                }

                string logFile = Path.Combine(logDir, string.Format("SenparcWeixinTrace-{0}.log", DateTime.Now.ToString("yyyyMMdd")));

#if NET35 || NET40 || NET45 || NET461

                System.IO.TextWriter logWriter = new System.IO.StreamWriter(logFile, true);
#elif NETSTANDARD2_0 || NETCOREAPP2_0 || NETCOREAPP2_1
                System.IO.TextWriter logWriter = new System.IO.StreamWriter(logFile, true);
#endif


#if NET35 || NET40 || NET45 || NET461
                _traceListener = new TextWriterTraceListener(logWriter);
                System.Diagnostics.Trace.Listeners.Add(_traceListener);
                System.Diagnostics.Trace.AutoFlush = true;
#elif NETSTANDARD2_0 || NETCOREAPP2_0 || NETCOREAPP2_1
                //TODO:如果这里不开通，netstandard1.6将无法使用日志记录功能
                //ILoggerFactory loggerFactory = new LoggerFactory();

                _traceListener = new TextWriterTraceListener(logWriter);
                System.Diagnostics.Trace.Listeners.Add(_traceListener);
                System.Diagnostics.Trace.AutoFlush = true;
#endif

            }
        }

        /// <summary>
        /// 关闭日志记录
        /// </summary>
        internal static void Close()
        {
            using (Cache.BeginCacheLock(LockName, ""))
            {
#if NET35 || NET40 || NET45 || NET461

                if (_traceListener != null && System.Diagnostics.Trace.Listeners.Contains(_traceListener))
                {
                    _traceListener.Close();
                    System.Diagnostics.Trace.Listeners.Remove(_traceListener);
                }
#elif NETSTANDARD2_0 || NETCOREAPP2_0 || NETCOREAPP2_1
                if (_traceListener != null && System.Diagnostics.Trace.Listeners.Contains(_traceListener))
                {
                    _traceListener.Close();
                    System.Diagnostics.Trace.Listeners.Remove(_traceListener);
                }
#endif
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

        /// <summary>
        /// 当前线程记录
        /// </summary>
        private static void ThreadLog()
        {
            Log("[线程：{0}]", Thread.CurrentThread.GetHashCode());
        }


        /// <summary>
        /// 退回一次缩进
        /// </summary>
        private static void Unindent()
        {
            using (Cache.BeginCacheLock(LockName, ""))
            {
#if NET35 || NET40 || NET45 || NET461
                System.Diagnostics.Trace.Unindent();
#elif NETSTANDARD2_0 || NETCOREAPP2_0 || NETCOREAPP2_1
                System.Diagnostics.Trace.Unindent();
#endif
            }
        }

        /// <summary>
        /// 缩进一次
        /// </summary>
        private static void Indent()
        {
            using (Cache.BeginCacheLock(LockName, ""))
            {
#if NET35 || NET40 || NET45 || NET461
                System.Diagnostics.Trace.Indent();
#elif NETSTANDARD2_0 || NETCOREAPP2_0 || NETCOREAPP2_1
                System.Diagnostics.Trace.Indent();
#endif
            }
        }

        /// <summary>
        /// 写入缓存到系统Trace
        /// </summary>
        private static void Flush()
        {
            using (Cache.BeginCacheLock(LockName, ""))
            {
#if NET35 || NET40 || NET45 || NET461
                System.Diagnostics.Trace.Flush();
#elif NETSTANDARD2_0 || NETCOREAPP2_0 || NETCOREAPP2_1
                System.Diagnostics.Trace.Flush();
#endif
            }
        }

        /// <summary>
        /// 开始记录日志
        /// </summary>
        /// <param name="title"></param>
        private static void LogBegin(string title = null)
        {
            Open();
            Log("");
            if (title != null)
            {
                Log("[{0}]", title);
            }
            TimeLog();//记录时间
            ThreadLog();//记录线程
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
#if NET35 || NET40 || NET45 || NET461
                System.Diagnostics.Trace.WriteLine(string.Format(messageFormat, args));
#elif NETSTANDARD2_0 || NETCOREAPP2_0 || NETCOREAPP2_1
                System.Diagnostics.Trace.WriteLine(string.Format(messageFormat, args));
#endif
            }
        }

        /// <summary>
        /// 结束日志记录
        /// </summary>
        private static void LogEnd()
        {
            Unindent();
            Flush();
            Close();

            if (OnLogFunc != null)
            {
                try
                {
                    OnLogFunc();
                }
                catch
                {
                }
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
#if NET35 || NET40 || NET45 || NET461
                System.Diagnostics.Trace.WriteLine(message);
#elif NETSTANDARD2_0 || NETCOREAPP2_0 || NETCOREAPP2_1
                System.Diagnostics.Trace.WriteLine(message);
#endif
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
        /// API请求日志（接收结果）
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
            //TODO:从源头加入AppId
            Log("URL：{0}", url);
            Log("Result：\r\n{0}", returnText);
            LogEnd();
        }

        /// <summary>
        /// API请求日志（Post发送消息）
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        public static void SendApiPostDataLog(string url, string data)
        {
            if (!Config.IsDebug)
            {
                return;
            }

            LogBegin("[[接口调用]]");
            Log("URL：{0}", url);
            Log("Post Data：\r\n{0}", data);
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

            LogBegin("[[WeixinException]]");
            Log(ex.GetType().Name);
            Log("AccessTokenOrAppId：{0}", ex.AccessTokenOrAppId);
            Log("Message：{0}", ex.Message);
            Log("StackTrace：{0}", ex.StackTrace);
            if (ex.InnerException != null)
            {
                Log("InnerException：{0}", ex.InnerException.Message);
                Log("InnerException.StackTrace：{0}", ex.InnerException.StackTrace);
            }

            if (OnWeixinExceptionFunc != null)
            {
                try
                {
                    OnWeixinExceptionFunc(ex);
                }
                catch
                {
                }
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

            LogBegin("[[ErrorJsonResultException]]");
            Log("ErrorJsonResultException");
            Log("AccessTokenOrAppId：{0}", ex.AccessTokenOrAppId ?? "null");
            Log("URL：{0}", ex.Url);
            Log("errcode：{0}", ex.JsonResult.errcode);
            Log("errmsg：{0}", ex.JsonResult.errmsg);

            if (OnWeixinExceptionFunc != null)
            {
                try
                {
                    OnWeixinExceptionFunc(ex);
                }
                catch
                {
                }
            }

            LogEnd();
        }

        #endregion
    }
}
