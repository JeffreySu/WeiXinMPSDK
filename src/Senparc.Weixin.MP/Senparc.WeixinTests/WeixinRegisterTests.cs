using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Senparc.WeixinTests
{
    [TestClass]
    public class WeixinRegisterTests
    {
        [TestMethod]
        public void MyTestMethod()
        {
            var pathSeparator = Path.DirectorySeparatorChar.ToString();
            var altPathSeparator = Path.AltDirectorySeparatorChar.ToString();
            Console.WriteLine(pathSeparator);
            Console.WriteLine(altPathSeparator);
        }
    }
}
