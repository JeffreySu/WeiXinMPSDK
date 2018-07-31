/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：MemberBase.cs
    文件功能描述：成员信息基础类
     
    
    创建标识：Senparc - 20180728

----------------------------------------------------------------*/

using System.Collections.Generic;

namespace Senparc.Weixin.Work.AdvancedAPIs.MailList.Member
{
    /// <summary>
    /// 成员信息基础类
    /// <para>必填项见属性说明，没有特殊说明的都为非必填</para>
    /// <para>文档：<see cref="http://work.weixin.qq.com/api/doc#10018"/></para>
    /// </summary>
    public abstract class MemberBase
    {
        /// <summary>
        /// [创建、更新 必填]成员UserID。对应管理端的帐号，企业内必须唯一。不区分大小写，长度为1~64个字节
        /// </summary>
        public string userid { get; set; }
        /// <summary>
        /// [创建必填]成员名称。长度为1~64个字符
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 英文名。长度为1-64个字节，由字母、数字、点(.)、减号(-)、空格或下划线(_)组成
        /// </summary>
        public string english_name { get; set; }
        /// <summary>
        /// 手机号码。企业内必须唯一，mobile/email二者不能同时为空
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// [创建必填]成员所属部门id列表,不超过20个
        /// </summary>
        public long[] department { get; set; }
        /// <summary>
        /// 部门内的排序值，默认为0，成员次序以创建时间从小到大排列。数量必须和department一致，数值越大排序越前面。有效的值范围是[0, 2^32)
        /// </summary>
        public long[] order { get; set; }
        /// <summary>
        /// 职位信息。长度为0~128个字符
        /// </summary>
        public string position { get; set; }
        /// <summary>
        /// 性别。1表示男性，2表示女性
        /// </summary>
        public string gender { get; set; }
        /// <summary>
        /// 邮箱。长度不超过64个字节，且为有效的email格式。企业内必须唯一，mobile/email二者不能同时为空
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// 上级字段，标识是否为上级。在审批等应用里可以用来标识上级审批人
        /// </summary>
        public int isleader { get; set; }
        /// <summary>
        /// 启用/禁用成员。1表示启用成员，0表示禁用成员
        /// </summary>
        public int enable { get; set; }
        
        /// <summary>
        /// 成员头像的mediaid，通过素材管理接口上传图片获得的mediaid
        /// </summary>
        public string avatar_mediaid { get; set; }

        /// <summary>
        /// 座机。由1-32位的纯数字或’-‘号组成。
        /// </summary>
        public string telephone { get; set; }
        /// <summary>
        /// 自定义字段。自定义字段需要先在WEB管理端添加，见扩展属性添加方法，否则忽略未知属性的赋值。自定义字段长度为0~32个字符，超过将被截断
        /// </summary>
        public Extattr extattr { get; set; }

        /*
        /// <summary>
        /// 是否邀请该成员使用企业微信（将通过微信服务通知或短信或邮件下发邀请，每天自动下发一次，最多持续3个工作日），默认值为true。
        /// </summary>
        public bool to_invite { get; set; }
        */

        /// <summary>
        /// 成员对外属性，字段详情见对外属性：
        /// <seealso cref="http://work.weixin.qq.com/api/doc#13450"/>
        /// </summary>
        public External_Profile external_profile { get; set; }
    }

    public class Extattr
    {
        public List<Attr> attrs { get; set; }
    }

    public class Attr
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    public class External_Profile
    {
        public External_Attr[] external_attr { get; set; }
    }

    public class External_Attr
    {
        public int type { get; set; }
        public string name { get; set; }
        public Text text { get; set; }
        public Web web { get; set; }
        public Miniprogram miniprogram { get; set; }
    }

    public class Text
    {
        public string value { get; set; }
    }

    public class Web
    {
        public string url { get; set; }
        public string title { get; set; }
    }

    public class Miniprogram
    {
        public string appid { get; set; }
        public string pagepath { get; set; }
        public string title { get; set; }
    }

}
