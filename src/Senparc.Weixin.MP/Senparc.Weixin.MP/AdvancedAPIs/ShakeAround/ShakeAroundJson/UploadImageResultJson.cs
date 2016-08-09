/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：UploadImageResultJson.cs
    文件功能描述：上传图片素材返回结果
    
    
    创建标识：Senparc - 20150512
----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.ShakeAround
{
    /// <summary>
    /// 上传图片素材返回结果
    /// </summary>
    public class UploadImageResultJson : WxJsonResult
    {
        /// <summary>
        /// 申请设备ID返回数据
        /// </summary>
        public UploadImage_Data data { get; set; }
    }

    public class UploadImage_Data
    {
        /// <summary>
        /// 图片url地址，用在“新增页面”和“编辑页面”的“icon_url”字段
        /// </summary>
        public string pic_url { get; set; }
    }
}