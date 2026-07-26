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

    文件名：AiCropJsonResult.cs
    文件功能描述：AiCropJsonResult 强类型数据模型


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
    /// 图片智能裁剪结果。
    /// </summary>
    public class AiCropJsonResult : WxJsonResult
    {
        /// <summary>
        /// 每个目标宽高比对应的裁剪区域。
        /// </summary>
        public List<AiCropResult> results { get; set; }

        /// <summary>
        /// 原图尺寸。
        /// </summary>
        public ImgSize img_size { get; set; }
    }

    /// <summary>
    /// 单个智能裁剪区域，坐标单位为像素。
    /// </summary>
    public class AiCropResult
    {
        /// <summary>
        /// 裁剪区域左边界。
        /// </summary>
        public int crop_left { get; set; }

        /// <summary>
        /// 裁剪区域上边界。
        /// </summary>
        public int crop_top { get; set; }

        /// <summary>
        /// 裁剪区域右边界。
        /// </summary>
        public int crop_right { get; set; }

        /// <summary>
        /// 裁剪区域下边界。
        /// </summary>
        public int crop_bottom { get; set; }
    }
}
