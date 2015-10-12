/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：Config.cs
    文件功能描述：全局设置
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using System.Diagnostics;
using System.IO;

namespace Senparc.Weixin
{
    /// <summary>
    /// 全局设置
    /// </summary>
    public static class Config
    {
        /// <summary>
        /// 请求超时设置（以毫秒为单位），默认为10秒。
        /// 说明：此处常量专为提供给方法的参数的默认值，不是方法内所有请求的默认超时时间。
        /// </summary>
        public const int TIME_OUT = 10000;


        private static bool _isDebug = false;

        private static TraceListener _traceListener = null;
        private static TextWriter _logWriter = TextWriter.Null;

        /// <summary>
        /// 指定是否是Debug状态，如果是，系统会自动输出日志
        /// </summary>
        public static bool IsDebug
        {
            get
            {
                return _isDebug;
            }
            set
            {
                _isDebug = value;

                if (_isDebug)
                {
                    if (_traceListener == null || !System.Diagnostics.Trace.Listeners.Contains(_traceListener))
                    {
                        var logDir = System.AppDomain.CurrentDomain.BaseDirectory + "App_Data";
                        string logFile = Path.Combine(logDir, "SenparcWeixinTrace.log");
                        System.IO.TextWriter logWriter = new System.IO.StreamWriter(logFile, true);
                        _traceListener = _traceListener ?? new TextWriterTraceListener(logWriter);
                        System.Diagnostics.Trace.Listeners.Add(_traceListener);
                    }
                }
                else
                {
                    if (_traceListener != null && System.Diagnostics.Trace.Listeners.Contains(_traceListener))
                    {
                        System.Diagnostics.Trace.Listeners.Remove(_traceListener);
                    }
                }
            }
        }
    }
}
