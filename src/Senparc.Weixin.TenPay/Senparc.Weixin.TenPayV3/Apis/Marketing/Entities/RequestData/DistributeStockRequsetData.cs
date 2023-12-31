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
  
    文件名：DistributeStockRequsetData.cs
    文件功能描述：发放代金券批次请求数据
    
    
    创建标识：Senparc - 20210831
    
----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.Marketing
{
    /// <summary>
    /// 激活代金券批次API请求数据
    /// <para><see href="https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_1_2.shtml">更多详细请参考微信支付官方文档</see></para>
    /// </summary>
    public class DistributeStockRequsetData
    {
        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="stock_id">批次号 微信为每个批次分配的唯一id</param>
        /// <param name="out_request_no">商户单据号 商户此次发放凭据号</param>
        /// <param name="appid">公众账号ID</param>
        /// <param name="stock_creator_mchid">创建批次的商户号 接口传入的批次号需由stock_creator_mchid所创建</param>
        public DistributeStockRequsetData(string stock_id, string out_request_no, string appid, string stock_creator_mchid)
        {
            this.stock_id = stock_id;
            this.out_request_no = out_request_no;
            this.appid = appid;
            this.stock_creator_mchid = stock_creator_mchid;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public DistributeStockRequsetData()
        {
        }

        /// <summary>
        /// 批次号
        /// <para>微信为每个批次分配的唯一id。<para>
        /// <para>校验规则：必须为代金券（全场券或单品券）批次号，不支持立减与折扣。<para>
        /// </summary>
        public string stock_id { get; set; }

        /// <summary>
        /// 商户单据号
        /// <para>商户此次发放凭据号（格式：商户id+日期+流水号），可包含英文字母，数字，|，_，*，-等内容，不允许出现其他不合法符号，商户侧需保持唯一性。</para>
        /// </summary>
        public string out_request_no { get; set; }

        /// <summary>
        /// 公众账号ID
        /// <para>微信为发券方商户分配的公众账号ID，接口传入的所有appid应该为公众号的appid或者小程序的appid（在mp.weixin.qq.com申请的）或APP的appid（在open.weixin.qq.com申请的）</para>
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 创建批次的商户号
        /// <para>校验规则：接口传入的批次号需由stock_creator_mchid所创建。</para>
        /// </summary>
        public string stock_creator_mchid { get; set; }
    }
}
