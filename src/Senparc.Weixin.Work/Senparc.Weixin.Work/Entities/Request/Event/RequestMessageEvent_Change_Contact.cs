/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
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
        /// 上级字段，标识是否为上级。第三方暂不支持
        /// </summary>
        public int IsLeader { get; set; }
        /// <summary>
        /// 头像url。注：如果要获取小图将url最后的”/0”改成”/100”即可。
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
        /// 英文名
        /// </summary>
        public string EnglishName { get; set; }
        /// <summary>
        /// 扩展属性
        /// </summary>
        public List<Item> ExtAttr { get; set; }
    }
    public class Item
    {
        public string Name { get; set; }
        public string Value { get; set; }
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