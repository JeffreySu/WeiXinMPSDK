using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Helpers.StringHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Helpers.StringHelper.Tests
{
    [TestClass()]
    public class ASCIISortTests
    {
        [TestMethod()]
        public void CompareTest()
        {
            {
                var str1 = "PK0000397757";
                var str2 = "otMXytwcamKAa3JmUoQ0N7OGDFuA";

                ArrayList arrayList = new ArrayList();
                ASCIISort asciiSort = new ASCIISort();
                arrayList.Add(str1);
                arrayList.Add(str2);
                arrayList.Sort(asciiSort);
                Assert.AreEqual(str1, arrayList[0]);
            }

            {
                var str1 = "PK00003977572";
                var str2 = "PK0000397757";
                ArrayList arrayList = new ArrayList();
                ASCIISort asciiSort = new ASCIISort();
                arrayList.Add(str1);
                arrayList.Add(str2);
                arrayList.Sort(asciiSort);
                Assert.AreEqual(str2, arrayList[0]);
            }

            {
                var str1 = "PK0000397757";
                var str2 = "PK0000397757";
                ArrayList arrayList = new ArrayList();
                ASCIISort asciiSort = new ASCIISort();
                arrayList.Add(str1);
                arrayList.Add(str2);
                arrayList.Sort(asciiSort);
                Assert.AreEqual(str1, arrayList[0]);
            }

            {
                var str1 = "otMXytwcamKAa3JmUoQ0N7OGDFuA";
                var str2 = "PK0000397757";
                ArrayList arrayList = new ArrayList();
                ASCIISort asciiSort = new ASCIISort();
                arrayList.Add(str1);
                arrayList.Add(str2);
                arrayList.Sort(asciiSort);
                Assert.AreEqual(str2, arrayList[0]);
            }



        }
    }
}