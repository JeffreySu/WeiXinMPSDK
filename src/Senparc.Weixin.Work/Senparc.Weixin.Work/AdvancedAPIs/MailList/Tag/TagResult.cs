/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：DepartmentResult.cs
    文件功能描述：标签接口返回结果
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口
    
    修改标识：Senparc - 20150409
    修改描述：整理接口

    修改标识：Senparc - 20171017
    修改描述：v1.2.0 部门id改为long类型

    修改标识：Senparc - 20171220
    修改描述：v1.2.10 修改 AddTagMemberResult.invalidparty 为 long 类型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.MailList
{
    /// <summary>
    /// 创建标签返回结果
    /// </summary>
    public class CreateTagResult : WorkJsonResult
    {
        /// <summary>
        /// 标签id
        /// </summary>
        public int tagid { get; set; }
    }

    /// <summary>
    /// 获取标签成员返回结果
    /// </summary>
    public class GetTagMemberResult : WorkJsonResult
    {
        /// <summary>
        /// 成员列表
        /// </summary>
        public List<Tag_UserList> userlist { get; set; }
        /// <summary>
        /// 部门列表
        /// </summary>
        public long[] partylist { get; set; }
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
    /// b)若部分userid非法，则返回{"errcode": 0,"errmsg": "invalid userlist failed","invalidlist"："usr1|usr2|usr","invalidparty"：[2,4]}
    /// c)当包含userid全部非法时返回{"errcode": 40070,"errmsg": "all list invalid "}
    /// 其中错误消息视具体出错情况而定，分别为：
    /// invalid userlist and partylist faild
    /// invalid userlist faild
    /// invalid partylist faild
    /// </summary>
    public class AddTagMemberResult : WorkJsonResult
    {
        public string invalidlist { get; set; }
        public long[] invalidparty { get; set; }
    }

    /// <summary>
    /// 添加标签成员返回结果
    /// a)正确时返回{"errcode": 0,"errmsg": "ok"}
    /// b)若部分userid非法，则返回{"errcode": 0,"errmsg": "invalid userlist failed","invalidlist"："usr1|usr2|usr","invalidparty": [2,4]}
    /// c)当包含userid全部非法时返回{"errcode": 40070,"errmsg": "all list invalid "}
    /// invalid userlist and partylist faild
    /// invalid userlist faild
    /// invalid partylist faild
    /// </summary>
    public class DelTagMemberResult : WorkJsonResult
    {
        public string invalidlist { get; set; }
        public long[] invalidparty { get; set; }
    }

    /// <summary>
    /// 获取标签列表返回结果
    /// </summary>
    public class GetTagListResult : WorkJsonResult
    {
        public List<TagItem> taglist { get; set; }
    }

    public class TagItem
    {
        public string tagid { get; set; }
        public string tagname { get; set; }
    }
}
