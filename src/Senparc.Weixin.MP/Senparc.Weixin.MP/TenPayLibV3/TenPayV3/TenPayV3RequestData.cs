using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.TenPayLibV3
{
    /// <summary>
    /// 微信支付提交的XML Data数据
    /// </summary>
    public class TenPayV3RequestData
    {
        /// <summary>
        /// 公众账号ID
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        public string MchId { get; set; }

        /// <summary>
        /// 随机字符串
        /// </summary>
        public string NonceStr { get; set; }


        public RequestHandler PackageRequestHandler
        {
            get { return new RequestHandler(null); }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Key { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="mchId"></param>
        /// <param name="key"></param>
        /// <param name="nonceStr"></param>
        public TenPayV3RequestData(string appId, string mchId, string key,
            string nonceStr)
        {
            AppId = appId;
            MchId = mchId;
            NonceStr = nonceStr;
            Key = key;

            #region 设置RequestHandler

            //初始化
            PackageRequestHandler.Init();
            #endregion
        }
    }
}