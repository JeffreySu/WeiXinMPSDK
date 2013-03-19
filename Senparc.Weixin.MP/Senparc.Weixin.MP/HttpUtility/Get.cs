using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Senparc.Weixin.MP.HttpUtility
{
    public static class Get
    {
        public static T GetJson<T>(string url)
        {
            string returnText = HttpUtility.RequestUtility.DownloadString(url);
            JavaScriptSerializer js = new JavaScriptSerializer();
            T result = js.Deserialize<T>(returnText);
            return result;
        }
    }
}
