/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：DeviceDataJson.cs
    文件功能描述：企业微信设备数据强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐设备授权、设备数据和门禁规则模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.DeviceData
{
    /// <summary>
    /// 获取设备授权信息请求。
    /// </summary>
    public class DeviceDataGetAuthInfoRequest
    {
        /// <summary>
        /// 应用 ID；不传时由当前凭证对应的应用确定。
        /// </summary>
        public int? agentid { get; set; }
    }

    /// <summary>
    /// 已授权设备列表包装对象。
    /// </summary>
    public class DeviceDataDeviceList
    {
        /// <summary>
        /// 已授权设备列表。
        /// </summary>
        public IList<DeviceDataDevice> item { get; set; }
    }

    /// <summary>
    /// 已授权设备信息。
    /// </summary>
    public class DeviceDataDevice
    {
        /// <summary>
        /// 设备类型。
        /// </summary>
        public int device_type { get; set; }

        /// <summary>
        /// 设备序列号。
        /// </summary>
        public string device_sn { get; set; }

        /// <summary>
        /// 设备出厂型号。
        /// </summary>
        public string model_name { get; set; }

        /// <summary>
        /// 设备出厂名称。
        /// </summary>
        public string default_name { get; set; }

        /// <summary>
        /// 企业设置的设备备注名。
        /// </summary>
        public string remark_name { get; set; }
    }

    /// <summary>
    /// 获取设备授权信息结果。
    /// </summary>
    public class DeviceDataGetAuthInfoResult : WorkJsonResult
    {
        /// <summary>
        /// 已授权设备列表。
        /// </summary>
        public DeviceDataDeviceList device_list { get; set; }
    }

    /// <summary>
    /// 设备记录分页查询请求。
    /// </summary>
    public class DeviceDataQueryRequest
    {
        /// <summary>
        /// 应用 ID；不传时由当前凭证对应的应用确定。
        /// </summary>
        public int? agentid { get; set; }

        /// <summary>
        /// 成员类型，按企业微信设备数据协议取值。
        /// </summary>
        public int user_type { get; set; }

        /// <summary>
        /// 设备上传记录的开始时间戳，单位为秒。
        /// </summary>
        public long begin_time { get; set; }

        /// <summary>
        /// 设备上传记录的结束时间戳，单位为秒。
        /// </summary>
        public long end_time { get; set; }

        /// <summary>
        /// 数据筛选类型，按对应设备数据接口的协议取值。
        /// </summary>
        public int data_filter_type { get; set; }

        /// <summary>
        /// 用于筛选的设备序列号列表。
        /// </summary>
        public IList<string> device_sn_list { get; set; }

        /// <summary>
        /// 用于筛选的 OpenUserId 列表。
        /// </summary>
        public IList<string> open_userid_list { get; set; }

        /// <summary>
        /// 分页游标；首次请求可不传。
        /// </summary>
        public string cursor { get; set; }

        /// <summary>
        /// 每页返回数量。
        /// </summary>
        public int? limit { get; set; }
    }

    /// <summary>
    /// 设备打卡记录列表包装对象。
    /// </summary>
    public class DeviceDataCheckinDataList
    {
        /// <summary>
        /// 设备打卡记录列表。
        /// </summary>
        public IList<DeviceDataCheckinRecord> items { get; set; }
    }

    /// <summary>
    /// 设备打卡记录。
    /// </summary>
    public class DeviceDataCheckinRecord
    {
        /// <summary>
        /// 成员的 OpenUserId。
        /// </summary>
        public string open_userid { get; set; }

        /// <summary>
        /// 上传记录的设备序列号。
        /// </summary>
        public string device_sn { get; set; }

        /// <summary>
        /// 打卡时间戳，单位为秒。
        /// </summary>
        public long checkin_time { get; set; }
    }

    /// <summary>
    /// 获取设备打卡数据结果。
    /// </summary>
    public class DeviceDataGetCheckinDataResult : WorkJsonResult
    {
        /// <summary>
        /// 设备打卡记录。
        /// </summary>
        public DeviceDataCheckinDataList checkindata { get; set; }

        /// <summary>
        /// 下一页游标；为空表示没有更多数据。
        /// </summary>
        public string next_cursor { get; set; }
    }

    /// <summary>
    /// 设备测温记录列表包装对象。
    /// </summary>
    public class DeviceDataTemperatureDataList
    {
        /// <summary>
        /// 设备测温记录列表。
        /// </summary>
        public IList<DeviceDataTemperatureRecord> items { get; set; }
    }

    /// <summary>
    /// 设备测温记录。
    /// </summary>
    public class DeviceDataTemperatureRecord
    {
        /// <summary>
        /// 成员类型。
        /// </summary>
        public int user_type { get; set; }

        /// <summary>
        /// 成员的 OpenUserId。
        /// </summary>
        public string open_userid { get; set; }

        /// <summary>
        /// 上传记录的设备序列号。
        /// </summary>
        public string device_sn { get; set; }

        /// <summary>
        /// 测温时间戳，单位为秒。
        /// </summary>
        public long timestamp { get; set; }

        /// <summary>
        /// 温度值字符串，保留企业微信协议中的小数格式。
        /// </summary>
        public string temperature { get; set; }

        /// <summary>
        /// 温度状态。
        /// </summary>
        public int status { get; set; }
    }

    /// <summary>
    /// 获取设备测温数据结果。
    /// </summary>
    public class DeviceDataGetTemperatureDataResult : WorkJsonResult
    {
        /// <summary>
        /// 设备测温记录。
        /// </summary>
        public DeviceDataTemperatureDataList temperature_data { get; set; }

        /// <summary>
        /// 下一页游标；为空表示没有更多数据。
        /// </summary>
        public string next_cursor { get; set; }
    }

    /// <summary>
    /// 设备门禁通行记录列表包装对象。
    /// </summary>
    public class DeviceDataAccessControlDataList
    {
        /// <summary>
        /// 设备门禁通行记录列表。
        /// </summary>
        public IList<DeviceDataAccessControlRecord> items { get; set; }
    }

    /// <summary>
    /// 设备门禁通行记录。
    /// </summary>
    public class DeviceDataAccessControlRecord
    {
        /// <summary>
        /// 成员类型。
        /// </summary>
        public int user_type { get; set; }

        /// <summary>
        /// 成员的 OpenUserId。
        /// </summary>
        public string open_userid { get; set; }

        /// <summary>
        /// 上传记录的设备序列号。
        /// </summary>
        public string device_sn { get; set; }

        /// <summary>
        /// 通行时间戳，单位为秒。
        /// </summary>
        public long timestamp { get; set; }

        /// <summary>
        /// 通行类型。
        /// </summary>
        public int pass_type { get; set; }

        /// <summary>
        /// 通行方式。
        /// </summary>
        public int pass_method { get; set; }
    }

    /// <summary>
    /// 获取设备门禁通行数据结果。
    /// </summary>
    public class DeviceDataGetAccessControlDataResult : WorkJsonResult
    {
        /// <summary>
        /// 设备门禁通行记录。
        /// </summary>
        public DeviceDataAccessControlDataList accesscontrol_data { get; set; }

        /// <summary>
        /// 下一页游标；为空表示没有更多数据。
        /// </summary>
        public string next_cursor { get; set; }
    }

    /// <summary>
    /// 获取门禁规则请求。
    /// </summary>
    public class DeviceDataGetAccessControlRuleRequest
    {
        /// <summary>
        /// 设备序列号。
        /// </summary>
        public string device_sn { get; set; }
    }

    /// <summary>
    /// 门禁规则生效成员。
    /// </summary>
    public class DeviceDataAccessControlEffectUser
    {
        /// <summary>
        /// 成员的 OpenUserId。
        /// </summary>
        public string open_userid { get; set; }

        /// <summary>
        /// 成员类型。
        /// </summary>
        public int? user_type { get; set; }
    }

    /// <summary>
    /// 新增或修改门禁规则时提交的通行规则。
    /// </summary>
    public class DeviceDataAccessControlPassRule
    {
        /// <summary>
        /// 通行时间规则列表，元素采用企业微信规定的规则表达式。
        /// </summary>
        public IList<string> rule_list { get; set; }

        /// <summary>
        /// 规则生效成员列表。
        /// </summary>
        public IList<DeviceDataAccessControlEffectUser> effect_open_userid_list { get; set; }
    }

    /// <summary>
    /// 查询返回的单条门禁规则。
    /// </summary>
    public class DeviceDataAccessControlRule
    {
        /// <summary>
        /// 门禁规则 ID。
        /// </summary>
        public string rule_id { get; set; }

        /// <summary>
        /// 门禁规则名称。
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 通行时间规则列表。
        /// </summary>
        public IList<string> rule_list { get; set; }

        /// <summary>
        /// 规则生效成员列表。
        /// </summary>
        public IList<DeviceDataAccessControlEffectUser> effect_open_userid_list { get; set; }

        /// <summary>
        /// 规则生效时间戳，单位为秒。
        /// </summary>
        public long effect_time { get; set; }
    }

    /// <summary>
    /// 查询返回的门禁规则列表包装对象。
    /// </summary>
    public class DeviceDataAccessControlRuleList
    {
        /// <summary>
        /// 门禁规则列表。
        /// </summary>
        public IList<DeviceDataAccessControlRule> items { get; set; }
    }

    /// <summary>
    /// 获取门禁规则结果。
    /// </summary>
    public class DeviceDataGetAccessControlRuleResult : WorkJsonResult
    {
        /// <summary>
        /// 普通门禁规则列表。
        /// </summary>
        public DeviceDataAccessControlRuleList pass_rule { get; set; }

        /// <summary>
        /// 远程开门规则列表。
        /// </summary>
        public DeviceDataAccessControlRuleList remote_pass_rule { get; set; }
    }

    /// <summary>
    /// 新增门禁规则请求。
    /// </summary>
    public class DeviceDataAddAccessControlRuleRequest
    {
        /// <summary>
        /// 门禁规则名称。
        /// </summary>
        public string rule_name { get; set; }

        /// <summary>
        /// 应用规则的设备序列号列表。
        /// </summary>
        public IList<string> device_sn_list { get; set; }

        /// <summary>
        /// 普通门禁规则。
        /// </summary>
        public DeviceDataAccessControlPassRule pass_rule { get; set; }

        /// <summary>
        /// 远程开门规则。
        /// </summary>
        public DeviceDataAccessControlPassRule remote_pass_rule { get; set; }
    }

    /// <summary>
    /// 新增门禁规则结果。
    /// </summary>
    public class DeviceDataAddAccessControlRuleResult : WorkJsonResult
    {
        /// <summary>
        /// 新增的门禁规则 ID。
        /// </summary>
        public string rule_id { get; set; }

        /// <summary>
        /// 请求中无法生效的 OpenUserId 列表。
        /// </summary>
        public IList<string> invalid_list { get; set; }
    }

    /// <summary>
    /// 修改门禁规则请求。
    /// </summary>
    public class DeviceDataModifyAccessControlRuleRequest : DeviceDataAddAccessControlRuleRequest
    {
        /// <summary>
        /// 待修改的门禁规则 ID。
        /// </summary>
        public string rule_id { get; set; }
    }

    /// <summary>
    /// 修改门禁规则结果。
    /// </summary>
    public class DeviceDataModifyAccessControlRuleResult : WorkJsonResult
    {
        /// <summary>
        /// 请求中无法生效的 OpenUserId 列表。
        /// </summary>
        public IList<string> invalid_list { get; set; }
    }

    /// <summary>
    /// 删除门禁规则请求。
    /// </summary>
    public class DeviceDataDeleteAccessControlRuleRequest
    {
        /// <summary>
        /// 待删除的门禁规则 ID。
        /// </summary>
        public string rule_id { get; set; }
    }

    /// <summary>
    /// 删除门禁规则结果。
    /// </summary>
    public class DeviceDataDeleteAccessControlRuleResult : WorkJsonResult
    {
    }
}
