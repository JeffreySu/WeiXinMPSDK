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

using System.Net;
using System.Text;

namespace Senparc.Weixin.TenPay.V2
{
    /// <summary>
    /// 生成Native支付方式使用的二维码
    /// </summary>
    public class QRCode
    {
        /// <summary>
        /// url编码，添加空格转成%20
        /// </summary>
        /// <param name="con">需要生成二维码的Url</param>
        /// <returns></returns>
        public static string UrlEncode1(string con)
        {
            string UrlEncode = "";

#if NET35 || NET40 || NET45 || NET461
            UrlEncode = System.Web.HttpUtility.UrlEncode(con, Encoding.UTF8);
#else
            UrlEncode = WebUtility.UrlEncode(con);
#endif
            UrlEncode = UrlEncode.Replace("+", "%20");
            return UrlEncode;
        }
        //' * google api 二维码生成【QRcode可以存储最多4296个字母数字类型的任意文本，具体可以查看二维码数据格式】
        //' * @param string $chl 二维码包含的信息，可以是数字、字符、二进制信息、汉字。不能混合数据类型，数据必须经过UTF-8 URL-encoded.如果需要传递的信息超过2K个字节请使用POST方式
        //' * @param int $widhtHeight 生成二维码的尺寸设置
        //' * @param string $EC_level 可选纠错级别，QR码支持四个等级纠错，用来恢复丢失的、读错的、模糊的、数据。
        //' *                         L-默认：可以识别已损失的7%的数据
        //' *                         M-可以识别已损失15%的数据
        //' *                         Q-可以识别已损失25%的数据
        //' *                         H-可以识别已损失30%的数据
        //' * @param int $margin 生成的二维码离图片边框的距离
        public static string QRfromGoogle(string chl)
        {
            int widhtHeight = 300;
            string EC_level = "L";
            int margin = 0;
            string QRfromGoogle;
            chl = UrlEncode1(chl);
            QRfromGoogle = "http://chart.apis.google.com/chart?chs=" + widhtHeight + "x" + widhtHeight + "&cht=qr&chld=" + EC_level + "|" + margin + "&chl=" + chl;
            return QRfromGoogle;
        }
    }
}
