/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：CodeResultJson.cs
    文件功能描述：Code相关接口返回结果
    
    
    创建标识：Senparc - 20150907
----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.Card
{
    /// <summary>
    /// 查询导入code数目返回结果
    /// </summary>
    public class GetDepositCountResultJson : WxJsonResult
    {
        /// <summary>
        /// 货架已经成功存入的code数目。
        /// </summary>
        public int count { get; set; }
    }

    /// <summary>
    /// 核查code返回结果
    /// </summary>
    public class CheckCodeResultJson : WxJsonResult
    {
        /// <summary>
        /// 已经成功存入的code。
        /// </summary>
        public List<string> exist_code { get; set; }
        /// <summary>
        /// 没有存入的code。
        /// </summary>
        public List<string> not_exist_code { get; set; }
    }
}
