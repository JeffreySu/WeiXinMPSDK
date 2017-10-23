using System.Collections.Generic;

namespace Senparc.Weixin.MP.AdvancedAPIs.Card
{
    /// <summary>
    /// 基本的卡券数据，所有卡券通用。作为 Card_BaseInfo和 的基类
    /// </summary>
    public class Card_AdvancedInfoBase
    {
        /// <summary>
        /// 使用门槛（条件）字段
        /// </summary>
        public UseCondition use_condition { get; set; }

        /// <summary>
        /// 封面摘要结构体
        /// </summary>
        public Abstract @abstract { get; set; }

        /// <summary>
        /// 图文列表，显示在详情内页，优惠券券开发者须至少传入一组图文列表
        /// </summary>
        public List<TextImage> text_image_list { get; set; }

        /// <summary>
        /// 使用时段限制
        /// </summary>
        public List<TimeLimit> time_limit { get; set; }

        /// <summary>
        /// 商家服务类型：BIZ_SERVICE_DELIVER 外卖服务；BIZ_SERVICE_FREE_PARK 停车位；BIZ_SERVICE_WITH_PET 可带宠物；BIZ_SERVICE_FREE_WIFI 免费wifi，可多选
        /// </summary>
        public List<string> business_service { get; set; }
    }

    public class UseCondition
    {
        /// <summary>
        /// 指定可用的商品类目，仅用于代金券类型，填入后将在券面拼写指定xx可用
        /// </summary>
        public string accept_category { get; set; }

        /// <summary>
        /// 指定不可用的商品类目，仅用于代金券类型，填入后将在券面拼写指定xx不可用
        /// </summary>
        public string reject_category { get; set; }

        /// <summary>
        /// 满减门槛字段，可用于兑换券和代金券，填入后将在全面拼写消费满xx元可用。
        /// </summary>
        public int least_cost { get; set; }

        /// <summary>
        /// 购买xx可用类型门槛，仅用于兑换券，填入后自动拼写购买xxx可用。
        /// </summary>
        public string object_use_for { get; set; }

        /// <summary>
        /// 不可以与其他类型共享门槛，填写false时系统将在使用须知里拼写不可与其他优惠共享，默认为true。
        /// </summary>
        public bool can_use_with_other_discount { get; set; }
    }

    public class Abstract
    {
        /// <summary>
        /// 封面摘要简介。
        /// </summary>
        public string @abstract { get; set; }

        /// <summary>
        /// 封面图片列表，仅支持填入一个封面图片链接，上传图片接口上传获取图片获得链接，填写非CDN链接会报错，并在此填入。建议图片尺寸像素850*350
        /// </summary>
        public List<string> icon_url_list { get; set; }
    }

    public class TextImage
    {
        /// <summary>
        /// 图片链接，必须调用上传图片接口上传图片获得链接，并在此填入，否则报错
        /// </summary>
        public string image_url { get; set; }

        /// <summary>
        /// 图文描述，5000字以内
        /// </summary>
        public string text { get; set; }
    }

    public class TimeLimit
    {
        /// <summary>
        /// 限制类型枚举值：支持填入
        /// MONDAY 周一 TUESDAY 周二 WEDNESDAY 周三 THURSDAY 周四 FRIDAY 周五 SATURDAY 周六 SUNDAY 周日 此处只控制显示，不控制实际使用逻辑，不填默认不显示
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 当前type类型下的起始时间（小时），如当前结构体内填写了MONDAY，此处填写了10，则此处表示周一 10:00可用
        /// </summary>
        public int begin_hour { get; set; }

        /// <summary>
        /// 当前type类型下的起始时间（分钟），如当前结构体内填写了MONDAY，begin_hour填写10，此处填写了59，则此处表示周一 10:59可用
        /// </summary>
        public int begin_minute { get; set; }

        /// <summary>
        /// 当前type类型下的结束时间（小时），如当前结构体内填写了MONDAY，此处填写了20，则此处表示周一 10:00-20:00可用
        /// </summary>
        public int end_hour { get; set; }

        /// <summary>
        /// 当前type类型下的结束时间（分钟），如当前结构体内填写了MONDAY，begin_hour填写10，此处填写了59，则此处表示周一 10:59-00:59可用
        /// </summary>
        public int end_minute { get; set; }
    }
}
