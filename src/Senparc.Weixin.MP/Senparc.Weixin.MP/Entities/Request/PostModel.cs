#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2019 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2019 Senparc
    
    文件名：PostModel.cs
    文件功能描述：微信公众服务器Post过来的加密参数集合（不包括PostData）
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口

    修改标识：Senparc - 20150321
    修改描述：v11.2.7 添加SetSecretInfo()方法
 
    修改标识：Senparc - 20181117
    修改描述：v16.5.0 添加 DomainId 属性

----------------------------------------------------------------*/

using Senparc.NeuChar;

namespace Senparc.Weixin.MP.Entities.Request
{
    /// <summary>
    /// 微信公众服务器Post过来的加密参数集合（不包括PostData）
    /// <para>如需使用 NeuChar，需要在 MessageHandler 中提供 PostModel 并设置 AppId</para>
    /// </summary>
    public class PostModel : EncryptPostModel
    {
        public override string DomainId { get => AppId; set => AppId = value; }

        //以下信息不会出现在微信发过来的信息中，都是微信后台需要设置（获取的）的信息，用于扩展传参使用

        /// <summary>
        /// 当前请求对应的微信 AppId
        /// 如需在 MessageHandler 中使用 NeuChar 进行消息处理，请无比在此处提供 AppId，并对AppId-Secret进行注册，以便自动调用高级接口（如自动发送客服消息）
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 设置服务器内部保密信息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="encodingAESKey"></param>
        /// <param name="appId"></param>
        public void SetSecretInfo(string token, string encodingAESKey, string appId)
        {
            Token = token;
            EncodingAESKey = encodingAESKey;
            AppId = appId;
        }
    }
}
