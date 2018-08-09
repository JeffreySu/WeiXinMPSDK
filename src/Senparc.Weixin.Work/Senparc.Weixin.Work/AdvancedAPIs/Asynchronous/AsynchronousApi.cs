/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：AsynchronousApi.cs
    文件功能描述：异步任务接口
    
    
    创建标识：Senparc - 20150408

    修改标识：Senparc - 20160720
    修改描述：增加其接口的异步方法
 
    修改标识：Senparc - 20170215
    修改描述：增加其接口的异步方法

    修改标识：Senparc - 20170712
    修改描述：v14.5.1 AccessToken HandlerWaper改造

----------------------------------------------------------------*/

/*
    官方文档：http://qydev.weixin.qq.com/wiki/index.php?title=%E5%BC%82%E6%AD%A5%E4%BB%BB%E5%8A%A1%E6%8E%A5%E5%8F%A3
 */

using System.Threading.Tasks;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.Work.AdvancedAPIs.Asynchronous;
using Senparc.Weixin.Work.CommonAPIs;

namespace Senparc.Weixin.Work.AdvancedAPIs
{
    /// <summary>
    /// 异步任务接口
    /// </summary>
    public static class AsynchronousApi
    {
        #region 同步方法

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
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
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
        public static AsynchronousJobId BatchSyncUser(string accessTokenOrAppKey, string mediaId, Asynchronous_CallBack callBack, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = Config.ApiWorkHost + "/cgi-bin/batch/syncuser?access_token={0}";

                var data = new
                {
                    media_id = mediaId,
                    callback = callBack
                };

                return CommonJsonSend.Send<AsynchronousJobId>(accessToken, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);


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
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
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
        public static AsynchronousJobId BatchReplaceUser(string accessTokenOrAppKey, string mediaId, Asynchronous_CallBack callBack, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = Config.ApiWorkHost + "/cgi-bin/batch/replaceuser?access_token={0}";

                var data = new
                {
                    media_id = mediaId,
                    callback = callBack
                };

                return CommonJsonSend.Send<AsynchronousJobId>(accessToken, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);


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
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
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
        public static AsynchronousJobId BatchReplaceParty(string accessTokenOrAppKey, string mediaId, Asynchronous_CallBack callBack, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = Config.ApiWorkHost + "/cgi-bin/batch/replaceparty?access_token={0}";

                var data = new
                {
                    media_id = mediaId,
                    callback = callBack
                };

                return CommonJsonSend.Send<AsynchronousJobId>(accessToken, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 获取异步更新或全面覆盖成员结果
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="jobId"></param>
        /// <returns></returns>
        public static AsynchronousReplaceUserResult GetReplaceUserResult(string accessTokenOrAppKey, string jobId)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/batch/getresult?access_token={0}&jobid={1}",
                                    accessToken.AsUrlData(), jobId.AsUrlData());

                return Get.GetJson<AsynchronousReplaceUserResult>(url);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 获取异步全面覆盖部门结果
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="jobId"></param>
        /// <returns></returns>
        public static AsynchronousReplacePartyResult GetReplacePartyResult(string accessTokenOrAppKey, string jobId)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/batch/getresult?access_token={0}&jobid={1}",
                                    accessToken.AsUrlData(), jobId.AsUrlData());

                return Get.GetJson<AsynchronousReplacePartyResult>(url);
            }, accessTokenOrAppKey);


        }

        #endregion
        #endregion

#if !NET35 && !NET40
        #region 异步方法

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
        /// 【异步方法】增量更新成员
        /// CSV模板下载地址：http://qydev.weixin.qq.com/batch_user_sample.csv
        /// 注意事项：
        /// 1.模板中的部门需填写部门ID，多个部门用分号分隔，部门ID必须为数字
        /// 2.文件中存在、通讯录中也存在的成员，更新成员在文件中指定的字段值
        /// 3.文件中存在、通讯录中不存在的成员，执行添加操作
        /// 4.通讯录中存在、文件中不存在的成员，保持不变
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
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
        public static async Task<AsynchronousJobId> BatchSyncUserAsync(string accessTokenOrAppKey, string mediaId, Asynchronous_CallBack callBack, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = Config.ApiWorkHost + "/cgi-bin/batch/syncuser?access_token={0}";

                var data = new
                {
                    media_id = mediaId,
                    callback = callBack
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<AsynchronousJobId>(accessToken, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 【异步方法】全量覆盖成员
        /// CSV模板下载地址：http://qydev.weixin.qq.com/batch_user_sample.csv
        /// 注意事项：
        /// 1.模板中的部门需填写部门ID，多个部门用分号分隔，部门ID必须为数字
        /// 2.文件中存在、通讯录中也存在的成员，完全以文件为准
        /// 3.文件中存在、通讯录中不存在的成员，执行添加操作
        /// 4.通讯录中存在、文件中不存在的成员，执行删除操作。出于安全考虑，如果需要删除的成员多于50人，且多于现有人数的20%以上，系统将中止导入并返回相应的错误码
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
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
        public static async Task<AsynchronousJobId> BatchReplaceUserAsync(string accessTokenOrAppKey, string mediaId, Asynchronous_CallBack callBack, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = Config.ApiWorkHost + "/cgi-bin/batch/replaceuser?access_token={0}";

                var data = new
                {
                    media_id = mediaId,
                    callback = callBack
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<AsynchronousJobId>(accessToken, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 【异步方法】全量覆盖部门
        /// CSV模板下载地址：http://qydev.weixin.qq.com/batch_party_sample.csv
        /// 注意事项：
        /// 1.文件中存在、通讯录中也存在的部门，执行修改操作
        /// 2.文件中存在、通讯录中不存在的部门，执行添加操作
        /// 3.文件中不存在、通讯录中存在的部门，当部门为空时，执行删除操作
        /// 4.CSV文件中，部门名称、部门ID、父部门ID为必填字段，部门ID必须为数字；排序为可选字段，置空或填0不修改排序
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
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
        public static async Task<AsynchronousJobId> BatchReplacePartyAsync(string accessTokenOrAppKey, string mediaId, Asynchronous_CallBack callBack, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = Config.ApiWorkHost + "/cgi-bin/batch/replaceparty?access_token={0}";

                var data = new
                {
                    media_id = mediaId,
                    callback = callBack
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<AsynchronousJobId>(accessToken, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 【异步方法】获取异步更新或全面覆盖成员结果
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="jobId"></param>
        /// <returns></returns>
        public static async Task<AsynchronousReplaceUserResult> GetReplaceUserResultAsync(string accessTokenOrAppKey, string jobId)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/batch/getresult?access_token={0}&jobid={1}",
                                    accessToken.AsUrlData(), jobId.AsUrlData());

                return await Get.GetJsonAsync<AsynchronousReplaceUserResult>(url);
            }, accessTokenOrAppKey);


        }

        /// <summary>
        /// 【异步方法】获取异步全面覆盖部门结果
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="jobId"></param>
        /// <returns></returns>
        public static async Task<AsynchronousReplacePartyResult> GetReplacePartyResultAsync(string accessTokenOrAppKey, string jobId)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/batch/getresult?access_token={0}&jobid={1}",
                                    accessToken.AsUrlData(), jobId.AsUrlData());

                return await Get.GetJsonAsync<AsynchronousReplacePartyResult>(url);
            }, accessTokenOrAppKey);


        }

        #endregion
        #endregion
#endif
    }
}
