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
  
    文件名：SetBusifavorCouponCodesReturnJson.cs
    文件功能描述：设置商家券的Code码返回Json类
    
    
    创建标识：Senparc - 20210907
    
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.TenPayV3.Apis.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.Marketing
{
    /// <summary>
    /// 设置商家券的Code码返回Json类
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_2_6.shtml </para>
    /// </summary>
    public class SetBusifavorCouponCodesReturnJson : ReturnJsonBase
    {

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="stock_id">批次号  <para>微信为每个商家券批次分配的唯一ID。</para><para>示例值：98065001</para></param>
        /// <param name="total_count">去重后上传code总数  <para>本次上传操作，去重后实际上传的code数目。</para><para>示例值：500</para></param>
        /// <param name="success_count">上传成功code个数  <para>本次上传操作上传成功个数。</para><para>示例值：20</para></param>
        /// <param name="success_codes">上传成功的code列表  <para>本次新增上传成功的code信息。特殊规则：单个券code长度为【1，32】，条目个数限制为【1，200】。</para><para>示例值：MMAA12345</para><para>可为null</para></param>
        /// <param name="success_time">上传成功时间  <para>上传操作完成时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日13点29分35秒。</para><para>示例值：2015-05-20T13:29:35+08:00</para></param>
        /// <param name="fail_count">上传失败code个数  <para>本次上传操作上传失败的code数。</para><para>示例值：10</para><para>可为null</para></param>
        /// <param name="fail_codes">上传失败的code及原因 <para>本次导入失败的code信息，请参照错误信息，修改后重试。</para><para>可为null</para></param>
        /// <param name="exist_codes">已存在的code列表  <para>历史已存在的code列表，本次不会重复导入。特殊规则：单个券code长度为【1，32】，条目个数限制为【1，200】。</para><para>示例值：ABCD2345</para><para>可为null</para></param>
        /// <param name="duplicate_codes">本次请求中重复的code列表  <para>本次重复导入的code会被自动过滤，仅保留一个做导入，如满足要求则成功；如不满足要求，则失败；请参照报错提示修改重试。特殊规则：单个券code长度为【1，32】，条目个数限制为【1，200】。</para><para>示例值：AACC2345</para><para>可为null</para></param>
        public SetBusifavorCouponCodesReturnJson(string stock_id, ulong total_count, ulong success_count, string[] success_codes, string success_time, ulong fail_count, Fail_Codes[] fail_codes, string[] exist_codes, string[] duplicate_codes)
        {
            this.stock_id = stock_id;
            this.total_count = total_count;
            this.success_count = success_count;
            this.success_codes = success_codes;
            this.success_time = success_time;
            this.fail_count = fail_count;
            this.fail_codes = fail_codes;
            this.exist_codes = exist_codes;
            this.duplicate_codes = duplicate_codes;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public SetBusifavorCouponCodesReturnJson()
        {
        }

        /// <summary>
        /// 批次号 
        /// <para>微信为每个商家券批次分配的唯一ID。 </para>
        /// <para>示例值：98065001 </para>
        /// </summary>
        public string stock_id { get; set; }

        /// <summary>
        /// 去重后上传code总数 
        /// <para>本次上传操作，去重后实际上传的code数目。 </para>
        /// <para>示例值：500 </para>
        /// </summary>
        public ulong total_count { get; set; }

        /// <summary>
        /// 上传成功code个数 
        /// <para>本次上传操作上传成功个数。 </para>
        /// <para>示例值：20 </para>
        /// </summary>
        public ulong success_count { get; set; }

        /// <summary>
        /// 上传成功的code列表 
        /// <para>本次新增上传成功的code信息。 特殊规则：单个券code长度为【1，32】，条目个数限制为【1，200】。</para>
        /// <para>示例值：MMAA12345 </para>
        /// <para>可为null</para>
        /// </summary>
        public string[] success_codes { get; set; }

        /// <summary>
        /// 上传成功时间 
        /// <para>上传操作完成时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日 13点29分35秒。</para>
        /// <para>示例值：2015-05-20T13:29:35+08:00 </para>
        /// </summary>
        public string success_time { get; set; }

        /// <summary>
        /// 上传失败code个数 
        /// <para>本次上传操作上传失败的code数。 </para>
        /// <para>示例值：10 </para>
        /// <para>可为null</para>
        /// </summary>
        public ulong fail_count { get; set; }

        /// <summary>
        /// 上传失败的code及原因
        /// <para>本次导入失败的code信息，请参照错误信息，修改后重试。 </para>
        /// <para>可为null</para>
        /// </summary>
        public Fail_Codes[] fail_codes { get; set; }

        /// <summary>
        /// 已存在的code列表 
        /// <para>历史已存在的code列表，本次不会重复导入。 特殊规则：单个券code长度为【1，32】，条目个数限制为【1，200】。</para>
        /// <para>示例值：ABCD2345 </para>
        /// <para>可为null</para>
        /// </summary>
        public string[] exist_codes { get; set; }

        /// <summary>
        /// 本次请求中重复的code列表 
        /// <para>本次重复导入的code会被自动过滤，仅保留一个做导入，如满足要求则成功；如不满足要求，则失败；请参照报错提示修改重试。 特殊规则：单个券code长度为【1，32】，条目个数限制为【1，200】。</para>
        /// <para>示例值：AACC2345 </para>
        /// <para>可为null</para>
        /// </summary>
        public string[] duplicate_codes { get; set; }

        #region 子数据类型
        public class Fail_Codes
        {

            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="coupon_code">上传失败的券code  <para>商户通过API上传的券code。</para><para>示例值：ABCD23456</para></param>
            /// <param name="code">上传失败错误码  <para>对应券code上传失败的错误码。</para><para>示例值：LENGTH_LIMIT</para></param>
            /// <param name="message">上传失败错误信息  <para>上传失败的错误信息描述。</para><para>示例值：长度超过最大值32位</para></param>
            public Fail_Codes(string coupon_code, string code, string message)
            {
                this.coupon_code = coupon_code;
                this.code = code;
                this.message = message;
            }

            /// <summary>
            /// 无参构造函数
            /// </summary>
            public Fail_Codes()
            {
            }

            /// <summary>
            /// 上传失败的券code 
            /// <para>商户通过API上传的券code。 </para>
            /// <para>示例值：ABCD23456 </para>
            /// </summary>
            public string coupon_code { get; set; }

            /// <summary>
            /// 上传失败错误码 
            /// <para>对应券code上传失败的错误码。 </para>
            /// <para>示例值：LENGTH_LIMIT </para>
            /// </summary>
            public string code { get; set; }

            /// <summary>
            /// 上传失败错误信息 
            /// <para>上传失败的错误信息描述。 </para>
            /// <para>示例值：长度超过最大值32位 </para>
            /// </summary>
            public string message { get; set; }

        }


        #endregion
    }


}
