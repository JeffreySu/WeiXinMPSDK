/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：WebhookApi.cs
    文件功能描述：Webhook群机器人相关Api
    
    
    创建标识：lishewen - 20190701

    修改标识：lishewen - 20190706
    修改描述：v3.5.8 丰富 Webhook 接口：SendImage

----------------------------------------------------------------*/

/*
    官方文档：https://work.weixin.qq.com/api/doc?notreplace=true#90000/90135/91760
 */


using Senparc.CO2NET.Helpers.Serializers;
using Senparc.NeuChar;
using Senparc.Weixin.Entities;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.Webhook
{
    /// <summary>
    /// Webhook群机器人相关Api
    /// </summary>
    public static class WebhookApi
    {
        private static string _urlFormat = Config.ApiWorkHost + "/cgi-bin/webhook/send?key={0}";
        #region 同步方法
        /// <summary>
        /// 群机器人发送文本信息方法
        /// </summary>
        /// <param name="key">机器人Key</param>
        /// <param name="content">文本内容，最长不超过2048个字节，必须是utf8编码</param>
        /// <param name="mentioned_list">userid的列表，提醒群中的指定成员(@某个成员)，@all表示提醒所有人，如果开发者获取不到userid，可以使用mentioned_mobile_list</param>
        /// <param name="mentioned_mobile_list">手机号列表，提醒手机号对应的群成员(@某个成员)，@all表示提醒所有人</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "WebhookApi.SendText", true)]
        public static WorkJsonResult SendText(string key, string content, string[] mentioned_list = null, string[] mentioned_mobile_list = null, int timeOut = Config.TIME_OUT)
        {
            var data = new
            {
                msgtype = "text",
                text = new
                {
                    content,
                    mentioned_list,
                    mentioned_mobile_list
                }
            };
            JsonSetting jsonSetting = new JsonSetting(true);
            return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<WorkJsonResult>(key, _urlFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
        }
        /// <summary>
        /// 群机器人发送markdown信息
        /// </summary>
        /// <param name="key">机器人Key</param>
        /// <param name="content">文本内容，最长不超过2048个字节，必须是utf8编码</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "WebhookApi.SendMarkdown", true)]
        public static WorkJsonResult SendMarkdown(string key, string content, int timeOut = Config.TIME_OUT)
        {
            var data = new
            {
                msgtype = "markdown",
                markdown = new
                {
                    content
                }
            };
            JsonSetting jsonSetting = new JsonSetting(true);
            return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<WorkJsonResult>(key, _urlFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
        }
        /// <summary>
        /// 群机器人发送图片信息
        /// </summary>
        /// <param name="key">机器人Key</param>
        /// <param name="base64">图片内容的base64编码</param>
        /// <param name="md5">图片内容（base64编码前）的md5值</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "WebhookApi.SendImage", true)]
        public static WorkJsonResult SendImage(string key, string base64, string md5, int timeOut = Config.TIME_OUT)
        {
            var data = new
            {
                msgtype = "image",
                image = new
                {
                    base64,
                    md5
                }
            };
            JsonSetting jsonSetting = new JsonSetting(true);
            return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<WorkJsonResult>(key, _urlFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
        }
        /// <summary>
        /// 群机器人发送图片信息
        /// </summary>
        /// <param name="key">机器人Key</param>
        /// <param name="filepath">图片文件路径</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "WebhookApi.SendImage", true)]
        public static WorkJsonResult SendImage(string key, string filepath, int timeOut = Config.TIME_OUT)
        {
            FileStream file = new FileStream(filepath, FileMode.Open);
            return SendImage(key, file, timeOut);
        }
        /// <summary>
        /// 群机器人发送图片信息
        /// </summary>
        /// <param name="key">机器人Key</param>
        /// <param name="stream">图片流</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "WebhookApi.SendImage", true)]
        public static WorkJsonResult SendImage(string key, Stream stream, int timeOut = Config.TIME_OUT)
        {
            var md5str = Senparc.CO2NET.Helpers.EncryptHelper.GetMD5(stream, false);
            string base64 = Senparc.CO2NET.Utilities.StreamUtility.GetBase64String(stream);
            //stream.Close();
            return SendImage(key, base64, md5str, timeOut);
        }
        /// <summary>
        /// 群机器人发送图文信息
        /// </summary>
        /// <param name="key">机器人Key</param>
        /// <param name="data">内容</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "WebhookApi.SendNews", true)]
        public static WorkJsonResult SendNews(string key, WebhookNews data, int timeOut = Config.TIME_OUT)
        {
            JsonSetting jsonSetting = new JsonSetting(true);
            return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<WorkJsonResult>(key, _urlFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
        }
        #endregion

        #region 异步方法
        /// <summary>
        /// 【异步方法】群机器人发送文本信息方法
        /// </summary>
        /// <param name="key">机器人Key</param>
        /// <param name="content">文本内容，最长不超过2048个字节，必须是utf8编码</param>
        /// <param name="mentioned_list">userid的列表，提醒群中的指定成员(@某个成员)，@all表示提醒所有人，如果开发者获取不到userid，可以使用mentioned_mobile_list</param>
        /// <param name="mentioned_mobile_list">手机号列表，提醒手机号对应的群成员(@某个成员)，@all表示提醒所有人</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "WebhookApi.SendTextAsync", true)]
        public static async Task<WorkJsonResult> SendTextAsync(string key, string content, string[] mentioned_list = null, string[] mentioned_mobile_list = null, int timeOut = Config.TIME_OUT)
        {
            var data = new
            {
                msgtype = "text",
                text = new
                {
                    content,
                    mentioned_list,
                    mentioned_mobile_list
                }
            };
            JsonSetting jsonSetting = new JsonSetting(true);
            return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WorkJsonResult>(key, _urlFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting).ConfigureAwait(false);
        }
        /// <summary>
        /// 【异步方法】群机器人发送markdown信息
        /// </summary>
        /// <param name="key">机器人Key</param>
        /// <param name="content">文本内容，最长不超过2048个字节，必须是utf8编码</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "WebhookApi.SendMarkdownAsync", true)]
        public static async Task<WorkJsonResult> SendMarkdownAsync(string key, string content, int timeOut = Config.TIME_OUT)
        {
            var data = new
            {
                msgtype = "markdown",
                markdown = new
                {
                    content
                }
            };
            JsonSetting jsonSetting = new JsonSetting(true);
            return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WorkJsonResult>(key, _urlFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting).ConfigureAwait(false);
        }
        /// <summary>
        /// 【异步方法】群机器人发送图片信息
        /// </summary>
        /// <param name="key">机器人Key</param>
        /// <param name="base64">图片内容的base64编码</param>
        /// <param name="md5">图片内容（base64编码前）的md5值</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "WebhookApi.SendImageAsync", true)]
        public static async Task<WorkJsonResult> SendImageAsync(string key, string base64, string md5, int timeOut = Config.TIME_OUT)
        {
            var data = new
            {
                msgtype = "image",
                image = new
                {
                    base64,
                    md5
                }
            };
            JsonSetting jsonSetting = new JsonSetting(true);
            return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WorkJsonResult>(key, _urlFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting).ConfigureAwait(false);
        }
        /// <summary>
        /// 【异步方法】群机器人发送图片信息
        /// </summary>
        /// <param name="key">机器人Key</param>
        /// <param name="filepath">图片文件路径</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "WebhookApi.SendImageAsync", true)]
        public static async Task<WorkJsonResult> SendImageAsync(string key, string filepath, int timeOut = Config.TIME_OUT)
        {
            FileStream file = new FileStream(filepath, FileMode.Open);
            return await SendImageAsync(key, file, timeOut);
        }
        /// <summary>
        /// 【异步方法】群机器人发送图片信息
        /// </summary>
        /// <param name="key">机器人Key</param>
        /// <param name="stream">图片流</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "WebhookApi.SendImageAsync", true)]
        public static async Task<WorkJsonResult> SendImageAsync(string key, Stream stream, int timeOut = Config.TIME_OUT)
        {
            var md5str = Senparc.CO2NET.Helpers.EncryptHelper.GetMD5(stream, false);
            string base64 = await Senparc.CO2NET.Utilities.StreamUtility.GetBase64StringAsync(stream);
            //执行异步时关闭流，有一定几率出现问题
            //stream.Close();

            return await SendImageAsync(key, base64, md5str, timeOut);
        }
        /// <summary>
        /// 【异步方法】群机器人发送图文信息
        /// </summary>
        /// <param name="key">机器人Key</param>
        /// <param name="data">内容</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "WebhookApi.SendNewsAsync", true)]
        public static async Task<WorkJsonResult> SendNewsAsync(string key, WebhookNews data, int timeOut = Config.TIME_OUT)
        {
            JsonSetting jsonSetting = new JsonSetting(true);
            return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WorkJsonResult>(key, _urlFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting).ConfigureAwait(false);
        }
        #endregion
    }
}
