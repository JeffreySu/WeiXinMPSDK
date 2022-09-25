/// <summary>
/// 提交申请单请求数据
/// 详细请参考微信支付官方文档: https://pay.weixin.qq.com/wiki/doc/apiv3_partner/apis/chapter11_1_1.shtml
/// <summary>
public class ApplyRequestData
{

    /// <summary>
    ///  业务申请编号 
    /// body1、只能由数字、字母或下划线组成，建议前缀为服务商商户号；
    /// 2、服务商自定义的唯一编号；
    /// 3、每个编号对应一个申请单，每个申请单审核通过后会生成一个微信支付商户号。
    /// 示例值：1900013511_10000
    /// 可为空: True
    /// </summary>
    public string[1, 124] business_code { get; set; }

    /// <summary>
    /// 超级管理员信息
    /// body超级管理员需在开户后进行签约，并可接收日常重要管理信息和进行资金操作，请确定其为商户法定代表人或负责人。
    /// 可为空: True
    /// </summary>
    public Contact_Info contact_info { get; set; }

    /// <summary>
    /// 主体资料
    /// body请填写商家的营业执照/登记证书、经营者/法人的证件等信息
    /// 可为空: True
    /// </summary>
    public Subject_Info subject_info { get; set; }

    /// <summary>
    /// 经营资料
    /// body请填写商家的经营业务信息、售卖商品/提供服务场景信息
    /// 可为空: True
    /// </summary>
    public Business_Info business_info { get; set; }

    /// <summary>
    /// 结算规则
    /// body请填写商家的结算费率规则、特殊资质等信息
    /// 可为空: True
    /// </summary>
    public Settlement_Info settlement_info { get; set; }

    /// <summary>
    /// 结算银行账户
    /// body请填写商家提现收款的银行账户信息
    /// 可为空: True
    /// </summary>
    public Bank_Account_Info bank_account_info { get; set; }

    /// <summary>
    /// 补充材料
    /// body根据实际审核情况，会额外要求商家提供指定的补充资料
    /// 可为空: False
    /// </summary>
    public Addition_Info addition_info { get; set; }


    #region 子数据类型

    /// <summary>
    /// 超级管理员信息
    /// body超级管理员需在开户后进行签约，并可接收日常重要管理信息和进行资金操作，请确定其为商户法定代表人或负责人。
    /// <summary>
    public class Contact_Info
    {

        /// <summary>
        ///  超级管理员类型 
        /// 1、主体为“个体工商户/企业/政府机关/事业单位/社会组织”，可选择：LEGAL：经营者/法人，SUPER：经办人 。（经办人：经商户授权办理微信支付业务的人员）。
        /// 枚举值：
        /// LEGAL：经营者/法人 
        /// SUPER：经办人
        /// 示例值：LEGAL
        /// 可为空: True
        /// </summary>
        public string contact_type { get; set; }

        /// <summary>
        ///  超级管理员姓名 
        /// 该字段需进行加密处理，加密方法详见敏感信息加密说明。(提醒：必须在HTTP头中上送Wechatpay-Serial) 
        /// 示例值：pVd1HJ6zyvPedzGaV+X3qtmrq9bb9tPROvwia4ibL+F6mfjbzQIzfb3HHLEjZ44EL5Kz4jBHLiCyOb+tI0m2qhZ9evAM+Jv1z0NVa8MRtelw/wDa4SzfeespQO/0kjiwfqdfg==
        /// 可为空: True
        /// </summary>
        public string[1, 2048] contact_name { get; set; }

        /// <summary>
        ///  超级管理员证件类型 
        /// 当超级管理员类型是经办人时，请上传超级管理员证件类型。 
        /// IDENTIFICATION_TYPE_IDCARD：中国大陆居民-身份证 
        /// IDENTIFICATION_TYPE_OVERSEA_PASSPORT：其他国家或地区居民-护照 
        /// IDENTIFICATION_TYPE_HONGKONG_PASSPORT：中国香港居民-来往内地通行证 
        /// IDENTIFICATION_TYPE_MACAO_PASSPORT：中国澳门居民-来往内地通行证 
        /// IDENTIFICATION_TYPE_TAIWAN_PASSPORT：中国台湾居民-来往大陆通行证 
        /// IDENTIFICATION_TYPE_FOREIGN_RESIDENT：外国人居留证 
        /// IDENTIFICATION_TYPE_HONGKONG_MACAO_RESIDENT：港澳居民证 
        /// IDENTIFICATION_TYPE_TAIWAN_RESIDENT：台湾居民证
        /// 示例值：IDENTIFICATION_TYPE_IDCARD
        /// 可为空: False
        /// </summary>
        public string contact_id_doc_type { get; set; }

        /// <summary>
        ///  超级管理员身份证件号码 
        /// 1、当超级管理员类型是经办人时，请上传超级管理员证件号码
        /// 2、可传身份证、来往内地通行证、来往大陆通行证、护照等证件号码。
        /// 3、超级管理员签约时，校验微信号绑定的银行卡实名信息，是否与该证件号码一致。
        /// 4、该字段需进行加密处理，加密方法详见敏感信息加密说明。(提醒：必须在HTTP头中上送Wechatpay-Serial) 
        /// 示例值：pVd1HJ6zyvPedzGaV+X3qtmrq9bb9tPROvwia4ibL+F6mfjbzQIzfb3HHLEjZ4YiR/cJiCrZxnAqi+pjeKIqhZ9evAM+Jv1z0NVa8MRtelw/wDa4SzfeespQO/0kjiwfqdfg==
        /// 可为空: False
        /// </summary>
        public string[1, 2048] contact_id_number { get; set; }

        /// <summary>
        ///  超级管理员证件正面照片 
        /// 1、当超级管理员类型是经办人时，请上传超级管理员证件的正面照片。
        /// 2、若证件类型为身份证，请上传人像面照片。
        /// 3、可上传1张图片，请填写通过图片上传API预先上传图片生成好的MediaID。
        /// 4、请上传彩色照片or彩色扫描件or复印件（需加盖公章鲜章），可添加“微信支付”相关水印（如微信支付认证）。
        /// 示例值：jTpGmxUXqRTvDujqhThn4ReFxikqJ5YW6zFQ
        /// 可为空: False
        /// </summary>
        public string[1, 256] contact_id_doc_copy { get; set; }

        /// <summary>
        ///  超级管理员证件反面照片 
        /// 1、当超级管理员类型是经办人时，请上传超级管理员证件的反面照片。
        /// 2、若证件类型为护照，无需上传反面照片。
        /// 3、可上传1张图片，请填写通过图片上传API预先上传图片生成好的MediaID。
        /// 4、请上传彩色照片or彩色扫描件or复印件（需加盖公章鲜章），可添加“微信支付”相关水印（如微信支付认证）。
        /// 示例值：jTpGmxUX3FBWVQ5NJTZvvDujqhThn4ReFxikqJ5YW6zFQ
        /// 可为空: False
        /// </summary>
        public string[1, 256] contact_id_doc_copy_back { get; set; }

        /// <summary>
        ///  超级管理员证件有效期开始时间 
        /// 1、当超级管理员类型是经办人时，请上传证件有效期开始时间。
        /// 2、请按照示例值填写。
        /// 3、结束时间大于开始时间。
        /// 示例值：2019-06-06
        /// 可为空: False
        /// </summary>
        public string[1, 128] contact_period_begin { get; set; }

        /// <summary>
        ///  超级管理员证件有效期结束时间 
        /// 1、当超级管理员类型是经办人时，请上传证件有效期结束时间。
        /// 2、请按照示例值填写，若证件有效期为长期，请填写：长期。
        /// 3、结束时间大于开始时间。
        /// 示例值：2026-06-06
        /// 可为空: False
        /// </summary>
        public string[1, 128] contact_period_end { get; set; }

        /// <summary>
        ///  业务办理授权函 
        /// 1、当超级管理员类型是经办人时，请上传业务办理授权函。
        /// 2、请参照[示例图]打印业务办理授权函，全部信息需打印，不支持手写商户信息，并加盖公章。
        /// 3、可上传1张图片，请填写通过图片上传API预先上传图片生成好的MediaID。
        /// 示例值：47ZC6GC-vnrbEny_Ie_An5-tCpqxucuxi-vByf3Gjm7KEIUv0OF4wFNIO4kqg05InE4d2I6_H7I4
        /// 可为空: False
        /// </summary>
        public string[1, 256] business_authorization_letter { get; set; }

