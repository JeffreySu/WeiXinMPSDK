/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：RequestMessageEvent_Kf_Account_Auth_Change.cs
    文件功能描述：RequestMessageEvent_Kf_Account_Auth_Change 强类型数据模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.32.1 补齐企业微信通讯录、安全、智能机器人、微信客服和获客助手接口

----------------------------------------------------------------*/

using System.Linq;
using System.Xml.Linq;

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>微信客服账号授权变更通知。</summary>
    public class RequestMessageEvent_Kf_Account_Auth_Change : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 创建授权变更事件；传入 XML 时解析可重复出现的客服账号 ID 节点。
        /// </summary>
        /// <param name="root">回调 XML 根节点；可为 <see langword="null"/>。</param>
        public RequestMessageEvent_Kf_Account_Auth_Change(XElement root = null)
        {
            if (root == null)
            {
                return;
            }

            AuthAddOpenKfId = root.Elements("AuthAddOpenKfId").Select(element => element.Value).ToArray();
            AuthDelOpenKfId = root.Elements("AuthDelOpenKfId").Select(element => element.Value).ToArray();
        }

        public override Event Event => Event.KF_ACCOUNT_AUTH_CHANGE;

        /// <summary>本次新增授权的微信客服账号 ID。</summary>
        public string[] AuthAddOpenKfId { get; }

        /// <summary>本次取消授权的微信客服账号 ID。</summary>
        public string[] AuthDelOpenKfId { get; }
    }
}
