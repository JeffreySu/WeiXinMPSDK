/*----------------------------------------------------------------
    文件名：WXBizMsgCrypt.cs
    文件功能描述：加解密算法


    创建标识：Senparc - 20140920

    修改标识：Senparc - 20150313
    修改描述：整理接口

    修改标识：Senparc - 20191005
    修改描述：合并更新官方最新示例（没有实质变化）：https://work.weixin.qq.com/api/doc#90000/90138/90307/c#%E5%BA%93

    修改标识：Senparc - 20260718
    修改描述：v3.31.1 规范 SHA1 哈希资源释放

    修改标识：Senparc - 20260724
    修改描述：v3.32.1 补齐企业微信通讯录、安全、智能机器人、微信客服和获客助手接口

----------------------------------------------------------------*/

using Senparc.CO2NET.Trace;
using System;
using System.Collections;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
//using System.Web;

//-40001 ： 签名验证错误
//-40002 :  xml解析失败
//-40003 :  sha加密生成签名失败
//-40004 :  AESKey 非法
//-40005 :  corpid 校验错误
//-40006 :  AES 加密失败
//-40007 ： AES 解密失败
//-40008 ： 解密后得到的buffer非法
//-40009 :  base64加密异常
//-40010 :  base64解密异常
namespace Senparc.Weixin.Work.Tencent
{
    public class WXBizMsgCrypt
    {
        string m_sToken;
        string m_sEncodingAESKey;
        string m_sReceiveId;
        enum WXBizMsgCryptErrorCode
        {
            WXBizMsgCrypt_OK = 0,
            WXBizMsgCrypt_ValidateSignature_Error = -40001,
            WXBizMsgCrypt_ParseXml_Error = -40002,
            WXBizMsgCrypt_ComputeSignature_Error = -40003,
            WXBizMsgCrypt_IllegalAesKey = -40004,
            WXBizMsgCrypt_ValidateCorpid_Error = -40005,
            WXBizMsgCrypt_EncryptAES_Error = -40006,
            WXBizMsgCrypt_DecryptAES_Error = -40007,
            WXBizMsgCrypt_IllegalBuffer = -40008,
            WXBizMsgCrypt_EncodeBase64_Error = -40009,
            WXBizMsgCrypt_DecodeBase64_Error = -40010
        };

        //构造函数
        // @param sToken: 企业微信后台，开发者设置的Token
        // @param sEncodingAESKey: 企业微信后台，开发者设置的EncodingAESKey
        // @param sReceiveId: 不同场景含义不同，详见文档说明（消息加密时为 CorpId）
        public WXBizMsgCrypt(string sToken, string sEncodingAESKey, string sReceiveId)
        {
            m_sToken = sToken;
            m_sReceiveId = sReceiveId;
            m_sEncodingAESKey = sEncodingAESKey;
        }

        //验证URL
        // @param sMsgSignature: 签名串，对应URL参数的msg_signature
        // @param sTimeStamp: 时间戳，对应URL参数的timestamp
        // @param sNonce: 随机串，对应URL参数的nonce
        // @param sEchoStr: 随机串，对应URL参数的echostr
        // @param sReplyEchoStr: 解密之后的echostr，当return返回0时有效
        // @return：成功0，失败返回对应的错误码
        public int VerifyURL(string sMsgSignature, string sTimeStamp, string sNonce, string sEchoStr, ref string sReplyEchoStr)
        {
            int ret = 0;
            if (m_sEncodingAESKey.Length != 43)
            {
                return (int)WXBizMsgCryptErrorCode.WXBizMsgCrypt_IllegalAesKey;
            }
            ret = VerifySignature(m_sToken, sTimeStamp, sNonce, sEchoStr, sMsgSignature);
            if (0 != ret)
            {
                return ret;
            }
            sReplyEchoStr = "";
            string cpid = "";
            try
            {
                sReplyEchoStr = Cryptography.AES_decrypt(sEchoStr, m_sEncodingAESKey, ref cpid); //m_sReceiveId);
            }
            catch (Exception)
            {
                sReplyEchoStr = "";
                return (int)WXBizMsgCryptErrorCode.WXBizMsgCrypt_DecryptAES_Error;
            }
            if (cpid != m_sReceiveId)
            {
                sReplyEchoStr = "";
                return (int)WXBizMsgCryptErrorCode.WXBizMsgCrypt_ValidateCorpid_Error;
            }
            return 0;
        }

