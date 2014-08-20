using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
namespace  Senparc.Weixin.MP.WeixinPayLib
{
    /// <summary>
    /// 密码类
    /// </summary>
    public static class CipherHelper
    {
        public static string key = CreateRandom();
        public static string vkey = CreateRandom();

        /// <summary>
        /// 对称加密采用的算法
        /// </summary>
        public enum SymmetricFormat
        {
            DES,
            TripleDES
        };

        /// <summary>
        /// 对字符串进行 Hash 加密 ,默认为MD5 32
        /// </summary>
        public static string Hash(this string inputString, HashFormat hashFormat = HashFormat.MD532)
        {
            HashAlgorithm algorithm = null;

            switch (hashFormat)
            {
                case HashFormat.MD516:
                    algorithm = MD5.Create();
                    break;
                case HashFormat.MD532:
                    algorithm = MD5.Create();
                    break;
                case HashFormat.SHA1:
                    algorithm = SHA1.Create();
                    break;
                case HashFormat.SHA256:
                    algorithm = SHA256.Create();
                    break;
                case HashFormat.SHA384:
                    algorithm = SHA384.Create();
                    break;
                case HashFormat.SHA512:
                    algorithm = SHA512.Create();
                    break;
            }

            algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));

            if (hashFormat == HashFormat.MD516)
            {
                return BitConverter.ToString(algorithm.Hash).Replace("-", "").Substring(8, 16).ToUpper();
            }
            else
            {
                return BitConverter.ToString(algorithm.Hash).Replace("-", "").ToUpper();
            }
        }

        /// <summary>
        /// 对字符串进行对称加密
        /// </summary>
        public static string SymmetricEncrypt(string inputString, SymmetricFormat symmetricFormat, string key, string iv)
        {
            SymmetricAlgorithm algorithm = null;

            switch (symmetricFormat)
            {
                case SymmetricFormat.DES:
                    algorithm = DES.Create();
                    break;
                case SymmetricFormat.TripleDES:
                    algorithm = TripleDES.Create();
                    break;
            }

            int keySize = algorithm.Key.Length;

            byte[] desString = Encoding.UTF8.GetBytes(inputString);

            byte[] desKey = Encoding.UTF8.GetBytes(key.Substring(0, keySize));

            byte[] desIV = Encoding.UTF8.GetBytes(iv.Substring(0, keySize));

            MemoryStream mStream = new MemoryStream();

            CryptoStream cStream = new CryptoStream(mStream, algorithm.CreateEncryptor(desKey, desIV), CryptoStreamMode.Write);

            cStream.Write(desString, 0, desString.Length);

            cStream.FlushFinalBlock();

            cStream.Close();

            return BitConverter.ToString(mStream.ToArray()).Replace("-", "").ToUpper();
        }

        /// <summary>
        /// 对字符串进行对称解密
        /// </summary>
        public static string SymmetricDecrypt(string inputString, SymmetricFormat symmetricFormat, string key, string iv)
        {
            SymmetricAlgorithm algorithm = null;

            switch (symmetricFormat)
            {
                case SymmetricFormat.DES:
                    algorithm = DES.Create();
                    break;
                case SymmetricFormat.TripleDES:
                    algorithm = TripleDES.Create();
                    break;
            }

            int keySize = algorithm.Key.Length;

            byte[] desString = new byte[inputString.Length / 2];

            for (int i = 0; i < inputString.Length; i += 2)
            {
                desString[i / 2] = byte.Parse(inputString.Substring(i, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
            }

            byte[] desKey = Encoding.UTF8.GetBytes(key.Substring(0, keySize));

            byte[] desIV = Encoding.UTF8.GetBytes(iv.Substring(0, keySize));

            MemoryStream mStream = new MemoryStream();

            CryptoStream cStream = new CryptoStream(mStream, algorithm.CreateDecryptor(desKey, desIV), CryptoStreamMode.Write);

            cStream.Write(desString, 0, desString.Length);

            cStream.FlushFinalBlock();

            cStream.Close();

            return Encoding.UTF8.GetString(mStream.ToArray());
        }

        /// <summary>
        /// 随机生成一个 8 位密码，包括大小写字母，数字和特殊符号。
        /// </summary>
        public static string CreateRandom()
        {
            string str = @"ABCDEFGHIJKLMNPQRSTUVWXYZabcdefghijklmnpqrstuvwxyz123456789#$%&";

            int len = 8;

            return CreateRandom(str, len);
        }

        /// <summary>
        /// 随机生成一个指定长度的密码，包括大小写字母，数字和特殊符号。
        /// </summary>
        public static string CreateRandom(int len)
        {
            string str = @"ABCDEFGHIJKLMNPQRSTUVWXYZabcdefghijklmnpqrstuvwxyz123456789#$%&";

            return CreateRandom(str, len);
        }

        /// <summary>
        /// 随机生成一个指定长度的密码，仅包括指定的字符。
        /// </summary>
        public static string CreateRandom(string str, int len)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentException("参数不能为空", "str");
            }

            if (len < 1)
            {
                throw new ArgumentException("参数不能小于 1", "len");
            }

            //将当前线程阻止 1 毫秒，以保证在同一方法中调用此方法产生的结果不同。
            Thread.Sleep(1);

            Random random = new Random();

            int strLen = str.Length;

            StringBuilder pass = new StringBuilder(len);

            for (int i = 0; i < len; i++)
            {
                pass.Append(str.Substring(random.Next(strLen), 1));
            }

            return pass.ToString();
        }
    }
    
    /// <summary>
    /// Hash 加密采用的算法
    /// </summary>
    public enum HashFormat
    {
        MD516,
        MD532,
        SHA1,
        SHA256,
        SHA384,
        SHA512
    };
}
