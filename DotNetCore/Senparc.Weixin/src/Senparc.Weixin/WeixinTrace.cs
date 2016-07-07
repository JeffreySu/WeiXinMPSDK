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
#if NET451
		private static TraceListener _traceListener = null;
#endif
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
#if NET451
			lock (TraceLock)
			{
				var logDir = AppDomain.CurrentDomain.BaseDirectory + "App_Data";
				string logFile = Path.Combine(logDir, "LSWWeixinTrace.log");
				TextWriter logWriter = new StreamWriter(logFile, true);
				_traceListener = new TextWriterTraceListener(logWriter);
				Trace.Listeners.Add(_traceListener);
				Trace.AutoFlush = true;
			}
#endif
		}

		internal static void Close()
		{
#if NET451
			lock (TraceLock)
			{
				if (_traceListener != null && Trace.Listeners.Contains(_traceListener))
				{
					_traceListener.Close();
					Trace.Listeners.Remove(_traceListener);
				}
			}
#endif
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
#if NET451
			lock (TraceLock)
			{
				Trace.Unindent();
			}
#endif
		}

		private static void Indent()
		{
#if NET451
			lock (TraceLock)
			{
				Trace.Indent();
			}
#endif
		}

		private static void Flush()
		{
#if NET451
			lock (TraceLock)
			{
				Trace.Flush();
			}
#endif
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
#if NET451
			lock (TraceLock)
			{
				Trace.WriteLine(message);
			}
#endif
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
