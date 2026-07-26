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

    文件名：QrCodeJsonResult.cs
    文件功能描述：QrCodeJsonResult 强类型数据模型


    创建标识：Senparc - 20190525

    修改标识：Senparc - 20260724
    修改描述：v16.25.1 补齐公众号 openApi、统计、图像、医疗、非税和一物一码官方接口

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.AdvancedAPIs.CV.OCR;

namespace Senparc.Weixin.MP.AdvancedAPIs.CV.Image
{
    /// <summary>
    /// 图片二维码/条码识别结果。
    /// </summary>
    public class QrCodeJsonResult : WxJsonResult
    {
        /// <summary>
        /// 图片中识别到的编码结果列表。
        /// </summary>
        public List<QrCodeResult> code_results { get; set; }

        /// <summary>
        /// 原图尺寸。
        /// </summary>
        public ImgSize img_size { get; set; }
    }

    /// <summary>
    /// 单个二维码、条码、DataMatrix 或 PDF417 识别结果。
    /// </summary>
    public class QrCodeResult
    {
        /// <summary>
        /// 编码类型名称。
        /// </summary>
        public string type_name { get; set; }

        /// <summary>
        /// 解码后的文本内容。
        /// </summary>
        public string data { get; set; }

        /// <summary>
        /// 编码在图片中的位置；部分条码或 PDF417 结果可能不返回此字段。
        /// </summary>
        public Pos pos { get; set; }
    }
}
