namespace Senparc.Weixin.MP.AdvancedAPIs.Wxa.MerchantJson
{
    /// <summary>
    /// 门店小程序数据
    /// </summary>
    public class ApplyMerchantData
    {
        /// <summary>
        /// 一级类目id 必填
        /// </summary>
        public int first_catid { get; set; }
        /// <summary>
        /// 二级类目id 必填
        /// </summary>
        public int second_catid { get; set; }
        /// <summary>
        /// 类目相关证件的临时素材mediaid 如果second_catid对应的sensitive_type为1 ，则qualification_list字段需要填 支持0~5个mediaid，例如mediaid1
        /// <para>选填</para>
        /// </summary>
        public string qualification_list { get; set; }
        /// <summary>
        /// 头像 --- 临时素材mediaid 用MediaApi.UploadTemporaryMedia接口得到的
        /// <para>必填</para>
        /// <see cref="MediaApi.UploadTemporaryMedia(string, UploadMediaFileType, string, int)"/>
        /// </summary>
        public string headimg_mediaid { get; set; }
        /// <summary>
        /// 门店小程序的昵称 名称长度为4-30个字符（中文算两个字符） 必填
        /// </summary>
        public string nickname { get; set; }
        /// <summary>
        /// 门店小程序的介绍 必填
        /// </summary>
        public string intro { get; set; }
        /// <summary>
        /// 营业执照或组织代码证 --- 临时素材mediaid 如果返回错误码85024，则该字段必填，否则不用填 
        /// <para>选填</para>
        /// </summary>
        public string org_code { get; set; }
        /// <summary>
        /// 补充材料 --- 临时素材mediaid 如果返回错误码85024，则可以选填 支持0~5个mediaid，例如mediaid1
        /// <para>选填</para>
        /// </summary>
        public string other_files { get; set; }
    }
}
