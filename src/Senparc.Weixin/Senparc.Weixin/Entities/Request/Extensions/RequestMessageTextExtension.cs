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
        public IRequestMessageText RequestMessage { get; set; }

        public ResponseMessageBase ResponseMessage { get; set; }

        internal string Keyword { get; set; }

        /// <summary>
        /// 是否已经匹配成功
        /// </summary>
        public bool MatchSuccessed { get { return ResponseMessage != null; } }

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
            return new RequestMessageTextKeywordHandler(requestMessage);
        }

        /// <summary>
        /// 匹配关键词
        /// </summary>
        /// <param name="requestMessageTextKeywordHandler"></param>
        /// <param name="keyword">关键词</param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static RequestMessageTextKeywordHandler Keyword(this RequestMessageTextKeywordHandler requestMessageTextKeywordHandler, string keyword, Func<ResponseMessageBase> func)
        {
            if (!requestMessageTextKeywordHandler.MatchSuccessed
                && requestMessageTextKeywordHandler.Keyword == keyword.Trim().ToUpper())
            {
                requestMessageTextKeywordHandler.ResponseMessage = func();
            }
            return requestMessageTextKeywordHandler;
        }

        /// <summary>
        /// 匹配正则表达式
        /// </summary>
        /// <param name="requestMessageTextKeywordHandler"></param>
        /// <param name="regex">正则表达式</param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static RequestMessageTextKeywordHandler Regex(this RequestMessageTextKeywordHandler requestMessageTextKeywordHandler, Regex regex, Func<ResponseMessageBase> func)
        {
            if (!requestMessageTextKeywordHandler.MatchSuccessed
               && regex.IsMatch(requestMessageTextKeywordHandler.Keyword))
            {
                requestMessageTextKeywordHandler.ResponseMessage = func();
            }
            return requestMessageTextKeywordHandler;
        }

    }
}
