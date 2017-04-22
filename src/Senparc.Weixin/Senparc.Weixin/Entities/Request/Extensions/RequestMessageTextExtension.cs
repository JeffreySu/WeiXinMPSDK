using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Senparc.Weixin.Entities.Request
{
    /// <summary>
    /// 所有RequestMessageText的接口
    /// </summary>
    public interface IRequestMessageText
    {
        /// <summary>
        /// 文本消息内容
        /// </summary>
        string Content { get; set; }
    }

    public class RequestMessageTextKeywordHandler
    {
        internal string Keyword { get; set; }

        internal Func<ResponseMessageBase> DefaultMessage;

        internal IRequestMessageText RequestMessage { get; set; }

        public IResponseMessageBase ResponseMessage { get; set; }

        /// <summary>
        /// 是否已经匹配成功
        /// </summary>
        public bool MatchSuccessed { get; set; }


        public RequestMessageTextKeywordHandler(IRequestMessageText requestMessage)
        {
            RequestMessage = requestMessage;
            Keyword = RequestMessage.Content.Trim().ToUpper();
        }
    }

    /// <summary>
    /// RequestMessageText 扩展
    /// </summary>
    public static class RequestMessageTextExtension
    {
        /// <summary>
        /// 开始匹配
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public static RequestMessageTextKeywordHandler StartHandler(this IRequestMessageText requestMessage)
        {
            var handler = new RequestMessageTextKeywordHandler(requestMessage);
            return handler;
        }

        /// <summary>
        /// 获取最终响应消息
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static IResponseMessageBase GetResponseMessage(this RequestMessageTextKeywordHandler handler)
        {
            if (!!handler.MatchSuccessed
                && handler.DefaultMessage != null)
            {
                handler.ResponseMessage = handler.DefaultMessage();
            }
            return handler.ResponseMessage;
        }


        /// <summary>
        /// 匹配关键词
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="keyword">关键词</param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static RequestMessageTextKeywordHandler Keyword(this RequestMessageTextKeywordHandler handler, string keyword, Func<IResponseMessageBase> func)
        {
            if (!handler.MatchSuccessed
                && handler.Keyword == keyword.Trim().ToUpper())
            {
                handler.MatchSuccessed = true;
                handler.ResponseMessage = func();
            }
            return handler;
        }

        /// <summary>
        /// 匹配关键词（只要有一个满足即可触发）
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="keywords">多个关键词</param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static RequestMessageTextKeywordHandler Keywords(this RequestMessageTextKeywordHandler handler, string[] keywords, Func<IResponseMessageBase> func)
        {
            foreach (var keyword in keywords)
            {
                handler.Keyword(keyword, func);
            }
            return handler;
        }

        /// <summary>
        /// 匹配正则表达式
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="regex">正则表达式</param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static RequestMessageTextKeywordHandler Regex(this RequestMessageTextKeywordHandler handler, Regex regex, Func<IResponseMessageBase> func)
        {
            if (!handler.MatchSuccessed
               && regex.IsMatch(handler.Keyword))
            {
                handler.MatchSuccessed = true;
                handler.ResponseMessage = func();
            }
            return handler;
        }

        /// <summary>
        /// 默认消息
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static RequestMessageTextKeywordHandler Default(this RequestMessageTextKeywordHandler handler, Func<IResponseMessageBase> func)
        {
            if (!handler.MatchSuccessed)
            {
                handler.ResponseMessage = func();
            }
            return handler;
        }
    }
}
