using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Entities;
using Senparc.Weixin.TenPayV3;
using Senparc.Weixin.TenPayV3.Apis.FundApp;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Test.Apis
{
    [TestClass]
    public class FundAppTransferTests
    {
        [TestMethod]
        public async Task TransferBillRejectsMissingPaymentPublicKeyBeforeSending()
        {
            var setting = new SenparcWeixinSettingItem
            {
                TenPayV3_TenPayPubKeyID = "PUB_KEY_ID_test"
            };
            var api = new FundAppApis(setting);

            var exception = await Assert.ThrowsExceptionAsync<TenpayApiRequestException>(
                () => api.TransferBillAsync(new TransferBillRequestData { out_bill_no = "bill1" }));
            StringAssert.Contains(exception.Message, "公钥 ID");
        }

        [TestMethod]
        public void CancelTransferUsesBodylessRequest()
        {
            var method = typeof(FundAppApis).GetMethod(nameof(FundAppApis.CancelTransferAsync));
            var stateMachine = method.GetCustomAttribute<AsyncStateMachineAttribute>().StateMachineType;
            var moveNext = stateMachine.GetMethod("MoveNext", BindingFlags.Instance | BindingFlags.NonPublic);
            var bytes = moveNext.GetMethodBody().GetILAsByteArray();
            var callsBodylessRequest = false;

            for (var index = 0; index <= bytes.Length - 5; index++)
            {
                if (bytes[index] != 0x28 && bytes[index] != 0x6F)
                    continue;

                try
                {
                    var called = moveNext.Module.ResolveMethod(BitConverter.ToInt32(bytes, index + 1));
                    if (called.Name == "RequestWithoutBodyAsync")
                        callsBodylessRequest = true;
                }
                catch (Exception ex) when (ex is ArgumentException || ex is BadImageFormatException)
                {
                }
            }

            Assert.IsTrue(callsBodylessRequest);
        }

    }
}
