/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：DeviceManageResultJson.cs
    文件功能描述：设备管理返回结果
    
    
    创建标识：Senparc - 20150512
----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.ShakeAround
{
    /// <summary>
    /// 申请设备ID返回结果
    /// </summary>
    public class DeviceApplyResultJson : WxJsonResult
    {
        /// <summary>
        /// 申请设备ID返回数据
        /// </summary>
        public DeviceApply_Data data { get; set; }
    }

    public class DeviceApply_Data
    {
        /// <summary>
        /// 申请的批次ID，可用在“查询设备列表”接口按批次查询本次申请成功的设备ID。
        /// </summary>
        public long apply_id { get; set; }
        /// <summary>
        /// 指定的设备ID列表
        /// </summary>
        public List<DeviceApply_Data_Device_Identifiers> device_identifiers { get; set; }
        /// <summary>
        /// 审核状态。0：审核未通过、1：审核中、2：审核已通过；审核会在三个工作日内完成
        /// </summary>
        public int audit_status { get; set; }
        /// <summary>
        /// 审核备注，包括审核不通过的原因
        /// </summary>
        public string audit_comment { get; set; }
    }

    /// <summary>
    /// 设备参数
    /// </summary>
    public class DeviceApply_Data_Device_Identifiers
    {
        /// <summary>
        /// 设备编号
        /// </summary>
        public long? device_id { get; set; }
        public string uuid { get; set; }
        public long major { get; set; }
        public long minor { get; set; }
    }

    #region 返回结果示例

    //当申请个数小于等于500时，
    //{
    //"data": {
    //       "apply_id": 123,
    //       "device_identifiers":[
    //            {
    //                "device_id":10100,	
    //                "uuid":"FDA50693-A4E2-4FB1-AFCF-C6EB07647825",		
    //                "major":10001,
    //                "minor":10002
    //            }
    //        ]
    //    },
    //    "errcode": 0,
    //    "errmsg": "success."
    //}

    //当申请个数大于500时，
    //{
    //"data": {
    //               "apply_id": 123,
    //        "audit_status": 0,	
    //        "audit_comment": "审核未通过"	
    //   },
    //   "errcode": 0,
    //   "errmsg": "success."
    //}

    #endregion

    /// <summary>
    /// 查询设备列表返回结果
    /// </summary>
    public class DeviceSearchResultJson : WxJsonResult
    {
        /// <summary>
        /// 申请设备ID返回数据
        /// </summary>
        public DeviceSearch_Data data { get; set; }
    }

    public class DeviceSearch_Data
    {
        /// <summary>
        /// 指定的设备信息列表
        /// </summary>
        public List<DeviceSearch_Data_Devices> devices { get; set; }
        /// <summary>
        /// 商户名下的设备总量
        /// </summary>
        public int total_count { get; set; }
    }

    public class DeviceSearch_Data_Devices
    {
        /// <summary>
        /// 设备的备注信息
        /// </summary>
        public string comment { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public long device_id { get; set; }
        public long major { get; set; }
        public long minor { get; set; }
        /// <summary>
        /// 与此设备关联的页面ID列表，用逗号隔开
        /// </summary>
        public string page_ids { get; set; }
        /// <summary>
        /// 激活状态，0：未激活，1：已激活（但不活跃），2：活跃
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 设备关联的门店ID
        /// </summary>
        public long poi_id { get; set; }
        public string uuid { get; set; }
    }
}