#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2023 chinanhb & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
/*----------------------------------------------------------------
    Copyright (C) 2023 Senparc
    
    文件名：WeiSpaceApi
    文件功能描述：企业微信微盘接口
    
    
    创建标识：chinanhb - 20230831
----------------------------------------------------------------*/

/*
    API：https://developer.work.weixin.qq.com/document/path/93654
    
 */
#endregion Apache License Version 2.0
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Work.AdvancedAPIs.WeDrive.WeDriveJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDrive
{
    /// <summary>
    /// 企业微信微盘接口
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_Work, true)]
    public class WeDriveApi
    {
        #region 同步方法
        /// <summary>
        /// 微盘内新建空间，创建者为应用本身
        /// </summary>
        /// <param name="accessTokenOrAppKey">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static SpaceCreateJsonResult SpaceCreate(string accessTokenOrAppKey, SpaceCreateModel data, int timeOut = Config.TIME_OUT)
        {
            ///https://developer.work.weixin.qq.com/document/path/93655
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {

                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/wedrive/space_create?access_token={0}", accessToken);
                return CommonJsonSend.Send<SpaceCreateJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 重命名已有空间
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WorkJsonResult SpaceRename(string accessTokenOrAppKey, SpaceRenameModel data, int timeOut = Config.TIME_OUT)
        {
            ///https://developer.work.weixin.qq.com/document/path/97856
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/wedrive/space_rename?access_token={0}", accessToken);
                return CommonJsonSend.Send<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 解散已有空间
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WorkJsonResult SpaceDismiss(string accessTokenOrAppKey, SpaceDismissModel data, int timeOut = Config.TIME_OUT)
        {
            ///https://developer.work.weixin.qq.com/document/path/97857
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/wedrive/space_dismiss?access_token={0}", accessToken);
                return CommonJsonSend.Send<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 获取空间信息
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static SpaceInfoJsonResult SpaceInfo(string accessTokenOrAppKey, SpaceInfoModel data, int timeOut = Config.TIME_OUT)
        {
            ///https://developer.work.weixin.qq.com/document/path/97858
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/wedrive/space_info?access_token={0}", accessToken);
                return CommonJsonSend.Send<SpaceInfoJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 指定空间添加成员/部门，可一次性添加多个
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WorkJsonResult SpaceAclAdd(string accessTokenOrAppKey,SpaceAclAddModel data,int timeOut=Config.TIME_OUT)
        {
            ///https://developer.work.weixin.qq.com/document/path/93656
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/wedrive/space_acl_add?access_token={0}", accessToken);
                return CommonJsonSend.Send<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 指定空间移除成员/部门，操作者为应用本身
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WorkJsonResult SpaceAclDel(string accessTokenOrAppKey, SpaceAclDelModel data, int timeOut = Config.TIME_OUT)
        {
            ///https://developer.work.weixin.qq.com/document/path/97875
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/wedrive/space_acl_del?access_token={0}", accessToken);
                return CommonJsonSend.Send<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 修改空间权限，应用通过api调用仅支持设置由本应用创建的空间。
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WorkJsonResult SpaceSetting(string accessTokenOrAppKey, SpaceAclDelModel data, int timeOut = Config.TIME_OUT)
        {
            ///https://developer.work.weixin.qq.com/document/path/97876
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/wedrive/space_setting?access_token={0}", accessToken);
                return CommonJsonSend.Send<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 获取空间邀请分享链接。
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static SpaceShareJsonResult SpaceShare(string accessTokenOrAppKey, SpaceShareModel data, int timeOut = Config.TIME_OUT)
        {
            ///https://developer.work.weixin.qq.com/document/path/97877
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/wedrive/space_share?access_token={0}", accessToken);
                return CommonJsonSend.Send<SpaceShareJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 获取空间信息。包括：空间成员及权限及安全设置
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="spaceid"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static SpaceInfoJsonResult NewSpaceInfo(string accessTokenOrAppKey,string spaceid,int timeOut = Config.TIME_OUT)
        {
            ///https://developer.work.weixin.qq.com/document/path/97878
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var data = new
                {
                    spaceid
                };
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/wedrive/new_space_info?access_token={0}", accessToken);
                return CommonJsonSend.Send<SpaceInfoJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 获取指定地址下的文件列表
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static FileListJsonResult FileList(string accessTokenOrAppKey,FileListModel data,int timeOut = Config.TIME_OUT)
        {
            ///https://developer.work.weixin.qq.com/document/path/93657
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/wedrive/file_list?access_token={0}", accessToken);
                return CommonJsonSend.Send<FileListJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 向微盘中的指定位置上传文件。
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static FileUploadJsonResult FileUpload(string accessTokenOrAppKey,FileUploadModel data,int timeOut = Config.TIME_OUT)
        {
            ///https://developer.work.weixin.qq.com/document/path/97880
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/wedrive/file_upload?access_token={0}", accessToken);
                return CommonJsonSend.Send<FileUploadJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static FileDownLoadJsonResult FileDownLoad(string accessTokenOrAppKey,FileDownLoadModel data,int timeOut = Config.TIME_OUT)
        {
            ///https://developer.work.weixin.qq.com/document/path/97881
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/wedrive/file_download?access_token={0}", accessToken);
                return CommonJsonSend.Send<FileDownLoadJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            },accessTokenOrAppKey);
        }
        /// <summary>
        /// 新建文件夹、文档
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static FileCreateJsonResult FileCreate(string accessTokenOrAppKey,FileCreateModel data,int timeOut=Config.TIME_OUT)
        {
            ///https://developer.work.weixin.qq.com/document/path/97882
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/wedrive/file_create?access_token={0}", accessToken);
                return CommonJsonSend.Send<FileCreateJsonResult>(null, url,data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 指定文件进行重命名。
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static FileRenameJsonResult FileRename(string accessTokenOrAppKey,FileRenameModel data,int timeOut = Config.TIME_OUT)
        {
            ///https://developer.work.weixin.qq.com/document/path/97883
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/wedrive/file_rename?access_token={0}", accessToken);
                return CommonJsonSend.Send<FileRenameJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 将文件移动到指定位置
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static FileMoveJsonResult FileMove(string accessTokenOrAppKey,FileMoveModel data,int timeOut=Config.TIME_OUT)
        {
            ///https://developer.work.weixin.qq.com/document/path/97884
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/wedrive/file_move?access_token={0}", accessToken);
                return CommonJsonSend.Send<FileMoveJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 删除指定文件
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WorkJsonResult FileDelete(string accessTokenOrAppKey,FileDeleteModel data,int timeOut=Config.TIME_OUT) 
        {
            ///https://developer.work.weixin.qq.com/document/path/97885
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/wedrive/file_delete?access_token={0}", accessToken);
                return CommonJsonSend.Send<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 获取指定文件的信息
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="Fileid"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static FileInfoJsonRetult FileInfo(string accessTokenOrAppKey,string Fileid,int timeOut=Config.TIME_OUT)
        {
            ///https://developer.work.weixin.qq.com/document/path/97886
            var data = new
            {
                fileid = Fileid
            };
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/wedrive/file_info?access_token={0}", accessToken);
                return CommonJsonSend.Send<FileInfoJsonRetult>(null, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 指定文件添加成员
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WorkJsonResult FileAclAdd(string accessTokenOrAppKey,FileAclAddModel data,int timeOut= Config.TIME_OUT)
        {
            ///https://developer.work.weixin.qq.com/document/path/93658
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/wedrive/file_acl_add?access_token={0}", accessToken);
                return CommonJsonSend.Send<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 删除指定文件的成员
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WorkJsonResult FileAclDel(string accessTokenOrAppKey,FileAclDelModel data,int timeOut=Config.TIME_OUT)
        {
            ///https://developer.work.weixin.qq.com/document/path/97888
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/wedrive/file_acl_del?access_token={0}", accessToken);
                return CommonJsonSend.Send<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 文件的分享设置
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WorkJsonResult FileSetting(string accessTokenOrAppKey,FileSettingModel data,int timeOut= Config.TIME_OUT)
        {
            ///https://developer.work.weixin.qq.com/document/path/97889
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/wedrive/file_setting?access_token={0}", accessToken);
                return CommonJsonSend.Send<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 获取分享链接
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="FileId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static FileShareJsonResult FileShare(string accessTokenOrAppKey,string FileId,int timeOut= Config.TIME_OUT)
        {
            var data = new
            {
                fileid = FileId
            };
            ///https://developer.work.weixin.qq.com/document/path/97890
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/wedrive/file_share?access_token={0}", accessToken);
                return CommonJsonSend.Send<FileShareJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 获取文件权限信息
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="FileId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static FilePermissionJsonResult FilePermission(string accessTokenOrAppKey,string FileId,int timeOut=Config.TIME_OUT)
        {
            ///https://developer.work.weixin.qq.com/document/path/97891
            var data = new
            {
                fileid = FileId
            };
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/wedrive/get_file_permission?access_token={0}", accessToken);
                return CommonJsonSend.Send<FilePermissionJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 修改文件安全设置
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WorkJsonResult FileSecureSetting(string accessTokenOrAppKey,FileSecureSettingModel data,int  timeOut=Config.TIME_OUT)
        {
            ///https://developer.work.weixin.qq.com/document/path/97892
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/wedrive/file_secure_setting?access_token={0}", accessToken);
                return CommonJsonSend.Send<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 获取盘专业版信息
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="userId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static MngProInfoJsonResult MngProInfo(string accessTokenOrAppKey,string userId,int timeOut=Config.TIME_OUT)
        {
            ///https://developer.work.weixin.qq.com/document/path/95856
            var data = new
            {
                userid = userId
            };
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/wedrive/mng_pro_info?access_token={0}", accessToken);
                return CommonJsonSend.Send<MngProInfoJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 获取盘容量信息
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static MngCapacityJsonResult MngCapacity(string accessTokenOrAppKey,int timeOut=Config.TIME_OUT)
        {
            ///https://developer.work.weixin.qq.com/document/path/95856
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string url = string.Format(Config.ApiWorkHost + "/cgi-bin/wedrive/mng_capacity?access_token={0}", accessToken);
                return CommonJsonSend.Send<MngCapacityJsonResult>(null, url, null, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        #endregion

        #region 异步方法
        /// <summary>
        /// 【异步方法】微盘内新建空间，创建者为应用本身
        /// </summary>
        /// <param name="accessTokenOrAppKey">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<SpaceCreateJsonResult> SpaceCreateAsync(string accessTokenOrAppKey, SpaceCreateModel data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/wedrive/space_create?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<SpaceCreateJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 【异步方法】重命名已有空间
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WorkJsonResult> SpaceRenameAsync(string accessTokenOrAppKey, SpaceRenameModel data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/wedrive/space_rename?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<WorkJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 【异步方法】解散已有空间
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WorkJsonResult> SpaceDismissAsync(string accessTokenOrAppKey, SpaceDismissModel data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/wedrive/space_dismiss?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<WorkJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 【异步方法】获取空间信息
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<SpaceInfoJsonResult> SpaceInfoAsync(string accessTokenOrAppKey, SpaceInfoModel data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/wedrive/space_info?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<SpaceInfoJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 【异步方法】指定空间添加成员/部门，可一次性添加多个
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WorkJsonResult> SpaceAclAddAsync(string accessTokenOrAppKey,SpaceAclAddModel data,int timeOut=Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/wedrive/space_acl_add?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<WorkJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 【异步方法】指定空间移除成员/部门，操作者为应用本身。
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WorkJsonResult> SpaceAclDelAsync(string accessTokenOrAppKey, SpaceAclDelModel data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/wedrive/space_acl_del?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<WorkJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 【异步方法】修改空间权限，应用通过api调用仅支持设置由本应用创建的空间。
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WorkJsonResult> SpaceSettingAsync(string accessTokenOrAppKey, SpaceAclDelModel data, int timeOut = Config.TIME_OUT)
        {
            ///https://developer.work.weixin.qq.com/document/path/97876
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/wedrive/space_setting?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<WorkJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 【异步方法】获取空间邀请分享链接。
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<SpaceShareJsonResult> SpaceShareAsync(string accessTokenOrAppKey, SpaceShareModel data, int timeOut = Config.TIME_OUT)
        {
            ///https://developer.work.weixin.qq.com/document/path/97877
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/wedrive/space_share?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<SpaceShareJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 【异步方法】获取空间信息。包括：空间成员及权限及安全设置
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="spaceid"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<SpaceInfoJsonResult> NewSpaceInfoAsync(string accessTokenOrAppKey, string spaceid, int timeOut = Config.TIME_OUT)
        {
            ///https://developer.work.weixin.qq.com/document/path/97878
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var data = new
                {
                    spaceid
                };
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/wedrive/new_space_info?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<SpaceInfoJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 【异步方法】获取指定地址下的文件列表
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<FileListJsonResult> FileListAsync(string accessTokenOrAppKey, FileListModel data, int timeOut = Config.TIME_OUT)
        {
            ///https://developer.work.weixin.qq.com/document/path/93657
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/wedrive/file_list?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<FileListJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 【异步方法】向微盘中的指定位置上传文件。
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<FileUploadJsonResult> FileUploadAsync(string accessTokenOrAppKey, FileUploadModel data, int timeOut = Config.TIME_OUT)
        {
            ///https://developer.work.weixin.qq.com/document/path/97880
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/wedrive/file_upload?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<FileUploadJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 【异步方法】下载文件
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<FileDownLoadJsonResult> FileDownLoadAsync(string accessTokenOrAppKey, FileDownLoadModel data, int timeOut = Config.TIME_OUT)
        {
            ///https://developer.work.weixin.qq.com/document/path/97881
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/wedrive/file_download?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<FileDownLoadJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 【异步方法】新建文件夹、文档
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<FileCreateJsonResult> FileCreateAsync(string accessTokenOrAppKey, FileCreateModel data, int timeOut = Config.TIME_OUT)
        {
            ///https://developer.work.weixin.qq.com/document/path/97882
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/wedrive/file_create?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<FileCreateJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 【异步方法】指定文件进行重命名。
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<FileRenameJsonResult> FileRenameAsync(string accessTokenOrAppKey, FileRenameModel data, int timeOut = Config.TIME_OUT)
        {
            ///https://developer.work.weixin.qq.com/document/path/97883
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/wedrive/file_rename?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<FileRenameJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 【异步方法】将文件移动到指定位置
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<FileMoveJsonResult> FileMoveAsync(string accessTokenOrAppKey, FileMoveModel data, int timeOut = Config.TIME_OUT)
        {
            ///https://developer.work.weixin.qq.com/document/path/97884
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/wedrive/file_move?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<FileMoveJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 【异步方法】删除指定文件
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WorkJsonResult> FileDeleteAsync(string accessTokenOrAppKey, FileDeleteModel data, int timeOut = Config.TIME_OUT)
        {
            ///https://developer.work.weixin.qq.com/document/path/97885
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/wedrive/file_delete?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<WorkJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 【异步方法】获取指定文件的信息
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="Fileid"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<FileInfoJsonRetult> FileInfoAsync(string accessTokenOrAppKey, string Fileid, int timeOut = Config.TIME_OUT)
        {
            ///https://developer.work.weixin.qq.com/document/path/97886
            var data = new
            {
                fileid = Fileid
            };
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/wedrive/file_info?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<FileInfoJsonRetult>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 【异步方法】指定文件添加成员
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WorkJsonResult> FileAclAddAsync(string accessTokenOrAppKey, FileAclAddModel data, int timeOut = Config.TIME_OUT)
        {
            ///https://developer.work.weixin.qq.com/document/path/93658
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/wedrive/file_acl_add?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<WorkJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 【异步方法】删除指定文件的成员
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WorkJsonResult> FileAclDelAsync(string accessTokenOrAppKey, FileAclDelModel data, int timeOut = Config.TIME_OUT)
        {
            ///https://developer.work.weixin.qq.com/document/path/97888
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/wedrive/file_acl_del?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<WorkJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 【异步方法】文件的分享设置
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WorkJsonResult> FileSettingAsync(string accessTokenOrAppKey, FileSettingModel data, int timeOut = Config.TIME_OUT)
        {
            ///https://developer.work.weixin.qq.com/document/path/97889
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/wedrive/file_setting?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<WorkJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 【异步方法】获取分享链接
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="FileId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<FileShareJsonResult> FileShareAsync(string accessTokenOrAppKey, string FileId, int timeOut = Config.TIME_OUT)
        {
            ///https://developer.work.weixin.qq.com/document/path/97890
            var data = new
            {
                fileid = FileId
            };
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/wedrive/file_share?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<FileShareJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 【异步方法】获取文件权限信息
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="FileId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<FilePermissionJsonResult> FilePermissionAsync(string accessTokenOrAppKey, string FileId, int timeOut = Config.TIME_OUT)
        {
            ///https://developer.work.weixin.qq.com/document/path/97891
            var data = new
            {
                fileid = FileId
            };
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/wedrive/get_file_permission?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<FilePermissionJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 【异步方法】修改文件安全设置
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WorkJsonResult> FileSecureSettingAsync(string accessTokenOrAppKey, FileSecureSettingModel data, int timeOut = Config.TIME_OUT)
        {
            ///https://developer.work.weixin.qq.com/document/path/97892
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/wedrive/file_secure_setting?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<WorkJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 【异步方法】获取盘专业版信息
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="userId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<MngProInfoJsonResult> MngProInfoAsync(string accessTokenOrAppKey, string userId, int timeOut = Config.TIME_OUT)
        {
            ///https://developer.work.weixin.qq.com/document/path/95856
            var data = new 
            {
                userid=userId
            };
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/wedrive/mng_pro_info?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<MngProInfoJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 【异步方法】获取盘容量信息
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<MngCapacityJsonResult> MngCapacityAsync(string accessTokenOrAppKey, int timeOut = Config.TIME_OUT)
        {
            ///https://developer.work.weixin.qq.com/document/path/95856
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string url = string.Format(Config.ApiWorkHost + "/cgi-bin/wedrive/mng_capacity?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<MngCapacityJsonResult>(accessToken, url, null, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        #endregion
    }
}
