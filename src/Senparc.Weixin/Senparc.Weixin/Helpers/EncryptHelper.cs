﻿#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2017 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2017 Senparc
 
    创建标识：Senparc - 20160808
    创建描述：安全帮助类，提供SHA-1、AES算法等

    修改标识：Senparc - 20170130
    修改描述：v4.9.15 添加AES加密、解密算法
    
    修改标识：Senparc - 20170313
    修改描述：v4.11.4 修改EncryptHelper.GetSha1(string encypStr)方法算法
    
----------------------------------------------------------------*/


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
//using System.Security.Cryptography.Hashing.Algorithms;

namespace Senparc.Weixin.Helpers
{
    ///
    /// 安全帮助类，提供SHA-1算法等
    ///
    public class EncryptHelper
    {
        /// <summary>
        /// 采用SHA-1算法加密字符串（小写）
        /// </summary>
        /// <param name="encypStr">需要加密的字符串</param>
        /// <returns></returns>
        public static string GetSha1(string encypStr)
        {
            throw new ArgumentException("暂时不可用");
            //byte[] strRes = Encoding.UTF8.GetBytes(sourceStr);
            //HashAlgorithm iSHA = new SHA1CryptoServiceProvider();
            //strRes = iSHA.ComputeHash(strRes);
            //StringBuilder enText = new StringBuilder();
            //foreach (byte iByte in strRes)
            //{
            //    enText.AppendFormat("{0:x2}", iByte);
            //}
            //return enText.ToString();

            //byte[] strRes = Encoding.Default.GetBytes(encypStr);
            //HashAlgorithm iSHA = new SHA1CryptoServiceProvider();
            //strRes = iSHA.ComputeHash(strRes);
            //StringBuilder enText = new StringBuilder();
            //foreach (byte iByte in strRes)
            //{
            //    enText.AppendFormat("{0:x2}", iByte);
            //}
        }

        /// <summary>
        /// 获取大写的MD5签名结果
        /// </summary>
        /// <param name="encypStr">需要加密的字符串</param>
        /// <param name="charset">编码</param>
        /// <returns></returns>
        public static string GetMD5(string encypStr, string charset)
        {
            string retStr;

            MD5 m5 = MD5.Create();

            //创建md5对象
            byte[] inputBye;
            byte[] outputBye;

            //使用GB2312编码方式把字符串转化为字节数组．
            try
            {
                inputBye = Encoding.GetEncoding(charset).GetBytes(encypStr);
            }
            catch (Exception ex)
            {
                inputBye = Encoding.GetEncoding("GB2312").GetBytes(encypStr);
            }
            outputBye = m5.ComputeHash(inputBye);

            retStr = BitConverter.ToString(outputBye);
            retStr = retStr.Replace("-", "").ToUpper();
            return retStr;
        }

        #region AES

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="inputdata">输入的数据</param>
        /// <param name="iv">向量</param>
        /// <param name="strKey">加密密钥</param>
        /// <returns></returns>
        public static byte[] AESEncrypt(byte[] inputdata, byte[] iv, string strKey)
        {
            //分组加密算法   
            SymmetricAlgorithm des = Aes.Create();
            byte[] inputByteArray = inputdata;//得到需要加密的字节数组       
                                              //设置密钥及密钥向量
            des.Key = Encoding.UTF8.GetBytes(strKey.Substring(0, 32));
            des.IV = iv;
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    byte[] cipherBytes = ms.ToArray();//得到加密后的字节数组   
                    //cs.Close();
                    //ms.Close();
                    return cipherBytes;
                }
            }
        }


        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="inputdata">输入的数据</param>
        /// <param name="iv">向量</param>
        /// <param name="strKey">key</param>
        /// <returns></returns>
        public static byte[] AESDecrypt(byte[] inputdata, byte[] iv, byte[] strKey)
        {
            SymmetricAlgorithm des = Aes.Create();
            des.Key = strKey;//Encoding.UTF8.GetBytes(strKey);//.Substring(0, 7)
            des.IV = iv;
            byte[] decryptBytes = new byte[inputdata.Length];
            using (MemoryStream ms = new MemoryStream(inputdata))
            {
                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    cs.Read(decryptBytes, 0, decryptBytes.Length);
                    //cs.Close();
                    //ms.Close();
                }
            }
            return decryptBytes;
        }

        #endregion
    }
}
