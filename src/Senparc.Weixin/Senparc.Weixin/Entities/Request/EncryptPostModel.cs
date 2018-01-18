#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2018 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2018 Senparc
    
    文件名：EncryptPostModel.cs
    文件功能描述：加解密消息统一基类 接口
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/



namespace Senparc.Weixin
{
    /// <summary>
    /// 接收加密信息统一接口
    /// </summary>
    public interface IEncryptPostModel
    {
        /// <summary>
        /// Signature
        /// </summary>
        string Signature { get; set; }
        /// <summary>
        /// Msg_Signature
        /// </summary>
        string Msg_Signature { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        string Timestamp { get; set; }
        /// <summary>
        /// Nonce
        /// </summary>
        string Nonce { get; set; }

        //以下信息不会出现在微信发过来的信息中，都是企业号后台需要设置（获取的）的信息，用于扩展传参使用

        /// <summary>
        /// Token
        /// </summary>
        string Token { get; set; }
        /// <summary>
        /// EncodingAESKey
        /// </summary>
        string EncodingAESKey { get; set; }
    }

    /// <summary>
    /// 接收加密信息统一基类
    /// </summary>
    public class EncryptPostModel : IEncryptPostModel
    {
        /// <summary>
        /// Signature
        /// </summary>
        public string Signature { get; set; }
        /// <summary>
        /// Msg_Signature
        /// </summary>
        public string Msg_Signature { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        public string Timestamp { get; set; }
        /// <summary>
        /// Nonce
        /// </summary>
        public string Nonce { get; set; }

        //以下信息不会出现在微信发过来的信息中，都是企业号后台需要设置（获取的）的信息，用于扩展传参使用

        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// EncodingAESKey
        /// </summary>
        public string EncodingAESKey { get; set; }

        /// <summary>
        /// 设置服务器内部保密信息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="encodingAESKey"></param>
        public virtual void SetSecretInfo(string token, string encodingAESKey)
        {
            Token = token;
            EncodingAESKey = encodingAESKey;
        }
    }
}