        /// <summary>
        ///  超级管理员微信OpenID 
        /// 1、超级管理员签约时，会校验微信号是否与该微信OpenID一致；
        /// 2、该字段需进行加密处理，加密方法详见敏感信息加密说明。(提醒：必须在HTTP头中上送Wechatpay-Serial) 
        /// 示例值：pVd1HJ6zyvPedzGaV+X3qtmrq9bb9tPROvwia4ibL+F6mfjbzQIzfb3HbDnuC4EL5Kz4jBHLiCyOb+tI0m2qhZ9evAM+Jv1z0NVa8MRtelw/wDa4SzfeespQO/0kjiwfqdfg== 
        /// 可为空: False
        /// </summary>
        public string[1, 2048] openid { get; set; }

        /// <summary>
        ///  联系手机 
        /// 1、输入11位数字；
        /// 2、用于接收微信支付的重要管理信息及日常操作验证码
        /// 3、该字段需进行加密处理，加密方法详见敏感信息加密说明。(提醒：必须在HTTP头中上送Wechatpay-Serial) 
        /// 示例值：pVd1HJ6zyvPedzGaV+X3qtmrq9bb9tPROvwia4ibL+F6mfjbzQIzfb369bDnuC4EL5Kz4jBHLiCyOb+tI0m2qhZ9evAM+Jv1z0NVa8MRtelw/wDa4SzfeespQO/0kjiwfqdfg==
        /// 可为空: True
        /// </summary>
        public string[1, 2048] mobile_phone { get; set; }

        /// <summary>
        ///  联系邮箱 
        /// 1、小微选填，个体工商户/企业/党政、机关及事业单位/其他组织必填；
        /// 2、用于接收微信支付的开户邮件及日常业务通知；
        /// 3、需要带@，遵循邮箱格式校验 ，该字段需进行加密处理，加密方法详见敏感信息加密说明。(提醒：必须在HTTP头中上送Wechatpay-Serial) 
        /// 示例值：pVd1HJ6zyvPedzGaV+X3qtmrq9bb9tPROvwia4ibL+F6mfjbzQIzfb3HHLEjZ4YiR/cJiCrZxnAqiOb+tI0m2qhZ9evAM+Jv1z0NVa8MRtelw/wDa4SzfeespQO/0kjiwfqdfg==
        /// 可为空: True
        /// </summary>
        public string[1, 2048] contact_email { get; set; }



    }


    /// <summary>
    /// 主体资料
    /// body请填写商家的营业执照/登记证书、经营者/法人的证件等信息
    /// <summary>
    public class Subject_Info
    {

        /// <summary>
        ///  主体类型 
        /// 主体类型需与营业执照/登记证书上一致，可参考选择主体指引。
        /// SUBJECT_TYPE_INDIVIDUAL（个体户）：营业执照上的主体类型一般为个体户、个体工商户、个体经营；
        /// SUBJECT_TYPE_ENTERPRISE（企业）：营业执照上的主体类型一般为有限公司、有限责任公司；
        /// SUBJECT_TYPE_GOVERNMENT （政府机关）：包括各级、各类政府机关，如机关党委、税务、民政、人社、工商、商务、市监等；
        /// SUBJECT_TYPE_INSTITUTIONS（事业单位）：包括国内各类事业单位，如：医疗、教育、学校等单位；
        /// SUBJECT_TYPE_OTHERS（社会组织）：	包括社会团体、民办非企业、基金会、基层群众性自治组织、农村集体经济组织等组织。
        /// 示例值：SUBJECT_TYPE_ENTERPRISE
        /// 可为空: True
        /// </summary>
        public string subject_type { get; set; }

        /// <summary>
        ///  是否是金融机构 
        /// 选填，请根据申请主体的实际情况填写，可参考选择金融机构指引：
        /// 1、若商户主体是金融机构，则填写：true。
        /// 2、若商户主体不是金融机构，则填写：false。
        /// 若未传入将默认填写：false。
        /// 示例值：true
        /// 可为空: False
        /// </summary>
        public boolean finance_institution { get; set; }

        /// <summary>
        /// 营业执照
        /// 1、主体为个体户/企业，必填
        /// 2、请上传“营业执照”，需年检章齐全，当年注册除外
        /// 可为空: False
        /// </summary>
        public Business_License_Info business_license_info { get; set; }

        /// <summary>
        /// 登记证书
        /// 主体为政府机关/事业单位/其他组织时，必填。
        /// 可为空: False
        /// </summary>
        public Certificate_Info certificate_info { get; set; }

        /// <summary>
        ///  单位证明函照片 
        /// 1、主体类型为政府机关、事业单位选传：
        /// （1）若上传，则审核通过后即可签约，无需汇款验证。
        /// （2）若未上传，则审核通过后，需汇款验证。
        /// 2、主体为个体户、企业、其他组织等，不需要上传本字段。
        /// 3、请参照示例图打印单位证明函，全部信息需打印，不支持手写商户信息，并加盖公章。
        /// 4、可上传1张图片，请填写通过图片上传API预先上传图片生成好的MediaID。
        /// 示例值：47ZC6GC-vnrbEny__Ie_An5-tCpqxucuxi-vByf3Gjm7KE53JXvGy9tqZm2XAUf-4KGprrKhpVBDIUv0OF4wFNIO4kqg05InE4d2I6_H7I4
        /// 可为空: False
        /// </summary>
        public string[1, 255] certificate_letter_copy { get; set; }

        /// <summary>
        /// 金融机构许可证信息
        /// 当主体是金融机构时，必填。
        /// 可为空: False
        /// </summary>
        public Finance_Institution_Info finance_institution_info { get; set; }

        /// <summary>
        /// 经营者/法人身份证件
        /// 1、个体户：请上传经营者的身份证件。
        /// 2、企业/社会组织：请上传法人的身份证件。
        /// 3、政府机关/事业单位：请上传法人/经办人的身份证件。
        /// 可为空: True
        /// </summary>
        public Identity_Info identity_info { get; set; }

        /// <summary>
        /// 最终受益人信息列表(UBO)
        /// 仅企业需要填写。
        /// 若经营者/法人不是最终受益所有人，则需补充受益所有人信息，最多上传4个。
        /// 若经营者/法人是最终受益所有人之一，可在此添加其他受益所有人信息，最多上传3个。
        /// 根据国家相关法律法规，需要提供公司受益所有人信息，受益所有人需符合至少以下条件之一：
        /// 1、直接或者间接拥有超过25%公司股权或者表决权的自然人。
        /// 2、通过人事、财务等其他方式对公司进行控制的自然人。
        /// 3、公司的高级管理人员，包括公司的经理、副经理、财务负责人、上市公司董事会秘书和公司章程规定的其他人员。
        /// 可为空: False
        /// </summary>
        public Ubo_Info_List[] ubo_info_list { get; set; }


        #region 子数据类型

        /// <summary>
        /// 营业执照
        /// 1、主体为个体户/企业，必填
        /// 2、请上传“营业执照”，需年检章齐全，当年注册除外
        /// <summary>
        public class Business_License_Info
        {

            /// <summary>
            ///  营业执照照片 
            /// 可上传1张图片，请填写通过图片上传API接口预先上传图片生成好的MediaID
            /// 示例值：47ZC6GC-vnrbEny__Ie_An5-tCpqxucuxi-vByf3Gjm7KE53JXvGy9tqZm2XAUf-4KGprrKhpVBDIUv0OF4wFNIO4kqg05InE4d2I6_H7I4
            /// 可为空: True
            /// </summary>
            public string[1, 255] license_copy { get; set; }

            /// <summary>
            ///  注册号/统一社会信用代码 
            /// 请填写营业执照上的营业执照注册号，注册号格式须为18位数字|大写字母。
            /// 示例值：123456789012345678
            /// 可为空: True
            /// </summary>
            public string[1, 32] license_number { get; set; }

            /// <summary>
            ///  商户名称 
            /// 1、请填写营业执照上的商户名称，2~110个字符，支持括号
            /// 2、个体户，不能以“公司”结尾
            /// 3、个体户，若营业执照上商户名称为空或为“无”，请填写"个体户+经营者姓名"，如“个体户张三”
            /// 示例值：腾讯科技有限公司
            /// 可为空: True
            /// </summary>
            public string[1, 128] merchant_name { get; set; }

            /// <summary>
            ///  个体户经营者/法人姓名 
            /// 请填写营业执照的经营者/法定代表人姓名
            /// 示例值：张三
            /// 可为空: True
            /// </summary>
            public string[1, 64] legal_person { get; set; }

            /// <summary>
            ///  注册地址 
            /// 选填，请填写登记证书的注册地址。
            /// 示例值：广东省深圳市南山区xx路xx号
            /// 可为空: False
            /// </summary>
            public string[1, 256] license_address { get; set; }

