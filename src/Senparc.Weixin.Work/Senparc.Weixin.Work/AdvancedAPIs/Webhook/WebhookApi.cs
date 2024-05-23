/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：WebhookApi.cs
    文件功能描述：Webhook群机器人相关Api
    
    
    创建标识：lishewen - 20190701

    修改标识：lishewen - 20190706
    修改描述：v3.5.8 丰富 Webhook 接口：SendImage

    修改标识：mc7246 - 20230211
    修改描述：丰富 Webhook 接口：SendTemplateCard， SendFile

----------------------------------------------------------------*/

/*
    官方文档：https://work.weixin.qq.com/api/doc?notreplace=true#90000/90135/91760
 */


using Senparc.CO2NET.Helpers.Serializers;
using Senparc.NeuChar;
using Senparc.Weixin.Entities;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.Webhook
{
    /// <summary>
    /// Webhook群机器人相关Api
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_Work, true)]
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
        /// 群机器人发送模版卡片
        /// </summary>
        /// <param name="key">机器人Key</param>
        /// <param name="requestData">模板信息数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WorkJsonResult SendTemplateCard(string key, TemplateCardRequestData requestData, int timeOut = Config.TIME_OUT)
        {
            var data = new
            {
                msgtype = "template_card",
                template_card = new
                {
                    requestData.card_type,
                    requestData.source,
                    requestData.main_title,
                    requestData.emphasis_content,
                    requestData.quote_area,
                    requestData.sub_title_text,
                    requestData.horizontal_content_list,
                    requestData.jump_list,
                    requestData.card_action,
                    requestData.card_image,
                    requestData.vertical_content_list,
                    requestData.image_text_area

                }
            };
            JsonSetting jsonSetting = new JsonSetting(true);
            return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<WorkJsonResult>(key, _urlFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
        }

        /// <summary>
        /// 群机器人发送文件信息
        /// </summary>
        /// <param name="key">机器人Key</param>
        /// <param name="media_id">文件id，通过文件上传接口获取</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WorkJsonResult SendFile(string key, string media_id, int timeOut=Config.TIME_OUT)
        {
            var data = new
            {
                msgtype = "file",
                media_id
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
        public static WorkJsonResult SendNews(string key, WebhookNews data, int timeOut = Config.TIME_OUT)
        {
            JsonSetting jsonSetting = new JsonSetting(true);
            return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<WorkJsonResult>(key, _urlFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="key"></param>
        /// <param name="filepath"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static UploadMediaResult UploadMedia(string key, string filepath, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiWorkHost + "/cgi-bin/webhook/upload_media?key={0}&type=file", key);
            var fileDictionary = new Dictionary<string, string>();
            fileDictionary["name"] = "media";
            fileDictionary["filename"] = filepath;

            return Senparc.CO2NET.HttpUtility.Post.PostFileGetJson<UploadMediaResult>(CommonDI.CommonSP, url, null, fileDictionary, null, timeOut: timeOut);
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
        /// 【异步方法】群机器人发送模版卡片
        /// </summary>
        /// <param name="key">机器人Key</param>
        /// <param name="requestData">模板信息数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WorkJsonResult> SendTemplateCardAsync(string key, TemplateCardRequestData requestData, int timeOut = Config.TIME_OUT)
        {
            var data = new
            {
                msgtype = "template_card",
                template_card = new
                {
                    requestData.card_type,
                    requestData.source,
                    requestData.main_title,
                    requestData.emphasis_content,
                    requestData.quote_area,
                    requestData.sub_title_text,
                    requestData.horizontal_content_list,
                    requestData.jump_list,
                    requestData.card_action,
                    requestData.card_image,
                    requestData.vertical_content_list,
                    requestData.image_text_area
                }
            };
            JsonSetting jsonSetting = new JsonSetting(true);
            return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WorkJsonResult>(key, _urlFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting).ConfigureAwait(false);
        }

        /// <summary>
        /// 群机器人发送文件信息
        /// </summary>
        /// <param name="key">机器人Key</param>
        /// <param name="media_id">文件id，通过文件上传接口获取</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WorkJsonResult> SendFileAsync(string key, string media_id, int timeOut = Config.TIME_OUT)
        {
            var data = new
            {
                msgtype = "file",
                media_id
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
        public static async Task<WorkJsonResult> SendNewsAsync(string key, WebhookNews data, int timeOut = Config.TIME_OUT)
        {
            JsonSetting jsonSetting = new JsonSetting(true);
            return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WorkJsonResult>(key, _urlFormat, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting).ConfigureAwait(false);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="key"></param>
        /// <param name="filepath"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<UploadMediaResult> UploadMediaAsync(string key, string filepath, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiWorkHost + "/cgi-bin/webhook/upload_media?key={0}&type=file", key);
            var fileDictionary = new Dictionary<string, string>();
            fileDictionary["name"] = "media";
            fileDictionary["filename"] = filepath;

            return await Senparc.CO2NET.HttpUtility.Post.PostFileGetJsonAsync<UploadMediaResult>(CommonDI.CommonSP, url, null, fileDictionary, null, timeOut: timeOut).ConfigureAwait(false);
        }
        #endregion
    }
}
