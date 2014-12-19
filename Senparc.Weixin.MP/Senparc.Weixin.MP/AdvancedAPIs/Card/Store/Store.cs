using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.HttpUtility;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 创建卡券接口
    /// </summary>
    public static class Store
    {
        /// <summary>
        /// 批量导入门店信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="data">门店数据</param>
        /// <returns></returns>
        public static StoreResultJson StoreBatchAdd(string accessToken, StoreLocationData data)
        {
            var urlFormat = string.Format("https://api.weixin.qq.com/card/location/batchadd?access_token={0}", accessToken);

            return CommonJsonSend.Send<StoreResultJson>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 拉取门店列表
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="offset">偏移量，0 开始</param>
        /// <param name="count">拉取数量</param>
        /// <returns></returns>
        public static StoreGetResultJson BatchGet(string accessToken, int offset, int count)
        {
            var urlFormat = string.Format("https://api.weixin.qq.com/card/location/batchget?access_token={0}", accessToken);

            var data = new
                {
                    offset = offset,
                    count = count
                };

            return CommonJsonSend.Send<StoreGetResultJson>(accessToken, urlFormat, data);
        }
    }
}