            /// <summary>
            ///  有效期限开始日期 
            /// 1、选填， 请参考示例值填写。
            /// 2、开始日期，开始日期需大于当前日期。
            /// 示例值：2019-08-01
            /// 可为空: False
            /// </summary>
            public string[1, 128] period_begin { get; set; }

            /// <summary>
            ///  有效期限结束日期 
            /// 1、选填，请参考示例值填写。
            /// 2、若证件有效期为长期，请填写：长期。
            /// 3、结束日期大于开始日期。
            /// 示例值：2029-08-01
            /// 可为空: False
            /// </summary>
            public string[1, 128] period_end { get; set; }



        }


        /// <summary>
        /// 登记证书
        /// 主体为政府机关/事业单位/其他组织时，必填。
        /// <summary>
        public class Certificate_Info
        {

            /// <summary>
            ///  登记证书照片 
            /// 1、请填写通过图片上传API接口预先上传图片生成好的MediaID
            /// 示例值：0P3ng6KTIW4-Q_l2FjKLZuhHjBWoMAjmVtCz7ScmhEIThCaV-4BBgVwtNkCHO_XXqK5dE5YdOmFJBZR9FwczhJehHhAZN6BKXQPcs-VvdSo
            /// 可为空: True
            /// </summary>
            public string[1, 255] cert_copy { get; set; }

            /// <summary>
            ///  登记证书类型 
            /// 1、主体为“政府机关/事业单位/社会组织”时，请上传登记证书类型。
            /// 2、主体为“个体工商户/企业”时，不填。
            /// 当主体为事业单位时，选择此枚举值：
            /// CERTIFICATE_TYPE_2388：事业单位法人证书
            /// 
            /// 当主体为政府机关，选择此枚举值：
            /// CERTIFICATE_TYPE_2389：统一社会信用代码证书
            /// 
            /// 当主体为社会组织，选择以下枚举值之一：
            /// CERTIFICATE_TYPE_2389：统一社会信用代码证书
            /// CERTIFICATE_TYPE_2394：社会团体法人登记证书
            /// CERTIFICATE_TYPE_2395：民办非企业单位登记证书
            /// CERTIFICATE_TYPE_2396：基金会法人登记证书
            /// 
            /// 
            /// CERTIFICATE_TYPE_2520：执业许可证/执业证 
            /// CERTIFICATE_TYPE_2521：基层群众性自治组织特别法人统一社会信用代码证 
            /// CERTIFICATE_TYPE_2522：农村集体经济组织登记证
            /// CERTIFICATE_TYPE_2399：宗教活动场所登记证
            /// CERTIFICATE_TYPE_2400：政府部门下发的其他有效证明文件
            /// 示例值：CERTIFICATE_TYPE_2388
            /// 可为空: True
            /// </summary>
            public string cert_type { get; set; }

            /// <summary>
            ///  证书号 
            /// 1、请填写登记证书上的证书编号
            /// 示例值：111111111111
            /// 可为空: True
            /// </summary>
            public string[1, 32] cert_number { get; set; }

            /// <summary>
            ///  商户名称 
            /// 1、请填写登记证书上的商户名称
            /// 示例值：xx公益团体
            /// 可为空: True
            /// </summary>
            public string[1, 128] merchant_name { get; set; }

            /// <summary>
            ///  注册地址 
            /// 1、请填写登记证书的注册地址
            /// 示例值：广东省深圳市南山区xx路xx号
            /// 可为空: True
            /// </summary>
            public string[1, 128] company_address { get; set; }

            /// <summary>
            ///  法定代表人 
            /// 1、只能由中文字符、英文字符、可见符号组成
            /// 2、请填写登记证书上的法定代表人姓名
            /// 示例值：李四
            /// 可为空: True
            /// </summary>
            public string[1, 64] legal_person { get; set; }

            /// <summary>
            ///  有效期限开始日期 
            /// 1、必填，请参考示例值填写；
            /// 2、结束日期大于开始日期；
            /// 示例值：2019-08-01
            /// 可为空: True
            /// </summary>
            public string period_begin { get; set; }

            /// <summary>
            ///  有效期限结束日期 
            /// 1、必填，请参考示例值填写；
            /// 2、若证件有效期为长期，请填写：长期；
            /// 3、结束日期大于开始日期；
            /// 示例值：2019-08-01
            /// 可为空: True
            /// </summary>
            public string period_end { get; set; }



        }


        /// <summary>
        /// 金融机构许可证信息
        /// 当主体是金融机构时，必填。
        /// <summary>
        public class Finance_Institution_Info
        {

            /// <summary>
            ///  金融机构类型 
            /// 金融机构类型需与营业执照/登记证书上一致，可参考选择金融机构指引。
            /// BANK_AGENT：银行业, 适用于商业银行、政策性银行、农村合作银行、村镇银行、开发性金融机构等
            /// PAYMENT_AGENT：支付机构, 适用于非银行类支付机构
            /// INSURANCE：保险业, 适用于保险、保险中介、保险代理、保险经纪等保险类业务
            /// TRADE_AND_SETTLE：交易及结算类金融机构, 适用于交易所、登记结算类机构、银行卡清算机构、资金清算中心等
            /// OTHER：其他金融机构, 适用于财务公司、信托公司、金融资产管理公司、金融租赁公司、汽车金融公司、贷款公司、货币经纪公司、消费金融公司、证券业、金融控股公司、股票、期货、货币兑换、小额贷款公司、金融资产管理、担保公司、商业保理公司、典当行、融资租赁公司、财经咨询等其他金融业务
            /// 示例值：BANK_AGENT
            /// 可为空: True
            /// </summary>
            public string finance_type { get; set; }

            /// <summary>
            ///  金融机构许可证图片 
            /// 1、根据所属金融机构类型的许可证要求提供，详情查看金融机构指引。
            /// 2、请提供为“申请商家主体”所属的许可证，可授权使用总公司/分公司的特殊资质。
            /// 3、最多可上传5张照片，请填写通过图片上传API预先上传图片生成好的MediaID。
            /// 示例值：0P3ng6KTIW4-Q_l2FjmFJBZR9FwczhJehHhAZN6BKXQPcs-VvdSo
            /// 可为空: True
            /// </summary>
            public string[] finance_license_pics { get; set; }



        }


        /// <summary>
        /// 经营者/法人身份证件
        /// 1、个体户：请上传经营者的身份证件。
        /// 2、企业/社会组织：请上传法人的身份证件。
        /// 3、政府机关/事业单位：请上传法人/经办人的身份证件。
        /// <summary>
        public class Identity_Info
        {

            /// <summary>
            ///  证件持有人类型 
            /// 1. 主体类型为政府机关、事业单位时选传：
            /// （1）若上传的是法人证件，则不需要上传该字段
            /// （2）若因特殊情况，无法提供法人证件时，可上传经办人。 （经办人：经商户授权办理微信支付业务的人员，授权范围包括但不限于签约，入驻过程需完成账户验证）。
            /// 2. 主体类型为企业、个体户、社会组织时，默认为经营者/法人，不需要上传该字段。
            /// LEGAL：法人
            /// SUPER：经办人
            /// 示例值：LEGAL
            /// 可为空: False
            /// </summary>
            public string id_holder_type { get; set; }

            /// <summary>
            ///  证件类型 
            /// 1、当证件持有人类型为法人时，填写。其他情况，无需上传。
            /// 2、个体户/企业/事业单位/社会组织：可选择任一证件类型，政府机关仅支持中国大陆居民-身份证类型。
            /// IDENTIFICATION_TYPE_IDCARD：中国大陆居民-身份证 
            /// IDENTIFICATION_TYPE_OVERSEA_PASSPORT：其他国家或地区居民-护照 
            /// IDENTIFICATION_TYPE_HONGKONG_PASSPORT：中国香港居民-来往内地通行证 
            /// IDENTIFICATION_TYPE_MACAO_PASSPORT：中国澳门居民-来往内地通行证 
            /// IDENTIFICATION_TYPE_TAIWAN_PASSPORT：中国台湾居民-来往大陆通行证 
            /// IDENTIFICATION_TYPE_FOREIGN_RESIDENT：外国人居留证 
            /// IDENTIFICATION_TYPE_HONGKONG_MACAO_RESIDENT：港澳居民证 
            /// IDENTIFICATION_TYPE_TAIWAN_RESIDENT：台湾居民证
            /// 示例值：IDENTIFICATION_TYPE_IDCARD
            /// 可为空: False
            /// </summary>
            public string id_doc_type { get; set; }

