/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：AsynchronousApi.cs
    文件功能描述：异步任务接口
    
    
    创建标识：Senparc - 20150408
----------------------------------------------------------------*/

/*
    官方文档：http://qydev.weixin.qq.com/wiki/index.php?title=%E5%BC%82%E6%AD%A5%E4%BB%BB%E5%8A%A1%E6%8E%A5%E5%8F%A3
 */

using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.QY.AdvancedAPIs.Asynchronous;
using Senparc.Weixin.QY.CommonAPIs;

namespace Senparc.Weixin.QY.AdvancedAPIs
{
    /// <summary>
    /// 异步任务接口
    /// </summary>
    public static class AsynchronousApi
    {
        /// <summary>
        /// 邀请成员关注
        /// </summary>
        /// <param name="accessToken">企业的access_token</param>
        /// <param name="toUser">成员ID列表，多个接收者用‘|’分隔，最多支持1000个。</param>
        /// <param name="toParty">部门ID列表，多个接收者用‘|’分隔，最多支持100个。</param>
        /// <param name="toTag">标签ID列表，多个接收者用‘|’分隔。</param>
        /// <param name="inviteTips">推送到微信上的提示语（只有认证号可以使用）。当使用微信推送时，该字段默认为“请关注XXX企业号”，邮件邀请时，该字段无效。</param>
        /// <param name="callBack">回调信息。任务完成后，通过callback推送事件给企业。具体请参考应用回调模式中的相应选项</param>
        /// <param name="timeOut"></param>
        /// post数据格式：
        /// {
        ///     "touser":"xxx|xxx",
        ///     "toparty":"xxx|xxx",
        ///     "totag":"xxx|xxx",
        ///     "invite_tips":"xxx",
        ///     "callback":
        ///     {
        ///         "url": "xxx",
        ///         "token": "xxx",
        ///         "encodingaeskey": "xxx"
        ///     }
        /// }
        /// <returns></returns>
        public static AsynchronousJobId BatchInviteUser(string accessToken, string toUser, string toParty, string toTag, string inviteTips, Asynchronous_CallBack callBack, int timeOut = Config.TIME_OUT)
        {
            var url = "https://qyapi.weixin.qq.com/cgi-bin/batch/inviteuser?access_token={0}";

            var data = new
                {
                    touser = toUser,
                    toparty = toParty,
                    totag = toTag,
                    invite_tips = inviteTips,
                    callback = callBack
                };

            return CommonJsonSend.Send<AsynchronousJobId>(accessToken, url, data, CommonJsonSendType.POST, timeOut);
        }

        #region 通讯录更新

        /*通讯录更新接口提供三种更新方法：1) 增量更新成员 2）全量覆盖成员 3) 全量覆盖部门。如果企业要做到与企业号通讯录完全一致，可先调用全量覆盖部门接口，再调用全量覆盖成员接口，即可保持通讯录完全一致。

            使用步骤为：

            1.下载接口对应的csv模板，如果有扩展字段，请自行添加

            2.按模板的格式，生成接口所需的数据文件

            3.通过上传媒体文件接口上传数据文件，获取media_id

            4.调用通讯录更新接口，传入media_id参数

            5.接收任务完成事件，并获取任务执行结果
        */

