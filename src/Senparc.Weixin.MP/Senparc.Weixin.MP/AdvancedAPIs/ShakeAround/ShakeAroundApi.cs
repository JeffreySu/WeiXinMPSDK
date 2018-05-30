#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2018 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc

    文件名：ShakeAroundApi.cs
    文件功能描述：摇一摇周边接口


    创建标识：Senparc - 20150512

    修改标识：Senparc - 20160216
    修改描述：添加 查询设备与页面的关联关系 接口

    修改标识：Senparc - 20160424
    修改描述：v13.7.5 添加 ShakeAroundApi.DeviceApplyStatus 接口
 
    修改标识：Senparc - 20160520
    修改描述：添加批量查询页面统计数据接口，新增分组接口，编辑分组信息接口，删除分组接口，查询分组列表接口，
              查询分组详情接口，添加设备到分组接口，从分组中移除设备接口，创建红包活动接口，录入红包信息接口，
              设置红包活动抽奖开关接口，红包查询接口
    修改标识：Senparc - 20160520
    修改描述：修改批量查询设备统计数据接口

    修改标识：Senparc 20160719
    修改描述：增加其接口的异步方法
 
    修改标识：Senparc 20160823
    修改描述：modify DeletePage,DeletePageAsync中的方法中的参数，即longn []pageIds改为long pageId 

    修改标识：Senparc 20170110
    修改描述：将lastSeen参数调整为long类型

    修改标识：Senparc 20170509
    修改描述：v14.4.7 修改 GroupGetAdddevice 数据结构

    修改标识：Senparc - 20170707
    修改描述：v14.5.1 完善异步方法async/await

    修改标识：Senparc - 20180522
    修改描述：v14.5.1 完善异步方法async/await

----------------------------------------------------------------*/