            /// <summary>
            ///  法定代表人说明函 
            /// 1、当证件持有人类型为经办人时，必须上传。其他情况，无需上传。
            /// 2、若因特殊情况，无法提供法定代表人证件时，请参照示例图打印法定代表人说明函，全部信息需打印，不支持手写商户信息，并加盖公章。
            /// 3、可上传1张图片，请填写通过图片上传APIAPI预先上传图片生成好的MediaID。
            /// 示例值：47ZC6GC-vnrbEny_Ie_An5-tCpqxucuxi-vByf3Gjm7KEIUv0OF4wFNIO4kqg05InE4d2I6_H7I4
            /// 可为空: False
            /// </summary>
            public string[1, 255] authorize_letter_copy { get; set; }

            /// <summary>
            /// 身份证信息
            /// 当证件持有人类型为经营者/法人且证件类型为“身份证”时填写。
            /// 可为空: False
            /// </summary>
            public Id_Card_Info id_card_info { get; set; }

            /// <summary>
            /// 其他类型证件信息
            /// 当证件持有人类型为经营者/法人且证件类型不为“身份证”时填写。
            /// 可为空: False
            /// </summary>
            public Id_Doc_Info id_doc_info { get; set; }

            /// <summary>
            ///  经营者/法人是否为受益人 
            /// 主体类型为企业时，需要填写：
            /// 1、若经营者/法人是最终受益人，则填写：true。
            /// 2、若经营者/法人不是最终受益人，则填写：false。
            /// 示例值：true
            /// 可为空: False
            /// </summary>
            public boolean owner { get; set; }


            #region 子数据类型

            /// <summary>
            /// 身份证信息
            /// 当证件持有人类型为经营者/法人且证件类型为“身份证”时填写。
            /// <summary>
            public class Id_Card_Info
            {

                /// <summary>
                ///  身份证人像面照片 
                /// 1、请上传个体户经营者/法人的身份证人像面照片
                /// 2、可上传1张图片，请填写通过图片上传API接口预先上传图片生成好的MediaID
                /// 3、请上传彩色照片or彩色扫描件or复印件（需加盖公章鲜章），可添加“微信支付”相关水印（如微信支付认证）。
                /// 示例值：jTpGmxUX3FBWVQ5NJTZvlKX_gdU4cRz7z5NxpnFuAxhBTEO_PvWkfSCJ3zVIn001D8daLC-ehEuo0BJqRTvDujqhThn4ReFxikqJ5YW6zFQ
                /// 可为空: True
                /// </summary>
                public string[1, 256] id_card_copy { get; set; }

                /// <summary>
                ///  身份证国徽面照片 
                /// 1、请上传个体户经营者/法定代表人的身份证国徽面照片
                /// 2、可上传1张图片，请填写通过图片上传API接口预先上传图片生成好的MediaID
                /// 3、请上传彩色照片or彩色扫描件or复印件（需加盖公章鲜章），可添加“微信支付”相关水印（如微信支付认证）。
                /// 示例值：47ZC6GC-vnrbEny__Ie_An5-tCpqxucuxi-vByf3Gjm7KE53JXvGy9tqZm2XAUf-4KGprrKhpVBDIUv0OF4wFNIO4kqg05InE4d2I6_H7I4
                /// 可为空: True
                /// </summary>
                public string[1, 256] id_card_national { get; set; }

                /// <summary>
                ///  身份证姓名 
                /// 1、请填写个体户经营者/法定代表人对应身份证的姓名，2~30个中文字符、英文字符、符号
                /// 2、该字段需进行加密处理，加密方法详见敏感信息加密说明。(提醒：必须在HTTP头中上送Wechatpay-Serial) 
                /// 示例值：pVd1HJ6zyvPedzGaV+X3qtmrq9bb9tPROvwia4ibL+F6mfjbzQIzfb3HHLEjZ4YiR/cv/69bDnuC4EL5Kz4jBHLiCyOb+tI0m2qhZ9evAM+Jv1z0NVa8MRtelw/wDa4SzfeespQO/0kjiwfqdfg==
                /// 可为空: True
                /// </summary>
                public string[1, 256] id_card_name { get; set; }

                /// <summary>
                ///  身份证号码 
                /// 1、请填写个体户经营者/法定代表人对应身份证的号码
                /// 2、15位数字或17位数字+1位数字|X ，该字段需进行加密处理，加密方法详见敏感信息加密说明。(提醒：必须在HTTP头中上送Wechatpay-Serial) 
                /// 示例值：AOZdYGISxo4y44/UgZ69bdu9X+tfMUJ9dl+LetjM45/zMbrYu+wWZ8gn4CTdo+jgKsadIKHXtb3JZKGZjduGdtkRJJp0/0eow96uY1Pk7Rq79Jtt7+I8juwEc4P4TG5xzchG/5IL9DBd+Z0zZXkw==
                /// 可为空: True
                /// </summary>
                public string[1, 256] id_card_number { get; set; }

                /// <summary>
                ///  身份证居住地址 
                /// 1、主体类型为企业时，需要填写。其他主体类型，无需上传。
                /// 2、请按照身份证住址填写，如广东省深圳市南山区xx路xx号xx室
                /// 3、 该字段需进行加密处理，加密方法详见敏感信息加密说明。(提醒：必须在HTTP头中上送Wechatpay-Serial)。
                /// 示例值：pVd1HJ6zyvPedzGaV+X3qtmrq9bb9tPROvwia4ibL+F6mfjbzQIzfb3HHLEjZ4YiR/cJiCrZxnAqiEL5Kz4jBHLiCyOb+tI0m2qhZ9evAM+Jv1z0NVa8MRtelw/wDa4SzfeespQO/0kjiwfqdfg==
                /// 可为空: False
                /// </summary>
                public string[1, 512] id_card_address { get; set; }

                /// <summary>
                /// 身份证有效期开始时间 
                /// 1、必填，请按照示例值填写。
                /// 2、结束时间大于开始时间。 
                /// 示例值：2026-06-06 
                /// 可为空: True
                /// </summary>
                public string card_period_begin { get; set; }

                /// <summary>
                /// 身份证有效期结束时间 
                /// 1、必填，请按照示例值填写。
                /// 2、若证件有效期为长期，请填写：长期。
                /// 3、结束时间大于开始时间。
                /// 示例值：2036-06-06
                /// 可为空: True
                /// </summary>
                public string card_period_end { get; set; }



            }


            /// <summary>
            /// 其他类型证件信息
            /// 当证件持有人类型为经营者/法人且证件类型不为“身份证”时填写。
            /// <summary>
            public class Id_Doc_Info
            {

                /// <summary>
                ///  证件正面照片 
                /// 1、证件类型不为“身份证”"时，上传证件正面照片。
                /// 2、可上传1张图片，请填写通过图片上传API接口预先上传图片生成好的MediaID。
                /// 3、请上传彩色照片or彩色扫描件or复印件（需加盖公章鲜章），可添加“微信支付”相关水印（如微信支付认证）。
                /// 示例值：jTpGmxUX3FBWVQ5NJTZvlKX_gdU4cRz7z5NxpnFuAxhBTEO_PvWkfSCJ3zVIn001D8daLC-ehEuo0BJqRTvDujqhThn4ReFxikqJ5YW6zFQ
                /// 可为空: True
                /// </summary>
                public string[1, 512] id_doc_copy { get; set; }

                /// <summary>
                ///  证件反面照片 
                /// 1、若证件类型为往来通行证、外国人居留证、港澳居住证、台湾居住证时，上传证件反面照片。
                /// 2、若证件类型为护照，无需上传反面照片
                /// 3、可上传1张图片，请填写通过图片上传API接口预先上传图片生成好的MediaID。
                /// 4、请上传彩色照片or彩色扫描件or复印件（需加盖公章鲜章），可添加“微信支付”相关水印（如微信支付认证）。
                /// 示例值：jTpGmxUX3FBWVQ5NJTZvvDujqhThn4ReFxikqJ5YW6zFQ
                /// 可为空: False
                /// </summary>
                public string[1, 512] id_doc_copy_back { get; set; }

                /// <summary>
                ///  证件姓名 
                /// 1、请填写经营者/法定代表人的证件姓名，2~30个中文字符、英文字符、符号
                /// 2、该字段需进行加密处理，加密方法详见敏感信息加密说明。(提醒：必须在HTTP头中上送Wechatpay-Serial) 
                /// 示例值：pVd1HJ6zyvPedzGaV+X3qtmrq9bb9tPROvwia4ibL+F6mfjbzQIzfb3HHLEjZ4YiR/cJiCrZxnAqi+pjeKz4jBHLiCyOb+tI0m2qhZ9evAM+Jv1z0NVa8MRtelw/wDa4SzfeespQO/0kjiwfqdfg==
                /// 可为空: True
                /// </summary>
                public string[1, 128] id_doc_name { get; set; }

