#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2017 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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

/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc
    
    文件名：Program.cs
    文件功能描述：当前工具用于清理项目在.NET Core（包括.NET Standard）编译后，
                  生成的中间文件会导致.NET Framework编译失败的情况。
    
    创建标识：Senparc - 20170921

    修改标识：Senparc - 20170929
    修改描述：v1.2 优化对异常的输出
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ClearDotNetCoreBadFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(@"ClearDotNetCoreBadFiles v1.2
Copyrignt 2017 Senparc

当前工具用于清理项目在.NET Core（包括.NET Standard）编译后，
生成的中间文件会导致.NET Framework编译失败的情况。

例如遇到错误信息：
---------------------------------------------
Your project.json doesn't have a runtimes section. You should add '""runtimes"": { ""win"": { } }' to your project.json and then re-run NuGet restore.
---------------------------------------------

工作原理为删除项目obj文件夹，弥补“清理解决方案”功能无法解决的问题。
请了解原理后再使用本工具，以免发生意外！点击任意键开始进行清理（电源键除外）...
");
            Console.ReadKey();

            Run();
        }

        private static void Run()
        {
            Console.WriteLine("清理开始……");

            CleanDir("../src/");
            CleanDir("../Samples/");


            Console.WriteLine("清理完毕，点击回车键再清理一次，其他任意键退出（电源键除外）...");
            if (Console.ReadKey().KeyChar == (int)ConsoleKey.Enter)
            {
                Run();
            }
            else
            {
                Console.WriteLine("Bye.");
            }
        }


        private static void CleanDir(string root)
        {
            var dirs = System.IO.Directory.GetDirectories(root);
            foreach (var dir in dirs)
            {
                var fileName = Path.GetFileName(dir).ToUpper();
                if (fileName == "OBJ"/* || fileName == "BIN"*/)
                {
                    try
                    {
                        Console.WriteLine("开始清除文件夹：" + dir);
                        System.IO.Directory.Delete(dir, true);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"清理文件夹{dir}发生错误：");
                        Console.WriteLine(e);
                        //throw;
                    }

                }
                else
                {
                    CleanDir(dir);
                }
            }
        }
    }
}
