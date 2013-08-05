using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Menu;
using Senparc.Weixin.MP.HttpUtility;

namespace Senparc.Weixin.MP.CommonAPIs
{
    public partial class CommonApi
    {
        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="buttonData">菜单内容</param>
        /// <returns></returns>
        public static WxJsonResult CreateMenu(string accessToken, ButtonGroup buttonData)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            var jsonString = js.Serialize(buttonData);
            CookieContainer cookieContainer = null;// new CookieContainer();

            using (MemoryStream ms = new MemoryStream())
            {
                var bytes = Encoding.Default.GetBytes(jsonString);
                ms.Write(bytes, 0, bytes.Length);
                ms.Seek(0, SeekOrigin.Begin);

                var url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}", accessToken);
                var result = Post.PostGetJson<WxJsonResult>(url, cookieContainer, ms);
                return result;
            }
        }

        /// <summary>
        /// 获取当前菜单
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static GetMenuResult GetMenu(string accessToken)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/get?access_token={0}", accessToken);

            GetMenuResult result = null;
            try
            {
                result = Get.GetJson<GetMenuResult>(url);
            }
            catch (ErrorJsonResultException ex)
            {
                //如果没有惨淡会返回错误代码：46003：menu no exist
            }
            return result;
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static WxJsonResult DeleteMenu(string accessToken)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/delete?access_token={0}", accessToken);
            var result = Get.GetJson<WxJsonResult>(url);
            return result;
        }
    }
}
