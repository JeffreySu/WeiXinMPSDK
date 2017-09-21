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
            Console.WriteLine(@"ClearDotNetCoreBadFiles v1.0
Copyrignt 2017 Senparc

当前工具用于清理项目在.NET Core（包括.NET Standard）编译后，
生成的中间文件会导致.NET Framework编译失败的情况。

例如遇到错误信息：
---------------------------------------------
Your project.json doesn't have a runtimes section. You should add '""runtimes"": { ""win"": { } }' to your project.json and then re-run NuGet restore.
---------------------------------------------

工作原理为删除项目obj文件夹，弥补“清理解决方案”功能无法解决的问题。
请了解原理后再使用本工具，以免发生意外！点击任意键开始进行清理...
");
            Console.ReadKey();
            Console.WriteLine("清理开始……");

            CleanDir("../src/");
          
            Console.WriteLine("清理完毕，点击任意键退出");
            Console.ReadKey();
        }

        private static void CleanDir(string root)
        {
            var dirs = System.IO.Directory.GetDirectories(root);
            foreach (var dir in dirs)
            {
                if (Path.GetFileName(dir).ToUpper() == "OBJ")
                {
                    Console.WriteLine("开始清除文件夹：" + dir);
                    System.IO.Directory.Delete(dir, true);
                }
                else
                {
                    CleanDir(dir);
                }
            }
        }
    }
}
