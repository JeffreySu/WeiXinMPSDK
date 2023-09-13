namespace Senparc.Weixin.Open.WxaAPIs.Icp.IcpJson
{
    /// <summary>
    /// 备案主体信息
    /// </summary>
    public class IcpSubjectModel
    {
        /// <summary>
        /// 主体基本信息
        /// </summary>
        public BaseInfoModel base_info { get; set; }

        /// <summary>
        /// 个人主体额外信息
        /// </summary>
        public PersonalInfoModel personal_info { get; set; }

        /// <summary>
        /// 主体额外信息（个人备案时，如果存在与主体负责人信息相同的字段，则填入相同的值）
        /// </summary>
        public OrganizeInfoModel organize_info { get; set; }

        /// <summary>
        /// 主体负责人信息
        /// </summary>
        public PrincipalInfoModel principal_info { get; set; }

        /// <summary>
        /// 法人信息（非个人备案，且主体负责人不是法人时，必填）
        /// </summary>
        public LegalPersonInfoModel legal_person_info { get; set; }
    }

    /// <summary>
    /// 主体基本信息
    /// </summary>
    public class BaseInfoModel
    {
        /// <summary>
        /// 主体性质
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// 主办单位名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 备案省份，使用省份代码
        /// </summary>
        public string province { get; set; }

        /// <summary>
        /// 备案城市，使用城市代码
        /// </summary>
        public string city { get; set; }

        /// <summary>
        /// 备案县区，使用县区代码
        /// </summary>
        public string district { get; set; }

        /// <summary>
        /// 通讯地址，必须属于备案省市区，地址开头的省市区不用填入
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// 主体信息备注，根据需要，如实填写
        /// </summary>
        public string comment { get; set; }

    }

    /// <summary>
    /// 个人主体额外信息
    /// </summary>
    public class PersonalInfoModel
    {
        /// <summary>
        /// 临时居住证明照片 media_id，个人备案且非本省人员，需要提供居住证、暂住证、社保证明、房产证等临时居住证明
        /// </summary>
        public string residence_permit { get; set; }
    }

    /// <summary>
    /// 主体额外信息（个人备案时，如果存在与主体负责人信息相同的字段，则填入相同的值）
    /// </summary>
    public class OrganizeInfoModel
    {
        /// <summary>
        /// 主体证件类型
        /// </summary>
        public int certificate_type { get; set; }

        /// <summary>
        /// 主体证件号码
        /// </summary>
        public string certificate_number { get; set; }

        /// <summary>
        /// 主体证件住所
        /// </summary>
        public string certificate_address { get; set; }

        /// <summary>
        /// 主体证件照片 media_id，如果小程序主体为非个人类型，则必填
        /// </summary>
        public string certificate_photo { get; set; }
    }

    /// <summary>
    /// 主体负责人信息
    /// </summary>
    public class PrincipalInfoModel
    {
        /// <summary>
        /// 负责人姓名，示例值：`"张三"`
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 负责人联系方式，示例值：`"13012344321"`
        /// </summary>
        public string mobile { get; set; }

        /// <summary>
        /// 负责人电子邮件，示例值：`"zhangsan@zhangsancorp.com"`
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// 负责人应急联系方式，示例值：`"17743211234"`
        /// </summary>
        public string emergency_contact { get; set; }

        /// <summary>
        /// 负责人证件类型，示例值：`2`(参考：获取证件类型接口，此处只能填入单位性质属于个人的证件类型)
        /// </summary>
        public int certificate_type { get; set; }

        /// <summary>
        /// 负责人证件号码，示例值：`"110105199001011234"`
        /// </summary>
        public string certificate_number { get; set; }

        /// <summary>
        /// 负责人证件有效期起始日期，格式为 YYYYmmdd，示例值：`"20230815"`
        /// </summary>
        public string certificate_validity_date_start { get; set; }

        /// <summary>
        /// 负责人证件有效期终止日期，格式为 YYYYmmdd，如证件长期有效，请填写 `"长期"`，示例值：`"20330815"`
        /// </summary>
        public string certificate_validity_date_end { get; set; }

        /// <summary>
        /// 负责人证件正面照片 media_id（身份证为人像面），示例值：`"4ahCGpd3CYkE6RpkNkUR5czt3LvG8xDnDdKAz6bBKttSfM8p4k5Rj6823HXugPwQBurgMezyib7"`
        /// </summary>
        public string certificate_photo_front { get; set; }

        /// <summary>
        /// 负责人证件背面照片 media_id（身份证为国徽面），示例值：`"4ahCGpd3CYkE6RpkNkUR5czt3LvG8xDnDdKAz6bBKttSfM8p4k5Rj6823HXugPwQBurgMezyib7"`
        /// </summary>
        public string certificate_photo_back { get; set; }

        /// <summary>
        /// 授权书 media_id，当主体负责人不是法人时需要主体负责人授权书，当小程序负责人不是法人时需要小程序负责人授权书，示例值：`"4ahCGpd3CYkE6RpkNkUR5czt3LvG8xDnDdKAz6bBKttSfM8p4k5Rj6823HXugPwQBurgMezyib7"`
        /// </summary>
        public string authorization_letter { get; set; }

        /// <summary>
        /// 扫脸认证任务id(扫脸认证接口返回的task_id)，仅小程序负责人需要扫脸，主体负责人无需扫脸，示例值：`"R5PqRPNb6GmG3i0rqd4pTg"`
        /// </summary>
        public string verify_task_id { get; set; }

    }

    /// <summary>
    /// 法人信息（非个人备案，且主体负责人不是法人时，必填）
    /// </summary>
    public class LegalPersonInfoModel
    {
        /// <summary>
        /// 法人代表姓名，示例值：`"张三"`
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 法人证件号码，示例值：`"110105199001011234"`
        /// </summary>
        public string certificate_number { get; set; }
    }
}