                /// <summary>
                ///  证件号码 
                /// 1、请填写经营者/法定代表人的证件号码
                /// 2、8-30位数字|字母|连字符 ，该字段需进行加密处理，加密方法详见敏感信息加密说明。(提醒：必须在HTTP头中上送Wechatpay-Serial) 
                /// 示例值：AOZdYGISxo4y44/UgZ69bdu9X+tfMUJ9dl+LetjM45/zMbrYu+wWZ8gn4CTdo+D/m9MrPg+V4smJZKGZjduGdtkRJJp0/0eow96uY1Pk7Rq79Jtt7+I8juwEc4P4TG5xzchG/5IL9DBd+Z0zZXkw==
                /// 可为空: True
                /// </summary>
                public string[1, 128] id_doc_number { get; set; }

                /// <summary>
                ///  证件居住地址 
                /// 1、主体类型为企业时，需要填写。其他主体类型，无需上传。
                /// 2、请按照证件上住址填写，若证件上无住址则按照实际住址填写，如广东省深圳市南山区xx路xx号xx室。
                /// 3、 该字段需进行加密处理，加密方法详见敏感信息加密说明。(提醒：必须在HTTP头中上送Wechatpay-Serial)。
                /// 示例值：pVd1HJ6zyvPedzGaV+X3qtmrq9bb9tPROvwia4ibL+F6mfjbzQIzfb3HHLEjZ4YiR/cJiCrZxnAqi+pjeKIEdkwzXRAI7FUhrf2qhZ9evAM+Jv1z0NVa8MRtelw/wDa4SzfeespQO/0kjiwfqdfg==
                /// 可为空: False
                /// </summary>
                public string[1, 256] id_doc_address { get; set; }

                /// <summary>
                ///  证件有效期开始时间 
                /// 1、必填，请按照示例值填写
                /// 2、结束时间大于开始时间；
                /// 示例值：2019-06-06
                /// 可为空: True
                /// </summary>
                public string[1, 128] doc_period_begin { get; set; }

                /// <summary>
                ///  证件有效期结束时间 
                /// 1、必填，请按照示例值填写
                /// 2、若证件有效期为长期，请填写：长期；
                /// 3、结束时间大于开始时间；
                /// 示例值：2026-06-06
                /// 可为空: True
                /// </summary>
                public string[1, 128] doc_period_end { get; set; }



            }

            #endregion
        }


        /// <summary>
        /// 最终受益人信息列表(UBO)
        /// 仅企业需要填写。
        /// 若经营者/法人不是最终受益所有人，则需补充受益所有人信息，最多上传4个。
        /// 若经营者/法人是最终受益所有人之一，可在此添加其他受益所有人信息，最多上传3个。
        /// 根据国家相关法律法规，需要提供公司受益所有人信息，受益所有人需符合至少以下条件之一：
        /// 1、直接或者间接拥有超过25%公司股权或者表决权的自然人。
        /// 2、通过人事、财务等其他方式对公司进行控制的自然人。
        /// 3、公司的高级管理人员，包括公司的经理、副经理、财务负责人、上市公司董事会秘书和公司章程规定的其他人员。
        /// <summary>
        public class Ubo_Info_List
        {

            /// <summary>
            ///  证件类型 
            /// 请填写受益人的证件类型。枚举值： 
            /// IDENTIFICATION_TYPE_IDCARD：中国大陆居民-身份证 
            /// IDENTIFICATION_TYPE_OVERSEA_PASSPORT：其他国家或地区居民-护照 
            /// IDENTIFICATION_TYPE_HONGKONG_PASSPORT：中国香港居民-来往内地通行证 
            /// IDENTIFICATION_TYPE_MACAO_PASSPORT：中国澳门居民-来往内地通行证 
            /// IDENTIFICATION_TYPE_TAIWAN_PASSPORT：中国台湾居民-来往大陆通行证 
            /// IDENTIFICATION_TYPE_FOREIGN_RESIDENT：外国人居留证 
            /// IDENTIFICATION_TYPE_HONGKONG_MACAO_RESIDENT：港澳居民证 
            /// IDENTIFICATION_TYPE_TAIWAN_RESIDENT：台湾居民证
            /// 示例值：IDENTIFICATION_TYPE_IDCARD
            /// 可为空: True
            /// </summary>
            public string ubo_id_doc_type { get; set; }

            /// <summary>
            ///  证件正面照片 
            /// 1、请上传受益人证件的正面照片。
            /// 2、若证件类型为身份证，请上传人像面照片。
            /// 3、可上传1张图片，请填写通过图片上传API预先上传图片生成好的MediaID。
            /// 4、请上传彩色照片or彩色扫描件or复印件（需加盖公章鲜章），可添加“微信支付”相关水印（如微信支付认证）。
            /// 示例值：jTpGmxUXqRTvDujqhThn4ReFxikqJ5YW6zFQ
            /// 可为空: True
            /// </summary>
            public string[1, 256] ubo_id_doc_copy { get; set; }

            /// <summary>
            ///  证件反面照片 
            /// 1、请上传受益人证件的反面照片。
            /// 2、若证件类型为身份证，请上传国徽面照片。
            /// 									3、若证件类型为护照，无需上传反面照片。
            /// 4、可上传1张图片，请填写通过图片上传API预先上传图片生成好的MediaID。
            /// 5、请上传彩色照片or彩色扫描件or复印件（需加盖公章鲜章），可添加“微信支付”相关水印（如微信支付认证）。
            /// 示例值：jTpGmxUX3FBWVQ5NJTZvvDujqhThn4ReFxikqJ5YW6zFQ
            /// 
            /// 可为空: False
            /// </summary>
            public string[1, 256] ubo_id_doc_copy_back { get; set; }

            /// <summary>
            ///  证件姓名 
            /// 该字段需进行加密处理，加密方法详见敏感信息加密说明。(提醒：必须在HTTP头中上送Wechatpay-Serial)
            /// 示例值：AOZdYGISxo4y44/Ug4P4TG5xzchG/5IL9DBd+Z0zZXkw==
            /// 可为空: True
            /// </summary>
            public string[1, 128] ubo_id_doc_name { get; set; }

            /// <summary>
            ///  证件号码 
            /// 该字段需进行加密处理，加密方法详见敏感信息加密说明。(提醒：必须在HTTP头中上送Wechatpay-Serial)
            /// 示例值：AOZdYGISxo4y44/Ug4P4TG5xzchG/5IL9DBd+Z0zZXkw==
            /// 可为空: True
            /// </summary>
            public string[1, 128] ubo_id_doc_number { get; set; }

            /// <summary>
            ///  证件居住地址 
            /// 1、请按照证件上住址填写，若证件上无住址则按照实际住址填写，如广东省深圳市南山区xx路xx号xx室。
            /// 2、 该字段需进行加密处理，加密方法详见敏感信息加密说明。(提醒：必须在HTTP头中上送Wechatpay-Serial)
            /// 示例值：pVd1HJ6zyvPedzGaV+X3qtmrq9bb9tPROvwia4ibL+F6mfevAM+Jv1z0NVa8MRtelw/wDa4SzfeespQO/0kjiwfqdfg==
            /// 可为空: True
            /// </summary>
            public string[1, 512] ubo_id_doc_address { get; set; }

            /// <summary>
            ///  证件有效期开始时间 
            /// 1、请按照示例值填写。
            /// 2、结束时间大于开始时间。
            /// 示例值：2019-06-06
            /// 可为空: True
            /// </summary>
            public string[1, 128] ubo_period_begin { get; set; }

            /// <summary>
            ///  证件有效期结束时间 
            /// 1、请按照示例值填写，若证件有效期为长期，请填写：长期。
            /// 2、结束时间大于开始时间。
            /// 示例值：2026-06-06
            /// 可为空: True
            /// </summary>
            public string[1, 128] ubo_period_end { get; set; }



        }

        #endregion
    }


    /// <summary>
    /// 经营资料
    /// body请填写商家的经营业务信息、售卖商品/提供服务场景信息
    /// <summary>
    public class Business_Info
    {

        /// <summary>
        ///  商户简称 
        /// 1、请输入2-30个字符，支持中文/字母/数字/特殊符号
        /// 2、在支付完成页向买家展示，需与微信经营类目相关；
        /// 3、简称要求
        ///     （1）不支持单纯以人名来命名，若为个体户经营，可用“个体户+经营者名称”或“经营者名称+业务”命名，如“个体户张三”或“张三餐饮店”；
        ///     （2）不支持无实际意义的文案，如“XX特约商户”、“800”、“XX客服电话XXX”；
        /// 示例值：张三餐饮店
        /// 可为空: True
        /// </summary>
        public string[1, 64] merchant_shortname { get; set; }

