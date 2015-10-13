/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
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

namespace Senparc.Weixin
{
    /// <summary>
    /// 微信日志跟踪
    /// </summary>
    public static class WeixinTrace
    {
        private static TraceListener _traceListener = null;
        private static readonly object TraceLock = new object();

        internal static void Open()
        {
            lock (TraceLock)
            {
                if (_traceListener == null || !System.Diagnostics.Trace.Listeners.Contains(_traceListener))
                {
                    var logDir = System.AppDomain.CurrentDomain.BaseDirectory + "App_Data";
                    string logFile = Path.Combine(logDir, "SenparcWeixinTrace.log");
                    System.IO.TextWriter logWriter = new System.IO.StreamWriter(logFile, true);
                    _traceListener = _traceListener ?? new TextWriterTraceListener(logWriter);
                    System.Diagnostics.Trace.Listeners.Add(_traceListener);
                    System.Diagnostics.Trace.AutoFlush = true;
                }
            }
        }

        internal static void Close()
        {
            lock (TraceLock)
            {
                if (_traceListener != null && System.Diagnostics.Trace.Listeners.Contains(_traceListener))
                {
                    System.Diagnostics.Trace.Listeners.Remove(_traceListener);
                }
            }
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="message"></param>
        public static
            void Log(string message)
        {
            lock (TraceLock)
            {
                System.Diagnostics.Trace.WriteLine(message);
                //System.Diagnostics.Trace.Flush();
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

            Log("");
            Log(string.Format("[{0}]", DateTime.Now));
            Log(string.Format("URL：{0}", url));
            Log(string.Format("Result：\r\n{0}", returnText));
        }

    }
}
