using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Open.WxaAPIs.NickName.CheckWxVerifyNickNameJson;
using Senparc.Weixin.Open.WxaAPIs.NickName.QueryNickNameJson;
using Senparc.Weixin.Open.WxaAPIs.NickName.SetNickNameJson;

namespace Senparc.Weixin.Open.WxaAPIs.NickName
{
    /// <summary>
    /// 小程序昵称设置
    /// <<para>https://open.weixin.qq.com/cgi-bin/showdocument?action=dir_list&t=resource/res_list&verify=1&id=21521706765hLoMO&token=&lang=zh_CN</para>
    /// </summary>
    public class NickNameApi
    {
        #region 同步方法

        /// <summary>
        /// 小程序名称设置及改名
        /// </summary>
        /// <para>https://api.weixin.qq.com/wxa/setnickname?access_token=TOKEN</para>
        /// <param name="accessToken">小程序的access_token</param>
        /// <param name="nick_name">昵称	必填</param>
        /// <param name="id_card">身份证照片–临时素材mediaid	个人号必填</param>
        /// <param name="license">组织机构代码证或营业执照–临时素材mediaid	组织号必填</param>
        /// <param name="naming_other_stuff_1">其他证明材料---临时素材 mediaid 选填</param>
        /// <param name="naming_other_stuff_2">其他证明材料---临时素材 mediaid 选填</param>
        /// <param name="naming_other_stuff_3">其他证明材料---临时素材 mediaid 选填</param>
        /// <param name="naming_other_stuff_4">其他证明材料---临时素材 mediaid 选填</param>
        /// <param name="naming_other_stuff_5">其他证明材料---临时素材 mediaid 选填</param>
        /// <returns></returns>
        public static SetNickNameJsonResult SetNickName(string accessToken, string nick_name, string id_card,
            string license,
            string naming_other_stuff_1 = "",
            string naming_other_stuff_2 = "",
            string naming_other_stuff_3 = "",
            string naming_other_stuff_4 = "",
            string naming_other_stuff_5 = "")
        {
            var url = $"{Config.ApiMpHost}/wxa/setnickname?access_token={accessToken.AsUrlData()}";

            var data = new
            {
                nick_name = nick_name,
                id_card = id_card,
                license = license,
                naming_other_stuff_1 = naming_other_stuff_1,
                naming_other_stuff_2 = naming_other_stuff_2,
                naming_other_stuff_3 = naming_other_stuff_3,
                naming_other_stuff_4 = naming_other_stuff_4,
                naming_other_stuff_5 = naming_other_stuff_5
            };
            return CommonJsonSend.Send<SetNickNameJsonResult>(null, url, data);
        }

        /// <summary>
        /// 小程序改名审核状态查询
        /// </summary>
        /// <param name="accessToken">小程序的access_token</param>
        /// <param name="audit_id">审核单id</param>
        /// <returns></returns>
        public static QueryNickNameJsonResult QueryNickName(string accessToken, int audit_id)
        {
            var url = $"{Config.ApiMpHost}/wxa/api_wxa_querynickname?access_token={accessToken.AsUrlData()}";

            var data = new
            {
                audit_id = audit_id
            };
            return CommonJsonSend.Send<QueryNickNameJsonResult>(null, url, data);
        }

        /// <summary>
        /// 微信认证名称检测
        /// </summary>
        /// <param name="accessToken">小程序的access_token</param>
        /// <param name="nick_name">名称（昵称）</param>
        /// <returns></returns>
        public static CheckWxVerifyNickNameJsonResult CheckWxVerifyNickName(string accessToken, string nick_name)
        {
            var url =
                $"{Config.ApiMpHost}/cgi-bin/wxverify/checkwxverifynickname?access_token={accessToken.AsUrlData()}";
            var data = new
            {
                nick_name = nick_name
            };
            return CommonJsonSend.Send<CheckWxVerifyNickNameJsonResult>(null, url, data);
        }

        #endregion



        #region 异步方法

        /// <summary>
        /// 小程序名称设置及改名
        /// </summary>
        /// <para>https://api.weixin.qq.com/wxa/setnickname?access_token=TOKEN</para>
        /// <param name="accessToken">小程序的access_token</param>
        /// <param name="nick_name">昵称	必填</param>
        /// <param name="id_card">身份证照片–临时素材mediaid	个人号必填</param>
        /// <param name="license">组织机构代码证或营业执照–临时素材mediaid	组织号必填</param>
        /// <param name="naming_other_stuff_1">其他证明材料---临时素材 mediaid 选填</param>
        /// <param name="naming_other_stuff_2">其他证明材料---临时素材 mediaid 选填</param>
        /// <param name="naming_other_stuff_3">其他证明材料---临时素材 mediaid 选填</param>
        /// <param name="naming_other_stuff_4">其他证明材料---临时素材 mediaid 选填</param>
        /// <param name="naming_other_stuff_5">其他证明材料---临时素材 mediaid 选填</param>
        /// <returns></returns>
        public static async Task<SetNickNameJsonResult> SetNickNameAsync(string accessToken, string nick_name,
            string id_card,
            string license,
            string naming_other_stuff_1 = "",
            string naming_other_stuff_2 = "",
            string naming_other_stuff_3 = "",
            string naming_other_stuff_4 = "",
            string naming_other_stuff_5 = "")
        {
            var url = $"{Config.ApiMpHost}/wxa/setnickname?access_token={accessToken.AsUrlData()}";

            var data = new
            {
                nick_name = nick_name,
                id_card = id_card,
                license = license,
                naming_other_stuff_1 = naming_other_stuff_1,
                naming_other_stuff_2 = naming_other_stuff_2,
                naming_other_stuff_3 = naming_other_stuff_3,
                naming_other_stuff_4 = naming_other_stuff_4,
                naming_other_stuff_5 = naming_other_stuff_5
            };
            return await CommonJsonSend.SendAsync<SetNickNameJsonResult>(null, url, data).ConfigureAwait(false);
        }

        /// <summary>
        /// 小程序改名审核状态查询
        /// </summary>
        /// <param name="accessToken">小程序的access_token</param>
        /// <param name="audit_id">审核单id</param>
        /// <returns></returns>
        public static async Task<QueryNickNameJsonResult> QueryNickNameAsync(string accessToken, int audit_id)
        {
            var url = $"{Config.ApiMpHost}/wxa/api_wxa_querynickname?access_token={accessToken.AsUrlData()}";

            var data = new
            {
                audit_id = audit_id
            };
            return await CommonJsonSend.SendAsync<QueryNickNameJsonResult>(null, url, data).ConfigureAwait(false);
        }

        /// <summary>
        /// 微信认证名称检测
        /// </summary>
        /// <param name="accessToken">小程序的access_token</param>
        /// <param name="nick_name">名称（昵称）</param>
        /// <returns></returns>
        public static async Task<CheckWxVerifyNickNameJsonResult> CheckWxVerifyNickNameAsync(string accessToken,
            string nick_name)
        {
            var url =
                $"{Config.ApiMpHost}/cgi-bin/wxverify/checkwxverifynickname?access_token={accessToken.AsUrlData()}";
            var data = new
            {
                nick_name = nick_name
            };
            return await CommonJsonSend.SendAsync<CheckWxVerifyNickNameJsonResult>(null, url, data).ConfigureAwait(false);
        }

        #endregion
    }
}