/*
    API：http://mp.weixin.qq.com/wiki/15/b9e012f917e3484b7ed02771156411f3.html
 */

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.MP.AdvancedAPIs.ShakeAround;
using Senparc.Weixin.MP.CommonAPIs;
using System.Linq;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 摇一摇周边接口
    /// </summary>
    public static class ShakeAroundApi
    {
        #region 同步方法

        /// <summary>
        /// 申请开通功能
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data"></param>
        /// <param name="industry_Id"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static RegisterResultJson Register(string accessTokenOrAppId, RegisterData data, IndustryId industry_Id, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string url = string.Format(Config.ApiMpHost + "/shakearound/account/register?access_token={0}", accessToken.AsUrlData());

                data.industry_id = RegisterData.ConvertIndustryId(industry_Id);
                return CommonJsonSend.Send<RegisterResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 查询审核状态
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <returns></returns>
        public static GetAuditStatusResultJson GetAuditStatus(string accessTokenOrAppId)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string url = string.Format(Config.ApiMpHost + "/shakearound/account/auditstatus?access_token={0}", accessToken.AsUrlData());

                return Get.GetJson<GetAuditStatusResultJson>(url);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 申请设备ID
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="quantity">申请的设备ID的数量，单次新增设备超过500个，需走人工审核流程</param>
        /// <param name="applyReason">申请理由，不超过100个字</param>
        /// <param name="comment">备注，不超过15个汉字或30个英文字母</param>
        /// <param name="poiId">设备关联的门店ID，关联门店后，在门店1KM的范围内有优先摇出信息的机会。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static DeviceApplyResultJson DeviceApply(string accessTokenOrAppId, int quantity, string applyReason, string comment = null, long? poiId = null, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string url = string.Format(Config.ApiMpHost + "/shakearound/device/applyid?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    quantity = quantity,
                    apply_reason = applyReason,
                    comment = comment,
                    poi_id = poiId
                };

                return CommonJsonSend.Send<DeviceApplyResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }


        /// <summary>
        /// 查询设备ID申请审核状态
        /// 接口说明 查询设备ID申请的审核状态。若单次申请的设备ID数量小于等于500个，系统会进行快速审核；若单次申请的设备ID数量大于500个，则在三个工作日内完成审核。
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="appId">批次ID，申请设备ID时所返回的批次ID</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetDeviceStatusResultJson DeviceApplyStatus(string accessTokenOrAppId, long appId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string url = string.Format(Config.ApiMpHost + "/shakearound/device/applystatus?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    apply_id = appId,
                };

                return CommonJsonSend.Send<GetDeviceStatusResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 编辑设备信息
        /// 设备编号，若填了UUID、major、minor，则可不填设备编号，若二者都填，则以设备编号为优先
        /// UUID、major、minor，三个信息需填写完整，若填了设备编号，则可不填此信息。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="deviceId">设备编号</param>
        /// <param name="uuId"></param>
        /// <param name="major"></param>
        /// <param name="minor"></param>
        /// <param name="comment"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult DeviceUpdate(string accessTokenOrAppId, long deviceId, string uuId, long major, long minor, string comment, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string url = string.Format(Config.ApiMpHost + "/shakearound/device/update?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    device_identifier = new
                    {
                        device_id = deviceId,
                        uuid = uuId,
                        major = major,
                        minor = minor
                    },
                    comment = comment
                };

                return CommonJsonSend.Send<WxJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 配置设备与门店的关联关系
        /// 设备编号，若填了UUID、major、minor，则可不填设备编号，若二者都填，则以设备编号为优先
        /// UUID、major、minor，三个信息需填写完整，若填了设备编号，则可不填此信息。
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="deviceId">设备编号</param>
        /// <param name="uuid"></param>
        /// <param name="major"></param>
        /// <param name="minor"></param>
        /// <param name="poiId">Poi_id 的说明改为：设备关联的门店ID，关联门店后，在门店1KM的范围内有优先摇出信息的机会。</param>
        /// <param name="type">为1时，关联的门店和设备归属于同一公众账号；为2时，关联的门店为其他公众账号的门店。不填默认为1</param>
        /// <param name="timeOut"></param>
        /// <param name="poiAppid">当Type为2时，必填	关联门店所归属的公众账号的APPID</param>
        /// <returns></returns>
        public static WxJsonResult DeviceBindLocatoin(string accessTokenOrAppId, long deviceId, string uuid, long major, long minor, long poiId, string poiAppid, int type = 1, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string url = string.Format(Config.ApiMpHost + "/shakearound/device/bindlocation?access_token={0}", accessToken.AsUrlData());

                var data = type == 2
                    ? new
                    {
                        device_identifier = new
                        {
                            device_id = deviceId,
                            uuid = uuid,
                            major = major,
                            minor = minor
                        },
                        poi_id = poiId,
                        type = type,
                        poi_appid = poiAppid
                    } as object
                    : new
                    {
                        device_identifier = new
                        {
                            device_id = deviceId,
                            uuid = uuid,
                            major = major,
                            minor = minor
                        },
                        poi_id = poiId
                    } as object;


                return CommonJsonSend.Send<WxJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 配置设备与其他门店的关联关系
        /// 设备编号，若填了UUID、major、minor，则可不填设备编号，若二者都填，则以设备编号为优先
        /// UUID、major、minor，三个信息需填写完整，若填了设备编号，则可不填此信息。
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="deviceId">设备编号</param>
        /// <param name="uuid"></param>
        /// <param name="major"></param>
        /// <param name="minor"></param>
        /// <param name="poiId">Poi_id 的说明改为：设备关联的门店ID，关联门店后，在门店1KM的范围内有优先摇出信息的机会。</param>
        /// <param name="timeOut"></param>
        /// <param name="type">为1时，关联的门店和设备归属于同一公众账号；为2时，关联的门店为其他公众账号的门店。不填默认为1</param>
        /// <returns></returns>
        public static WxJsonResult DeviceBindLocatoin(string accessTokenOrAppId, long deviceId, string uuid, long major, long minor, long poiId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string url = string.Format(Config.ApiMpHost + "/shakearound/device/bindlocation?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    device_identifier = new
                    {
                        device_id = deviceId,
                        uuid = uuid,
                        major = major,
                        minor = minor
                    },
                    poi_id = poiId
                };

                return CommonJsonSend.Send<WxJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }

        #region 查询设备列表

        /// <summary>
        /// 查询设备列表Api url
        /// </summary>
        private static string searchDeviceUrl = Config.ApiMpHost + "/shakearound/device/search?access_token={0}";

        /// <summary>
        /// 根据指定的设备Id查询设备列表
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="deviceIdentifiers">设备Id列表</param>
        /// 设备编号，若填了UUID、major、minor，则可不填设备编号，若二者都填，则以设备编号为优先
        /// UUID、major、minor，三个信息需填写完整，若填了设备编号，则可不填此信息。
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static DeviceSearchResultJson SearchDeviceById(string accessTokenOrAppId, List<DeviceApply_Data_Device_Identifiers> deviceIdentifiers, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var data = new
                {
                    device_identifiers = deviceIdentifiers
                };

                return CommonJsonSend.Send<DeviceSearchResultJson>(accessToken, searchDeviceUrl, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 根据分页查询或者指定范围查询设备列表
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="lastSeen">前一次查询列表末尾的设备ID，第一次查询lastSeen为0</param>
        /// <param name="count">待查询的设备数量，不能超过50个</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static DeviceSearchResultJson SearchDeviceByRange(string accessTokenOrAppId, int lastSeen, int count, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var data = new
                {
                    last_seen = lastSeen,
                    count = count
                };

                return CommonJsonSend.Send<DeviceSearchResultJson>(accessToken, searchDeviceUrl, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 根据批次ID查询设备列表
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="applyId">批次ID，申请设备ID时所返回的批次ID</param>
        /// <param name="lastSeen">前一次查询列表末尾的设备ID，第一次查询lastSeen为0</param>
        /// <param name="count">待查询的设备数量，不能超过50个</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static DeviceSearchResultJson SearchDeviceByApplyId(string accessTokenOrAppId, long applyId, long lastSeen, int count, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var data = new
                {
                    apply_id = applyId,
                    last_seen = lastSeen,
                    count = count
                };

                return CommonJsonSend.Send<DeviceSearchResultJson>(accessToken, searchDeviceUrl, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }

        #endregion

        /// <summary>
        /// 上传图片素材
        /// 上传在摇一摇页面展示的图片素材，素材保存在微信侧服务器上。 格式限定为：jpg,jpeg,png,gif，图片大小建议120px*120 px，限制不超过200 px *200 px，图片需为正方形。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="file"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static UploadImageResultJson UploadImage(string accessTokenOrAppId, string file, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/shakearound/material/add?access_token={0}", accessToken.AsUrlData());
                var fileDictionary = new Dictionary<string, string>();
                fileDictionary["media"] = file;
                return Post.PostFileGetJson<UploadImageResultJson>(url, null, fileDictionary, null, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 新增页面
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="title">在摇一摇页面展示的主标题，不超过6个字</param>
        /// <param name="description">在摇一摇页面展示的副标题，不超过7个字</param>
        /// <param name="pageUrl">点击页面跳转链接</param>
        /// <param name="iconUrl">在摇一摇页面展示的图片。图片需先上传至微信侧服务器，用“素材管理-上传图片素材”接口上传图片，返回的图片URL再配置在此处</param>
        /// <param name="comment">页面的备注信息，不超过15个字</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static AddPageResultJson AddPage(string accessTokenOrAppId, string title, string description, string pageUrl,
            string iconUrl, string comment = null, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/shakearound/page/add?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    title = title,
                    description = description,
                    page_url = pageUrl,
                    comment = comment,
                    icon_url = iconUrl
                };

                return CommonJsonSend.Send<AddPageResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 编辑页面信息
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="pageId">摇周边页面唯一ID</param>
        /// <param name="title">在摇一摇页面展示的主标题，不超过6个字</param>
        /// <param name="description">在摇一摇页面展示的副标题，不超过7个字</param>
        /// <param name="pageUrl">点击页面跳转链接</param>
        /// <param name="iconUrl">在摇一摇页面展示的图片。图片需先上传至微信侧服务器，用“素材管理-上传图片素材”接口上传图片，返回的图片URL再配置在此处</param>
        /// <param name="comment">页面的备注信息，不超过15个字</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static UpdatePageResultJson UpdatePage(string accessTokenOrAppId, long pageId, string title, string description, string pageUrl,
            string iconUrl, string comment = null, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/shakearound/page/update?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    page_id = pageId,
                    title = title,
                    description = description,
                    page_url = pageUrl,
                    comment = comment,
                    icon_url = iconUrl
                };

                return CommonJsonSend.Send<UpdatePageResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }

        #region 查询页面列表

        private static string searchPageUrl =
            Config.ApiMpHost + "/shakearound/page/search?access_token={0}";

        /// <summary>
        /// 根据页面Id查询页面列表
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="pageIds">指定页面的Id数组</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static SearchPagesResultJson SearchPagesByPageId(string accessTokenOrAppId, long[] pageIds,
            int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var data = new
                {
                    page_ids = pageIds
                };

                return CommonJsonSend.Send<SearchPagesResultJson>(accessToken, searchPageUrl, data, CommonJsonSendType.POST,
                    timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 根据分页或者指定范围查询页面列表
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="begin"></param>
        /// <param name="count"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static SearchPagesResultJson SearchPagesByRange(string accessTokenOrAppId, int begin, int count,
            int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var data = new
                {
                    begin = begin,
                    count = count
                };

                return CommonJsonSend.Send<SearchPagesResultJson>(accessToken, searchPageUrl, data, CommonJsonSendType.POST,
                    timeOut);

            }, accessTokenOrAppId);
        }

        #endregion

        /// <summary>
        /// 删除页面
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="pageId">指定页面的Id数组</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult DeletePage(string accessTokenOrAppId, long pageId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/shakearound/page/delete?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    page_id = pageId
                };

                return CommonJsonSend.Send<WxJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 配置设备与页面的关联关系
        /// 配置设备与页面的关联关系。支持建立或解除关联关系，也支持新增页面或覆盖页面等操作。配置完成后，在此设备的信号范围内，即可摇出关联的页面信息。若设备配置多个页面，则随机出现页面信息。一个设备最多可配置30个关联页面。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="deviceIdentifier"></param>
        /// 设备编号，若填了UUID、major、minor，则可不填设备编号，若二者都填，则以设备编号为优先
        /// UUID、major、minor，三个信息需填写完整，若填了设备编号，则可不填此信息
        /// <param name="pageIds"></param>
        /// <param name="bindType">关联操作标志位， 0为解除关联关系，1为建立关联关系</param>
        /// <param name="appendType">新增操作标志位， 0为覆盖，1为新增</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult BindPage(string accessTokenOrAppId, DeviceApply_Data_Device_Identifiers deviceIdentifier, long[] pageIds, ShakeAroundBindType bindType, ShakeAroundAppendType appendType, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/shakearound/device/bindpage?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    device_identifier = deviceIdentifier,
                    page_ids = pageIds,
                    bind = (int)bindType,
                    append = (int)appendType
                };

                return CommonJsonSend.Send<WxJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        ///  查询设备的关联关系
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="deviceIdentifier">指定的设备</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static RelationSearchResultJson RelationSearch(string accessTokenOrAppId, DeviceApply_Data_Device_Identifiers deviceIdentifier, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/shakearound/relation/search?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    type = 1,
                    device_identifier = deviceIdentifier
                };

                return CommonJsonSend.Send<RelationSearchResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 查询页面的关联关系
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="pageId">指定的页面id</param>
        /// <param name="begin">关联关系列表的起始索引值</param>
        /// <param name="count">待查询的关联关系数量，不能超过50个</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static RelationSearchResultJson RelationSearch(string accessTokenOrAppId, long pageId, int begin, int count, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/shakearound/relation/search?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    type = 2,
                    page_id = pageId,
                    begin = begin,
                    count = count
                };

                return CommonJsonSend.Send<RelationSearchResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取摇周边的设备及用户信息
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="ticket">摇周边业务的ticket，可在摇到的URL中得到，ticket生效时间为30分钟，每一次摇都会重新生成新的ticket</param>
        /// <param name="needPoi">是否需要返回门店poi_id，传1则返回，否则不返回</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetShakeInfoResultJson GetShakeInfo(string accessTokenOrAppId, string ticket, int needPoi = 1,
            int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/shakearound/user/getshakeinfo?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    ticket = ticket,
                    need_poi = needPoi
                };

                return CommonJsonSend.Send<GetShakeInfoResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 以设备为维度的数据统计接口
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="deviceIdentifier">指定页面的设备ID</param>
        /// 设备编号，若填了UUID、major、minor，即可不填设备编号，二者选其一
        /// UUID、major、minor，三个信息需填写完成，若填了设备编辑，即可不填此信息，二者选其一
        /// <param name="beginDate">起始日期时间戳，最长时间跨度为30天</param>
        /// <param name="endDate">结束日期时间戳，最长时间跨度为30天</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static StatisticsResultJson StatisticsByDevice(string accessTokenOrAppId,
            DeviceApply_Data_Device_Identifiers deviceIdentifier, long beginDate, long endDate,
            int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/shakearound/statistics/device?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    device_identifier = deviceIdentifier,
                    begin_date = beginDate,
                    end_date = endDate
                };

                return CommonJsonSend.Send<StatisticsResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 以页面为维度的数据统计接口
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="pageId">指定页面的设备ID</param>
        /// <param name="beginDate">起始日期时间戳，最长时间跨度为30天</param>
        /// <param name="endDate">结束日期时间戳，最长时间跨度为30天</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static StatisticsResultJson StatisticsByPage(string accessTokenOrAppId,
           long pageId, long beginDate, long endDate,
           int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/shakearound/statistics/page?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    page_id = pageId,
                    begin_date = beginDate,
                    end_date = endDate
                };

                return CommonJsonSend.Send<StatisticsResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 批量查询设备统计数据接口
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="date">指定查询日期时间戳，单位为秒</param>
        /// <param name="pageIndex">指定查询的结果页序号；返回结果按摇周边人数降序排序，每50条记录为一页</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static DeviceListResultJson DeviceList(string accessTokenOrAppId, long date, string pageIndex, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/shakearound/statistics/devicelist?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    date = date,
                    page_index = pageIndex

                };

                return CommonJsonSend.Send<DeviceListResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 批量查询页面统计数据接口
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="date">指定查询日期时间戳，单位为秒</param>
        /// <param name="pageIndex">指定查询的结果页序号；返回结果按摇周边人数降序排序，每50条记录为一页</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static PageListResultJson PageList(string accessTokenOrAppId, long date, int pageIndex, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/shakearound/statistics/pagelist?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    date = date,
                    page_index = pageIndex

                };

                return CommonJsonSend.Send<PageListResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 新增分组
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="groupName">分组名称，不超过100汉字或200个英文字母</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GroupAddResultJson GroupAdd(string accessTokenOrAppId, string groupName, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/shakearound/device/group/add?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    group_name = groupName
                };

                return CommonJsonSend.Send<GroupAddResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 编辑分组信息
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="groupid">分组唯一标识，全局唯一</param>
        /// <param name="groupName">分组名称，不超过100汉字或200个英文字母</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static RegisterResultJson GroupUpdate(string accessTokenOrAppId, string groupid, string groupName, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/shakearound/device/group/update?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    group_id = groupid,
                    group_name = groupName


                };

                return CommonJsonSend.Send<RegisterResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 删除分组
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="groupId">分组唯一标识，全局唯一</param>
        /// <param name="timeOut"></param>

        /// <returns></returns>
        public static RegisterResultJson GroupDelete(string accessTokenOrAppId, string groupId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/shakearound/device/group/delete?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    group_id = groupId
                };

                return CommonJsonSend.Send<RegisterResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 查询分组列表
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="begin">分组列表的起始索引值</param>
        /// <param name="count">待查询的分组数量，不能超过1000个</param>
        /// <returns></returns>
        public static GroupGetListResultJson GroupGetList(string accessTokenOrAppId, int begin, int count, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/shakearound/device/group/getlist?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    begin = begin,
                    count = count
                };

                return CommonJsonSend.Send<GroupGetListResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 查询分组详情
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="groupId">分组唯一标识，全局唯一</param>
        /// <param name="begin">分组列表的起始索引值</param>
        /// <param name="count">待查询的分组数量，不能超过1000个</param>
        /// <returns></returns>
        public static GroupGetDetailResultJson GroupGetDetail(string accessTokenOrAppId, string groupId, int begin, int count, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/shakearound/device/group/getdetail?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    group_id = groupId,
                    begin = begin,
                    count = count
                };

                return CommonJsonSend.Send<GroupGetDetailResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 添加设备到分组
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="groupId">分组唯一标识，全局唯一</param>
        /// <param name="deviceIdentifier">分组列表的起始索引值</param>

        /// <returns></returns>
        public static RegisterResultJson GroupGetAdddevice(string accessTokenOrAppId, string groupId, List<DeviceApply_Data_Device_Identifiers> deviceIdentifiers, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/shakearound/device/group/adddevice?access_token={0}", accessToken.AsUrlData());
                object data = new
                {
                    group_id = groupId,
                    device_identifiers = deviceIdentifiers.Select(item =>
                    {
                        object itemData = null;
                        if (!item.device_id.HasValue)
                        {
                            /* UUID、major、minor，三个信息需填写完成，
                             * 若填了设备编号，即可不填此信息，二者选其一
                             */
                            itemData = new
                            {
                                uuid = item.uuid,
                                major = item.major,
                                minor = item.minor
                            };
                        }
                        else
                        {
                            /*设备编号，若填了UUID、major、minor，
                             * 即可不填设备编号，二者选其一
                             */
                            itemData = new
                            {
                                device_id = item.device_id
                            };
                        }
                        return itemData;
                    })
                };

                return CommonJsonSend.Send<RegisterResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 从分组中移除设备
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="groupId">分组唯一标识，全局唯一</param>
        /// <param name="deviceIdentifier">分组列表的起始索引值</param>

        /// <returns></returns>
        public static RegisterResultJson GroupDeleteDevice(string accessTokenOrAppId, string groupId, DeviceApply_Data_Device_Identifiers deviceIdentifier, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/shakearound/device/group/deletedevice?access_token={0}", accessToken.AsUrlData());
                var data = new object();
                if (!deviceIdentifier.device_id.HasValue)
                {
                    data = new
                    {
                        group_id = groupId,
                        uuid = deviceIdentifier.uuid,
                        major = deviceIdentifier.major,
                        minor = deviceIdentifier.minor

                    };
                }
                else
                {
                    data = new
                    {
                        group_id = groupId,
                        device_id = deviceIdentifier.device_id.Value
                    };
                }


                return CommonJsonSend.Send<RegisterResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 创建红包活动
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="title">抽奖活动名称（选择使用模板时，也作为摇一摇消息主标题），最长6个汉字，12个英文字母。</param>
        /// <param name="desc">抽奖活动描述（选择使用模板时，也作为摇一摇消息副标题），最长7个汉字，14个英文字母。</param>
        /// <param name="onoff">抽奖开关。0关闭，1开启，默认为1</param>
        /// <param name="beginTime">	抽奖活动开始时间，unix时间戳，单位秒</param>
        /// <param name="expireTime">	抽奖活动结束时间，unix时间戳，单位秒,红包活动有效期最长为91天</param>
        /// <param name="sponsorAppid">红包提供商户公众号的appid，需与预下单中的公众账号appid（wxappid）一致</param>
        /// <param name="total">红包总数，红包总数是录入红包ticket总数的上限，因此红包总数应该大于等于预下单时红包ticket总数。</param>
        /// <param name="jumpUrl">红包关注界面后可以跳转到第三方自定义的页面</param>
        /// <param name="key">开发者自定义的key，用来生成活动抽奖接口的签名参数，长度32位。使用方式见sign生成规则</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        public static AddLotteryInfoResultJson AddLotteryInfo(string accessTokenOrAppId, string title, string desc, int onoff, long beginTime, long expireTime, string sponsorAppid, long total, string jumpUrl, string key, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/shakearound/lottery/addlotteryinfo?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    title = title,
                    desc = desc,
                    onoff = onoff,
                    begin_time = beginTime,
                    expire_time = expireTime,
                    sponsor_appid = sponsorAppid,
                    total = total,
                    jumpurl = jumpUrl,
                    key = key
                };

                return CommonJsonSend.Send<AddLotteryInfoResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 录入红包信息
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="lotteryId">红包抽奖id，来自addlotteryinfo返回的lottery_id</param>
        /// <param name="mchid">红包提供者的商户号，，需与预下单中的商户号mch_id一致</param>
        /// <param name="sponsorAppid">红包提供商户公众号的appid，需与预下单中的公众账号appid（wxappid）一致</param>
        /// <param name="prizeInfoList">红包ticket列表，如果红包数较多，可以一次传入多个红包，批量调用该接口设置红包信息。每次请求传入的红包个数上限为100</param>

        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static SetPrizeBucketResultJson SetPrizeBucket(string accessTokenOrAppId, string lotteryId, string mchid, string sponsorAppid, PrizeInfoList prizeInfoList, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/shakearound/lottery/setprizebucket?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    lottery_id = lotteryId,
                    mchid = mchid,
                    sponsorapp_id = sponsorAppid,
                    prizeinfolist = prizeInfoList


                };

                return CommonJsonSend.Send<SetPrizeBucketResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        ///设置红包活动抽奖开关
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="lotteryId">红包抽奖id，来自addlotteryinfo返回的lottery_id</param>
        /// <param name="onOff">活动抽奖开关，0：关闭，1：开启</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult SetLotterySwitch(string accessTokenOrAppId, string lotteryId, int onOff, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/shakearound/lottery/setlotteryswitch?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    lottery_id = lotteryId,
                    onoff = onOff
                };

                return CommonJsonSend.Send<WxJsonResult>(null, url, data, CommonJsonSendType.GET, timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        ///红包查询接口
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="lotteryId">红包抽奖id，来自addlotteryinfo返回的lottery_id</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static QueryLotteryJsonResult QueryLottery(string accessTokenOrAppId, string lotteryId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/shakearound/lottery/querylottery?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    lottery_id = lotteryId
                };

                return CommonJsonSend.Send<QueryLotteryJsonResult>(null, url, data, CommonJsonSendType.GET, timeOut);

            }, accessTokenOrAppId);
        }
        #endregion

