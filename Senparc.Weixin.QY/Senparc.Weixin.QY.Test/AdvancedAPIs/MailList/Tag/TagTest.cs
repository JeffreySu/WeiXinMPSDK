using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.QY.AdvancedAPIs;
using Senparc.Weixin.QY.CommonAPIs;

namespace Senparc.Weixin.QY.Test.AdvancedAPIs
{
    /// <summary>
    /// CommonApiTest 的摘要说明
    /// </summary>
    [TestClass]
    public partial class TagTest
    {
        protected string _corpId = "wxccd01c4e6bf59232"; //换成你的信息
        protected string _corpSecret = "ejXcV7rb9OtakBucpMji1kUtPmnKy4hNCskW_bUKLx8lRxO_aVrcc0gVTMEv13G1"; //换成你的信息

        public TagTest()
        {
            //全局只需注册一次
            AccessTokenContainer.Register(_corpId, _corpSecret);
        }

        //[TestMethod]
        public int CreateTagTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId);
            var result = Tag.CreateTag(accessToken, "ceshi1");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.errcode == ReturnCode.请求成功);
            return result.tagid;
        }

        //[TestMethod]
        public void UpdateMemberTest(int tagId)
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId);
            var result = Tag.UpdateTag(accessToken, tagId, "ceshi2");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.errcode == ReturnCode.请求成功);
        }

        //[TestMethod]
        public void DeleteTagTest(int tagId)
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId);
            var result = Tag.DeleteTag(accessToken, tagId);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.errcode == ReturnCode.请求成功);
        }

        //[TestMethod]
        public void GetTagMemberTest(int tagId)
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId);
            var result = Tag.GetTagMember(accessToken, tagId);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.errcode == ReturnCode.请求成功);
        }

        //[TestMethod]
        public void AddTagMemberTest(int tagId)
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId);
            var result = Tag.AddTagMember(accessToken, tagId, new[] { "TYSZCC" });
            Assert.IsNotNull(result);
            Assert.IsTrue(result.errcode == ReturnCode.请求成功);
        }

        //[TestMethod]
        public void DelTagMemberTest(int tagId)
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId);
            var result = Tag.DelTagMember(accessToken, tagId, new[] { "TYSZCC" });
            Assert.IsNotNull(result);
            Assert.IsTrue(result.errcode == ReturnCode.请求成功);
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