        // 检验消息的真实性，并且获取解密后的明文
        // @param sMsgSignature: 签名串，对应URL参数的msg_signature
        // @param sTimeStamp: 时间戳，对应URL参数的timestamp
        // @param sNonce: 随机串，对应URL参数的nonce
        // @param sPostData: 密文，对应POST请求的数据
        // @param sMsg: 解密后的原文，当return返回0时有效
        // @return: 成功0，失败返回对应的错误码
        public int DecryptMsg(string sMsgSignature, string sTimeStamp, string sNonce, string sPostData, ref string sMsg)
        {
            if (m_sEncodingAESKey.Length != 43)
            {
                return (int)WXBizMsgCryptErrorCode.WXBizMsgCrypt_IllegalAesKey;
            }
            XmlDocument doc = new Senparc.CO2NET.ExtensionEntities.XmlDocument_XxeFixed();
            XmlNode root;
            string sEncryptMsg;
            try
            {
                doc.LoadXml(sPostData);
                root = doc.FirstChild;
                sEncryptMsg = root["Encrypt"].InnerText;
            }
            catch (Exception)
            {
                return (int)WXBizMsgCryptErrorCode.WXBizMsgCrypt_ParseXml_Error;
            }
            //verify signature
            int ret = 0;
            ret = VerifySignature(m_sToken, sTimeStamp, sNonce, sEncryptMsg, sMsgSignature);
            if (ret != 0)
                return ret;
            //decrypt
            string cpid = "";
            try
            {
                sMsg = Cryptography.AES_decrypt(sEncryptMsg, m_sEncodingAESKey, ref cpid);
            }
            catch (FormatException)
            {
                sMsg = "";
                return (int)WXBizMsgCryptErrorCode.WXBizMsgCrypt_DecodeBase64_Error;
            }
            catch (Exception)
            {
                sMsg = "";
                return (int)WXBizMsgCryptErrorCode.WXBizMsgCrypt_DecryptAES_Error;
            }
            if (cpid != m_sReceiveId)
                return (int)WXBizMsgCryptErrorCode.WXBizMsgCrypt_ValidateCorpid_Error;
            return 0;
        }

        /// <summary>
        /// 校验并解密 JSON 回调。适用于企业微信智能机器人等直接以 encrypt 字段承载密文的场景。
        /// </summary>
        /// <param name="sMsgSignature">企业微信消息签名。</param>
        /// <param name="sTimeStamp">签名时间戳。</param>
        /// <param name="sNonce">签名随机字符串。</param>
        /// <param name="sEncryptMsg">待解密的密文。</param>
        /// <param name="sMsg">输出的解密消息。</param>
        /// <returns>微信接口返回结果。</returns>
        public int DecryptJsonMsg(string sMsgSignature, string sTimeStamp, string sNonce,
            string sEncryptMsg, ref string sMsg)
        {
            if (m_sEncodingAESKey.Length != 43)
            {
                return (int)WXBizMsgCryptErrorCode.WXBizMsgCrypt_IllegalAesKey;
            }

            var ret = VerifySignature(m_sToken, sTimeStamp, sNonce, sEncryptMsg, sMsgSignature);
            if (ret != 0)
            {
                return ret;
            }

            var receiveId = "";
            try
            {
                sMsg = Cryptography.AES_decrypt(sEncryptMsg, m_sEncodingAESKey, ref receiveId);
            }
            catch (FormatException)
            {
                sMsg = "";
                return (int)WXBizMsgCryptErrorCode.WXBizMsgCrypt_DecodeBase64_Error;
            }
            catch (Exception)
            {
                sMsg = "";
                return (int)WXBizMsgCryptErrorCode.WXBizMsgCrypt_DecryptAES_Error;
            }

            return receiveId == m_sReceiveId
                ? 0
                : (int)WXBizMsgCryptErrorCode.WXBizMsgCrypt_ValidateCorpid_Error;
        }

        /// <summary>
        /// 加密 JSON 回复并生成企业微信要求的签名字段。
        /// </summary>
        /// <param name="sReplyMsg">待加密的回复消息。</param>
        /// <param name="sTimeStamp">签名时间戳。</param>
        /// <param name="sNonce">签名随机字符串。</param>
        /// <param name="sEncryptedReply">输出的加密回复 JSON。</param>
        /// <returns>微信接口返回结果。</returns>
        public int EncryptJsonMsg(string sReplyMsg, string sTimeStamp, string sNonce,
            ref BotEncryptedReply sEncryptedReply)
        {
            if (m_sEncodingAESKey.Length != 43)
            {
                return (int)WXBizMsgCryptErrorCode.WXBizMsgCrypt_IllegalAesKey;
            }

            string encrypted;
            try
            {
                encrypted = Cryptography.AES_encrypt(sReplyMsg, m_sEncodingAESKey, m_sReceiveId);
            }
            catch (Exception)
            {
                return (int)WXBizMsgCryptErrorCode.WXBizMsgCrypt_EncryptAES_Error;
            }

            var signature = "";
            var ret = GenarateSinature(m_sToken, sTimeStamp, sNonce, encrypted, ref signature);
            if (ret != 0)
            {
                return ret;
            }

            sEncryptedReply = new BotEncryptedReply
            {
                encrypt = encrypted,
                msgsignature = signature,
                timestamp = long.TryParse(sTimeStamp, out var timestamp) ? timestamp : 0,
                nonce = sNonce
            };
            return 0;
        }