        /// <summary>
        ///  客服电话 
        /// 将在交易记录中向买家展示，请确保电话畅通以便平台回拨确认
        /// 示例值：0758XXXXX
        /// 可为空: True
        /// </summary>
        public string[1, 32] service_phone { get; set; }

        /// <summary>
        /// 经营场景
        /// 请根据实际经营情况，填写经营场景
        /// 可为空: True
        /// </summary>
        public Sales_Info sales_info { get; set; }


        #region 子数据类型

        /// <summary>
        /// 经营场景
        /// 请根据实际经营情况，填写经营场景
        /// <summary>
        public class Sales_Info
        {

            /// <summary>
            ///  经营场景类型 
            /// 1、请勾选实际售卖商品/提供服务场景（至少一项），以便为你开通需要的支付权限。
            /// 2、建议只勾选目前必须的场景，以便尽快通过入驻审核，其他支付权限可在入驻后再根据实际需要发起申请。
            /// SALES_SCENES_STORE：线下场所
            /// SALES_SCENES_MP：公众号
            /// SALES_SCENES_MINI_PROGRAM：小程序
            /// SALES_SCENES_WEB：互联网网站
            /// SALES_SCENES_APP：APP
            /// SALES_SCENES_WEWORK：企业微信
            /// 示例值：SALES_SCENES_STORE
            /// 可为空: True
            /// </summary>
            public string[] sales_scenes_type { get; set; }

            /// <summary>
            /// 线下场所场景
            /// 1、审核通过后，服务商可帮商户发起付款码支付、JSAPI支付。
            /// 2、当"经营场景类型"选择"SALES_SCENES_STORE"，该场景资料必填。
            /// 可为空: False
            /// </summary>
            public Biz_Store_Info biz_store_info { get; set; }

            /// <summary>
            /// 公众号场景
            /// 1、审核通过后，服务商可帮商家发起JSAPI支付
            /// 2、当"经营场景类型"选择"SALES_SCENES_MP"，该场景资料必填。 
            /// 可为空: False
            /// </summary>
            public Mp_Info mp_info { get; set; }

            /// <summary>
            /// 小程序场景
            /// 1、审核通过后，服务商可帮商家发起JSAPI支付
            /// 2、当"经营场景类型"选择"SALES_SCENES_MINI_PROGRAM"，该场景资料必填。
            /// 可为空: False
            /// </summary>
            public Mini_Program_Info mini_program_info { get; set; }

            /// <summary>
            /// App场景
            /// 1、审核通过后，服务商可帮商家发起App支付
            /// 2、当"经营场景类型"选择"SALES_SCENES_APP"，该场景资料必填。
            /// 可为空: False
            /// </summary>
            public App_Info app_info { get; set; }

            /// <summary>
            /// 互联网网站场景
            /// 1、审核通过后，服务商可帮商家发起JSAPI支付、Native支付
            /// 2、当"经营场景类型"选择"SALES_SCENES_WEB"，该场景资料必填。 
            /// 可为空: False
            /// </summary>
            public Web_Info web_info { get; set; }

            /// <summary>
            /// 企业微信场景
            /// 1、审核通过后，服务商可帮商家发起企业微信支付
            /// 2、当"经营场景类型"选择"SALES_SCENES_WEWORK"，该场景资料必填。
            /// 可为空: False
            /// </summary>
            public Wework_Info wework_info { get; set; }


            #region 子数据类型

            /// <summary>
            /// 线下场所场景
            /// 1、审核通过后，服务商可帮商户发起付款码支付、JSAPI支付。
            /// 2、当"经营场景类型"选择"SALES_SCENES_STORE"，该场景资料必填。
            /// <summary>
            public class Biz_Store_Info
            {

                /// <summary>
                ///  线下场所名称 
                /// 请填写门店名称
                /// 示例值：大郎烧饼
                /// 可为空: True
                /// </summary>
                public string[1, 128] biz_store_name { get; set; }

                /// <summary>
                ///  线下场所省市编码 
                /// 1、只能由数字组成
                /// 2、详细参见微信支付提供的省市对照表
                /// 示例值：440305
                /// 可为空: True
                /// </summary>
                public string[1, 128] biz_address_code { get; set; }

                /// <summary>
                ///  线下场所地址 
                /// 请填写详细的经营场所信息，如有多个场所，选择一个主要场所填写即可。
                /// 示例值：南山区xx大厦x层xxxx室
                /// 可为空: True
                /// </summary>
                public string[1, 128] biz_store_address { get; set; }

                /// <summary>
                ///  线下场所门头照片 
                /// 1、请上传门店照片（要求门店招牌清晰可见）。若为停车场、售卖机等无固定门头照片 的经营场所，请提供真实的经营现场照片即可；
                /// 2、请填写通过图片上传API接口预先上传图片生成好的MediaID。
                /// 示例值：0P3ng6KTIW4-Q_l2FjKLZuhHjBWoMAjmVtCz7ScmhEIThCaV-4BBgVwtNkCHO_XXqK5dE5YdOmFJBZR9FwczhJehHhAZN6BKXQPcs-VvdSo
                /// 可为空: True
                /// </summary>
                public string[] store_entrance_pic { get; set; }

                /// <summary>
                ///  线下场所内部照片 
                /// 1、请上传门店内部环境照片。若为停车场、售卖机等无固定门头照片的经营场所，请提 供真实的经营现场照片即可；
                /// 2、请填写通过图片上传API接口预先上传图片生成好的MediaID。
                /// 示例值：0P3ng6KTIW4-Q_l2FjKLZuhHjBWoMAjmVtCz7ScmhEIThCaV-4BBgVwtNkCHO_XXqK5dE5YdOmFJBZR9FwczhJehHhAZN6BKXQPcs-VvdSo
                /// 可为空: True
                /// </summary>
                public string[] indoor_pic { get; set; }

                /// <summary>
                ///  线下场所对应的商家AppID 
                /// 1、可填写与商家主体一致且已认证的公众号、小程序、APP的AppID，其中公众号AppID需是已认证的服务号、政府或媒体类型的订阅号；
                /// 2、审核通过后，系统将额外为商家开通付款码支付、JSAPI支付的自有交易权限，并完成商家商户号与该AppID的绑定；
                /// 示例值：wx1234567890123456
                /// 可为空: False
                /// </summary>
                public string[1, 256] biz_sub_appid { get; set; }



            }


            /// <summary>
            /// 公众号场景
            /// 1、审核通过后，服务商可帮商家发起JSAPI支付
            /// 2、当"经营场景类型"选择"SALES_SCENES_MP"，该场景资料必填。 
            /// <summary>
            public class Mp_Info
            {

                /// <summary>
                ///  服务商公众号AppID 
                /// 1、服务商公众号APPID与商家公众号APPID，二选一必填。
                /// 2、可填写当前服务商商户号已绑定的公众号APPID。 
                /// 示例值：wx1234567890123456
                /// 可为空: False
                /// </summary>
                public string mp_appid { get; set; }

                /// <summary>
                ///  商家公众号AppID 
                /// 1、服务商公众号APPID与商家公众号APPID，二选一必填。
                /// 2、可填写与商家主体一致且已认证的公众号APPID，需是已认证的服务号、政府或媒体类型的订阅号。
                /// 3、审核通过后，系统将发起特约商家商户号与该AppID的绑定（即配置为sub_appid），服务商随后可在发起支付时选择传入该appid，以完成支付，并获取sub_openid用于数据统计，营销等业务场景 。 
                /// 示例值：wx1234567890123456 多选一
                /// 可为空: False
                /// </summary>
                public string mp_sub_appid { get; set; }

                /// <summary>
                ///  公众号页面截图 
                /// 1、请提供展示商品/服务的页面截图/设计稿（最多5张），若公众号未建设完善或未上线请务必提供。
                /// 2、请填写通过图片上传API预先上传图片生成好的MediaID。 
                /// 示例值：0P3ng6KTIW4-Q_l2FjmFJBZR9FwczhJehHhAZN6BKXQPcs-VvdSo 
                /// 可为空: True
                /// </summary>
                public string[] mp_pics { get; set; }



            }


            /// <summary>
            /// 小程序场景
            /// 1、审核通过后，服务商可帮商家发起JSAPI支付
            /// 2、当"经营场景类型"选择"SALES_SCENES_MINI_PROGRAM"，该场景资料必填。
            /// <summary>
            public class Mini_Program_Info
            {

