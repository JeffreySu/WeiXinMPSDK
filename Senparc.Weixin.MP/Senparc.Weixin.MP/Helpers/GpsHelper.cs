using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.Helpers
{
    public class GpsHelper
    {
        /// <summary>
        ///计算两点GPS坐标的距离（单位：米）
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
        /// 获取精度差
        /// </summary>
        /// <param name="km">千米</param>
        /// <returns></returns>
        public static double GetLongitudeDifference(double km)
        {
            return km * 1 / 110;
        }
    }
}