        //将企业号回复用户的消息加密打包
        // @param sReplyMsg: 企业号待回复用户的消息，xml格式的字符串
        // @param sTimeStamp: 时间戳，可以自己生成，也可以用URL参数的timestamp
        // @param sNonce: 随机串，可以自己生成，也可以用URL参数的nonce
        // @param sEncryptMsg: 加密后的可以直接回复用户的密文，包括msg_signature, timestamp, nonce, encrypt的xml格式的字符串,
        //						当return返回0时有效
        // return：成功0，失败返回对应的错误码
        public int EncryptMsg(string sReplyMsg, string sTimeStamp, string sNonce, ref string sEncryptMsg)
        {
            if (m_sEncodingAESKey.Length != 43)
            {
                return (int)WXBizMsgCryptErrorCode.WXBizMsgCrypt_IllegalAesKey;
            }
            string raw = "";
            try
            {
                raw = Cryptography.AES_encrypt(sReplyMsg, m_sEncodingAESKey, m_sReceiveId);
            }
            catch (Exception)
            {
                return (int)WXBizMsgCryptErrorCode.WXBizMsgCrypt_EncryptAES_Error;
            }
            string MsgSigature = "";
            int ret = 0;
            ret = GenarateSinature(m_sToken, sTimeStamp, sNonce, raw, ref MsgSigature);
            if (0 != ret)
                return ret;
            sEncryptMsg = "";

            string EncryptLabelHead = "<Encrypt><![CDATA[";
            string EncryptLabelTail = "]]></Encrypt>";
            string MsgSigLabelHead = "<MsgSignature><![CDATA[";
            string MsgSigLabelTail = "]]></MsgSignature>";
            string TimeStampLabelHead = "<TimeStamp><![CDATA[";
            string TimeStampLabelTail = "]]></TimeStamp>";
            string NonceLabelHead = "<Nonce><![CDATA[";
            string NonceLabelTail = "]]></Nonce>";
            sEncryptMsg = sEncryptMsg + "<xml>" + EncryptLabelHead + raw + EncryptLabelTail;
            sEncryptMsg = sEncryptMsg + MsgSigLabelHead + MsgSigature + MsgSigLabelTail;
            sEncryptMsg = sEncryptMsg + TimeStampLabelHead + sTimeStamp + TimeStampLabelTail;
            sEncryptMsg = sEncryptMsg + NonceLabelHead + sNonce + NonceLabelTail;
            sEncryptMsg += "</xml>";
            return 0;
        }

        public class DictionarySort : System.Collections.IComparer
        {
            public int Compare(object oLeft, object oRight)
            {
                string sLeft = oLeft as string;
                string sRight = oRight as string;
                int iLeftLength = sLeft.Length;
                int iRightLength = sRight.Length;
                int index = 0;
                while (index < iLeftLength && index < iRightLength)
                {
                    if (sLeft[index] < sRight[index])
                        return -1;
                    else if (sLeft[index] > sRight[index])
                        return 1;
                    else
                        index++;
                }
                return iLeftLength - iRightLength;

            }
        }
        //Verify Signature
        private static int VerifySignature(string sToken, string sTimeStamp, string sNonce, string sMsgEncrypt, string sSigture)
        {
            string hash = "";
            int ret = 0;
            ret = GenarateSinature(sToken, sTimeStamp, sNonce, sMsgEncrypt, ref hash);
            if (ret != 0)
                return ret;
            if (hash == sSigture)
                return 0;
            else
            {
                return (int)WXBizMsgCryptErrorCode.WXBizMsgCrypt_ValidateSignature_Error;
            }
        }

        public static int GenarateSinature(string sToken, string sTimeStamp, string sNonce, string sMsgEncrypt, ref string sMsgSignature)
        {
            //校验规则：https://work.weixin.qq.com/api/doc#90000/90139/90968/%E6%B6%88%E6%81%AF%E4%BD%93%E7%AD%BE%E5%90%8D%E6%A0%A1%E9%AA%8C
            ArrayList AL = new ArrayList();
            AL.Add(sToken);
            AL.Add(sTimeStamp);
            AL.Add(sNonce);
            AL.Add(sMsgEncrypt);
            AL.Sort(new DictionarySort());
            string raw = "";
            for (int i = 0; i < AL.Count; ++i)
            {
                raw += AL[i];
            }

            ASCIIEncoding enc;
            string hash = "";
            try
            {
#if NET462
                using (var sha = new SHA1CryptoServiceProvider())
#else
                using (var sha = SHA1.Create())
#endif
                {
                    enc = new ASCIIEncoding();
                    byte[] dataToHash = enc.GetBytes(raw);
                    byte[] dataHashed = sha.ComputeHash(dataToHash);
                    hash = BitConverter.ToString(dataHashed).Replace("-", "");
                    hash = hash.ToLower();
                }
            }
            catch (Exception)
            {
                return (int)WXBizMsgCryptErrorCode.WXBizMsgCrypt_ComputeSignature_Error;
            }
            sMsgSignature = hash;
            return 0;
        }
    }
}
