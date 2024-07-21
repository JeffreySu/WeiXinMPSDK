using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.GM;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Encoders;
using System;
using System.Text;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace Senparc.Weixin.TenPayV3.Helpers
{
    public class GmHelper
    {
        /// <summary>
        /// 使用SM2椭圆曲线私钥密码算法的数字签名算法（SM2Sign with SM3）
        /// </summary>
        /// <param name="eCPrivateKeyParameters">圆曲线私钥参数</param>
        /// <param name="message">签名数据</param>
        /// <param name="userID">签名使用的ID，默认的ID值为1234567812345678</param>
        /// <param name="asn1Encoding">结果是否转换为RS_ASN1格式，默认值true</param>
        /// <returns></returns>
        public static byte[] SignSm3WithSm2(ECPrivateKeyParameters eCPrivateKeyParameters, string message, string userID = "1234567812345678", bool asn1Encoding = true)
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            ISigner signer = SignerUtilities.GetSigner("SM3withSM2");
            signer.Init(true, new ParametersWithID(eCPrivateKeyParameters, Encoding.UTF8.GetBytes(userID)));
            signer.BlockUpdate(messageBytes, 0, messageBytes.Length);
            byte[] signBytes = signer.GenerateSignature();
            byte[] rsDer;

            //默认返回的是ASN.1编码
            if (!asn1Encoding)
            {
                rsDer = ConvertAsn1ToRs(signBytes);
                return rsDer;
            }

            return signBytes;
        }

        /// <summary>
        /// 使用SM2椭圆曲线公钥密码算法的签名验证算法（签名格式RS_ASN1）
        /// </summary>
        /// <param name="eCPublicKeyParameters">圆曲线公钥参数</param>
        /// <param name="message">签名数据</param>
        /// <param name="signStr">签名字符串</param>
        /// <param name="userID">签名使用的ID，默认的ID值为1234567812345678</param>
        /// <param name="asn1Encoding">结果是否转换为RS_ASN1格式。默认值true</param>
        /// <returns></returns>
        public static bool VerifySm3WithSm2(ECPublicKeyParameters eCPublicKeyParameters, string message, string signStr, string userID = "1234567812345678", bool asn1Encoding = true)
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            ISigner signer = SignerUtilities.GetSigner("SM3withSM2");
            signer.Init(false, new ParametersWithID(eCPublicKeyParameters, Encoding.UTF8.GetBytes(userID)));
            signer.BlockUpdate(messageBytes, 0, messageBytes.Length);
            byte[] signBytes = Convert.FromBase64String(signStr);

            if (!asn1Encoding)
            {
                signBytes = ConvertRsToAsn1(signBytes);
            }

            return signer.VerifySignature(signBytes);
        }

        /// <summary>
        /// 国标Sm2加密
        /// </summary>
        /// <param name="eCPublicKeyParameters">圆曲线公钥参数</param>
        /// <param name="message">加密数据</param>
        /// <param name="asn1Encoding">加密结果是否为 ASN.1 编码的形式。默认值true</param>
        /// <returns></returns>
        public static string Sm2Encrypt(ECPublicKeyParameters eCPublicKeyParameters, string message, bool asn1Encoding = true)
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            SM2Engine sm2Engine = new SM2Engine(Mode.C1C3C2);
            sm2Engine.Init(true, new ParametersWithRandom(eCPublicKeyParameters));
            var b = sm2Engine.ProcessBlock(messageBytes, 0, messageBytes.Length);
            if (asn1Encoding)
            {
                b = ConvertC1C3C2ToAsn1(b);
            }
            return Convert.ToBase64String(b);
        }

        /// <summary>
        /// 国标Sm2解密
        /// </summary>
        /// <param name="eCPrivateKeyParameters">圆曲线私钥参数</param>
        /// <param name="message">加密数据</param>
        /// <param name="asn1Encoding">加密结果是否为 ASN.1 编码的形式。默认值true</param>
        /// <returns></returns>
        public static string Sm2Decrypt(ECPrivateKeyParameters eCPrivateKeyParameters, string message, bool asn1Encoding = true)
        {
            byte[] messageBytes = Convert.FromBase64String(message);

            if (asn1Encoding)
            {
                messageBytes = ConvertAsn1ToC1C3C2(messageBytes);
            }

            SM2Engine sm2Engine = new SM2Engine(Mode.C1C3C2);
            sm2Engine.Init(false, eCPrivateKeyParameters);
            var b = sm2Engine.ProcessBlock(messageBytes, 0, messageBytes.Length);

            return Encoding.UTF8.GetString(b);
        }

        /// <summary>
        /// 国标Sm4GCM加密
        /// </summary>
        /// <param name="apiV3Key">APIv3密钥</param>
        /// <param name="nonce">随机字符串初始化向量</param>
        /// <param name="plaintext">明文</param>
        /// <returns>密文</returns>
        public static string Sm4EncryptGCM(string apiV3Key, string nonce, string associatedData, string plaintext)
        {
            byte[] plaintextBytes = Encoding.UTF8.GetBytes(plaintext);
            var associatedDataBytes = associatedData is not null ? Encoding.UTF8.GetBytes(associatedData) : null;
            var gcmBlockCipher = CipherUtilities.GetCipher("SM4/GCM/NoPadding");
            var keyParam = new KeyParameter(Arrays.CopyOf(Sm3(apiV3Key), 16));
            var parameters = new AeadParameters(keyParam, 128, Encoding.UTF8.GetBytes(nonce), associatedDataBytes);

            gcmBlockCipher.Init(true, parameters);
            var cipherText = new byte[gcmBlockCipher.GetOutputSize(plaintextBytes.Length)];
            var len = gcmBlockCipher.ProcessBytes(plaintextBytes, 0, plaintextBytes.Length, cipherText, 0);
            gcmBlockCipher.DoFinal(cipherText, len);

            return Convert.ToBase64String(cipherText);
        }

        /// <summary>
        /// 国标Sm4GCM解密
        /// </summary>
        /// <param name="apiV3Key">APIv3密钥</param>
        /// <param name="nonce">随机字符串初始化向量</param>
        /// <param name="associatedData">密文</param>
        /// <returns>UTF-8编码的明文</returns>
        public static string Sm4DecryptGCM(string apiV3Key, string nonce, string associatedData, string cipherText)
        {
            var cipherBytes = Convert.FromBase64String(cipherText);
            var gcmBlockCipher = CipherUtilities.GetCipher("SM4/GCM/NoPadding");
            var keyParam = new KeyParameter(Arrays.CopyOf(Sm3(apiV3Key), 16));
            var associatedDataBytes = associatedData is not null ? Encoding.UTF8.GetBytes(associatedData) : null;

            ICipherParameters cipherParams = new AeadParameters(keyParam, 128, Encoding.UTF8.GetBytes(nonce), associatedDataBytes);

            gcmBlockCipher.Init(false, cipherParams);
            byte[] plainBytes = new byte[gcmBlockCipher.GetOutputSize(cipherBytes.Length)];
            int len = gcmBlockCipher.ProcessBytes(cipherBytes, 0, cipherBytes.Length, plainBytes, 0);
            gcmBlockCipher.DoFinal(plainBytes, len);

            return Encoding.UTF8.GetString(plainBytes);
        }

        public static byte[] Sm3(string data)
        {
            byte[] bytes= Encoding.UTF8.GetBytes(data);
            SM3Digest sm3Digest = new SM3Digest();
            sm3Digest.BlockUpdate(bytes, 0, bytes.Length);
            return DigestUtilities.DoFinal(sm3Digest);
        }

        private static byte[] ConvertAsn1ToRs(byte[] asn1)
        {
            Asn1Sequence sequence = Asn1Sequence.GetInstance(asn1);
            byte[] r = BigIntToFixexLengthBytes(DerInteger.GetInstance(sequence[0]).Value);
            byte[] s = BigIntToFixexLengthBytes(DerInteger.GetInstance(sequence[1]).Value);

            byte[] rsDer = new byte[32 * 2];
            Buffer.BlockCopy(r, 0, rsDer, 0, r.Length);
            Buffer.BlockCopy(s, 0, rsDer, 32, s.Length);
            return rsDer;
        }

        private static byte[] ConvertRsToAsn1(byte[] rs)
        {
            BigInteger r = new BigInteger(1, Arrays.CopyOfRange(rs, 0, 32));
            BigInteger s = new BigInteger(1, Arrays.CopyOfRange(rs, 32, 32 * 2));

            Asn1EncodableVector vector = new Asn1EncodableVector
            {
                new DerInteger(r),
                new DerInteger(s)
            };

            DerSequence sequence = new DerSequence(vector);
            return sequence.GetEncoded("DER");
        }

        private static byte[] BigIntToFixexLengthBytes(BigInteger rOrS)
        {
            byte[] byteArray = rOrS.ToByteArray();
            if (byteArray.Length == 32)
                return byteArray;
            if (byteArray.Length == 33 && byteArray[0] == 0)
                return Arrays.CopyOfRange(byteArray, 1, 33);
            if (byteArray.Length >= 32)
                throw new ArgumentException("err rs: " + Hex.ToHexString(byteArray));
            byte[] fixexLengthBytes = new byte[32];
            Arrays.Fill(fixexLengthBytes, 0);
            Buffer.BlockCopy(byteArray, 0, fixexLengthBytes, 32 - byteArray.Length, byteArray.Length);
            return fixexLengthBytes;
        }

        private static byte[] ConvertC1C3C2ToAsn1(byte[] c1c3c2)
        {
            byte[] c1 = Arrays.CopyOfRange(c1c3c2, 0, 65);
            byte[] c3 = Arrays.CopyOfRange(c1c3c2, 65, 65 + 32);
            byte[] c2 = Arrays.CopyOfRange(c1c3c2, 65 + 32, c1c3c2.Length);
            byte[] c1X = Arrays.CopyOfRange(c1, 1, 33);
            byte[] c1Y = Arrays.CopyOfRange(c1, 33, 65);

            BigInteger r = new BigInteger(1, c1X);
            BigInteger s = new BigInteger(1, c1Y);

            DerInteger x = new DerInteger(r);
            DerInteger y = new DerInteger(s);
            DerOctetString derDig = new DerOctetString(c3);
            DerOctetString derEnc = new DerOctetString(c2);

            Asn1EncodableVector vector = new Asn1EncodableVector
            {
                x,
                y,
                derDig,
                derEnc
            };

            DerSequence sequence = new DerSequence(vector);
            return sequence.GetEncoded("DER");
        }

        private static byte[] ConvertAsn1ToC1C3C2(byte[] asn1)
        {
            Asn1Sequence sequence = Asn1Sequence.GetInstance(asn1);

            BigInteger x = DerInteger.GetInstance(sequence[0]).Value;
            BigInteger y = DerInteger.GetInstance(sequence[1]).Value;

            byte[] c3 = Asn1OctetString.GetInstance(sequence[2]).GetOctets();
            byte[] c2 = Asn1OctetString.GetInstance(sequence[3]).GetOctets();
            X9ECParameters x9ECParameters = GMNamedCurves.GetByName("SM2P256v1");
            ECPoint c1Point = x9ECParameters.Curve.CreatePoint(x, y);
            byte[] c1 = c1Point.GetEncoded(false);

            return Arrays.ConcatenateAll(c1, c3, c2);
        }
    }
}
