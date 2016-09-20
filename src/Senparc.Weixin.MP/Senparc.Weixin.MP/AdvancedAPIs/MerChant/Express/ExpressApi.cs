using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.CommonAPIs;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 微小店接口，官方API：http://mp.weixin.qq.com/wiki/index.php?title=%E5%BE%AE%E4%BF%A1%E5%B0%8F%E5%BA%97%E6%8E%A5%E5%8F%A3
    /// </summary>
    public static class ExpressApi
    {
        /// <summary>
        /// 增加邮费模板
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="addExpressData">增加邮费模板需要Post的数据</param>
        /// <returns></returns>
        public static AddExpressResult AddExpress(string accessToken, AddExpressData addExpressData)
        {
            var urlFormat = "https://api.weixin.qq.com/merchant/express/add?access_token={0}";

            return CommonJsonSend.Send<AddExpressResult>(accessToken, urlFormat, addExpressData);
        }

        /// <summary>
        /// 删除邮费模板
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="templateId">邮费模板Id</param>
        /// <returns></returns>
        public static WxJsonResult DeleteExpress(string accessToken, int templateId)
        {
            var urlFormat = "https://api.weixin.qq.com/merchant/express/del?access_token={0}";

            var data = new
            {
                template_id = templateId
            };

            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 修改邮费模板
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="upDateExpressData">修改邮费模板需要Post的数据</param>
        /// <returns></returns>
        public static WxJsonResult UpDateExpress(string accessToken, UpDateExpressData upDateExpressData)
        {
            var urlFormat = "https://api.weixin.qq.com/merchant/express/update?access_token={0}";

            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, upDateExpressData);
        }

        /// <summary>
        /// 获取指定ID的邮费模板
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="templateId">邮费模板Id</param>
        /// <returns></returns>
        public static GetByIdExpressResult GetByIdExpress(string accessToken, int templateId)
        {
            var urlFormat = "https://api.weixin.qq.com/merchant/express/getbyid?access_token={0}";

            var data = new
            {
                template_id = templateId
            };

            return CommonJsonSend.Send<GetByIdExpressResult>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 获取所有邮费模板
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static GetAllExpressResult GetAllExpress(string accessToken)
        {
            var urlFormat = "https://api.weixin.qq.com/merchant/express/getall?access_token={0}";

            return CommonJsonSend.Send<GetAllExpressResult>(accessToken, urlFormat, null, CommonJsonSendType.GET);
        }
    }
}
