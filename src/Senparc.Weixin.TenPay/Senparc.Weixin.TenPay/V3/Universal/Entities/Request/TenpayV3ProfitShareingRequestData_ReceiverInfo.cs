using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPay.V3
{

    /// <summary>
    /// 分账时传入的接收方分账的信息
    /// 服务商(单次分账): https://pay.weixin.qq.com/wiki/doc/api/allocation_sl.php?chapter=25_1&index=1
    /// 境内普通商户(单次分账): https://pay.weixin.qq.com/wiki/doc/api/allocation.php?chapter=27_1&index=1
    /// 服务商(多次分账): https://pay.weixin.qq.com/wiki/doc/api/allocation_sl.php?chapter=25_6&index=2
    /// 境内普通商户(多次分账): https://pay.weixin.qq.com/wiki/doc/api/allocation.php?chapter=27_6&index=2
    /// </summary>
    public class TenpayV3ProfitShareingRequestData_ReceiverInfo
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public TenpayV3ProfitShareingAddReceiver_ReceiverInfo_Type receiveType { get; set; }

        /// <summary>
        /// 分账接收方类型,<see cref="receiveType"/> 转换为字符串而来; 
        /// MERCHANT_ID：商户ID 
        /// PERSONAL_WECHATID：个人微信号
        /// PERSONAL_OPENID：个人openid（由父商户APPID转换得到）
        /// PERSONAL_SUB_OPENID: 个人sub_openid（由子商户APPID转换得到）
        /// </summary>
        public string type
        {
            get
            {
                return receiveType.ToString();
            }
        }

        /// <summary>
        /// 分账接收方帐号
        /// <see cref="type"/>是MERCHANT_ID时，是商户ID;
        /// <see cref="type"/>是PERSONAL_WECHATID时，是个人微信号;
        /// <see cref="type"/>是PERSONAL_OPENID时，是个人openid;
        /// <see cref="type"/>是PERSONAL_SUB_OPENID时，是个人sub_openid;
        /// </summary>
        public string account { get; set; }

        /// <summary>
        /// 分账金额,分为单位
        /// </summary>
        public int amount { get; set; }

        /// <summary>
        /// 分账描述,必须传入
        /// </summary>
        public string description { get; set; }

    }


    /// <summary>
    /// 分账查询时的接收方分账的信息
    /// 服务商特约商户: https://pay.weixin.qq.com/wiki/doc/api/allocation_sl.php?chapter=25_2&index=3
    /// 境内普通商户: https://pay.weixin.qq.com/wiki/doc/api/allocation.php?chapter=27_2&index=3
    /// </summary>
    public class TenpayV3ProfitShareingQuery_ReceiverInfo
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public TenpayV3ProfitShareingAddReceiver_ReceiverInfo_Type receiveType { get; set; }

        /// <summary>
        /// 分账接收方类型,<see cref="receiveType"/> 转换为字符串而来; 
        /// MERCHANT_ID：商户ID 
        /// PERSONAL_WECHATID：个人微信号
        /// PERSONAL_OPENID：个人openid（由父商户APPID转换得到）
        /// PERSONAL_SUB_OPENID: 个人sub_openid（由子商户APPID转换得到）
        /// </summary>
        public string type
        {
            get
            {
                return receiveType.ToString();
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    receiveType = (TenpayV3ProfitShareingAddReceiver_ReceiverInfo_Type)Enum.Parse(typeof(TenpayV3ProfitShareingAddReceiver_ReceiverInfo_Type), value);
                }
            }
        }

        /// <summary>
        /// 分账接收方帐号
        /// <see cref="type"/>是MERCHANT_ID时，是商户ID;
        /// <see cref="type"/>是PERSONAL_WECHATID时，是个人微信号;
        /// <see cref="type"/>是PERSONAL_OPENID时，是个人openid;
        /// <see cref="type"/>是PERSONAL_SUB_OPENID时，是个人sub_openid;
        /// </summary>
        public string account { get; set; }

        /// <summary>
        /// 分账金额,分为单位
        /// </summary>
        public int amount { get; set; }

        /// <summary>
        /// 分账描述,必须传入
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// PENDING:待分账 
        /// SUCCESS:分账成功
        /// ADJUST:分账失败待调账
        /// RETURNED:已转回分账方
        /// CLOSED: 已关闭
        /// </summary>
        public string result { get; set; }

        /// <summary>
        /// 分账完成时间 
        /// </summary>
        public string finish_time { get; set; }

    }


    /// <summary>
    /// 新增分账接收方时传入的接收方的信息
    /// 服务商: https://pay.weixin.qq.com/wiki/doc/api/allocation_sl.php?chapter=25_3&index=4
    /// 境内普通商户: https://pay.weixin.qq.com/wiki/doc/api/allocation.php?chapter=27_3&index=4
    /// </summary>
    public class TenpayV3ProfitShareingAddReceiverRequestData_ReceiverInfo
    {        
        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public TenpayV3ProfitShareingAddReceiver_ReceiverInfo_Type receiveType { get; set; }

        /// <summary>
        /// 分账接收方类型,<see cref="receiveType"/> 转换为字符串而来; 
        /// MERCHANT_ID：商户ID 
        /// PERSONAL_WECHATID：个人微信号
        /// PERSONAL_OPENID：个人openid（由父商户APPID转换得到）
        /// PERSONAL_SUB_OPENID: 个人sub_openid（由子商户APPID转换得到）
        /// </summary>
        public string type 
        { 
            get
            {
                return receiveType.ToString();
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    receiveType = (TenpayV3ProfitShareingAddReceiver_ReceiverInfo_Type)Enum.Parse(typeof(TenpayV3ProfitShareingAddReceiver_ReceiverInfo_Type), value);
                }
            }
        }

        /// <summary>
        /// 分账接收方帐号
        /// <see cref="type"/>是MERCHANT_ID时，是商户ID;
        /// <see cref="type"/>是PERSONAL_WECHATID时，是个人微信号;
        /// <see cref="type"/>是PERSONAL_OPENID时，是个人openid;
        /// <see cref="type"/>是PERSONAL_SUB_OPENID时，是个人sub_openid;
        /// </summary>
        public string account { get; set; }

        /// <summary>
        /// 分账接收方全称
        /// 添加接收方可能包含此参数;
        /// 添加接收方的响应中不会包含此参数;
        /// <see cref="type"/>是MERCHANT_ID时，是商户全称（必传） ;
        /// <see cref="type"/>是PERSONAL_NAME 时，是个人姓名（必传） ;
        /// <see cref="type"/>是PERSONAL_OPENID时，是个人姓名（选传，传则校验）; 
        /// <see cref="type"/>PERSONAL_SUB_OPENID时，是个人姓名（选传，传则校验） ;
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 与分账方的关系类型 
        /// </summary>
        [JsonIgnore]
        public TenpayV3ProfitShareingAddReceiver_ReceiverInfo_RelationType receiverRelationType
        {
            get; set;
        }

        /// <summary>
        /// 与分账方的关系类型, <see cref="receiverRelationType"/> 转换为字符串而来;
        /// 子商户与接收方的关系。 
        /// 本字段值为枚举： 
        /// SERVICE_PROVIDER：服务商
        /// STORE：门店
        /// STAFF：员工
        /// STORE_OWNER：店主
        /// PARTNER：合作伙伴
        /// HEADQUARTER：总部
        /// BRAND：品牌方
        /// DISTRIBUTOR：分销商
        /// USER：用户
        /// SUPPLIER：供应商
        /// CUSTOM：自定义
        /// </summary>
        public string relation_type 
        {   
            get
            {
                return receiverRelationType.ToString();
            }
        }

        /// <summary>
        /// 自定义的分账关系 
        /// 子商户与接收方具体的关系，本字段最多10个字。 
        ///当字段relation_type的值为CUSTOM时，本字段必填
        ///当字段relation_type的值不为CUSTOM时，本字段无需填写
        /// </summary>
        public string custom_relation
        { get; set; }
    }


    /// <summary>
    /// 删除分账接收方时传入的接收方的信息\新增分账接收方成功后的接收方的信息
    /// 服务商特约商户: https://pay.weixin.qq.com/wiki/doc/api/allocation_sl.php?chapter=25_4&index=5
    /// 境内普通商户: https://pay.weixin.qq.com/wiki/doc/api/allocation.php?chapter=27_4&index=5
    /// </summary>
    public class TenpayV3ProfitShareing_ReceiverInfo
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public TenpayV3ProfitShareingAddReceiver_ReceiverInfo_Type receiveType { get; set; }

        /// <summary>
        /// 分账接收方类型,<see cref="receiveType"/> 转换为字符串而来; 
        /// MERCHANT_ID：商户ID 
        /// PERSONAL_WECHATID：个人微信号
        /// PERSONAL_OPENID：个人openid（由父商户APPID转换得到）
        /// PERSONAL_SUB_OPENID: 个人sub_openid（由子商户APPID转换得到）
        /// </summary>
        public string type
        {
            get
            {
                return receiveType.ToString();
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    receiveType = (TenpayV3ProfitShareingAddReceiver_ReceiverInfo_Type)Enum.Parse(typeof(TenpayV3ProfitShareingAddReceiver_ReceiverInfo_Type), value);
                }
            }
        }

        /// <summary>
        /// 分账接收方帐号
        /// <see cref="type"/>是MERCHANT_ID时，是商户ID;
        /// <see cref="type"/>是PERSONAL_WECHATID时，是个人微信号;
        /// <see cref="type"/>是PERSONAL_OPENID时，是个人openid;
        /// <see cref="type"/>是PERSONAL_SUB_OPENID时，是个人sub_openid;
        /// </summary>
        public string account { get; set; }

    }


    /// <summary>
    /// 分账接收方枚举类型 
    /// </summary>
    public enum TenpayV3ProfitShareingAddReceiver_ReceiverInfo_Type
    {
        /// <summary>
        /// 商户ID 
        /// </summary>
        MERCHANT_ID,
        /// <summary>
        /// 个人微信号
        /// </summary>
        PERSONAL_WECHATID,
        /// <summary>
        /// 个人openid（由父商户APPID转换得到）
        /// </summary>
        PERSONAL_OPENID,
        /// <summary>
        /// 个人sub_openid（由子商户APPID转换得到） 
        /// </summary>
        PERSONAL_SUB_OPENID
    }

    /// <summary>
    /// 分账接收方和商户的关系类型枚举
    /// </summary>
    public enum TenpayV3ProfitShareingAddReceiver_ReceiverInfo_RelationType
    {
        /// <summary>
        /// 服务商
        /// </summary>
        SERVICE_PROVIDER,
        /// <summary>
        /// 门店
        /// </summary>
        STORE,
        /// <summary>
        /// 员工
        /// </summary>
        STAFF,
        /// <summary>
        /// 店主
        /// </summary>
        STORE_OWNER,
        /// <summary>
        /// 合作伙伴
        /// </summary>
        PARTNER,
        /// <summary>
        /// 总部
        /// </summary>
        HEADQUARTER,
        /// <summary>
        /// 品牌方
        /// </summary>
        BRAND,
        /// <summary>
        /// 分销商
        /// </summary>
        DISTRIBUTOR,
        /// <summary>
        /// 用户
        /// </summary>
        USER,
        /// <summary>
        /// 供应商
        /// </summary>
        SUPPLIER,
        /// <summary>
        /// 自定义
        /// </summary>
        CUSTOM
    }
}
