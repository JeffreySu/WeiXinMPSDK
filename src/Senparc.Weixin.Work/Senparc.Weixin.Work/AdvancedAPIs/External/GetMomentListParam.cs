/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：GetMomentListParam.cs
    文件功能描述：获取企业全部的发表内容 参数
    
    
    创建标识：WangDrama - 20210714

    修改标识：Senparc - 20230528
    修改描述：v3.15.18.1 GetMomentListLocation.latitude / longitude 设置为 long 类型 （Issue #2823）

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.Work.AdvancedAPIs.External
{
    /// <summary>
    /// 获取企业全部的发表内容 参数
    /// </summary>
    public class GetMomentListParam
    {
        /// <summary>
        /// 朋友圈记录开始时间。Unix时间戳
        /// </summary>
        public long start_time { get; set; }
        /// <summary>
        /// 朋友圈记录结束时间。Unix时间戳
        /// </summary>
        public long end_time { get; set; }
        /// <summary>
        /// 否 朋友圈创建人的userid
        /// </summary>
        public string creator { get; set; }

        /// <summary>
        /// 否   朋友圈类型。0：企业发表 1：个人发表 2：所有，包括个人创建以及企业创建，默认情况下为所有类型
        /// </summary>
        public FilterType filter_type { get; set; }


        /// <summary>
        /// 否 用于分页查询的游标，字符串类型，由上一次调用返回，首次调用可不填
        /// </summary>
        public string cursor { get; set; }
        /// <summary>
        /// 返回的最大记录数，整型，最大值100，默认值100，超过最大值时取默认值
        /// </summary>
        public int limit { get; set; }

    }

    public enum FilterType
    {
        企业发表 = 0,
        个人发表 = 1,
        所有 = 2
    }

    public class GetMomentListResult : WorkJsonResult
    {
        /// <summary>
        /// 朋友圈列表
        /// </summary>
        public GetMomentList[] moment_list { get; set; }
        /// <summary>
        /// 分页游标，下次请求时填写以获取之后分页的记录。如果该字段返回空则表示已没有更多数据
        /// </summary>
        public string next_cursor { get; set; }
    }

    public class GetMomentList
    {

        /// <summary>
        /// 朋友圈id
        /// </summary>
        public string moment_id { get; set; }
        /// <summary>
        /// 朋友圈创建者userid
        /// </summary>
        public string creator { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public long create_time { get; set; }
        /// <summary>
        /// 朋友圈创建来源。0：企业 1：个人
        /// </summary>
        public short create_type { get; set; }

        /// <summary>
        /// 可见范围类型。0：部分可见 1：公开 
        /// </summary>
        public short visible_type { get; set; }

        public GetMomentListText text { get; set; }
        public List<GetMomentListImage> image { get; set; }
        public GetMomentListVideo video { get; set; }
        public GetMomentListLink link { get; set; }
        public GetMomentListLocation location { get; set; }
    }
    public class GetMomentListText
    {
        /// <summary>
        /// 文本消息结构
        /// </summary>
        public string content { get; set; }
    }
    public class GetMomentListImage
    {
        /// <summary>
        /// 图片的media_id列表，可以通过获取临时素材下载资源
        /// </summary>
        public string media_id { get; set; }
    }
    public class GetMomentListVideo
    {
        /// <summary>
        /// 视频media_id，可以通过获取临时素材下载资源
        /// </summary>
        public string media_id { get; set; }

        /// <summary>
        /// 视频封面media_id，可以通过获取临时素材下载资源
        /// </summary>
        public string thumb_media_id { get; set; }
    }
    public class GetMomentListLink
    {
        /// <summary>
        /// 网页链接标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 网页链接url
        /// </summary>
        public string url { get; set; }
    }

    public class GetMomentListLocation
    {
        /// <summary>
        /// 地理位置纬度
        /// </summary>
        public long latitude { get; set; }

        /// <summary>
        /// 地理位置经度
        /// </summary>
        public long longitude { get; set; }

        /// <summary>
        /// 地理位置名称
        /// </summary>
        public string name { get; set; }
    }


    public class GetMomentTaskParam
    {
        /// <summary>
        /// 是 朋友圈id, 仅支持企业发表的朋友圈id
        /// </summary>
        public string moment_id { get; set; }
        /// <summary>
        /// 否 用于分页查询的游标，字符串类型，由上一次调用返回，首次调用可不填
        /// </summary>
        public string cursor { get; set; }
        /// <summary>
        /// 否 返回的最大记录数，整型，最大值1000，默认值500，超过最大值时取默认值
        /// </summary>
        public int limit { get; set; }

    }

    public class GetMomentTaskResult : WorkJsonResult
    {
        /// <summary>
        /// 朋友圈列表
        /// </summary>
        public GetMomentTask[] task_list { get; set; }
        /// <summary>
        /// 分页游标，下次请求时填写以获取之后分页的记录。如果该字段返回空则表示已没有更多数据
        /// </summary>
        public string next_cursor { get; set; }
    }
    public class GetMomentTask
    {
        /// <summary>
        /// 发表成员用户userid
        /// </summary>
        public string userid { get; set; }
        /// <summary>
        /// 成员发表状态。0:未发表 1：已发表 
        /// </summary>
        public short publish_status { get; set; }
    }
}
