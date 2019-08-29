/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：GetExternalContactResult.cs
    文件功能描述：获取外部联系人详情返回结果
     
    
    创建标识：Senparc - 20181009

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.External
{

    public class GetExternalContactResultJson : WorkJsonResult
    {
        public External_Contact external_contact { get; set; }
        public Follow_User[] follow_user { get; set; }
    }

    public class External_Contact
    {
        public string external_userid { get; set; }
        public string name { get; set; }
        public string position { get; set; }
        public string avatar { get; set; }
        public string corp_name { get; set; }
        public string corp_full_name { get; set; }
        public int type { get; set; }
        public int gender { get; set; }
        public string unionid { get; set; }
        public External_Profile external_profile { get; set; }
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

    public class Follow_User
    {
        public string userid { get; set; }
        public string remark { get; set; }
        public string description { get; set; }
        public int createtime { get; set; }
    }


}
