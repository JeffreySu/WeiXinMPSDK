using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.TenPayV3.Apis.Transfer;
using Senparc.Weixin.TenPayV3.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.Transfer.Tests
{
    [TestClass()]
    public class TransferApisTests : BaseTenPayTest
    {
        [TestMethod()]
        public void BatchesAsyncTest()
        {
            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);
            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            var transferApis = new TransferApis();
            try
            {
                var requestData = new BatchesRequestData()
                {
                    appid = TenPayV3Info.AppId,
                    batch_name = "SenparcLocalTest20220630F01",
                    batch_remark = "单元测试订单",
                    out_batch_no = "SenparcLocalTest20220630F01",
                    total_amount = 1,
                    total_num = 1,
                    transfer_detail_list = new Transfer_Detail_List[] {
                         new Transfer_Detail_List(){
                          openid="olPjZjsXuQPJoV0HlruZkNzKc91E",
                          out_detail_no="SenparcLocalTest20220630F01D01",
                          transfer_amount=1,
                          transfer_remark="单元测试转账",
                          user_name =null
                         }
                    }
                };
                var result = transferApis.BatchesAsync(requestData).GetAwaiter().GetResult();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}