using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportNet35And40
{
    class Program
    {
        static void Main(string[] args)
        {
            ReplaceCode("../src/");

            Console.ReadKey();
        }

        private static void ReplaceCode(string root)
        {
            Console.WriteLine("开始扫描文件夹：" + root);
            var dirs = System.IO.Directory.GetDirectories(root);
            foreach (var dir in dirs)
            {
                var files = System.IO.Directory.GetFiles(dir, "*.cs");
                foreach (var file in files)
                {
                    Console.WriteLine("扫描文件：" + file);

                    using (var fs = new FileStream(file, FileMode.Open))
                    {
                        string newContent = null;
                        var sr = new StreamReader(fs);
                        var fileContent = sr.ReadToEnd();
                        var hit = fileContent.Contains("\r\n\r\n        #region 异步请求");
                        if (hit)
                        {
                            Console.WriteLine("命中，改写中");
                            newContent = fileContent.Replace("\r\n\r\n        #region 异步请求",
                               "\r\n\r\n#if !NET35 && !NET40\r\n        #region 异步请求");

                            var endContent = "#endregion";
                            var endIndex = newContent.LastIndexOf(endContent);
                            if (endIndex >= 0)
                            {
                                newContent.Insert(endIndex + endContent.Length, "\r\n#endif");

                                //var frontContent = newContent.Substring(0, endIndex + endContent.Length);
                                //newContent = newContent.Replace(frontContent, frontContent + "\r\n#endif");
                            }

                            if (newContent.Contains("using System.Threading.Tasks;"))
                            {
                                newContent = newContent.Replace("using System.Threading.Tasks;", "#if !NET35\r\nusing System.Threading.Tasks;\r\n#endif");
                            }
                        }

                        if (newContent != null)
                        {
                            fs.Seek(0, SeekOrigin.Begin);
                            var sw = new StreamWriter(fs);
                            sw.Write(newContent);
                            fs.Flush();
                            Console.WriteLine("已保存");
                        }
                    }
                }
                ReplaceCode(dir);
            }
        }
    }
}
