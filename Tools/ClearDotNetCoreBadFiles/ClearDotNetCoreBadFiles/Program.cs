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
            Console.WriteLine("当前工具用于清理项目在.NET Core（包括.NET Standard）编译后，");
            Console.WriteLine("生成的中间文件会导致.NET Framework编译失败的情况。");
            Console.WriteLine(@"例如：Your project.json doesn't have a runtimes section. You should add '""runtimes"": { ""win"": { } }' to your project.json and then re-run NuGet restore.	Senparc.Weixin.Work
");
            Console.WriteLine("点击任意键进行清理");
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
