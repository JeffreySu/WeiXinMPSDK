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

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 添加邮费模板返回结果
    /// </summary>
    public class AddExpressResult : WxJsonResult
    {
        /// <summary>
        /// 邮费模板ID
        /// </summary>
        public long template_id { get; set; }
    }

    /// <summary>
    /// 获取指定ID的邮费模板返回结果
    /// </summary>
    public class GetByIdExpressResult : WxJsonResult
    {
        public Template_Info template_info { get; set; }
    }

    public class Template_Info
    {
        /// <summary>
        /// 邮费模板ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 邮费模板名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 支付方式(0-买家承担运费, 1-卖家承担运费)
        /// </summary>
        public int Assumer { get; set; }
        /// <summary>
        /// 计费单位(0-按件计费, 1-按重量计费, 2-按体积计费，目前只支持按件计费，默认为0)
        /// </summary>
        public int Valuation { get; set; }
        /// <summary>
        /// 具体运费计算
        /// </summary>
        public List<TopFeeItem> TopFee { get; set; }
    }

    /// <summary>
    /// 获取所有邮费模板
    /// </summary>
    public class GetAllExpressResult : WxJsonResult
    {
        /// <summary>
        /// 所有邮费模板集合
        /// </summary>
        public List<Template_Info> templates_info { get; set; } 
    }
}

