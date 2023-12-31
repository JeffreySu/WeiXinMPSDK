/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：GetFollowUserListResult.cs
    文件功能描述：获取配置了客户联系功能的成员列表 返回结果
     
    
    创建标识：Senparc - 20210504

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.External
{
    /// <summary>
    /// 获取配置了客户联系功能的成员列表 返回结果
    /// </summary>
    public class GetFollowUserListResult: WorkJsonResult
    {
        /// <summary>
        /// 配置了客户联系功能的成员userid列表
        /// </summary>
        public string[] follow_user { get; set; }
    }
}
