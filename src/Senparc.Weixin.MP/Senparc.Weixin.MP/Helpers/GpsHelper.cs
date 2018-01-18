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

/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：GpsHelper.cs
    文件功能描述：处理坐标距离
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using System;

namespace Senparc.Weixin.MP.Helpers
{
    /// <summary>
    /// GPS 帮助类
    /// </summary>
    public class GpsHelper
    {
        /// <summary>
        /// 计算两点GPS坐标的距离（单位：米）
        /// </summary>
        /// <param name="n1">第一点的纬度坐标</param>
        /// <param name="e1">第一点的经度坐标</param>
        /// <param name="n2">第二点的纬度坐标</param>
        /// <param name="e2">第二点的经度坐标</param>
        /// <returns></returns>
        public static double Distance(double n1, double e1, double n2, double e2)
        {
            double jl_jd = 102834.74258026089786013677476285;//每经度单位米;
            double jl_wd = 111712.69150641055729984301412873;//每纬度单位米; 
            double b = Math.Abs((e1 - e2) * jl_jd);
            double a = Math.Abs((n1 - n2) * jl_wd);
            return Math.Sqrt((a * a + b * b));
        }

        /// <summary>
        /// 获取维度差
        /// </summary>
        /// <param name="km">千米</param>
        /// <returns></returns>
        public static double GetLatitudeDifference(double km)
        {
            return km * 1 / 111;
        }

        /// <summary>
        /// 获取经度差
        /// </summary>
        /// <param name="km">千米</param>
        /// <returns></returns>
        public static double GetLongitudeDifference(double km)
        {
            return km * 1 / 110;
        }
    }
}