        /// <summary>
        /// 增量更新成员
        /// CSV模板下载地址：http://qydev.weixin.qq.com/batch_user_sample.csv
        /// 注意事项：
        /// 1.模板中的部门需填写部门ID，多个部门用分号分隔，部门ID必须为数字
        /// 2.文件中存在、通讯录中也存在的成员，更新成员在文件中指定的字段值
        /// 3.文件中存在、通讯录中不存在的成员，执行添加操作
        /// 4.通讯录中存在、文件中不存在的成员，保持不变
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="mediaId">上传的csv文件的media_id</param>
        /// <param name="callBack">回调信息。任务完成后，通过callback推送事件给企业。具体请参考应用回调模式中的相应选项</param>
        /// <param name="timeOut"></param>
        /// post数据格式：
        /// {
        ///    "media_id":"xxxxxx",
        ///    "callback":
        ///    {
        ///        "url": "xxx",
        ///        "token": "xxx",
        ///        "encodingaeskey": "xxx"
        ///    }
        /// }
        /// <returns></returns>
        public static AsynchronousJobId BatchSyncUser(string accessToken, string mediaId, Asynchronous_CallBack callBack, int timeOut = Config.TIME_OUT)
        {
            var url = "https://qyapi.weixin.qq.com/cgi-bin/batch/syncuser?access_token={0}";

            var data = new
                {
                    media_id = mediaId,
                    callback = callBack
                };

            return CommonJsonSend.Send<AsynchronousJobId>(accessToken, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 全量覆盖成员
        /// CSV模板下载地址：http://qydev.weixin.qq.com/batch_user_sample.csv
        /// 注意事项：
        /// 1.模板中的部门需填写部门ID，多个部门用分号分隔，部门ID必须为数字
        /// 2.文件中存在、通讯录中也存在的成员，完全以文件为准
        /// 3.文件中存在、通讯录中不存在的成员，执行添加操作
        /// 4.通讯录中存在、文件中不存在的成员，执行删除操作。出于安全考虑，如果需要删除的成员多于50人，且多于现有人数的20%以上，系统将中止导入并返回相应的错误码
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="mediaId">上传的csv文件的media_id</param>
        /// <param name="callBack">回调信息。任务完成后，通过callback推送事件给企业。具体请参考应用回调模式中的相应选项</param>
        /// <param name="timeOut"></param>
        /// post数据格式：
        /// {
        ///    "media_id":"xxxxxx",
        ///    "callback":
        ///    {
        ///        "url": "xxx",
        ///        "token": "xxx",
        ///        "encodingaeskey": "xxx"
        ///    }
        /// }
        /// <returns></returns>
        public static AsynchronousJobId BatchReplaceUser(string accessToken, string mediaId, Asynchronous_CallBack callBack, int timeOut = Config.TIME_OUT)
        {
            var url = "https://qyapi.weixin.qq.com/cgi-bin/batch/replaceuser?access_token={0}";

            var data = new
            {
                media_id = mediaId,
                callback = callBack
            };

            return CommonJsonSend.Send<AsynchronousJobId>(accessToken, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 全量覆盖部门
        /// CSV模板下载地址：http://qydev.weixin.qq.com/batch_party_sample.csv
        /// 注意事项：
        /// 1.文件中存在、通讯录中也存在的部门，执行修改操作
        /// 2.文件中存在、通讯录中不存在的部门，执行添加操作
        /// 3.文件中不存在、通讯录中存在的部门，当部门为空时，执行删除操作
        /// 4.CSV文件中，部门名称、部门ID、父部门ID为必填字段，部门ID必须为数字；排序为可选字段，置空或填0不修改排序
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="mediaId">上传的csv文件的media_id</param>
        /// <param name="callBack">回调信息。任务完成后，通过callback推送事件给企业。具体请参考应用回调模式中的相应选项</param>
        /// <param name="timeOut"></param>
        /// post数据格式：
        /// {
        ///    "media_id":"xxxxxx",
        ///    "callback":
        ///    {
        ///        "url": "xxx",
        ///        "token": "xxx",
        ///        "encodingaeskey": "xxx"
        ///    }
        /// }
        /// <returns></returns>
        public static AsynchronousJobId BatchReplaceParty(string accessToken, string mediaId, Asynchronous_CallBack callBack, int timeOut = Config.TIME_OUT)
        {
            var url = "https://qyapi.weixin.qq.com/cgi-bin/batch/replaceparty?access_token={0}";

            var data = new
            {
                media_id = mediaId,
                callback = callBack
            };

            return CommonJsonSend.Send<AsynchronousJobId>(accessToken, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 获取异步邀请成员关注结果
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="jobId"></param>
        /// <returns></returns>
        public static AsynchronousInviteUserResult GetInviteUserResult(string accessToken, string jobId)
        {
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/batch/getresult?access_token={0}&jobid={1}",
                                    accessToken.AsUrlData(), jobId.AsUrlData());

            return Get.GetJson<AsynchronousInviteUserResult>(url);
        }

        /// <summary>
        /// 获取异步更新或全面覆盖成员结果
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="jobId"></param>
        /// <returns></returns>
        public static AsynchronousReplaceUserResult GetReplaceUserResult(string accessToken, string jobId)
        {
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/batch/getresult?access_token={0}&jobid={1}",
                                    accessToken.AsUrlData(), jobId.AsUrlData());

            return Get.GetJson<AsynchronousReplaceUserResult>(url);
        }

        /// <summary>
        /// 获取异步全面覆盖部门结果
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="jobId"></param>
        /// <returns></returns>
        public static AsynchronousReplacePartyResult GetReplacePartyResult(string accessToken, string jobId)
        {
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/batch/getresult?access_token={0}&jobid={1}",
                                    accessToken.AsUrlData(), jobId.AsUrlData());

            return Get.GetJson<AsynchronousReplacePartyResult>(url);
        }

        #endregion
    }
}
