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
            var cookieContainer = new CookieContainer();
            using (MemoryStream ms = new MemoryStream())
            {
                using (var sr = new StreamWriter(ms))
                {
                    sr.Write(jsonString);
                }
                var url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}", accessToken);
                var result = Post.PostGetJson<WxJsonResult>(url, cookieContainer, ms);
                return result;
            }
        }
    }
}
