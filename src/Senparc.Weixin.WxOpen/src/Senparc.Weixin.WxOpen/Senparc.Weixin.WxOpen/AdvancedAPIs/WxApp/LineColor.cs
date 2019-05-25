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

/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
  
    文件名：LineColor.cs
    文件功能描述：小程序二维码线条颜色（RGB颜色）
    
    
    创建标识：Senparc - 20170302

    修改标识：Senparc - 20180728
    修改描述：完善构造函数

----------------------------------------------------------------*/


namespace Senparc.Weixin.WxOpen.AdvancedAPIs.WxApp
{
    /// <summary>
    /// 小程序二维码线条颜色（RGB颜色）
    /// </summary>
    public class LineColor
    {
        /// <summary>
        /// 红色
        /// </summary>
        public int r { get; set; }
        /// <summary>
        /// 绿色
        /// </summary>
        public int g { get; set; }
        /// <summary>
        /// 蓝色
        /// </summary>
        public int b { get; set; }


        public LineColor() { }


        /// <summary>
        /// LineColor 构造函数
        /// </summary>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        public LineColor(int red, int green, int blue)
        {
            r = red;
            g = green;
            b = blue;
        }
    }
}
