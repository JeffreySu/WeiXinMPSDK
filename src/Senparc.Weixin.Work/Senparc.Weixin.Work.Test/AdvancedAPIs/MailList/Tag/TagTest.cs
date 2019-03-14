#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2019 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.Work.AdvancedAPIs;
using Senparc.Weixin.Work.AdvancedAPIs.MailList;
using Senparc.Weixin.Work.CommonAPIs;
using Senparc.Weixin.Work.Containers;
using Senparc.Weixin.Work.Test.CommonApis;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs
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
            var accessToken = AccessTokenContainer.GetToken(_corpId, base._corpSecret);
            var result = MailListApi.CreateTag(accessToken, "ceshi1");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.errcode == ReturnCode_Work.请求成功);
            return result.tagid;
        }

        //[TestMethod]
        public void UpdateMemberTest(int tagId)
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId, base._corpSecret);
            var result = MailListApi.UpdateTag(accessToken, tagId, "ceshi2");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.errcode == ReturnCode_Work.请求成功);
        }

        //[TestMethod]
        public void DeleteTagTest(int tagId)
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId, base._corpSecret);
            var result = MailListApi.DeleteTag(accessToken, tagId);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.errcode == ReturnCode_Work.请求成功);
        }

        //[TestMethod]
        public void GetTagMemberTest(int tagId)
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId, base._corpSecret);
            var result = MailListApi.GetTagMember(accessToken, tagId);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.errcode == ReturnCode_Work.请求成功);
        }

        //[TestMethod]
        public void AddTagMemberTest(int tagId)
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId, base._corpSecret);
            var result = MailListApi.AddTagMember(accessToken, tagId, new[] { "TYSZCC" });
            Assert.IsNotNull(result);
            Assert.IsTrue(result.errcode == ReturnCode_Work.请求成功);
        }

        //[TestMethod]
        public void DelTagMemberTest(int tagId)
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId, base._corpSecret);
            var result = MailListApi.DelTagMember(accessToken, tagId, new[] { "TYSZCC" });
            Assert.IsNotNull(result);
            Assert.IsTrue(result.errcode == ReturnCode_Work.请求成功);
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
