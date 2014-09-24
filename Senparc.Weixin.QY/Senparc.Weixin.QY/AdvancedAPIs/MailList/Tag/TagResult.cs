using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.QY.AdvancedAPIs
{
    /// <summary>
    /// 创建标签返回结果
    /// </summary>
    public class CreateTagResult : WxJsonResult
    {
        public int tagid { get; set; }//标签id 
    }

    /// <summary>
    /// 获取标签成员返回结果
    /// </summary>
    public class GetTagMemberResult : WxJsonResult
    {
        public List<Tag_UserList> userlist { get; set; }//成员列表
    }

    public class Tag_UserList
    {
        public string userid { get; set; }//员工UserID
        public string name { get; set; }//成员名称
    }

    /// <summary>
    /// 添加标签成员返回结果
    /// a)正确时返回{"errcode": 0,"errmsg": "ok"}
    /// b)若部分userid非法，则返回{"errcode": 0,"errmsg": "invalid userlist failed","invalidlist"："usr1|usr2|usr"}
    /// c)当包含userid全部非法时返回{"errcode": 40070,"errmsg": "all list invalid "}
    /// </summary>
    public class AddTagMemberResult : WxJsonResult
    {
        public string invalidlist { get; set; }
    }

    /// <summary>
    /// 添加标签成员返回结果
    /// a)正确时返回{"errcode": 0,"errmsg": "ok"}
    /// b)若部分userid非法，则返回{"errcode": 0,"errmsg": "invalid userlist failed","invalidlist"："usr1|usr2|usr"}
    /// c)当包含userid全部非法时返回{"errcode": 40070,"errmsg": "all list invalid "}
    /// </summary>
    public class DelTagMemberResult : WxJsonResult
    {
        public string invalidlist { get; set; }
    }
}
