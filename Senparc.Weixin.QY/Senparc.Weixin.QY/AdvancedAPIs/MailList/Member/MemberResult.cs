﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.QY.AdvancedAPIs
{
    public class GetMemberResult : WxJsonResult
    {
        public string userid { get; set; }//员工UserID 
        public string name { get; set; }//成员名称
        public int[] department { get; set; }//成员所属部门id列表
        public string position { get; set; }//职位信息 
        public string mobile { get; set; }//手机号码 

        //最新接口去除了以下两个属性
        //public int gender { get; set; }//性别。gender=0表示男，=1表示女 
        //public string tel { get; set; }//办公电话

        public string email { get; set; }//邮箱
        public string weixinid { get; set; }//微信号 
        public string avatar { get; set; }//头像url。注：小图将url最后的"/0"改成"/64"
        public int status { get; set; }//关注状态: 1=已关注，2=已冻结，4=未关注 
        public Extattr extattr { get; set; }
    }

    public class GetDepartmentMemberResult : WxJsonResult
    {
        public List<UserList_Simple> userlist { get; set; }//成员列表
    }

    public class UserList_Simple
    {
        public string userid { get; set; }//员工UserID
        public string name { get; set; }//成员名称
    }

    /// <summary>
    /// 扩展属性
    /// </summary>
    public class Extattr
    {
        public List<Attr> attrs { get; set; }
    }

    public class Attr
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    /// <summary>
    /// 获取部门成员(详情)返回结果
    /// </summary>
    public class GetDepartmentMemberInfoResult : WxJsonResult
    {
        public List<GetMemberResult> userlist { get; set; }//成员列表
    }
}
