/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：OAuth2Result.cs
    文件功能描述：获取成员信息返回结果
    http://work.weixin.qq.com/api/doc#10028
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口
 
    修改标识：Senparc - 20150316
    修改描述：添加DeviceId字段
 
    修改标识：Senparc - 20150316
    修改描述：GetUserIdResult变更为GetUserInfoResult，增加OpenId字段

    修改标识：Senparc - 20170909
    修改描述：修改注释

    修改标识：Senparc - 20180815
    修改描述：添加 CorpId 属性

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.OAuth2
{
    /// <summary>
    /// 获取成员信息返回结果
    /// <para>https://work.weixin.qq.com/api/doc#10975/%E7%BD%91%E9%A1%B5%E6%8E%88%E6%9D%83%E7%99%BB%E5%BD%95%E7%AC%AC%E4%B8%89%E6%96%B9</para>
    /// </summary>
    public class GetUserInfoResult : WorkJsonResult
    {
        /* 
           a) 当用户为企业成员时返回示例如下：

{
   "errcode": 0,
   "errmsg": "ok",
   "UserId":"USERID",
   "DeviceId":"DEVICEID",
   "user_ticket": "USER_TICKET"，
   "expires_in":7200
}

            b) 非企业成员授权时返回示例如下：

{
   "errcode": 0,
   "errmsg": "ok",
   "OpenId":"OPENID",
   "DeviceId":"DEVICEID"
}

    */

        /// <summary>
        /// 用户所属企业的corpid
        /// </summary>
        public string CorpId { get; set; }
        /// <summary>
        /// 员工UserID
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 非企业成员的OpenId
        /// （此属性在Work最新文档中没有）
        /// </summary>
        public string OpenId { get; set; }
        /// <summary>
        /// 手机设备号(由微信在安装时随机生成) 
        /// （此属性在Work最新文档中没有）
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// 成员票据，最大为512字节。
        /// scope为snsapi_userinfo或snsapi_privateinfo，且用户在应用可见范围之内时返回此参数。
        /// 后续利用该参数可以获取用户信息或敏感信息。
        /// </summary>
        public string user_ticket { get; set; }

        /// <summary>
        /// user_token的有效时间（秒），随user_ticket一起返回
        /// </summary>
        public int expires_in { get; set; }

    }


    /// <summary>
    /// "使用user_ticket获取成员详情"接口返回结果
    /// </summary>
    public class GetUserDetailResult : WorkJsonResult
    {
        /*
         {
   "userid":"lisi",
   "name":"李四",
   "department":[3],
   "position": "后台工程师",
   "mobile":"15050495892",
   "gender":1,
   "email":"xxx@xx.com",
   "avatar":"http://shp.qpic.cn/bizmp/xxxxxxxxxxx/0"
        }
        */

        /// <summary>
        /// 成员UserID
        /// </summary>
        public string userid { get; set; }
        /// <summary>
        /// 成员姓名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 成员所属部门
        /// </summary>
        public int[] department { get; set; }
        /// <summary>
        /// 职位信息
        /// </summary>
        public string position { get; set; }
        /// <summary>
        /// 成员手机号，仅在用户同意snsapi_privateinfo授权时返回
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 性别。0表示未定义，1表示男性，2表示女性
        /// </summary>
        public int gender { get; set; }
        /// <summary>
        /// 成员邮箱，仅在用户同意snsapi_privateinfo授权时返回
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// 头像url。注：如果要获取小图将url最后的”/0”改成”/64”即可
        /// </summary>
        public string avatar { get; set; }
    }
}