                /// <summary>
                /// 服务商小程序APPID 
                /// 1、服务商小程序APPID与商家小程序APPID，二选一必填。
                /// 2、可填写当前服务商商户号已绑定的小程序APPID。 
                /// 示例值：wx1234567890123456 
                /// 可为空: False
                /// </summary>
                public string mini_program_appid { get; set; }

                /// <summary>
                /// 商家小程序APPID 
                /// 1、服务商小程序APPID与商家小程序APPID，二选一必填；
                /// 2、请填写已认证的小程序APPID；
                /// 3、完成进件后，系统发起特约商户号与该AppID的绑定（即配置为sub_appid可在发起支付时传入）
                /// （1）若APPID主体与商家主体/服务商主体一致，则直接完成绑定；
                /// （2）若APPID主体与商家主体/服务商主体不一致，则商户签约时显示《联合营运承诺函》，并且AppID的管理员需登录公众平台确认绑定意愿； 
                /// 示例值：wx1234567890123456 多选一
                /// 可为空: False
                /// </summary>
                public string mini_program_sub_appid { get; set; }

                /// <summary>
                /// 小程序截图 
                /// 1、请提供展示商品/服务的页面截图/设计稿（最多5张），若小程序未建设完善或未上线 请务必提供；
                /// 2、请填写通过图片上传API预先上传图片生成好的MediaID。 
                /// 示例值：0P3ng6KTIW4-Q_l2FjmFJBZR9FwczhJehHhAZN6BKXQPcs-VvdSo 
                /// 可为空: False
                /// </summary>
                public string[] mini_program_pics { get; set; }



            }


            /// <summary>
            /// App场景
            /// 1、审核通过后，服务商可帮商家发起App支付
            /// 2、当"经营场景类型"选择"SALES_SCENES_APP"，该场景资料必填。
            /// <summary>
            public class App_Info
            {

                /// <summary>
                /// 服务商应用APPID 
                /// 1、服务商应用APPID与商家应用APPID，二选一必填。
                /// 2、可填写当前服务商商户号已绑定的应用APPID。 
                /// 示例值：wx1234567890123456 
                /// 可为空: False
                /// </summary>
                public string app_appid { get; set; }

                /// <summary>
                /// 商家应用APPID 
                /// 1、服务商应用APPID与商家应用APPID，二选一必填。
                /// 2、可填写与商家主体一致且已认证的应用APPID，需是已认证的APP。
                /// 3、审核通过后，系统将发起特约商家商户号与该AppID的绑定（即配置为sub_appid），服务商随后可在发起支付时选择传入该appid，以完成支付，并获取sub_openid用于数据统计，营销等业务场景。
                /// 示例值：wx1234567890123456多选一
                /// 可为空: False
                /// </summary>
                public string app_sub_appid { get; set; }

                /// <summary>
                /// APP截图 
                /// 1、请提供APP首页截图、尾页截图、应用内截图、支付页截图各1张。
                /// 2、请填写通过图片上传API预先上传图片生成好的MediaID。
                /// 示例值：0P3ng6KTIW4-Q_l2FjmFJBZR9FwczhJehHhAZN6BKXQPcs-VvdSo 
                /// 可为空: True
                /// </summary>
                public string[] app_pics { get; set; }



            }


            /// <summary>
            /// 互联网网站场景
            /// 1、审核通过后，服务商可帮商家发起JSAPI支付、Native支付
            /// 2、当"经营场景类型"选择"SALES_SCENES_WEB"，该场景资料必填。 
            /// <summary>
            public class Web_Info
            {

                /// <summary>
                /// 互联网网站域名 
                /// 1、如为PC端商城、智能终端等场景，可上传官网链接。
                /// 2、网站域名需ICP备案，若备案主体与申请主体不同，请上传加盖公章的网站授权函。 
                /// 示例值：http://www.qq.com
                /// 可为空: True
                /// </summary>
                public string domain { get; set; }

                /// <summary>
                /// 网站授权函 
                /// 1、若备案主体与申请主体不同，请务必上传加盖公章的网站授权函。
                /// 2、请填写通过图片上传API预先上传图片生成好的MediaID。 
                /// 示例值：
                /// 47ZC6GC-vnrbEnyVBDIUv0OF4wFNIO4kqg05InE4d2I6_H7I4 
                /// 可为空: False
                /// </summary>
                public string web_authorisation { get; set; }

                /// <summary>
                /// 互联网网站对应的商家APPID 
                /// 1、可填写已认证的公众号、小程序、应用的APPID，其中公众号APPID需是已认证的服务
                /// 号、政府或媒体类型的订阅号；
                /// 2、完成进件后，系统发起特约商户号与该AppID的绑定（即配置为sub_appid，可在发起支付时传入）
                ///    （1）若APPID主体与商家主体一致，则直接完成绑定；
                ///    （2）若APPID主体与商家主体不一致，则商户签约时显示《联合营运承诺函》，并且
                /// AppID的管理员需登录公众平台确认绑定意愿；（ 暂不支持绑定异主体的应用APPID）。
                /// 示例值：wx1234567890123456 
                /// 可为空: False
                /// </summary>
                public string web_appid { get; set; }



            }


            /// <summary>
            /// 企业微信场景
            /// 1、审核通过后，服务商可帮商家发起企业微信支付
            /// 2、当"经营场景类型"选择"SALES_SCENES_WEWORK"，该场景资料必填。
            /// <summary>
            public class Wework_Info
            {

                /// <summary>
                /// 商家企业微信CorpID 
                /// 1、可填写与商家主体一致且已认证的企业微信CorpID。
                /// 2、审核通过后，系统将为商家开通企业微信专区的自有交易权限，并完成商家商户号与该APPID的绑定，商家可自行发起交易。 
                /// 示例值：wx1234567890123456
                /// 可为空: True
                /// </summary>
                public string sub_corp_id { get; set; }

                /// <summary>
                ///  企业微信页面截图 
                /// 1、最多可上传5张照片
                /// 2、请填写通过图片上传API接口预先上传图片生成好的MediaID 
                /// 示例值：0P3ng6KTIW4-Q_l2FjmFJBZR9FwczhJehHhAZN6BKXQPcs-VvdSo
                /// 
                /// 可为空: True
                /// </summary>
                public string[] wework_pics { get; set; }



            }

            #endregion
        }

        #endregion
    }


    /// <summary>
    /// 结算规则
    /// body请填写商家的结算费率规则、特殊资质等信息
    /// <summary>
    public class Settlement_Info
    {

        /// <summary>
        ///  入驻结算规则ID 
        /// 请选择结算规则ID，详细参见费率结算规则对照表
        /// 示例值：719
        /// 可为空: True
        /// </summary>
        public string[1, 3] settlement_id { get; set; }

        /// <summary>
        ///  所属行业 
        /// 填写指定行业名称，详细参见费率结算规则对照表
        /// 示例值：餐饮
        /// 可为空: True
        /// </summary>
        public string[1, 128] qualification_type { get; set; }

        /// <summary>
        ///  特殊资质图片 
        /// 1、根据所属行业的特殊资质要求提供，详情查看费率结算规则对照表
        /// 2、最多可上传5张照片，请填写通过图片上传API接口预先上传图片生成好的MediaID
        /// 示例值：0P3ng6KTIW4-Q_l2FjmFJBZR9FwczhJehHhAZN6BKXQPcs-VvdSo 
        /// 可为空: False
        /// </summary>
        public string[] qualifications { get; set; }

        /// <summary>
        /// 优惠费率活动ID 
        /// 选择指定活动ID，如果商户有意向报名优惠费率活动，该字段必填。详细参见优惠费率活动对照表。 
        /// 示例值：20191030111cff5b5e
        /// 可为空: False
        /// </summary>
        public string activities_id { get; set; }

        /// <summary>
        ///  优惠费率活动值 
        /// 根据优惠费率活动规则，由服务商自定义填写，支持两个小数点，需在优惠费率活动ID指定费率范围内
        /// 示例值：0.6
        /// 可为空: False
        /// </summary>
        public string[1, 50] activities_rate { get; set; }

        /// <summary>
        ///  优惠费率活动补充材料 
        /// 1、根据所选优惠费率活动，提供相关材料，详细参见优惠费率活动对照表
        /// 2、最多可上传5张照片，请填写通过图片上传API接口预先上传图片生成好的MediaID
        /// 示例值：0P3ng6KTIW4-Q_l2FjmFJBZR9FwczhJehHhAZN6BKXQPcs-VvdSo 
        /// 可为空: False
        /// </summary>
        public string[] activities_additions { get; set; }



    }


    /// <summary>
    /// 结算银行账户
    /// body请填写商家提现收款的银行账户信息
    /// <summary>
    public class Bank_Account_Info
    {

