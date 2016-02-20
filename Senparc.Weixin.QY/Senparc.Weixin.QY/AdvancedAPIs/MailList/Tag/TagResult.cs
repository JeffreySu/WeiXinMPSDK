/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：DepartmentResult.cs
    文件功能描述：标签接口返回结果
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口
    
    修改标识：Senparc - 20150409
    修改描述：整理接口
----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.QY.AdvancedAPIs.MailList
{
    /// <summary>
    /// 创建标签返回结果
    /// </summary>
    public class CreateTagResult : QyJsonResult
    {
        /// <summary>
        /// 标签id
        /// </summary>
        public int tagid { get; set; }
    }

    /// <summary>
    /// 获取标签成员返回结果
    /// </summary>
    public class GetTagMemberResult : QyJsonResult
    {
        /// <summary>
        /// 成员列表
        /// </summary>
        public List<Tag_UserList> userlist { get; set; }
        /// <summary>
        /// 部门列表
        /// </summary>
        public int[] partylist { get; set; }
    }

    public class Tag_UserList
    {
        /// <summary>
        /// 员工UserID
        /// </summary>
        public string userid { get; set; }
        /// <summary>
        /// 成员名称
        /// </summary>
        public string name { get; set; }
    }

    /// <summary>
    /// 添加标签成员返回结果
    /// a)正确时返回{"errcode": 0,"errmsg": "ok"}
    /// b)若部分userid非法，则返回{"errcode": 0,"errmsg": "invalid userlist failed","invalidlist"："usr1|usr2|usr"}
    /// c)当包含userid全部非法时返回{"errcode": 40070,"errmsg": "all list invalid "}
    /// </summary>
    public class AddTagMemberResult : QyJsonResult
    {
        public string invalidlist { get; set; }
    }

    /// <summary>
    /// 添加标签成员返回结果
    /// a)正确时返回{"errcode": 0,"errmsg": "ok"}
    /// b)若部分userid非法，则返回{"errcode": 0,"errmsg": "invalid userlist failed","invalidlist"："usr1|usr2|usr"}
    /// c)当包含userid全部非法时返回{"errcode": 40070,"errmsg": "all list invalid "}
    /// </summary>
    public class DelTagMemberResult : QyJsonResult
    {
        public string invalidlist { get; set; }
    }

    /// <summary>
    /// 获取标签列表返回结果
    /// </summary>
    public class GetTagListResult : QyJsonResult
    {
        public List<TagItem> taglist { get; set; }
    }

    public class TagItem
    {
        public string tagid { get; set; }
        public string tagname { get; set; }
    }
}
