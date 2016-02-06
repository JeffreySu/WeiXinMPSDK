/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：WeixinTrace.cs
    文件功能描述：跟踪日志相关
    
    
    创建标识：Senparc - 20151012
    
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Exceptions;

namespace Senparc.Weixin
{
    /// <summary>
    /// 微信日志跟踪
    /// </summary>
    public static class WeixinTrace
    {
        private static TraceListener _traceListener = null;
        private static readonly object TraceLock = new object();

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
            lock (TraceLock)
            {
                var logDir = System.AppDomain.CurrentDomain.BaseDirectory + "App_Data";
                string logFile = Path.Combine(logDir, "SenparcWeixinTrace.log");
                System.IO.TextWriter logWriter = new System.IO.StreamWriter(logFile, true);
                _traceListener = new TextWriterTraceListener(logWriter);
                System.Diagnostics.Trace.Listeners.Add(_traceListener);
                System.Diagnostics.Trace.AutoFlush = true;
            }
        }

        internal static void Close()
        {
            lock (TraceLock)
            {
                if (_traceListener != null && System.Diagnostics.Trace.Listeners.Contains(_traceListener))
                {
                    _traceListener.Close();
                    System.Diagnostics.Trace.Listeners.Remove(_traceListener);
                }
            }
        }

        /// <summary>
        /// 统一时间格式
        /// </summary>
        private static void TimeLog()
        {
            Log(string.Format("[{0}]", DateTime.Now));
        }

        private static void Unindent()
        {
            lock (TraceLock)
            {
                System.Diagnostics.Trace.Unindent();
            }
        }

        private static void Indent()
        {
            lock (TraceLock)
            {
                System.Diagnostics.Trace.Indent();
            }
        }

        private static void Flush()
        {
            lock (TraceLock)
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
                Log(String.Format("[{0}]", title));
            }
            TimeLog();
            Indent();
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="message"></param>
        public static void Log(string message)
        {
            lock (TraceLock)
            {
                System.Diagnostics.Trace.WriteLine(message);
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

        /// <summary>
        /// API请求日志
        /// </summary>
        /// <param name="url"></param>
        /// <param name="returnText"></param>
        public static void SendLog(string url, string returnText)
        {
            if (!Config.IsDebug)
            {
                return;
            }

            LogBegin("接口调用");
            Log(string.Format("URL：{0}", url));
            Log(string.Format("Result：\r\n{0}", returnText));
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
            Log(string.Format("URL：{0}", ex.Url));
            Log(string.Format("errcode：{0}", ex.JsonResult.errcode));
            Log(string.Format("errmsg：{0}", ex.JsonResult.errmsg));
            LogEnd();

            if (OnErrorJsonResultExceptionFunc != null)
            {
                OnErrorJsonResultExceptionFunc(ex);
            }
        }
    }
}