#if !NET35 && !NET40
        #region 异步方法
        /// <summary>
        /// 【异步方法】申请开通功能
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data"></param>
        /// <param name="industry_Id"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<RegisterResultJson> RegisterAsync(string accessTokenOrAppId, RegisterData data, IndustryId industry_Id, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string url = string.Format(Config.ApiMpHost + "/shakearound/account/register?access_token={0}", accessToken.AsUrlData());

                data.industry_id = RegisterData.ConvertIndustryId(industry_Id);
                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<RegisterResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】查询审核状态
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <returns></returns>
        public static async Task<GetAuditStatusResultJson> GetAuditStatusAsync(string accessTokenOrAppId)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string url = string.Format(Config.ApiMpHost + "/shakearound/account/auditstatus?access_token={0}", accessToken.AsUrlData());

                return await Get.GetJsonAsync<GetAuditStatusResultJson>(url);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】申请设备ID
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="quantity">申请的设备ID的数量，单次新增设备超过500个，需走人工审核流程</param>
        /// <param name="applyReason">申请理由，不超过100个字</param>
        /// <param name="comment">备注，不超过15个汉字或30个英文字母</param>
        /// <param name="poiId">设备关联的门店ID，关联门店后，在门店1KM的范围内有优先摇出信息的机会。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<DeviceApplyResultJson> DeviceApplyAsync(string accessTokenOrAppId, int quantity, string applyReason, string comment = null, long? poiId = null, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string url = string.Format(Config.ApiMpHost + "/shakearound/device/applyid?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    quantity = quantity,
                    apply_reason = applyReason,
                    comment = comment,
                    poi_id = poiId
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<DeviceApplyResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }


        /// <summary>
        /// 【异步方法】查询设备ID申请审核状态
        /// 接口说明 查询设备ID申请的审核状态。若单次申请的设备ID数量小于等于500个，系统会进行快速审核；若单次申请的设备ID数量大于500个，则在三个工作日内完成审核。
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="appId">批次ID，申请设备ID时所返回的批次ID</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetDeviceStatusResultJson> DeviceApplyStatusAsync(string accessTokenOrAppId, long appId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string url = string.Format(Config.ApiMpHost + "/shakearound/device/applystatus?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    apply_id = appId,
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetDeviceStatusResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】编辑设备信息
        /// 设备编号，若填了UUID、major、minor，则可不填设备编号，若二者都填，则以设备编号为优先
        /// UUID、major、minor，三个信息需填写完整，若填了设备编号，则可不填此信息。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="deviceId">设备编号</param>
        /// <param name="uuId"></param>
        /// <param name="major"></param>
        /// <param name="minor"></param>
        /// <param name="comment"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WxJsonResult> DeviceUpdateAsync(string accessTokenOrAppId, long deviceId, string uuId, long major, long minor, string comment, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string url = string.Format(Config.ApiMpHost + "/shakearound/device/update?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    device_identifier = new
                    {
                        device_id = deviceId,
                        uuid = uuId,
                        major = major,
                        minor = minor
                    },
                    comment = comment
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】配置设备与门店的关联关系
        /// 设备编号，若填了UUID、major、minor，则可不填设备编号，若二者都填，则以设备编号为优先
        /// UUID、major、minor，三个信息需填写完整，若填了设备编号，则可不填此信息。
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="deviceId">设备编号</param>
        /// <param name="uuid"></param>
        /// <param name="major"></param>
        /// <param name="minor"></param>
        /// <param name="poiId">Poi_id 的说明改为：设备关联的门店ID，关联门店后，在门店1KM的范围内有优先摇出信息的机会。</param>
        /// <param name="type">为1时，关联的门店和设备归属于同一公众账号；为2时，关联的门店为其他公众账号的门店。不填默认为1</param>
        /// <param name="timeOut"></param>
        /// <param name="poiAppid">当Type为2时，必填	关联门店所归属的公众账号的APPID</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> DeviceBindLocatoinAsync(string accessTokenOrAppId, long deviceId, string uuid, long major, long minor, long poiId, string poiAppid, int type = 1, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string url = string.Format(Config.ApiMpHost + "/shakearound/device/bindlocation?access_token={0}", accessToken.AsUrlData());

                var data = type == 2
                    ? new
                    {
                        device_identifier = new
                        {
                            device_id = deviceId,
                            uuid = uuid,
                            major = major,
                            minor = minor
                        },
                        poi_id = poiId,
                        type = type,
                        poi_appid = poiAppid
                    } as object
                    : new
                    {
                        device_identifier = new
                        {
                            device_id = deviceId,
                            uuid = uuid,
                            major = major,
                            minor = minor
                        },
                        poi_id = poiId
                    } as object;


                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】配置设备与其他门店的关联关系
        /// 设备编号，若填了UUID、major、minor，则可不填设备编号，若二者都填，则以设备编号为优先
        /// UUID、major、minor，三个信息需填写完整，若填了设备编号，则可不填此信息。
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="deviceId">设备编号</param>
        /// <param name="uuid"></param>
        /// <param name="major"></param>
        /// <param name="minor"></param>
        /// <param name="poiId">Poi_id 的说明改为：设备关联的门店ID，关联门店后，在门店1KM的范围内有优先摇出信息的机会。</param>
        /// <param name="timeOut"></param>
        /// <param name="type">为1时，关联的门店和设备归属于同一公众账号；为2时，关联的门店为其他公众账号的门店。不填默认为1</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> DeviceBindLocatoinAsync(string accessTokenOrAppId, long deviceId, string uuid, long major, long minor, long poiId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string url = string.Format(Config.ApiMpHost + "/shakearound/device/bindlocation?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    device_identifier = new
                    {
                        device_id = deviceId,
                        uuid = uuid,
                        major = major,
                        minor = minor
                    },
                    poi_id = poiId
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }

        #region 查询设备列表

        /// <summary>
        /// 【异步方法】查询设备列表Api url
        /// </summary>
        // private static string searchDeviceUrl = Config.ApiMpHost + "/shakearound/device/search?access_token={0}";

        /// <summary>
        /// 根据指定的设备Id查询设备列表
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="deviceIdentifiers">设备Id列表</param>
        /// 设备编号，若填了UUID、major、minor，则可不填设备编号，若二者都填，则以设备编号为优先
        /// UUID、major、minor，三个信息需填写完整，若填了设备编号，则可不填此信息。
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<DeviceSearchResultJson> SearchDeviceByIdAsync(string accessTokenOrAppId, List<DeviceApply_Data_Device_Identifiers> deviceIdentifiers, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var data = new
                {
                    device_identifiers = deviceIdentifiers
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<DeviceSearchResultJson>(accessToken, searchDeviceUrl, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】根据分页查询或者指定范围查询设备列表
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="begin"></param>
        /// <param name="count"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<DeviceSearchResultJson> SearchDeviceByRangeAsync(string accessTokenOrAppId, int begin, int count, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var data = new
                {
                    begin = begin,
                    count = count
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<DeviceSearchResultJson>(accessToken, searchDeviceUrl, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】根据批次ID查询设备列表
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="applyId"></param>
        /// <param name="begin"></param>
        /// <param name="count"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<DeviceSearchResultJson> SearchDeviceByApplyIdAsync(string accessTokenOrAppId, long applyId, int begin, int count, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var data = new
                {
                    apply_id = applyId,
                    begin = begin,
                    count = count
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<DeviceSearchResultJson>(accessToken, searchDeviceUrl, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }

        #endregion

        /// <summary>
        /// 【异步方法】上传图片素材
        /// 上传在摇一摇页面展示的图片素材，素材保存在微信侧服务器上。 格式限定为：jpg,jpeg,png,gif，图片大小建议120px*120 px，限制不超过200 px *200 px，图片需为正方形。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="file"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<UploadImageResultJson> UploadImageAsync(string accessTokenOrAppId, string file, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/shakearound/material/add?access_token={0}", accessToken.AsUrlData());
                var fileDictionary = new Dictionary<string, string>();
                fileDictionary["media"] = file;
                return await Post.PostFileGetJsonAsync<UploadImageResultJson>(url, null, fileDictionary, null, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】新增页面
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="title">在摇一摇页面展示的主标题，不超过6个字</param>
        /// <param name="description">在摇一摇页面展示的副标题，不超过7个字</param>
        /// <param name="pageUrl">点击页面跳转链接</param>
        /// <param name="iconUrl">在摇一摇页面展示的图片。图片需先上传至微信侧服务器，用“素材管理-上传图片素材”接口上传图片，返回的图片URL再配置在此处</param>
        /// <param name="comment">页面的备注信息，不超过15个字</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<AddPageResultJson> AddPageAsync(string accessTokenOrAppId, string title, string description, string pageUrl,
            string iconUrl, string comment = null, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/shakearound/page/add?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    title = title,
                    description = description,
                    page_url = pageUrl,
                    comment = comment,
                    icon_url = iconUrl
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<AddPageResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】编辑页面信息
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="pageId">摇周边页面唯一ID</param>
        /// <param name="title">在摇一摇页面展示的主标题，不超过6个字</param>
        /// <param name="description">在摇一摇页面展示的副标题，不超过7个字</param>
        /// <param name="pageUrl">点击页面跳转链接</param>
        /// <param name="iconUrl">在摇一摇页面展示的图片。图片需先上传至微信侧服务器，用“素材管理-上传图片素材”接口上传图片，返回的图片URL再配置在此处</param>
        /// <param name="comment">页面的备注信息，不超过15个字</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<UpdatePageResultJson> UpdatePageAsync(string accessTokenOrAppId, long pageId, string title, string description, string pageUrl,
            string iconUrl, string comment = null, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/shakearound/page/update?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    page_id = pageId,
                    title = title,
                    description = description,
                    page_url = pageUrl,
                    comment = comment,
                    icon_url = iconUrl
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<UpdatePageResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }

        #region 查询页面列表

        /*private static string searchPageUrl =Config.ApiMpHost + "/shakearound/page/search?access_token={0}";*/

        /// <summary>
        /// 【异步方法】根据页面Id查询页面列表
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="pageIds">指定页面的Id数组</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<SearchPagesResultJson> SearchPagesByPageIdAsync(string accessTokenOrAppId, long[] pageIds,
int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var data = new
                {
                    page_ids = pageIds
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<SearchPagesResultJson>(accessToken, searchPageUrl, data, CommonJsonSendType.POST,
                    timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】根据分页或者指定范围查询页面列表
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="lastSeen"></param>
        /// <param name="count"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<SearchPagesResultJson> SearchPagesByRangeAsync(string accessTokenOrAppId, long lastSeen, int count,
            int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var data = new
                {
                    last_seen = lastSeen,
                    count = count
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<SearchPagesResultJson>(accessToken, searchPageUrl, data, CommonJsonSendType.POST,
                    timeOut);

            }, accessTokenOrAppId);
        }

        #endregion

        /// <summary>
        /// 【异步方法】删除页面
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="pageId">指定页面的Id数组</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WxJsonResult> DeletePageAsync(string accessTokenOrAppId, long pageId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/shakearound/page/delete?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    page_id = pageId
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】配置设备与页面的关联关系
        /// 配置设备与页面的关联关系。支持建立或解除关联关系，也支持新增页面或覆盖页面等操作。配置完成后，在此设备的信号范围内，即可摇出关联的页面信息。若设备配置多个页面，则随机出现页面信息。一个设备最多可配置30个关联页面。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="deviceIdentifier"></param>
        /// 设备编号，若填了UUID、major、minor，则可不填设备编号，若二者都填，则以设备编号为优先
        /// UUID、major、minor，三个信息需填写完整，若填了设备编号，则可不填此信息
        /// <param name="pageIds"></param>
        /// <param name="bindType">关联操作标志位， 0为解除关联关系，1为建立关联关系</param>
        /// <param name="appendType">新增操作标志位， 0为覆盖，1为新增</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WxJsonResult> BindPageAsync(string accessTokenOrAppId, DeviceApply_Data_Device_Identifiers deviceIdentifier, long[] pageIds, ShakeAroundBindType bindType, ShakeAroundAppendType appendType, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/shakearound/device/bindpage?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    device_identifier = deviceIdentifier,
                    page_ids = pageIds,
                    bind = (int)bindType,
                    append = (int)appendType
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】查询设备的关联关系
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="deviceIdentifier">指定的设备</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<RelationSearchResultJson> RelationSearchAsync(string accessTokenOrAppId, DeviceApply_Data_Device_Identifiers deviceIdentifier, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/shakearound/relation/search?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    type = 1,
                    device_identifier = deviceIdentifier
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<RelationSearchResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】查询页面的关联关系
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="pageId">指定的页面id</param>
        /// <param name="begin">关联关系列表的起始索引值</param>
        /// <param name="count">待查询的关联关系数量，不能超过50个</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<RelationSearchResultJson> RelationSearchAsync(string accessTokenOrAppId, long pageId, int begin, int count, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/shakearound/relation/search?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    type = 2,
                    page_id = pageId,
                    begin = begin,
                    count = count
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<RelationSearchResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】获取摇周边的设备及用户信息
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="ticket">摇周边业务的ticket，可在摇到的URL中得到，ticket生效时间为30分钟，每一次摇都会重新生成新的ticket</param>
        /// <param name="needPoi">是否需要返回门店poi_id，传1则返回，否则不返回</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetShakeInfoResultJson> GetShakeInfoAsync(string accessTokenOrAppId, string ticket, int needPoi = 1,
            int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/shakearound/user/getshakeinfo?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    ticket = ticket,
                    need_poi = needPoi
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetShakeInfoResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】以设备为维度的数据统计接口
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="deviceIdentifier">指定页面的设备ID</param>
        /// 设备编号，若填了UUID、major、minor，即可不填设备编号，二者选其一
        /// UUID、major、minor，三个信息需填写完成，若填了设备编辑，即可不填此信息，二者选其一
        /// <param name="beginDate">起始日期时间戳，最长时间跨度为30天</param>
        /// <param name="endDate">结束日期时间戳，最长时间跨度为30天</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<StatisticsResultJson> StatisticsByDeviceAsync(string accessTokenOrAppId,
            DeviceApply_Data_Device_Identifiers deviceIdentifier, long beginDate, long endDate,
            int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/shakearound/statistics/device?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    device_identifier = deviceIdentifier,
                    begin_date = beginDate,
                    end_date = endDate
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<StatisticsResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】以页面为维度的数据统计接口
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="pageId">指定页面的设备ID</param>
        /// <param name="beginDate">起始日期时间戳，最长时间跨度为30天</param>
        /// <param name="endDate">结束日期时间戳，最长时间跨度为30天</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<StatisticsResultJson> StatisticsByPageAsync(string accessTokenOrAppId,
           long pageId, long beginDate, long endDate,
           int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/shakearound/statistics/page?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    page_id = pageId,
                    begin_date = beginDate,
                    end_date = endDate
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<StatisticsResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 【异步方法】批量查询设备统计数据接口
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="date">指定查询日期时间戳，单位为秒</param>
        /// <param name="pageIndex">指定查询的结果页序号；返回结果按摇周边人数降序排序，每50条记录为一页</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<DeviceListResultJson> DeviceListAsync(string accessTokenOrAppId, long date, string pageIndex, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/shakearound/statistics/devicelist?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    date = date,
                    page_index = pageIndex

                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<DeviceListResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 【异步方法】批量查询页面统计数据接口
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="date">指定查询日期时间戳，单位为秒</param>
        /// <param name="pageIndex">指定查询的结果页序号；返回结果按摇周边人数降序排序，每50条记录为一页</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<PageListResultJson> PageListAsync(string accessTokenOrAppId, long date, int pageIndex, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/shakearound/statistics/pagelist?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    date = date,
                    page_index = pageIndex

                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<PageListResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 【异步方法】新增分组
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="groupName">分组名称，不超过100汉字或200个英文字母</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GroupAddResultJson> GroupAddAsync(string accessTokenOrAppId, string groupName, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/shakearound/device/group/add?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    group_name = groupName


                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GroupAddResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 【异步方法】编辑分组信息
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="groupid">分组唯一标识，全局唯一</param>
        /// <param name="groupName">分组名称，不超过100汉字或200个英文字母</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<RegisterResultJson> GroupUpdateAsync(string accessTokenOrAppId, string groupid, string groupName, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/shakearound/device/group/update?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    group_id = groupid,
                    group_name = groupName


                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<RegisterResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 【异步方法】删除分组
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="groupId">分组唯一标识，全局唯一</param>
        /// <param name="timeOut"></param>

        /// <returns></returns>
        public static async Task<RegisterResultJson> GroupDeleteAsync(string accessTokenOrAppId, string groupId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/shakearound/device/group/delete?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    group_id = groupId
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<RegisterResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 【异步方法】查询分组列表
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="begin">分组列表的起始索引值</param>
        /// <param name="count">待查询的分组数量，不能超过1000个</param>
        /// <returns></returns>
        public static async Task<GroupGetListResultJson> GroupGetListAsync(string accessTokenOrAppId, int begin, int count, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/shakearound/device/group/getlist?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    begin = begin,
                    count = count
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GroupGetListResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 【异步方法】查询分组详情
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="groupId">分组唯一标识，全局唯一</param>
        /// <param name="begin">分组列表的起始索引值</param>
        /// <param name="count">待查询的分组数量，不能超过1000个</param>
        /// <returns></returns>
        public static async Task<GroupGetDetailResultJson> GroupGetDetailAsync(string accessTokenOrAppId, string groupId, int begin, int count, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/shakearound/device/group/getdetail?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    group_id = groupId,
                    begin = begin,
                    count = count
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GroupGetDetailResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 【异步方法】添加设备到分组
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="groupId">分组唯一标识，全局唯一</param>
        /// <param name="deviceIdentifiers">分组列表的起始索引值</param>
        /// <returns></returns>
        public static async Task<RegisterResultJson> GroupGetAdddeviceAsync(string accessTokenOrAppId, string groupId, List<DeviceApply_Data_Device_Identifiers> deviceIdentifiers, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
           {
               var url = string.Format(Config.ApiMpHost + "/shakearound/device/group/adddevice?access_token={0}", accessToken.AsUrlData());
               object data = new
               {
                   group_id = groupId,
                   device_identifiers = deviceIdentifiers.Select(item =>
                   {
                       object itemData = null;
                       if (!item.device_id.HasValue)
                       {
                           /* UUID、major、minor，三个信息需填写完成，
                            * 若填了设备编号，即可不填此信息，二者选其一
                            */
                           itemData = new
                           {
                               uuid = item.uuid,
                               major = item.major,
                               minor = item.minor
                           };
                       }
                       else
                       {
                           /*设备编号，若填了UUID、major、minor，
                            * 即可不填设备编号，二者选其一
                            */
                           itemData = new
                           {
                               device_id = item.device_id
                           };
                       }
                       return itemData;
                   })
               };

               return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<RegisterResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);

           }, accessTokenOrAppId);
        }
        /// <summary>
        /// 【异步方法】从分组中移除设备
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="groupId">分组唯一标识，全局唯一</param>
        /// <param name="deviceIdentifier">分组列表的起始索引值</param>

        /// <returns></returns>
        public static async Task<RegisterResultJson> GroupDeleteDeviceAsync(string accessTokenOrAppId, string groupId, DeviceApply_Data_Device_Identifiers deviceIdentifier, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
           {
               var url = string.Format(Config.ApiMpHost + "/shakearound/device/group/deletedevice?access_token={0}", accessToken.AsUrlData());
               var data = new object();
               if (!deviceIdentifier.device_id.HasValue)
               {
                   data = new
                   {
                       group_id = groupId,
                       uuid = deviceIdentifier.uuid,
                       major = deviceIdentifier.major,
                       minor = deviceIdentifier.minor

                   };
               }
               else
               {
                   data = new
                   {
                       group_id = groupId,
                       device_id = deviceIdentifier.device_id.Value
                   };
               }


               return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<RegisterResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);

           }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】创建红包活动
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="title">抽奖活动名称（选择使用模板时，也作为摇一摇消息主标题），最长6个汉字，12个英文字母。</param>
        /// <param name="desc">抽奖活动描述（选择使用模板时，也作为摇一摇消息副标题），最长7个汉字，14个英文字母。</param>
        /// <param name="onoff">抽奖开关。0关闭，1开启，默认为1</param>
        /// <param name="beginTime">	抽奖活动开始时间，unix时间戳，单位秒</param>
        /// <param name="expireTime">	抽奖活动结束时间，unix时间戳，单位秒,红包活动有效期最长为91天</param>
        /// <param name="sponsorAppid">红包提供商户公众号的appid，需与预下单中的公众账号appid（wxappid）一致</param>
        /// <param name="total">红包总数，红包总数是录入红包ticket总数的上限，因此红包总数应该大于等于预下单时红包ticket总数。</param>
        /// <param name="jumpUrl">红包关注界面后可以跳转到第三方自定义的页面</param>
        /// <param name="key">开发者自定义的key，用来生成活动抽奖接口的签名参数，长度32位。使用方式见sign生成规则</param>
        /// <param name="timeOut">请求超时时间</param>
        /// <returns></returns>
        public static async Task<AddLotteryInfoResultJson> AddLotteryInfoAsync(string accessTokenOrAppId, string title, string desc, int onoff, long beginTime, long expireTime, string sponsorAppid, long total, string jumpUrl, string key, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
           {
               var url = string.Format(Config.ApiMpHost + "/shakearound/lottery/addlotteryinfo?access_token={0}", accessToken.AsUrlData());

               var data = new
               {
                   title = title,
                   desc = desc,
                   onoff = onoff,
                   begin_time = beginTime,
                   expire_time = expireTime,
                   sponsor_appid = sponsorAppid,
                   total = total,
                   jumpurl = jumpUrl,
                   key = key
               };

               return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<AddLotteryInfoResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);

           }, accessTokenOrAppId);
        }
        /// <summary>
        /// 【异步方法】录入红包信息
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="lotteryId">红包抽奖id，来自addlotteryinfo返回的lottery_id</param>
        /// <param name="mchid">红包提供者的商户号，，需与预下单中的商户号mch_id一致</param>
        /// <param name="sponsorAppid">红包提供商户公众号的appid，需与预下单中的公众账号appid（wxappid）一致</param>
        /// <param name="prizeInfoList">红包ticket列表，如果红包数较多，可以一次传入多个红包，批量调用该接口设置红包信息。每次请求传入的红包个数上限为100</param>

        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<SetPrizeBucketResultJson> SetPrizeBucketAsync(string accessTokenOrAppId, string lotteryId, string mchid, string sponsorAppid, PrizeInfoList prizeInfoList, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
           {
               var url = string.Format(Config.ApiMpHost + "/shakearound/lottery/setprizebucket?access_token={0}", accessToken.AsUrlData());

               var data = new
               {
                   lottery_id = lotteryId,
                   mchid = mchid,
                   sponsorapp_id = sponsorAppid,
                   prizeinfolist = prizeInfoList


               };

               return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<SetPrizeBucketResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);

           }, accessTokenOrAppId);
        }
        /// <summary>
        ///【异步方法】设置红包活动抽奖开关
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="lotteryId">红包抽奖id，来自addlotteryinfo返回的lottery_id</param>
        /// <param name="onOff">活动抽奖开关，0：关闭，1：开启</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WxJsonResult> SetLotterySwitchAsync(string accessTokenOrAppId, string lotteryId, int onOff, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
           {
               var url = string.Format(Config.ApiMpHost + "/shakearound/lottery/setlotteryswitch?access_token={0}", accessToken.AsUrlData());

               var data = new
               {
                   lottery_id = lotteryId,
                   onoff = onOff
               };

               return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(null, url, data, CommonJsonSendType.GET, timeOut);

           }, accessTokenOrAppId);
        }
        /// <summary>
        ///【异步方法】红包查询接口
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="lotteryId">红包抽奖id，来自addlotteryinfo返回的lottery_id</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<QueryLotteryJsonResult> QueryLotteryAsync(string accessTokenOrAppId, string lotteryId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
           {
               var url = string.Format(Config.ApiMpHost + "/shakearound/lottery/querylottery?access_token={0}", accessToken.AsUrlData());

               var data = new
               {
                   lottery_id = lotteryId
               };

               return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<QueryLotteryJsonResult>(null, url, data, CommonJsonSendType.GET, timeOut);

           }, accessTokenOrAppId);
        }
        #endregion
#endif
    }

}
