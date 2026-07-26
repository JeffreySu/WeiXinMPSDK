/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：OpenHardwareVisitorJson.cs
    文件功能描述：企业微信智慧硬件访客数据占位模型


    创建标识：Senparc - 20260726

    修改标识：Senparc - 20260726
    修改描述：v3.32.1 保留无权限访客接口的强类型占位模型

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.OpenHardware
{
    /// <summary>
    /// 全量获取访客数据占位请求。
    /// 官方文档 96060 当前无权查看，因此在取得可验证契约前不定义任何请求字段。
    /// </summary>
    public class OpenHardwareGetVisitorByPageRequest
    {
    }

    /// <summary>
    /// 全量获取访客数据占位结果。
    /// 官方文档 96060 当前无权查看，因此在取得可验证契约前不定义任何响应字段。
    /// </summary>
    public class OpenHardwareGetVisitorByPageResult : WorkJsonResult
    {
    }

    /// <summary>
    /// 获取指定访客数据占位请求。
    /// 官方文档 96061 当前无权查看，因此在取得可验证契约前不定义任何请求字段。
    /// </summary>
    public class OpenHardwareGetVisitorByIdsRequest
    {
    }

    /// <summary>
    /// 获取指定访客数据占位结果。
    /// 官方文档 96061 当前无权查看，因此在取得可验证契约前不定义任何响应字段。
    /// </summary>
    public class OpenHardwareGetVisitorByIdsResult : WorkJsonResult
    {
    }
}