        /// <summary>
        ///  账户类型 
        /// 1、若主体为企业/政府机关/事业单位/社会组织，可填写：对公银行账户。
        /// 2、若主体为个体户，可选择填写：对公银行账户或经营者个人银行卡。
        /// BANK_ACCOUNT_TYPE_CORPORATE：对公银行账户
        /// BANK_ACCOUNT_TYPE_PERSONAL：经营者个人银行卡
        /// 示例值：BANK_ACCOUNT_TYPE_CORPORATE
        /// 可为空: True
        /// </summary>
        public string bank_account_type { get; set; }

        /// <summary>
        ///  开户名称 
        /// 1、选择“经营者个人银行卡”时，开户名称必须与“经营者证件姓名”一致，
        /// 2、选择“对公银行账户”时，开户名称必须与营业执照上的“商户名称”一致
        /// 3、该字段需进行加密处理，加密方法详见敏感信息加密说明。(提醒：必须在HTTP头中上送Wechatpay-Serial) 
        /// 示例值：AOZdYGISxo4y44/UgZ69bdu9X+tfMUJ9dl+LetjM45/zMbrYu+wWZ8gn4CTdo+D/m9MrPg+V4sm73oxqdQu/hj7aWyDl4GQtPXVdaztB9jVbVZh3QFzV+BEmytMNQp9dt1uWJktlfdDdLR3AMWyMB377xd+m9bSr/ioDTzagEcGe+vLYiKrzcroQv3OR0p3ppFYoQ3IfYeU/04S4t9rNFL+kyblK2FCCqQ11NdbbHoCrJc7NV4oASq6ZFonjTtgjjgKsadIKHXtb3JZKGZjduGdtkRJJp0/0eow96uY1Pk7Rq79Jtt7+I8juwEc4P4TG5xzchG/5IL9DBd+Z0zZXkw==
        /// 可为空: True
        /// </summary>
        public string[1, 2048] account_name { get; set; }

        /// <summary>
        /// 开户银行 
        /// 开户银行，传参规则如下：
        /// 1、17家直连银行，请根据开户银行对照表直接填写银行名 ;
        /// 2、非17家直连银行，该参数请填写为“其他银行”。
        /// 示例值：工商银行 
        /// 可为空: True
        /// </summary>
        public string account_bank { get; set; }

        /// <summary>
        /// 开户银行省市编码 
        /// 至少精确到市，详细参见省市区编号对照表。
        /// 注：
        /// 仅当省市区编号对照表中无对应的省市区编号时，可向上取该银行对应市级编号或省级编号。
        /// 示例值：110000
        /// 可为空: True
        /// </summary>
        public string bank_address_code { get; set; }

        /// <summary>
        /// 开户银行联行号 
        /// 1、17家直连银行无需填写，如为其他银行，则开户银行全称（含支行）和开户银行联行号二选一。
        /// 2、详细参见开户银行全称（含支行）对照表。
        /// 示例值：402713354941 
        /// 可为空: False
        /// </summary>
        public string bank_branch_id { get; set; }

        /// <summary>
        /// 开户银行全称（含支行）
        /// 1、17家直连银行无需填写，如为其他银行，则开户银行全称（含支行）和 开户银行联行号二选一。
        /// 2、需填写银行全称，如"深圳农村商业银行XXX支行"，详细参见开户银行全称（含支行）对照表。
        /// 示例值：施秉县农村信用合作联社城关信用社多选一
        /// 可为空: False
        /// </summary>
        public string bank_name { get; set; }

        /// <summary>
        /// 银行账号 
        /// 1、数字，长度遵循系统支持的卡号长度要求表。
        /// 2、该字段需进行加密处理，加密方法详见敏感信息加密说明。(提醒：必须在HTTP头中上送Wechatpay-Serial) 
        /// 示例值：d+xT+MQCvrLHUVDWC1PHN4C7Rsu3WL8sPndjXTd75kPkyjqnoMRrEEaYQE8ZRGYoeorwC+w== 
        /// 可为空: True
        /// </summary>
        public string account_number { get; set; }

        /// <summary>
        /// 银行账户证明材料
        /// 1. 当主体类型是“政府机关/事业单位”时或所属行业为“党费”时，支持在有合法资金管理关系的情况下结算账户设置为非同名。
        /// 2. 若结算账户设置为非同名，则需填写非同名证明材料，若结算账户为同名，则无需填写。
        /// 可为空: False
        /// </summary>
        public Account_Cert_Info account_cert_info { get; set; }


        #region 子数据类型

        /// <summary>
        /// 银行账户证明材料
        /// 1. 当主体类型是“政府机关/事业单位”时或所属行业为“党费”时，支持在有合法资金管理关系的情况下结算账户设置为非同名。
        /// 2. 若结算账户设置为非同名，则需填写非同名证明材料，若结算账户为同名，则无需填写。
        /// <summary>
        public class Account_Cert_Info
        {

            /// <summary>
            ///  结算证明函 
            /// 1. 请参照示例图打印结算证明函。
            /// 2、可上传1张图片，请填写通过图片上传API预先上传图片生成好的MediaID。
            /// 示例值：47ZC6GC-vnrbEny_Ie_An5-tCpqxucuxi-vByf3Gjm7KEIUv0OF4wFNIO4kqg05InE4d2I6_H7I4
            /// 可为空: True
            /// </summary>
            public string[1, 255] settlement_cert_pic { get; set; }

            /// <summary>
            ///  关系证明函 
            /// 1. 请参照示例图打印关系证明函。
            /// 2、可上传1张图片，请填写通过图片上传API预先上传图片生成好的MediaID。
            /// 示例值：47ZC6GC-vnrbEny_Ie_An5-tCpqxucuxi-vByf3Gjm7KEIUv0OF4wFNIO4kqg05InE4d2I6_H7I4
            /// 可为空: True
            /// </summary>
            public string[1, 255] relation_cert_pic { get; set; }

            /// <summary>
            ///  其他补充证明 
            /// 1. 请提供非同名结算的法律法规、政策通知、政府或上级部门公文等证明文件，以作上述材料的补充证明。
            /// 2、可上传1-3张图片，请填写通过图片上传API预先上传图片生成好的MediaID。
            /// 示例值：47ZC6GC-vnrbEny_Ie_An5-tCpqxucuxi-vByf3Gjm7KEIUv0OF4wFNIO4kqg05InE4d2I6_H7I4
            /// 可为空: True
            /// </summary>
            public string[] other_cert_pics { get; set; }



        }

        #endregion
    }


    /// <summary>
    /// 补充材料
    /// body根据实际审核情况，会额外要求商家提供指定的补充资料
    /// <summary>
    public class Addition_Info
    {

        /// <summary>
        ///  法人开户承诺函 
        /// 1、请上传法定代表人或负责人亲笔签署的开户承诺函扫描件（下载模板）。亲笔签名承诺函内容清晰可见，不得有涂污，破损，字迹不清晰现象。
        /// 2、请填写通过图片上传API预先上传图片生成好的MediaID。 
        /// 示例值：
        /// 47ZC6GC-vnrIUv0OF4wFNIO4kqg05InE4d2I6_H7I4 
        /// 可为空: False
        /// </summary>
        public string legal_person_commitment { get; set; }

        /// <summary>
        ///  法人开户意愿视频 
        /// 1、建议法人按如下话术录制“法人开户意愿视频”：
        /// 我是#公司全称#的法定代表人（或负责人），特此证明本公司申请的商户号为我司真实意愿开立且用于XX业务（或XX服务）。我司现有业务符合法律法规及腾讯的相关规定。
        /// 2、支持上传5M内的视频，格式可为avi、wmv、mpeg、mp4、mov、mkv、flv、f4v、m4v、rmvb；
        /// 3、请填写通过视频上传API预先上传视频生成好的MediaID 。
        /// 示例值：47ZC6GC-vnrbEny__Ie_An5-tCpqxucuxi-vByf3Gjm7KE53JXvGy9tqZm2XAUf-4KGprrKhpVBDIUv0OF4wFNIO4kqg05InE4d2I6_H7I4
        /// 可为空: False
        /// </summary>
        public string legal_person_video { get; set; }

        /// <summary>
        ///  补充材料 
        /// 1、最多可上传5张照片
        /// 2、请填写通过图片上传API预先上传图片生成好的MediaID。 
        /// 示例值：47ZC6GC-NIO4kqg05InE4d2I6_H7I4 
        /// 可为空: False
        /// </summary>
        public string[] business_addition_pics { get; set; }

        /// <summary>
        ///  补充说明 
        /// 512字以内
        /// 示例值：特殊情况，说明原因
        /// 可为空: False
        /// </summary>
        public string business_addition_msg { get; set; }



    }

    #endregion
}
