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
    文件功能描述：当前工具用于清理项目 的 Bin 文件夹，清除开发和测试过程中的各种干扰

    创建标识：Senparc - 20180707

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RemoveBinFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(@"RemoveBinFiles v1.0.0
Copyrignt 2018 Senparc

当前工具用于清理项目 的 Bin 文件夹，清除开发和测试过程中的各种干扰
按键盘 T 键，则只清理带“Test”关键字的测试项目，其他键清理所有项目
");
            var key = Console.ReadKey();

            Run(key);
        }

        private static void Run(ConsoleKeyInfo key)
        {
            Console.WriteLine("清理开始……");

            CleanDir("../src/", key.Key == ConsoleKey.T);
            CleanDir("../Samples/", key.Key == ConsoleKey.T);


            Console.WriteLine("按键盘 T 键，则只清理带“Test”关键字的测试项目，其他键清理所有项目（电源键除外）...");
            Run(Console.ReadKey());
        }


        private static void CleanDir(string root, bool justCleanTestProject)
        {
            var dirs = System.IO.Directory.GetDirectories(root);
            foreach (var dir in dirs)
            {
                if (justCleanTestProject && dir.IndexOf("Test", StringComparison.OrdinalIgnoreCase) == -1)
                {
                    continue;
                }

                if (Path.GetFileName(dir).ToUpper() == "BIN")
                {
                    try
                    {
                        Console.WriteLine("开始清除文件夹：" + dir);
                        System.IO.Directory.Delete(dir, true);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"清理文件夹 {dir} 发生错误：");
                        Console.WriteLine(e);
                        //throw;
                    }

                }
                else
                {
                    CleanDir(dir, justCleanTestProject);
                }
            }
        }
    }
}
