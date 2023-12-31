#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2024 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2024 Senparc
    
    文件名：RequestMessageMiniProgramPage.cs
    文件功能描述：接收小程序页面消息
    
    
    创建标识：Senparc - 20210520
    
----------------------------------------------------------------*/

/*
XML 格式
<xml>
  <ToUserName><![CDATA[toUser]]></ToUserName>
  <FromUserName><![CDATA[fromUser]]></FromUserName>
  <CreateTime>1482048670</CreateTime>
  <MsgType><![CDATA[miniprogrampage]]></MsgType>
  <MsgId>1234567890123456</MsgId>
  <Title><![CDATA[Title]]></Title>
  <AppId><![CDATA[AppId]]></AppId>
  <PagePath><![CDATA[PagePath]]></PagePath>
  <ThumbUrl><![CDATA[ThumbUrl]]></ThumbUrl>
  <ThumbMediaId><![CDATA[ThumbMediaId]]></ThumbMediaId>
</xml>

JSON 格式
{
  "ToUserName": "toUser",
  "FromUserName": "fromUser",
  "CreateTime": 1482048670,
  "MsgType": "miniprogrampage",
  "MsgId": 1234567890123456,
  "Title":"title",
  "AppId":"appid",
  "PagePath":"path",
  "ThumbUrl":"",
  "ThumbMediaId":""
}
 */


using Senparc.NeuChar;
using Senparc.NeuChar.Entities;

namespace Senparc.Weixin.WxOpen.Entities
{
    /// <summary>
    /// 接收小程序页面消息
    /// </summary>
    public class RequestMessageMiniProgramPage : RequestMessageBase, IRequestMessageMiniProgramPage
    {
        public override RequestMsgType MsgType
        {
            get { return RequestMsgType.MiniProgramPage; }
        }

        /// <summary>
        /// 文本消息内容
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 小程序appid
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 小程序页面路径
        /// </summary>
        public string PagePath { get; set; }
        /// <summary>
        /// 封面图片的临时cdn链接
        /// </summary>
        public string ThumbUrl { get; set; }
        /// <summary>
        /// 封面图片的临时素材id
        /// </summary>
        public string ThumbMediaId { get; set; }
    }
}
