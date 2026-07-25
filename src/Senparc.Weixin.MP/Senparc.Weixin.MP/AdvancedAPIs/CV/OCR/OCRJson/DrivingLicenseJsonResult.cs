#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2026 Senparc

    文件名：DrivingLicenseJsonResult.cs
    文件功能描述：DrivingLicenseJsonResult 强类型数据模型


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v16.25.1 补齐公众号 openApi、统计、图像、医疗、非税和一物一码官方接口

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.CV.OCR
{
    /// <summary>
    /// 驾驶证 OCR 识别结果。
    /// </summary>
    /// <remarks>
    /// 微信官方文档：<see href="https://developers.weixin.qq.com/doc/service/api/openpoc/ocr/api_drivinglicenseocr"/>。
    /// </remarks>
    public class DrivingLicenseJsonResult : WxJsonResult
    {
        /// <summary>
        /// 证号。
        /// </summary>
        public string id_num { get; set; }

        /// <summary>
        /// 姓名。
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 性别。
        /// </summary>
        public string sex { get; set; }

        /// <summary>
        /// 国籍。
        /// </summary>
        public string nationality { get; set; }

        /// <summary>
        /// 住址。
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// 出生日期。
        /// </summary>
        public string birth_date { get; set; }

        /// <summary>
        /// 初次领证日期。
        /// </summary>
        public string issue_date { get; set; }

        /// <summary>
        /// 准驾车型。
        /// </summary>
        public string car_class { get; set; }

        /// <summary>
        /// 有效期限起始日期。
        /// </summary>
        public string valid_from { get; set; }

        /// <summary>
        /// 有效期限截止日期。
        /// </summary>
        public string valid_to { get; set; }

        /// <summary>
        /// 发证机关印章文字。
        /// </summary>
        public string official_seal { get; set; }
    }
}
