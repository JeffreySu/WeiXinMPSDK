/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：KFResult.cs
    文件功能描述：客服返回结果
    
    
    创建标识：Senparc - 2015060309
----------------------------------------------------------------*/
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.QY.AdvancedAPIs.KF
{
    /// <summary>
    /// 客服返回结果
    /// </summary>
    public class GetKFListResult : QyJsonResult
    {
        public KF_Item @internal { get; set; }
        public KF_Item external { get; set; }
    }

    public class KF_Item
    {
        public string[] user { get; set; }
        public int[] party { get; set; }
        public int[] tag { get; set; }
    }
}
