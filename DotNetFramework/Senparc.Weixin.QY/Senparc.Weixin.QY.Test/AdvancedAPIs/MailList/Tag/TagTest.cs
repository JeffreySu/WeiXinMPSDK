using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.QY.AdvancedAPIs;
using Senparc.Weixin.QY.AdvancedAPIs.MailList;
using Senparc.Weixin.QY.CommonAPIs;
using Senparc.Weixin.QY.Test.CommonApis;

namespace Senparc.Weixin.QY.Test.AdvancedAPIs
{
    /// <summary>
    /// CommonApiTest 的摘要说明
    /// </summary>
    [TestClass]
    public partial class TagTest : CommonApiTest
    {
        //[TestMethod]
        public int CreateTagTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId);
            var result = MailListApi.CreateTag(accessToken, "ceshi1");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.errcode == ReturnCode_QY.请求成功);
            return result.tagid;
        }

        //[TestMethod]
        public void UpdateMemberTest(int tagId)
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId);
            var result = MailListApi.UpdateTag(accessToken, tagId, "ceshi2");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.errcode == ReturnCode_QY.请求成功);
        }

        //[TestMethod]
        public void DeleteTagTest(int tagId)
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId);
            var result = MailListApi.DeleteTag(accessToken, tagId);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.errcode == ReturnCode_QY.请求成功);
        }

        //[TestMethod]
        public void GetTagMemberTest(int tagId)
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId);
            var result = MailListApi.GetTagMember(accessToken, tagId);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.errcode == ReturnCode_QY.请求成功);
        }

        //[TestMethod]
        public void AddTagMemberTest(int tagId)
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId);
            var result = MailListApi.AddTagMember(accessToken, tagId, new[] { "TYSZCC" });
            Assert.IsNotNull(result);
            Assert.IsTrue(result.errcode == ReturnCode_QY.请求成功);
        }

        //[TestMethod]
        public void DelTagMemberTest(int tagId)
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId);
            var result = MailListApi.DelTagMember(accessToken, tagId, new[] { "TYSZCC" });
            Assert.IsNotNull(result);
            Assert.IsTrue(result.errcode == ReturnCode_QY.请求成功);
        }

        [TestMethod]
        public void TagTestAllSet()
        {
            //int tagId = CreateTagTest();
            //UpdateMemberTest(tagId);
            //GetTagMemberTest(tagId);
            //AddTagMemberTest(tagId);
            //DelTagMemberTest(tagId);
            //DeleteTagTest(tagId);
            int tagId = 5;
            UpdateMemberTest(tagId);
            GetTagMemberTest(tagId);
            AddTagMemberTest(tagId);
            DelTagMemberTest(tagId);
            DeleteTagTest(tagId);
        }
    }
}
