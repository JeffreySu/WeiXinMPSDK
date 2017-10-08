using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SyncVersion
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("开始更新AssemblyInfo.cs文件的Version版本");
            SyncVersion("../src/");
            Console.WriteLine("完成");
        }

        private static void SyncVersion(string root)
        {
            //Console.WriteLine("开始扫描文件夹：" + root);
            var dirs = System.IO.Directory.GetDirectories(root);
            foreach (var dir in dirs)
            {
                var files = System.IO.Directory.GetFiles(dir, "*.csproj");
                foreach (var file in files)
                {
                    Console.WriteLine();
                    Console.WriteLine("扫描文件：" + file);

                    XDocument doc = XDocument.Load(file);
                    var versionElement = doc.Root?.Element("PropertyGroup")?.Element("Version");
                    if (versionElement != null)
                    {
                        var version = versionElement.Value;
                        Console.WriteLine("版本号：" + version);
                        var versions = version.Split(new[] { ".", "-" }, StringSplitOptions.None);
                        var versionStr = string.Join(".", versions.Take(3)) + ".*";
                        Console.WriteLine(".NET 4.5 使用版本号：" + versionStr);

                        //寻找AssemblyInfo.cs文件
                        var assemblyInfoFilePath = Path.Combine(dir, "Properties", "AssemblyInfo.cs");
                        if (File.Exists(assemblyInfoFilePath))
                        {
                            Console.WriteLine("找到AssemblyInfo.cs");
                            string newContent = null;
                            using (var sr = new StreamReader(assemblyInfoFilePath, Encoding.UTF8))
                            {
                                var fileContent = sr.ReadToEnd();
                                var regex = new Regex(@"(?<=[^ ])(?<=\[assembly: AssemblyVersion\("")(\d+\.\d+\.\d+\.\*)(?=""\)\])");
                                var match = regex.Match(fileContent);
                                if (match.Success)
                                {
                                    Console.WriteLine("匹配成功");
                                    newContent = regex.Replace(fileContent, versionStr);
                                }
                                else
                                {
                                    Console.WriteLine("匹配失败");
                                }
                                //newContent = Regex.Replace(fileContent,
                                //    @"(?<=\[assembly: AssemblyVersion\("")(\d +\.\d +\.\d +\.\*)(?= ""\)\])",versionStr);

                            }
                            if (newContent != null)
                            {
                                using (var fs = new FileStream(assemblyInfoFilePath, FileMode.Create))
                                {
                                    var sw = new StreamWriter(fs, Encoding.UTF8);
                                    sw.Write(newContent);
                                    sw.Flush();
                                    fs.Flush(true);
                                }
                                Console.WriteLine("已保存");
                            }
                        }
                        else
                        {
                            Console.WriteLine("未找到AssemblyInfo.cs");
                        }
                    }
                    else
                    {
                        Console.WriteLine("非Multi-Targeting项目");
                    }
                }
                SyncVersion(dir);
            }
        }
    }
}
