/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：RequestMessageEvent_Change_Contact.cs
    文件功能描述：事件之上报通讯录变更事件


    创建标识：pekrr1e - 20180503
    
    修改标识：pekrr1e - 20180503
    修改描述：整理接口 v1.4.1 增加“接收通讯录变更事件”

    修改标识：Senparc - 20181030
    修改描述：v3.1.16 fix bug：RequestMessageEvent_Change_Contact_User_Create.Department 属性类型错误，添加 DepartmentList 自动转成 long[]

    修改标识：Senparc - 20190214
    修改描述：v3.3.7 修复 IsLeader 参数大小写问题

----------------------------------------------------------------*/

using Senparc.CO2NET.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>
    /// 上报通讯录变更事件
    /// </summary>
    public interface IRequestMessageEvent_Change_Contact_Base : IRequestMessageEventBase
    {
        ContactChangeType ChangeType
        {
            get;
        }
    }
    public class RequestMessageEvent_Change_Contact_Base : RequestMessageEventBase, IRequestMessageEvent_Change_Contact_Base
    {
        public override Event Event
        {
            get { return Event.change_contact; }
        }
        public virtual ContactChangeType ChangeType
        {
            get { return ContactChangeType.create_party; }
        }
    }
    public class RequestMessageEvent_Change_Contact_User_Base : RequestMessageEvent_Change_Contact_Base
    {
        public override ContactChangeType ChangeType
        {
            get { return ContactChangeType.delete_user; }
        }
        /// <summary>
        /// 成员UserID/变更信息的成员UserID
        /// </summary>
        public string UserID { get; set; }
    }

    /// <summary>
    /// update_user 事件
    /// </summary>
    public class RequestMessageEvent_Change_Contact_User_Update : RequestMessageEvent_Change_Contact_User_Create
    {
        public override ContactChangeType ChangeType
        {
            get { return ContactChangeType.update_user; }
        }
        /// <summary>
        /// 新的UserID，变更时推送（userid由系统生成时可更改一次）
        /// </summary>
        public string NewUserID { get; set; }
    }

    /// <summary>
    /// create_user 事件
    /// </summary>
    public class RequestMessageEvent_Change_Contact_User_Create : RequestMessageEvent_Change_Contact_User_Base
    {
        public override ContactChangeType ChangeType
        {
            get { return ContactChangeType.create_user; }
        }
        /// <summary>
        /// 成员名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 成员所属部门id列表（格式：[1,2,3]）
        /// </summary>
        public string Department { get; set; }
        /// <summary>
        /// 从 Department 属性自动转成的数组
        /// </summary>
        public long[] DepartmentIdList
        {
            get
            {
                if (Department.IsNullOrEmpty())
                {
                    return new long[0];
                }
                return Department.Split(',').Select(z => long.Parse(z)).ToArray();
            }
        }

        /// <summary>
        /// 主部门
        /// </summary>
        public long MainDepartment { get; set; }

        /// <summary>
        /// 职位信息
        /// </summary>
        public string Position { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 性别。Gender=0表示男，=1表示女 
        /// </summary>
        public int Gender { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 企业邮箱，代开发自建应用需要管理员授权且成员oauth2授权获取；第三方仅通讯录应用可获取；对于非第三方创建的成员，第三方通讯录应用也不可获取；上游企业不可获取下游企业成员该字段
        /// </summary>
        public string BizMail { get; set; }
        /// <summary>
        /// 表示所在部门是否为部门负责人，0-否，1-是，顺序与Department字段的部门逐一对应。上游共享的应用不返回该字段。如：1,0,0
        /// </summary>
        public string IsLeaderInDept { get; set; }
        /// <summary>
        /// IsLeaderInDept 解析之后的参数
        /// </summary>
        public int[] IsLeaderInDeptList
        {
            get
            {
                if (IsLeaderInDept.IsNullOrEmpty())
                {
                    return new int[0];
                }
                return IsLeaderInDept.Split(',').Select(z => int.Parse(z)).ToArray();
            }
        }
        /// <summary>
        /// 直属上级UserID，最多5个。代开发的自建应用和上游共享的应用不返回该字段
        /// <para>如：lisi,wangwu</para>
        /// </summary>
        public string DirectLeader { get; set; }
        /// <summary>
        /// DirectLeader 解析之后的参数
        /// </summary>
        public string[] DirectLeaderList
        {
            get
            {
                if (DirectLeader.IsNullOrEmpty())
                {
                    return new string[0];
                }
                return DirectLeader.Split(',');
            }
        }

        /// <summary>
        /// 头像url。 注：如果要获取小图将url最后的”/0”改成”/100”即可。代开发自建应用需要管理员授权且成员oauth2授权获取；第三方仅通讯录应用可获取；对于非第三方创建的成员，第三方通讯录应用也不可获取；上游企业不可获取下游企业成员该字段
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// 激活状态: 1=已激活，2=已禁用，4=未激活 已激活代表已激活企业微信或已关注微信插件。未激活代表既未激活企业微信又未关注微信插件。
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 座机
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 地址。代开发自建应用需要管理员授权且成员oauth2授权获取；第三方仅通讯录应用可获取；对于非第三方创建的成员，第三方通讯录应用也不可获取；上游企业不可获取下游企业成员该字段
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 成员别名。上游共享的应用不返回该字段
        /// </summary>
        public string Alias { get; set; }
        /// <summary>
        /// 英文名，最新文档中已删除，请使用 Alias
        /// </summary>
        [Obsolete("最新文档中已删除，请使用 Alias。文档：https://developer.work.weixin.qq.com/document/path/90970")]
        public string EnglishName { get; set; }
        /// <summary>
        /// 扩展属性，变更时推送;代开发自建应用需要管理员授权才返回。上游共享的应用不返回该字段
        /// </summary>
        public List<Item> ExtAttr { get; set; }
    }

    /// <summary>
    /// 扩展属性项
    /// </summary>
    public class Item
    {
        /// <summary>
        /// 名称，如“爱好”“卡号”等
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 扩展属性类型: 0-本文 1-网页
        /// </summary>
        public int Type { get; set; }

        public Text Text { get; set; }

        public Web Web { get; set; }
    }

    /// <summary>
    /// 文本属性类型，扩展属性类型为0时填写
    /// </summary>
    public class Text
    {
        /// <summary>
        /// 文本属性内容
        /// </summary>
        public string Value { get; set; }
    }

    /// <summary>
    /// 网页类型属性，扩展属性类型为1时填写
    /// </summary>
    public class Web
    {
        /// <summary>
        /// 网页的展示标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 网页的url
        /// </summary>
        public string Url { get; set; }
    }


    public class RequestMessageEvent_Change_Contact_Party_Base : RequestMessageEvent_Change_Contact_Base
    {
        public override ContactChangeType ChangeType
        {
            get { return ContactChangeType.delete_party; }
        }
        /// <summary>
        /// 部门Id
        /// </summary>
        public long Id { get; set; }
    }
    public class RequestMessageEvent_Change_Contact_Party_Update : RequestMessageEvent_Change_Contact_Party_Base
    {
        public override ContactChangeType ChangeType
        {
            get { return ContactChangeType.update_party; }
        }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 父部门id
        /// </summary>
        public long ParentId { get; set; }
    }
    public class RequestMessageEvent_Change_Contact_Party_Create : RequestMessageEvent_Change_Contact_Party_Update
    {
        public override ContactChangeType ChangeType
        {
            get { return ContactChangeType.create_party; }
        }
        /// <summary>
        /// 部门排序
        /// </summary>
        public long Order { get; set; }
    }
    public class RequestMessageEvent_Change_Contact_Tag_Update : RequestMessageEvent_Change_Contact_Base
    {
        public override ContactChangeType ChangeType
        {
            get { return ContactChangeType.update_tag; }
        }
        /// <summary>
        /// 标签Id
        /// </summary>
        public long TagId { get; set; }
        /// <summary>
        /// 标签中新增的成员userid列表，用逗号分隔
        /// </summary>
        public string AddUserItems { get; set; }
        /// <summary>
        /// 标签中删除的成员userid列表，用逗号分隔
        /// </summary>
        public string DelUserItems { get; set; }
        /// <summary>
        /// 标签中新增的部门id列表，用逗号分隔
        /// </summary>
        public string AddPartyItems { get; set; }
        /// <summary>
        /// 标签中删除的部门id列表，用逗号分隔
        /// </summary>
        public string DelPartyItems { get; set; }
    }
    public enum ContactChangeType
    {
        /// <summary>
        /// 新增成员事件
        /// </summary>
        create_user,
        /// <summary>
        /// 更新成员事件
        /// </summary>
        update_user,
        /// <summary>
        /// 删除成员事件
        /// </summary>
        delete_user,
        /// <summary>
        /// 新增部门事件
        /// </summary>
        create_party,
        /// <summary>
        /// 更新部门事件
        /// </summary>
        update_party,
        /// <summary>
        /// 删除部门事件
        /// </summary>
        delete_party,
        /// <summary>
        /// 标签成员变更事件
        /// </summary>
        update_tag,
    }
}